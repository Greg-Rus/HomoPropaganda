using UnityEngine;
using System.Collections;

public class KrychaGunController : MonoBehaviour {
	public Vector3 target;
	public float gunTurnDegreeDelta = 5f;
	public GameObject projectile;
	private ParticleSystemPlayer gunShotFX;
	
	// Use this for initialization
	void Start () {
		gunShotFX = this.GetComponentInChildren<ParticleSystemPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
		{
			RotateGun(this.transform, target); 
		}
	}
	
	private void RotateGun(Transform gun, Vector3 target)
	{
		Quaternion GunGoalRotation = Utilities.rotationFromForwardToVector(target - gun.position);
		//gun.rotation = Quaternion.LerpUnclamped(gun.rotation, GunGoalRotation, Time.deltaTime * gunTurnSpeed);
		gun.rotation = Quaternion.RotateTowards(gun.rotation, GunGoalRotation, gunTurnDegreeDelta);
	}
	
	public void Fire()
	{
		gunShotFX.Play();
		Instantiate(projectile, this.transform.position + this.transform.right * 1f, this.transform.rotation);
	}
	
	public void SetTarget(Vector3 target)
	{
		this.target = target;
	}
}
