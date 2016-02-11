using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	public float ballBoostPower;
	public float reboudAngleUnit = 0.02f;
	public float maxReboudAngle = 70f;
	public float maxEextentY;
	public GameObject Ball;
	private Ball ballController;
	private Rigidbody2D ballRigidBody;
	private Vector3 startPosition;
	// Use this for initialization
	void Start () {
		maxEextentY = (this.GetComponent<BoxCollider2D>().bounds.extents).y;
		//ballRigidBody = Ball.GetComponent<Rigidbody2D>();
		ballController = Ball.GetComponent<Ball>();
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.instance.gamePaused && GameController.instance.gameStarted)
		{
			UpdatePlatformPosition();
			if(Input.GetMouseButton(0))
			{
				Vector2 newVelocity = VectorToPlayer();
				DisplayMarker(newVelocity * -1f);
			}	
			if(Input.GetMouseButtonUp(0))
			{
				Vector2 newVelocity = VectorToPlayer();
				LinearBallBoost(newVelocity, -1f);
			}
			
			if(Input.GetMouseButton(1))
			{
				Vector2 newVelocity = VectorToPlayer();
				DisplayMarker(newVelocity * 1f);
			}
			if(Input.GetMouseButtonUp(1))
			{
				Vector2 newVelocity = VectorToPlayer();
				LinearBallBoost(newVelocity, 1f);
			}
			if(Input.GetButtonDown("Jump"))
			{
				Time.timeScale = 0.5f;
			}
			if(Input.GetButtonUp("Jump"))
			{
				Time.timeScale = 1f;
			}
		}
		
	}
	
	private void DisplayMarker(Vector2 velocity)
	{
		ballController.DisplayMarker(velocity);
	}
	
	private Vector2 VectorToPlayer()
	{
		return (this.transform.position - Ball.transform.position).normalized;
	}
	
	private void LinearBallBoost(Vector2 velocity, float direction)
	{
		ballController.SetVelocity(velocity  * ballBoostPower * direction);
		//ballRigidBody.velocity = velocity  * ballBoostPower * direction;
		ballController.SetGravityScale(0f);
		//ballRigidBody.gravityScale = 0;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Ball")
		{
			Vector2 boostDirection = (coll.contacts[0].point - (Vector2)this.transform.position).normalized * ballBoostPower;
			float paddleHitPoint = (transform.InverseTransformPoint(coll.contacts[0].point)).y;

			
			float angle = paddleHitPoint * maxReboudAngle / maxEextentY;
			float roundedAngleUnits = angle / reboudAngleUnit;
			roundedAngleUnits = Mathf.Floor(roundedAngleUnits);
			float rebaoundAngle = reboudAngleUnit * roundedAngleUnits;
			//Debug.Log ("Hit at: " + paddleHitPoint + " Angle: " + rebaoundAngle);
			Vector2 direction;
			direction = Vector2FromAngle(rebaoundAngle);
			direction.x = direction.x * -1f;
			direction = direction.normalized;
			Debug.DrawRay(this.transform.position, direction, Color.cyan, 2f);
			//Rigidbody2D ball = 	coll.gameObject.GetComponent<Rigidbody2D>();
			ballController.SetGravityScale(0f);
			ballController.SetVelocity(direction * ballBoostPower);
			//ball.gravityScale = 0f;
			//ball.velocity = direction * ballBoostPower;
			
			
			//coll.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * ballBoostPower, ForceMode2D.Impulse);
			
		}
		
	}
	void UpdatePlatformPosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 10.0f;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		
		
		Vector3 newPlatformPosition = this.transform.position;
		if(mousePosition.y < -Camera.main.orthographicSize)
		{
			newPlatformPosition.y = -Camera.main.orthographicSize;
			this.transform.position = newPlatformPosition;
		}
		else if(mousePosition.y > Camera.main.orthographicSize)
		{
			newPlatformPosition.y = Camera.main.orthographicSize;
			this.transform.position = newPlatformPosition;
		}
		else
		{
			newPlatformPosition.y = mousePosition.y;
			this.transform.position = newPlatformPosition;
		}
	}
	public Vector2 Vector2FromAngle(float a)
	{
		a *= Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
	}
	
	public void Reset()
	{
		this.transform.position = startPosition;
		Vector2 newVelocity = VectorToPlayer();
		LinearBallBoost(newVelocity, -1f);
	}
}
