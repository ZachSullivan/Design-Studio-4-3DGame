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
    public Text playerHealth;
    public Text gameTime;
    public Text lapText;

    //This is our lap counter
    public Collider LapCollider;
    int lapCounter = 0;

    public float health = 100;

    //The amount of damage each pedestrian will inflict on the player
    public int dmgAmount = 1;

    float minutes;
    float seconds;

    

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

        if (health <= 0) {
            Destroy(this.gameObject);
        }

        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        gameTime.text = "Time " + minutes.ToString("00") + ":" + seconds.ToString("00");

    }

    void OnTriggerEnter(Collider other)
    {
        health -= dmgAmount;
        playerHealth.text = "Health " + (health);

        if (other == LapCollider) {


            lapCounter += 1;
            lapText.text = "Lap " + (lapCounter) + "/3";

            //reset the player health
            health = 100;
            playerHealth.text = "Health " + (health);
        }

    }

}
