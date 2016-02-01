using UnityEngine;
using System.Collections;
using System;

public class GlamDudeController : MonoBehaviour {
	public Vector3 runGoalPosition;
	public float wallPositionX;
	public float runSpeed;
	private Action currentAction;
	private Animator myAnimator;
	// Use this for initialization
	void Start () {
		runGoalPosition = this.transform.position;
		runGoalPosition.x = wallPositionX + UnityEngine.Random.Range(0f,1.5f);
		currentAction = RunToGoalPosition;
		myAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		currentAction();
	}
	
	private void RunToGoalPosition()
	{
	
		if(this.transform.position.x < runGoalPosition.x)
		{
			Vector3 newPosition = this.transform.position;
			newPosition.x += runSpeed * Time.deltaTime;
			this.transform.position = newPosition;
		}
		else
		{
			Vector3 newScale = this.transform.localScale;
			newScale.x = -1f;
			this.transform.localScale = newScale;
			currentAction = Dance;
			myAnimator.SetBool("Dancing", true);
		}

	}
	
	private void Dance()
	{}
}
