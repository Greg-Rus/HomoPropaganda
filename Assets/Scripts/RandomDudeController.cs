using UnityEngine;
using System.Collections;

public class RandomDudeController : MonoBehaviour {
	public GameObject GlamVersion;
	public GameObject loveBurst;
	public int hatePoints;
	public int currentHatePoints;
	// Use this for initialization
	void Start () {
		currentHatePoints = hatePoints;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Ball")
		{
			currentHatePoints--;
			if(currentHatePoints == 0)
			{
				Glamorize();
			}
		}
	}
	
	private void Glamorize()
	{
		GameController.instance.OponentGlamorized(hatePoints);
		GameObject newInstance =  Instantiate(GlamVersion, this.transform.position, Quaternion.identity) as GameObject;
		newInstance.transform.parent = this.transform.parent;
		Destroy(this.gameObject);
		//Instantiate(loveBurst, this.transform.position, Quaternion.identity);
		Destroy (Instantiate(loveBurst, this.transform.position, Quaternion.identity), 1f);
	}
}
