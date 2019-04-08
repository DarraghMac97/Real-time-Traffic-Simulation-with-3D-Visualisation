using System;
using System.Collections;
using System.Collections.Generic;
using CodingConnected.TraCI.NET;
using CodingConnected.TraCI.NET.Types;
using UnityEngine;

using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CodingConnected.TraCI.NET.Commands;
using Color = UnityEngine.Color;
using Object = System.Object;

public class Traci_one : MonoBehaviour
{
    
    public Light ttLight;
    public GameObject tLight;
    public GameObject egoVehicle;
    public Material roadmat;
    public List<string> vehicleidlist;
    public List<GameObject> carlist;
    public GameObject NPCVehicle;
    public TraCIClient client;
    private List<string> tlightids;
    private int phaser;
    private List<traLights> listy;
    public Dictionary<string, List<traLights>> dicti;
    

    // Start is called before the first frame update
    
    void Start()
    {
       

        
        client = new TraCIClient();
        client.Connect("127.0.0.1", 4001); //connects to SUMO simulation
       
       tlightids = client.TrafficLight.GetIdList().Content; //all traffic light IDs in the simulation
       client.Gui.TrackVehicle("View #0", "0");
       client.Gui.SetZoom("View #0", 1200); //tracking the player vehicle
        
        
        
        createTLS();
        client.Control.SimStep();
        client.Control.SimStep();//making sure vehicle is loaded in
        
        client.Vehicle.SetSpeed("0", 0); //stops SUMO controlling player vehicle
        var shape = client.Vehicle.GetPosition("0").Content;
        var angle = client.Vehicle.GetAngle("0").Content;
        //puts the player vehicle in the starting position
        egoVehicle.transform.position = new Vector3((float) shape.X, 1.33f, (float) shape.Y);
        egoVehicle.transform.rotation = Quaternion.Euler(0, (float)angle, 0);
        
        
        carlist.Add(egoVehicle);
        


    }

    private void OnApplicationQuit()
    {
        client.Control.Close();//terminates the connection upon ending of the scene
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        
        var newvehicles = client.Simulation.GetDepartedIDList("0").Content; //new vehicles this step
        var vehiclesleft = client.Simulation.GetArrivedIDList("0").Content; //vehicles that have left the simulation
        


        //if any vehicles have left the scene they are removed and destroyed
        for (int j = 0; j < vehiclesleft.Count; j++)
        {
            GameObject toremove = GameObject.Find(vehiclesleft[j]);
         
            if (toremove)
            {
                
                carlist.Remove(toremove);
                Destroy(toremove);
                

            }

        }


        //road and lane client are on, necessary for manipulation in SUMO
        var road = client.Vehicle.GetRoadID(egoVehicle.name).Content;
        var lane = client.Vehicle.GetLaneIndex(egoVehicle.name).Content;
        /*
         * Updates the ego-vehicle's position in the SUMO scene
         * @params id: ego-vehicle's name in the SUMO simulation
         * @params road: current edge the vehicle is on in the SUMO simulation
         * @params lane: current lane number the ego vehicle is on
         * @params xPosition: X-axis position of the ego-vehicle in the Unity scene
         * @params yPosition: Z-axis position of the ego-vehicle in the Unity scene
         * @params angle: The angle that the ego vehicle is facing at
         * @params keepRoute: maps the ego-vehicle to the exact X-Y position in the SUMO simulation
         */
        client.Vehicle.MoveToXY("0", road, lane, (double) egoVehicle.transform.position.x,
            (double) egoVehicle.transform.position.z, (double) egoVehicle.transform.eulerAngles.y, 2);
        
        for (int carid = 1; carid <carlist.Count;carid++)
        {
            

            
                var carpos = client.Vehicle.GetPosition(carlist[carid].name).Content; //gets position of NPC vehicle
                
                carlist[carid].transform.position = new Vector3((float) carpos.X, 1.33f, (float) carpos.Y);

                
                
                var newangle = client.Vehicle.GetAngle(carlist[carid].name).Content; //gets angle of NPC vehicle
                carlist[carid].transform.rotation = Quaternion.Euler(0f, (float) newangle, 0f);
                
                
            
        }

        for (int i = 0; i < newvehicles.Count; i++)
            {

                


                
               
                var newcarposition = client.Vehicle.GetPosition(newvehicles[i]).Content; //gets position of new vehicle
                


                GameObject newcar = GameObject.Instantiate(NPCVehicle); //creates the vehicle GameObject
                newcar.transform.position = new Vector3((float) newcarposition.X, 1.33f,
                    (float) newcarposition.Y);//maps its initial position
                var newangle = client.Vehicle.GetAngle(newvehicles[i]).Content;
                newcar.transform.rotation = Quaternion.Euler(0f, (float) newangle, 0f);//maps initial angle
                
                newcar.name = newvehicles[i];//object name the same as SUMO simulation version
                carlist.Add(newcar);
               
            }
        var currentphase = client.TrafficLight.GetCurrentPhase("42443658");
        client.Control.SimStep();
        //checks traffic light's phase to see if it has changed
        if (client.TrafficLight.GetCurrentPhase("42443658").Content != currentphase.Content)
        {
            changeTrafficLights();
        }







    }

    
    
