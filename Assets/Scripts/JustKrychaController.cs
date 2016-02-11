using UnityEngine;
using System.Collections;

public class JustKrychaController : MonoBehaviour {
	Animator myAnimator;
	public Vector3 escapePosition;
	public float runTime;
	public bool startRun =false;
	private Vector3 startPosition;
	private float time = 0;
	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator>();
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("KrychaRun"))
		{
			time = time + (Time.deltaTime / runTime);
			transform.position = Vector3.Lerp(startPosition, escapePosition, time);
			if(time >= 1f)
			{
				GameController.instance.OutroFinished();
			}
		}
		
	
	}
}
