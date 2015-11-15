using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AIPedestrian : MonoBehaviour {

    public List<GameObject> waypoints;

    Vector3 start;
    Vector3 destination;

    public GameObject player;

    float distance;

    //Check which path the user wants the AI to follow
    public bool usePath1 = true;

    float speed = 0.3f;
    void Start () {

        if (usePath1){
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointPed1"));
        }
        else {
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointPed2"));
        }
        waypoints.Reverse();

        start = new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, waypoints[0].transform.position.z);
        destination = new Vector3(waypoints[1].transform.position.x, waypoints[1].transform.position.y, waypoints[1].transform.position.z);
    }
	
	void FixedUpdate () {
        if (distance < 1)
        {
            distance += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, destination, distance);
        }
        else {
            waypoints.Reverse();
            start = new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, waypoints[0].transform.position.z);
            destination = new Vector3(waypoints[1].transform.position.x, waypoints[1].transform.position.y, waypoints[1].transform.position.z);
            distance = 0;
        }
    }



}
