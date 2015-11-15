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
    // Use this for initialization
    void Start () {

        GetComponent<Rigidbody>().centerOfMass = new Vector3 (0,-0.9f,0);

        waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointCar1"));
        waypoints.Reverse();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 RelWaypointPos = transform.InverseTransformPoint(new Vector3(
            waypoints[currentWaypoint].transform.position.x,
            transform.position.y,
            waypoints[currentWaypoint].transform.position.z));

        float aiSteer = RelWaypointPos.x / RelWaypointPos.magnitude;
        float aiTorque = RelWaypointPos.z / RelWaypointPos.magnitude - Mathf.Abs(aiSteer);

        print(aiTorque);

        if (RelWaypointPos.magnitude < 5)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }

        wheelRR.motorTorque = maxTorgue * aiTorque;
        wheelRL.motorTorque = maxTorgue * aiTorque;

        wheelFL.steerAngle = 10 * aiSteer;
        wheelFR.steerAngle = 10 * aiSteer;

        /*wheelRR.motorTorque = maxTorgue * Input.GetAxis("Vertical");
        wheelRL.motorTorque = maxTorgue * Input.GetAxis("Vertical");

        speedometer.text = "Speed " + (GetComponent<Rigidbody>().velocity.magnitude * 3.6f).ToString("F0") + " km/h";

        

        if (!Input.GetButton("Vertical"))
        {
            wheelRR.brakeTorque = deAccelerationSpeed;
            wheelRL.brakeTorque = deAccelerationSpeed;
        }
        else {
            wheelRR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
        }*/

    }

    void Update() {
        wheelFLTrans.Rotate(wheelFL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelFRTrans.Rotate(wheelFR.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRLTrans.Rotate(wheelRL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRRTrans.Rotate(wheelRR.rpm * 6.0f * Time.deltaTime, 0, 0);
    }

    void GetWaypoints()
    {
        // Now, this function basically takes the container object for the waypoints, then finds all of the transforms in it,
        // once it has the transforms, it checks to make sure it's not the container, and adds them to the array of waypoints.
        
        //var potentialWaypoints : Array = waypointContainer.GetComponentsInChildren(Transform);
       /* Array waypointArr = waypointContainer.GetComponentsInChildren(Transform);
        waypoints = new Array();

        for (var potentialWaypoint : Transform in potentialWaypoints)
        {
            if (potentialWaypoint != waypointContainer.transform)
            {
                waypoints[waypoints.length] = potentialWaypoint;
            }
        }*/
    }


}
