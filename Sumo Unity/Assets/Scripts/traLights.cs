using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traLights
{
	public int index;
	public Light[] lights;
	public string lightid;
	public Light r1;
	public Light y1;
	public Light g1;
	public Light r2;
	public Light y2;
	public Light g2;
	public bool isdual;
    /*
     * The Traffic light class
     * @params lightid: junction ID
     * @params lights: list of light objects belonging to the traffic light
     * @params index: the index in the SUMO state
     */
	public traLights(string lightid, Light[] lights, int index, bool isdual)
	{
	    this.lightid = lightid;
		this.lights = lights;
		this.index = index;
	    this.isdual = isdual;
		if (isdual)
		{
			 this.y1 = lights[0];
			 this.r1 = lights[1];
			 this.g1 = lights[2];
			 this.y2 = lights[3];
			 this.g2 = lights[4];
			 this.r2 = lights[5];
			 r1.intensity = 0;
			 r1.range = .8f;
			 y1.intensity = 0;
			 y1.range = .8f;
			 g1.intensity = 0;
			 g1.range = .8f;
			 r2.intensity = 0;
			 r2.range = .8f;
			 y2.intensity = 0;
			 y2.range = .8f;
			 g2.intensity = 0;
			 g2.range = .8f;
		}
		else
		{
			r1 = lights[0];
			
			this.g1 = lights[1];
			this.y1 = lights[2];
			r1.intensity = 0;
			r1.range = .8f;
			y1.intensity = 0;
			g1.range = .8f;
			g1.intensity = 0;
			g1.range = .8f;
		}
	}
    //Changes the state of a single traffic light
	public void changeState(string state)
	{
		
		switch (state)
		{
			case "r":
				r1.intensity = 10;
				y1.intensity = 0;
				break;
			case "y":
				y1.intensity = 10;
				g1.intensity = 0;
				break;
			case "g":
				r1.intensity = 0;
				g1.intensity = 10;
				break;
				 
		}
	}
	//Changes the state of a dual traffic light
	public void changeStateDual(string state)
	{
		
		switch (state)
		{
			case "r":
				r1.intensity = 10;
				y1.intensity = 0;
				r2.intensity = 10;
				y2.intensity = 0;
				break;
			case "y":
				y1.intensity = 10;
				g1.intensity = 0;
				y2.intensity = 10;
				g2.intensity = 0;
				break;
			case "g":
				r1.intensity = 0;
				g1.intensity = 10;
				r2.intensity = 0;
				g2.intensity = 10;
				break;
				 
		}
	}
	
   


}
