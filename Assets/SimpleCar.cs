using UnityEngine;
using System.Collections;

public class SimpleCar : MonoBehaviour
{

    /*
     * 4 Wheels, set those with the 4 WheelColliders that are parented to your car cube
     */
    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelFrontRight;
    public WheelCollider wheelRearLeft;
    public WheelCollider wheelRearRight;

    /*
     * Speed, breakingPower and turning angle
     */
    public float speed = 50.0f;
    public float breakPower = 100.0f;
    public float turning = 20.0f;

    // Update is called once per frame
    void Update()
    {

        /* front wheel drive*/
        wheelFrontLeft.motorTorque = Input.GetAxis("Vertical") * speed;
        wheelFrontRight.motorTorque = Input.GetAxis("Vertical") * speed;

        /* Make sure we are not breaking, we handle that later */
        wheelRearLeft.brakeTorque = 0;
        wheelRearRight.brakeTorque = 0;
        wheelFrontLeft.brakeTorque = 0;
        wheelFrontRight.brakeTorque = 0;

        /* turning */
        //wheelFrontLeft.steerAngle = Input.GetAxis("Horizontal") * turning;
        //wheelFrontRight.steerAngle = Input.GetAxis("Horizontal") * turning;

        /* apply breaking when break key is pressed*/
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Lol");
            wheelRearLeft.brakeTorque = breakPower;
            wheelRearRight.brakeTorque = breakPower;
        }
    }
}