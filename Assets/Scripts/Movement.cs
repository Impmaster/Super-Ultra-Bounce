using UnityEngine;
using UnityEngine.UI;


public class Movement : MonoBehaviour {


    //Object references
    public Text totalSpeed;
    public Text movementType;
    private BoxCollider feet;
    private CharacterController controller;
    private BhopMovement bhopScript;


    //Variables
    [SerializeField]
    private float fallSpeed;

    [SerializeField]
    private float fallAcceleration = 1;

    [SerializeField]
    private float maxFallSpeed = 100;

    [SerializeField]
    private bool isJumping;

    [SerializeField]
    private float jumpSpeed = 15;

    [SerializeField]
    private float jumpDistance;

    [SerializeField]
    private float distanceMoved;

    //Deals with the movement on the ground and in the air
    [SerializeField]
    private Vector3 horizontalVelocity;

    //Deals with falling and jumping
    [SerializeField]
    private Vector3 verticalVelocity;

    //Magnitude of the horizontalVelocity
    private float speed;
    
    [SerializeField]
    private bool isGrounded;


    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        bhopScript = gameObject.GetComponent<BhopMovement>();
        verticalVelocity = new Vector3(0, 0, 0);
        horizontalVelocity = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {

        //Shows the player how fast he's going
        speed = horizontalVelocity.magnitude;
        Mathf.Round(speed);
        totalSpeed.text = speed.ToString();

        //Apply all the movement
        controller.Move(verticalVelocity * Time.deltaTime);
        controller.Move(horizontalVelocity * Time.deltaTime);
                   
        //The direction that the new movement will go           
        Vector3 targetDir = new Vector3(0, 0, 0);

        //Direction to move
        if (Input.GetKey(KeyCode.A))
        {
            targetDir += (-transform.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetDir += (transform.right);
        }
        if (Input.GetKey(KeyCode.W))
        {
            targetDir += (transform.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            targetDir += (-transform.forward);
        }

        targetDir.Normalize();
        
        //If on the ground
        if (isGrounded) {
            movementType.text = "Ground";
            horizontalVelocity = bhopScript.MoveGround(targetDir, horizontalVelocity);
        //If in the air
        } else {
            movementType.text = "Air ";
            horizontalVelocity = bhopScript.MoveAir(targetDir, horizontalVelocity);
        }

        //Jumping


        //If the character is ready to jump
        if (isJumping == false && isGrounded)
        {
            fallSpeed = 0;
            verticalVelocity = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
        }

        if (isJumping)
        {

            verticalVelocity = new Vector3(0, jumpSpeed, 0);
            distanceMoved += verticalVelocity.y * Time.deltaTime;

            if (distanceMoved > jumpDistance) {
                verticalVelocity = new Vector3(0, 0, 0);
                isJumping = false;
                distanceMoved = 0;
            }
            
            
        }

        


        if (!isJumping) {
            //Falling Code
            if (fallSpeed < maxFallSpeed)
            {
                //Make falling faster
                fallSpeed += fallAcceleration;
                if (fallSpeed > maxFallSpeed)
                {
                    //Limit the fall Speed
                    fallSpeed = maxFallSpeed;
                }
            }
            if (isGrounded)
            {
                
                fallSpeed = 0;
                verticalVelocity = new Vector3(0, 0, 0);
                isJumping = false;

            }

        }

        

        //Limit the falling
        if (verticalVelocity.y > -maxFallSpeed) {
            //Make the player fall
            verticalVelocity += (new Vector3(0, -1, 0) * Time.deltaTime * fallSpeed);
        } else {
            verticalVelocity = new Vector3(0, -maxFallSpeed, 0);
        }
        

    }



    //Checks the feet to see if the player is on the floor or not
    void OnTriggerEnter() {
       isGrounded = true; 
    }

    void OnTriggerStay() {
        isGrounded = true;
    }

    void OnTriggerExit() {
        isGrounded = false;
    }



}
