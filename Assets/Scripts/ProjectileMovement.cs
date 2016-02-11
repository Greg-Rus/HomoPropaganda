using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {
	public float projectileSpeed = 7f;
	public float torque = 7f;
	private Rigidbody2D myRigidBody;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myRigidBody.AddRelativeForce(Vector3.right * projectileSpeed);
		myRigidBody.AddTorque(torque);
	}
	
	// Update is called once per frame

	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			GameController.instance.PlayerInerceptedBossProjectile();
		}
		else if(coll.gameObject.tag == "Wall")
		{
			GameController.instance.BossHitFlag();
		}
		else{
		}
		Destroy(this.gameObject);
	}
}
