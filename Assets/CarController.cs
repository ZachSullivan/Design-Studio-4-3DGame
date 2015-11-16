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

    public GameObject Checkpoint1;
    public GameObject Checkpoint2;
    public GameObject Checkpoint3;
    Transform currentCheckpoint;


    // Use this for initialization
    void Start () {

        //Reset the center of mass to avoid flipping
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

        //When player health reaches 0, restart the game
        if (health <= 0) {
            Application.LoadLevel(Application.loadedLevelName);
        }

        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        gameTime.text = "Time " + minutes.ToString("00") + ":" + seconds.ToString("00");

        if (Input.GetKeyDown(KeyCode.Space)) {
            this.transform.position = currentCheckpoint.transform.position;
            this.transform.rotation = currentCheckpoint.transform.rotation;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != Checkpoint1.GetComponent<Collider>() 
            || other != Checkpoint2.GetComponent<Collider>()
            || other != Checkpoint3.GetComponent<Collider>()) {
        
            health -= dmgAmount;
            playerHealth.text = "Health " + (health);
        }
        //Check if the player reached the lap collider
        if (other == LapCollider)
        {


            lapCounter += 1;
            lapText.text = "Lap " + (lapCounter) + "/3";

            if (lapCounter >= 3)
            {
                Application.LoadLevel(Application.loadedLevelName);
            }

            //reset the player health
            health = 100;
            playerHealth.text = "Health " + (health);
        }

        //Check if player has reached any of the checkpoints
        if (other == Checkpoint1.GetComponent<Collider>())
        {
            currentCheckpoint = Checkpoint1.transform;
        }

        if (other == Checkpoint2.GetComponent<Collider>())
        {
            currentCheckpoint = Checkpoint2.transform;
        }

        if (other == Checkpoint3.GetComponent<Collider>())
        {
            currentCheckpoint = Checkpoint3.transform;
        }
    }
}
