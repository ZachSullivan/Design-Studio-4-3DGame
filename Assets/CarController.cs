using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CarController : MonoBehaviour {
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

    //Max amount of force that can be applied to the wheel
    public float maxTorque = 50.0f;

    //Current rigidbody found on our car
    public Rigidbody carBody;

    //GUI text for each dynamic UI element
    public Text speedometer;
    public Text playerHealth;
    public Text gameTime;
    public Text lapText;

    //This is our lap counter
    public Collider LapCollider;
    int lapCounter = 0;

    //User defined player health
    public float health = 100;

    //The amount of damage each pedestrian will inflict on the player
    public int dmgAmount = 1;

    //Variable to hold the current min + sec for the GUI timer
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

    //Update the game based on tick count rather than frame dependant
    void FixedUpdate () {

        //Move the car forward based on the max amount of torque and the current button pressed
        wheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
        wheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");

        //Show the speed to the screen with no decimal places
        speedometer.text = "Speed " + (GetComponent<Rigidbody>().velocity.magnitude * 3.6f).ToString("F0") + " km/h";

        //Steer the car based on a steering sensitivity and the current button pressed
        wheelFL.steerAngle = 10 * Input.GetAxis("Horizontal");
        wheelFR.steerAngle = 10 * Input.GetAxis("Horizontal");

        //Deaccelerate the car if there is no button pressed
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

    //Update the visual tire rotation based on the current framerate (better looking)
    void Update() {
        wheelFLTrans.Rotate(wheelFL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelFRTrans.Rotate(wheelFR.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRLTrans.Rotate(wheelRL.rpm * 6.0f * Time.deltaTime, 0, 0);
        wheelRRTrans.Rotate(wheelRR.rpm * 6.0f * Time.deltaTime, 0, 0);

        //When player health reaches 0, restart the game
        if (health <= 0) {
            Application.LoadLevel(Application.loadedLevelName);
        }

        //Update the current seconds and minutes for the GUI timer
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        gameTime.text = "Time " + minutes.ToString("00") + ":" + seconds.ToString("00");

        //If the player presses the space bar, respawn the player at the last known checkpoint
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.transform.position = currentCheckpoint.transform.position;
            this.transform.rotation = currentCheckpoint.transform.rotation;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    //Check when the player collides with an obstical (istrigger triggered)
    void OnTriggerEnter(Collider other) {

        //Only non checkpoints should trigger healthloss
        if (other != Checkpoint1.GetComponent<Collider>() 
            || other != Checkpoint2.GetComponent<Collider>()
            || other != Checkpoint3.GetComponent<Collider>()) {
            
            //Inflict damage to the player based on user defined amount
            health -= dmgAmount;
            playerHealth.text = "Health " + (health);
        }
        //Check if the player reached the lap collider
        if (other == LapCollider) {
            
            lapCounter += 1;
            lapText.text = "Lap " + (lapCounter) + "/2";

            if (lapCounter >= 3) {
                Application.LoadLevel(Application.loadedLevelName);
            }

            //reset the player health
            health = 100;
            playerHealth.text = "Health " + (health);
        }

        //Check if player has reached any of the checkpoints
        if (other == Checkpoint1.GetComponent<Collider>()) {
            currentCheckpoint = Checkpoint1.transform;
        }

        if (other == Checkpoint2.GetComponent<Collider>()) {
            currentCheckpoint = Checkpoint2.transform;
        }

        if (other == Checkpoint3.GetComponent<Collider>()) {
            currentCheckpoint = Checkpoint3.transform;
        }
    }
}
