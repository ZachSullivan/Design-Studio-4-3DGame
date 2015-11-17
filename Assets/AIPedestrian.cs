using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AIPedestrian : MonoBehaviour {

    //List of all waypoints the NPC should move between
    public List<GameObject> waypoints;

    //Start and end points
    Vector3 start;
    Vector3 destination;

    public GameObject player;

    float distance;

    //Check which path the user wants the AI to follow
    public bool usePath1 = true;

    //Speed at which the NPC should move
    float speed = 0.3f;
    void Start () {

        //Use the correct waypoints for the correct NPC 1 or 2
        if (usePath1){
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointPed1"));
        }
        else {
            waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("WaypointPed2"));
        }
        waypoints.Reverse();

        //Assign start and end points
        start = new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, waypoints[0].transform.position.z);
        destination = new Vector3(waypoints[1].transform.position.x, waypoints[1].transform.position.y, waypoints[1].transform.position.z);
    }
	
	void FixedUpdate () {

        //If the distance between the start and end is less then an amount then keep moving
        if (distance < 1) {

            distance += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, destination, distance);
        }
        else {
            //Otherwise flip the List and move again
            waypoints.Reverse();
            start = new Vector3(waypoints[0].transform.position.x, waypoints[0].transform.position.y, waypoints[0].transform.position.z);
            destination = new Vector3(waypoints[1].transform.position.x, waypoints[1].transform.position.y, waypoints[1].transform.position.z);
            distance = 0;
        }
    }



}
