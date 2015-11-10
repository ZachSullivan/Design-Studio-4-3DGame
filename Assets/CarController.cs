using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CarController : MonoBehaviour {

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

    public Rigidbody carBody;

    public Text speedometer;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody>().centerOfMass = new Vector3 (0,-0.9f,0);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        wheelRR.motorTorque = maxTorgue * Input.GetAxis("Vertical");
        wheelRL.motorTorque = maxTorgue * Input.GetAxis("Vertical");

        speedometer.text = "Speed " + (GetComponent<Rigidbody>().velocity.magnitude * 3.6f).ToString("F0") + " km/h";

        wheelFL.steerAngle = 10 * Input.GetAxis("Horizontal");
        wheelFR.steerAngle = 10 * Input.GetAxis("Horizontal");

        if (!Input.GetButton("Vertical"))
        {
            wheelRR.brakeTorque = deAccelerationSpeed;
            wheelRL.brakeTorque = deAccelerationSpeed;
        }
        else {
            wheelRR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
        }

	}

    void Update() {
        wheelFLTrans.Rotate(wheelFL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelFRTrans.Rotate(wheelFR.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRLTrans.Rotate(wheelRL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRRTrans.Rotate(wheelRR.rpm * 6.0f * Time.deltaTime, 0, 0);
    }

}
