using UnityEngine;
using System.Collections;

public class camLook : MonoBehaviour {

    float h; //horizontal movement of the mouse
    float v; //vertical movement of the mouse

    public float hSpeed; //horizontal speed
    public float vSpeed; //vertical speed

    public float maxAngle; //Angle where the cam stops rotating

    Transform player;

    // Use this for initialization
    void Start () {
        player = this.transform.parent;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        h = Input.GetAxis("Mouse X") * hSpeed * Time.deltaTime;
        v += Input.GetAxis("Mouse Y") * vSpeed * Time.deltaTime;

        v = Mathf.Clamp(v, -maxAngle, maxAngle);


        transform.localEulerAngles = new Vector3(-v, transform.localEulerAngles.y, transform.localEulerAngles.z);
        //transform.Rotate(-v, 0, 0);

        player.Rotate(0, h, 0, Space.World);
    }
}
    