    //Changes traffic lights to their next phase
    void changeTrafficLights()
    {
        for (int i = 0; i < tlightids.Count; i++)
        {
            //for each traffic light value of a junctions name
            for (int k = 0; k < dicti[tlightids[i]].Count; k++)
            {
				
                var newstate = client.TrafficLight.GetState(tlightids[i]).Content;
                var lightchange = dicti[tlightids[i]][k]; //retrieves traffic light object from list
                
                var chartochange = newstate[lightchange.index].ToString();//traffic lights new state based on its index
                if (lightchange.isdual == false)
                {
                    lightchange.changeState(chartochange.ToLower());//single traffic light change
                }
                else
                {
                    lightchange.changeStateDual(chartochange.ToLower());//dual traffic light change
                }

            }
        }

    }
    
    
    // Creates the TLS for of all junctions in the SUMO simulation
    
    void createTLS()
    {
        dicti = new Dictionary<string, List<traLights>>(); //the dictionary to hold each junctions traffic lights
        for (int ids = 0; ids < tlightids.Count; ids++)	
        {
            List<traLights> traLightslist = new List<traLights>();
            int numconnections = 0;  //The index that represents the traffic light's state value
            var newjunction = GameObject.Find(tlightids[ids]); //the traffic light junction
            for (int i = 0; i < newjunction.transform.childCount; i++)
            {
                bool isdouble = false;
                var trafficlight = newjunction.transform.GetChild(i);//the next traffic light in the junction
                //Checks if the traffic light has more than 3 lights
                if (trafficlight.childCount > 3)
                {
                    isdouble = true;
                }
                Light[] newlights = trafficlight.GetComponentsInChildren<Light>();//list of light objects belonging to
                                                                                  //the traffic light
               //Creation of the traffic light object, with its junction name, list of lights, index in the junction
               //and if it is a single or dual traffic light
                traLights newtraLights = new traLights(newjunction.name, newlights, numconnections, isdouble);
                traLightslist.Add(newtraLights);
                var linkcount = client.TrafficLight.GetControlledLinks(newjunction.name).Content.NumberOfSignals;
                var laneconnections = client.TrafficLight.GetControlledLinks(newjunction.name).Content.Links;
                if (numconnections+1 < linkcount - 1)
                {
                    numconnections++;//index increases
                    //increases index value until the next lane is reached
                    while ((laneconnections[numconnections][0] == laneconnections[numconnections - 1][0] || isdouble) &&
                           numconnections < linkcount - 1)
                    {
						//if the next lane is reached but the traffic light is a dual lane, continue until the
						//lane after is reached
                        if (laneconnections[numconnections][0] != laneconnections[numconnections - 1][0] && isdouble)
                        {
                            isdouble = false;
                        }
                        numconnections++;
                    }
                }
            }
            dicti.Add(newjunction.name, traLightslist);
        }
        changeTrafficLights(); //displays the initial state of all traffic lights
        print(550.4 + + 0.54 +776.4);
    }
    

   

    




}
