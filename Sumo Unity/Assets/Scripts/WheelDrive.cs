using UnityEngine;
using System;

[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    [Tooltip("Maximum steering angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

	private bool isReverse = false;
    private WheelCollider[] m_Wheels;
    private float speed;

    // Find all the WheelColliders down in the hierarchy.
	void Start()
	{
		//initialising the Logitech wheel module
		Debug.Log("SteeringInit:" + LogitechGSDK.LogiSteeringInitialize(false));
		
		var inert = GetComponent<Rigidbody>().inertiaTensor;
	
		
		m_Wheels = GetComponentsInChildren<WheelCollider>();
		
		

		for (int i = 0; i < m_Wheels.Length; ++i) 
		{
			var wheel = m_Wheels [i];

			// Create wheel shapes only when needed.
			if (wheelShape != null)
			{
				var ws = Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}
	}
	


	
	void OnApplicationQuit()
	{
		Debug.Log("SteeringShutdown:" + LogitechGSDK.LogiSteeringShutdown());
	}
     //Displays the Speed that the ego-vehicle is currently travelling at
	private void OnGUI()
	{
		int mstokmh = (int)( speed * 3.6);
		
		GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), mstokmh.ToString() + " km/h");
	}

	// This is a really simple approach to updating wheels.
	// We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
	// This helps us to figure our which wheels are front ones and which are rear.
	void Update()
	{
		if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
		{
			LogitechGSDK.LogiPlaySpringForce(0, 0, 50, 100);
		}
		speed = GetComponent<Rigidbody>().velocity.magnitude;
		
		m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);
			float angle = 0;
		

		if (Input.GetKey(KeyCode.JoystickButton12) && isReverse == false)
		{
			
			isReverse = true;
			
		}
		if (Input.GetKey(KeyCode.JoystickButton13) && isReverse)
		{
			
			isReverse = false;
			
		}
		
		if (Input.GetAxis("Horizontal" )> 0.1175f || Input.GetAxis("Horizontal" )< -0.1175f)
		{
			
			 angle = maxAngle * Input.GetAxis("Horizontal");
		}

		float torque = 0;
		if (Input.GetAxis("Vertical") > 0 && isReverse == false){
			 torque = maxTorque * Input.GetAxis("Vertical");
		}
		if (Input.GetAxis("Vertical")> 0 && isReverse == true){
			torque = maxTorque * Input.GetAxis("Vertical")*-1;
		}
		if (speed >= 13.8889f)
		{
			torque = 0;
		}

		
		float handBrake = 0;
		if (Input.GetAxis("Brake") > 0)
		{
			handBrake = brakeTorque;
		}
		

		

		foreach (WheelCollider wheel in m_Wheels)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
			}

			if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
			{
				
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
			{
			 
				wheel.motorTorque = torque;
			}

			// Update visual wheels if any.
			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// Assume that the only child of the wheelcollider is the wheel shape.
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}
		}
	}
	
}
