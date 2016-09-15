using UnityEngine;

public class OLDMOVEMENT : MonoBehaviour {
    
    public float speed;

    Vector3 velocity;

    public float fallSpeed;
    public float fallAcceleration;
    public float maxFallSpeed;

    public bool isJumping;
    public float jumpSpeed;


    CharacterController controller;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        velocity = new Vector3(0, 0, 0);


        //Direction to move
        if (Input.GetKey(KeyCode.A))
        {
            velocity += (-transform.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += (transform.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            velocity += (transform.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += (-transform.forward * Time.deltaTime * speed);
        }

        //Jumping
        
        if (controller.isGrounded)
        {
            isJumping = false;
        }
        if (isJumping == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
        }

        if (isJumping)
        {
            velocity += (new Vector3(0, 1, 0) * Time.deltaTime * jumpSpeed);
        }
        



        //Fall
        if (fallSpeed < maxFallSpeed)
        {
            fallSpeed += fallAcceleration;
            if (fallSpeed > maxFallSpeed)
            {
                fallSpeed = maxFallSpeed;
            }
        }
        if (controller.isGrounded)
        {
            fallSpeed = 0;
        }

        velocity += (new Vector3(0, -1, 0) * Time.deltaTime * fallSpeed);

        controller.Move(velocity);

    }


}
