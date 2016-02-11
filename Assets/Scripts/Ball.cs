using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Vector3 direction;
	public float speed;
	public float gravityModifier = 0.1f;
	public float velocityMag;
	public float correctiveAcceleration = 300f;
	public GameObject Marker;
	Rigidbody2D myRigidBody;
	private Vector3 startPosition;
	// Use this for initialization
	void Start () {
		direction = Vector3.up;
		myRigidBody = GetComponent<Rigidbody2D>();
		//myRigidBody.AddRelativeForce(Vector3.right * speed);
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
		//transform.position = newPosition;
		velocityMag = myRigidBody.velocity.magnitude;
		if(velocityMag < 20f)
		{
			myRigidBody.AddRelativeForce(myRigidBody.velocity * correctiveAcceleration);
		}
		if(velocityMag > 21f)
		{
			myRigidBody.AddRelativeForce(myRigidBody.velocity * -correctiveAcceleration);
		}
		if(!GameController.instance.gamePaused && GameController.instance.gameStarted)
		{
			if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				Marker.SetActive(true);
			}
			if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
			{
				Marker.SetActive(false);
			}
		}

	}
	
	public void SetGravityScale(float gravity)
	{
		myRigidBody.gravityScale = gravity;
	}
	
	public void SetVelocity(Vector3 velocity)
	{
		myRigidBody.velocity = velocity;
	}
	
	public void DisplayMarker( Vector2 direction)
	{
		Marker.transform.rotation = Utilities.rotationFromForwardToVector(new Vector3(direction.x, direction.y, 0f));
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Oponent")
		{
			//Destroy (coll.gameObject);	
			AudioFXController.instance.playHitSound();
			if(myRigidBody.gravityScale < 1f)
			{
				myRigidBody.gravityScale +=gravityModifier;
			}
		}
		else if(coll.gameObject.tag == "Wall")
		{
			AudioFXController.instance.playBounceSound();
			GameController.instance.BallHitWall();
		}
		else 
		{
			AudioFXController.instance.playBounceSound();
		}
	
	}
	public void Reset()
	{
		myRigidBody.isKinematic = true;
		myRigidBody.transform.position = startPosition;
		myRigidBody.isKinematic = false;
		//myRigidBody.velocity = Vector3.zero;
		//myRigidBody.gravityScale = 2f;
		
	}

}
