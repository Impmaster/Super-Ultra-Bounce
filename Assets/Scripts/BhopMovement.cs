using UnityEngine;

public class BhopMovement : MonoBehaviour {

	// accelDir: normalized direction that the player has requested to move (taking into account the movement keys and look direction)
	// prevVelocity: The current velocity of the player, before any additional calculations
	// accelerate: The server-defined player acceleration value
	// max_velocity: The server-defined maximum player velocity (this is not strictly adhered to due to strafejumping)

	[SerializeField]
	private float friction = 8;

	[SerializeField]
	private float maxVelocityAir = 2;

	[SerializeField]
	private float maxVelocityGround = 4;

	[SerializeField]
	private float airAccelerate = 100;

	[SerializeField]
	private float groundAccelerate = 50;

	private Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float max_velocity)
	{
		float projVel = Vector3.Dot(prevVelocity, accelDir); // Vector projection of Current velocity onto accelDir.
		float accelVel = accelerate * Time.fixedDeltaTime; // Accelerated velocity in direction of movement

		// If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
		if(projVel + accelVel > max_velocity)
			accelVel = max_velocity - projVel;

		return prevVelocity + accelDir * accelVel;
	}

	public Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity)
	{
		// Apply Friction
		float speed = prevVelocity.magnitude;
		if (speed != 0) // To avoid divide by zero errors
		{
			float drop = speed * friction * Time.fixedDeltaTime;
			prevVelocity *= Mathf.Max(speed - drop, 0) / speed; // Scale the velocity based on friction.
		}

		// groundAccelerate and maxVelocityGround are server-defined movement variables
		return Accelerate(accelDir, prevVelocity, groundAccelerate, maxVelocityGround);
	}

	public Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity)
	{
		// airAccelerate and maxVelocityAir are server-defined movement variables
		return Accelerate(accelDir, prevVelocity, airAccelerate, maxVelocityAir);
	}
}
