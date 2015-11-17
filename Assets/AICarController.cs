using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AICarController : MonoBehaviour {

    //Assign all wheel coliders
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    //Obtain all wheel transforms
    public Transform wheelFLTrans;
    public Transform wheelFRTrans;
    public Transform wheelRLTrans;
    public Transform wheelRRTrans;

    //Speed at which car deaccellerates at
    public float deAccelerationSpeed = 250.0f;
    
    //List containing waypoints AI will move towards IN ORDER
    public List<GameObject> waypoints;
    public int currentWaypoint;

    //Check which path the user wants the AI to follow OLD**
    public bool aiCar1 = true;

    // Use this for initialization
    void Start () {

        //Obtain the rigidbody of the car and reset the center of mass to avoid rolls
        GetComponent<Rigidbody>().centerOfMass = new Vector3 (0, -0.9f, 0);

        //***OLD while this works, I was receieving ordering problems, opted to manually assign waypoints in editor
        /*if (aiCar1){
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointCar1"));
        }
        else {
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointCar2"));
        }*/
        //waypoints = new List<GameObject>();

        //waypoints.Reverse();
	}

    //Update the game based on tick count rather than frame dependant
	void FixedUpdate () {

        //Point the car in the correct direction of the next waypoint
        Vector3 steerDir = transform.InverseTransformPoint(new Vector3(waypoints[currentWaypoint].transform.position.x, transform.position.y, waypoints[currentWaypoint].transform.position.z));

        //new temp variable is assigned a steering sensitivity then applied to the wheels
        float newSteer = 100 * (steerDir.x / steerDir.magnitude);
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;

        //If the distance between the car and the currentwaypoint is less then an amount increase to the nextwaypoint, otherwise reset
        if (steerDir.magnitude <= 5) {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count) {
                currentWaypoint = 0;
            }
        }

        //Current speed based on the radius of the wheel as well as the current RPM
        float currentSpeed = 2 * (22 / 7) * wheelRL.radius * wheelRL.rpm * 60 / 1000;

        //Normalize the current speed
        currentSpeed = Mathf.Round(currentSpeed);

        //Check that the current speed is no greater then a set amount
        if (currentSpeed <= 40) {
            wheelRL.motorTorque = 500;
            wheelRR.motorTorque = 500;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
        else { 
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            wheelRL.brakeTorque = 100;
            wheelRR.brakeTorque = 100;
        }
    }

    //Update the visual tire rotation based on the current framerate (better looking)
    void Update() {
        wheelFLTrans.Rotate(wheelFL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelFRTrans.Rotate(wheelFR.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRLTrans.Rotate(wheelRL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRRTrans.Rotate(wheelRR.rpm * 6.0f * Time.deltaTime, 0, 0);
    }
}
