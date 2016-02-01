using UnityEngine;
using System.Collections;

public class SpriteRenderOrderSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer[] renderers =  GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer rendererComponenet in renderers)
		{
			int currentOrder = rendererComponenet.sortingOrder;
			int newOrder = currentOrder + (int)(this.transform.position.y * -100f);
			rendererComponenet.sortingOrder = newOrder;
		}
	}

}
