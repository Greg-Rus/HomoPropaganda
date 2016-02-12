using UnityEngine;
using System.Collections;
using System;

public class KrychaController : MonoBehaviour {
	public Transform[] waypoints;
	public float speed;
	public float reloadTime;
	public Transform LeftGun;
	public Transform RightGun;
	public Transform Target;
//	public float gunTurnDegreeDelta = 5f;
//	public GameObject projectile;
	public float maxHP = 100f;
	public float currentHP;
	private Action currentAction;
	private int currentWaypointIndex;
	private Animator myAnimator;
	private float walkAnimationSpeed;
	public float nextRightShotTime;
	public float nextLeftShotTime;
//	private Vector3 LeftGunTargetPosition;
//	private Vector3 RightGunTargetPosition;
	private Vector3 targetExtents;
//	private ParticleSystemPlayer RightGunShotFX;
//	private ParticleSystemPlayer LeftGunShotFX;
	private KrychaGunController rightGunController;
	private KrychaGunController leftGunController;
	private Vector3 startPosition;
	// Use this for initialization
	void Awake()
	{
		startPosition = this.transform.position;
	}
	
	void Start () {
		currentWaypointIndex = 0;
		currentAction = GoToWaypoint;
		myAnimator = GetComponentInChildren<Animator>();
		nextLeftShotTime = reloadTime*3; //TODO The *3 is to delay the shot until Krycha enters the arena. Need to do this in a proper way.
		nextRightShotTime = reloadTime*3 + reloadTime * 0.5f;
		targetExtents = Target.GetComponent<BoxCollider2D>().bounds.extents;
		currentHP = maxHP;
		rightGunController = LeftGun.GetComponent<KrychaGunController>();
		leftGunController = RightGun.GetComponent<KrychaGunController>();
		
//		RightGunShotFX = RightGun.GetComponentInChildren<ParticleSystemPlayer>();
//		LeftGunShotFX = LeftGun.GetComponentInChildren<ParticleSystemPlayer>();
//		if(RightGunShotFX == null || LeftGunShotFX == null) Debug.LogError("ParticlePlayers not found");
	}
	void OnEnable() {
		this.transform.position = startPosition;
		nextLeftShotTime = Time.timeSinceLevelLoad + reloadTime*3; //TODO The *3 is to delay the shot until Krycha enters the arena. Need to do this in a proper way.
		nextRightShotTime = Time.timeSinceLevelLoad + reloadTime*3 + reloadTime * 0.5f;
		currentHP = maxHP;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		currentAction();
		UpdateGuns();
	}
	
	
	private void UpdateGuns()
	{
//		RotateGun(LeftGun, LeftGunTargetPosition);
//		Debug.DrawLine(LeftGun.position, LeftGunTargetPosition, Color.red);
//		RotateGun(RightGun, RightGunTargetPosition);
//		Debug.DrawLine(RightGun.position, RightGunTargetPosition, Color.blue);

		if(nextLeftShotTime <= Time.timeSinceLevelLoad)
		{
			nextLeftShotTime += reloadTime;
			leftGunController.Fire();
			leftGunController.SetTarget(AcquireTarget());
			//FireGun(LeftGun, LeftGunShotFX, );
		}	
		if(nextRightShotTime <= Time.timeSinceLevelLoad)
		{
			nextRightShotTime += reloadTime;
			rightGunController.Fire();
			rightGunController.SetTarget(AcquireTarget());
			//FireGun(RightGun);
		}
		
	}
	/*	
	private void RotateGun(Transform gun, Vector3 target)
	{
		Quaternion GunGoalRotation = Utilities.rotationFromForwardToVector(target - gun.position);
		//gun.rotation = Quaternion.LerpUnclamped(gun.rotation, GunGoalRotation, Time.deltaTime * gunTurnSpeed);
		gun.rotation = Quaternion.RotateTowards(gun.rotation, GunGoalRotation, gunTurnDegreeDelta);
	}
	
	private void FireGuns()
	{
		RightGunShotFX.Play();
		LeftGunShotFX.Play();
	
		Instantiate(projectile, LeftGun.position + LeftGun.right * 1f, LeftGun.rotation);
		Instantiate(projectile, RightGun.position + RightGun.right * 1f, RightGun.rotation);
		
		LeftGunTargetPosition = AcquireTarget();
		RightGunTargetPosition = AcquireTarget();
	}
	*/
	private Vector3 AcquireTarget()
	{
		Vector3 target = new Vector3(Target.position.x, UnityEngine.Random.Range(-targetExtents.y, targetExtents.y), Target.position.z);
		return target;
	}
	
	private void SelectNewWaypoint()
	{
		int newWaypointIndex = UnityEngine.Random.Range(0, waypoints.Length);
		if(newWaypointIndex == currentWaypointIndex)
		{
			if(newWaypointIndex == 0)
			{
				newWaypointIndex++;
			}
			else 
			{
				newWaypointIndex--;
			}
		}
		if(waypoints[currentWaypointIndex].position.x > waypoints[newWaypointIndex].position.x)
		{
			myAnimator.SetFloat("PlaybackSpeed", -1f);
		}
		else 
		{
			myAnimator.SetFloat("PlaybackSpeed", 1f);
		}
		currentWaypointIndex = newWaypointIndex;
		currentAction = GoToWaypoint;
	}
	
	private void GoToWaypoint()
	{
		if (this.transform.position != waypoints[currentWaypointIndex].position)
		{
			float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(this.transform.position, waypoints[currentWaypointIndex].position, step);
		}
		else
		{
			currentAction = SelectNewWaypoint;
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
	    
		
		if(coll.gameObject.tag == "Ball")
		{
			currentHP = currentHP - 10f;
			ContactPoint2D[] contacts = coll.contacts;
			foreach(ContactPoint2D contact in contacts)
			{
				//Debug.Log (contact.otherCollider.name);
				if(contact.otherCollider.tag == "BossVulnerability")
				{
					currentHP = currentHP - 20f;
				}
			}
		}
		UpdateHealth();
	}
	private void UpdateHealth()
	{
		float percentHP = currentHP / maxHP;
		GameController.instance.BossHit(percentHP);
	}
}
