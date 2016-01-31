using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Vector3 direction;
	public float speed;
	public float gravityModifier = 0.1f;
	Rigidbody2D myRigidBody;
	// Use this for initialization
	void Start () {
		direction = Vector3.up;
		myRigidBody = GetComponent<Rigidbody2D>();
		myRigidBody.AddRelativeForce(Vector3.right * speed);
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
		//transform.position = newPosition;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Oponent")
		{
			Destroy (coll.gameObject);	
			if(myRigidBody.gravityScale < 1f)
			{
				myRigidBody.gravityScale +=gravityModifier;
			}
		}
	
	}
}
