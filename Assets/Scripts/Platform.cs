using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	public float ballBoostPower;
	public float reboudAngleUnit = 0.02f;
	public float maxReboudAngle = 70f;
	public float maxEextentY;
	// Use this for initialization
	void Start () {
		maxEextentY = (this.GetComponent<BoxCollider2D>().bounds.extents).y;
	}
	
	// Update is called once per frame
	void Update () {

		UpdatePlatformPosition();
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Ball")
		{
			Vector2 boostDirection = (coll.contacts[0].point - (Vector2)this.transform.position).normalized * ballBoostPower;
			//coll.gameObject.GetComponent<Rigidbody2D>().AddForce(boostDirection, ForceMode2D.Impulse);
			//coll.gameObject.GetComponent<Rigidbody2D>().velocity = boostDirection;
			//float angle = Mathf.Atan2(boostDirection.y,boostDirection.x) * Mathf.Rad2Deg;
			float paddleHitPoint = (transform.InverseTransformPoint(coll.contacts[0].point)).y;
			//float unitsFromCenter = paddleHitPoint / reboudAngleUnit;
			//unitsFromCenter = Mathf.Floor(unitsFromCenter);
			//float reboundAngle = unitsFromCenter * (MaxReboudAngle * reboudAngleUnit);
			
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
			Rigidbody2D ball = 	coll.gameObject.GetComponent<Rigidbody2D>();
			ball.gravityScale = 0f;
			ball.velocity = direction * ballBoostPower;
			
			
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
}
