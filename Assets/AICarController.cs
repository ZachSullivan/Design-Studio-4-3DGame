using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AICarController : MonoBehaviour {

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    public Transform wheelFLTrans;
    public Transform wheelFRTrans;
    public Transform wheelRLTrans;
    public Transform wheelRRTrans;

    public float deAccelerationSpeed = 250.0f;

    public float maxTorgue = 50.0f;

    public float speed = 10.0f;
    public float rotSpeed = 100.0f;

    public List<GameObject> waypoints;
    public int currentWaypoint;

    //Check which path the user wants the AI to follow
    public bool aiCar1 = true;

    // Use this for initialization
    void Start () {

        GetComponent<Rigidbody>().centerOfMass = new Vector3 (0,-0.9f,0);

        if (aiCar1){
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointCar1"));
        }
        else {
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointCar2"));
        }
        
        waypoints.Reverse();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 steerVector = transform.InverseTransformPoint(new Vector3(waypoints[currentWaypoint].transform.position.x, transform.position.y, waypoints[currentWaypoint].transform.position.z));
        float newSteer = 100 * (steerVector.x / steerVector.magnitude);
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;

        if (steerVector.magnitude <= 5)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
                currentWaypoint = 0;
        }

        float currentSpeed = 2 * (22 / 7) * wheelRL.radius * wheelRL.rpm * 60 / 1000;
        currentSpeed = Mathf.Round(currentSpeed);
        if (currentSpeed <= 40)
        {
            wheelRL.motorTorque = 500;
            wheelRR.motorTorque = 500;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
        else
        {
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            wheelRL.brakeTorque = 100;
            wheelRR.brakeTorque = 100;
        }
    }

    void Update() {
        wheelFLTrans.Rotate(wheelFL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelFRTrans.Rotate(wheelFR.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRLTrans.Rotate(wheelRL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRRTrans.Rotate(wheelRR.rpm * 6.0f * Time.deltaTime, 0, 0);
    }
}
