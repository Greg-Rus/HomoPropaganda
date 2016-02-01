using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Text scoreFiled;
	public Text scoreMultiplyerField;
	public int score = 0;
	public int scoreMultiplyer = 1;
	
	public static GameController instance;
	// Use this for initialization
	
	void Awake ()
	{
		instance = this;
	}
	void Start () {
		UpdateUI();
	}
	
	// Update is called once per fram
	
	public void OponentGlamorized(int HP)
	{
		scoreMultiplyer += HP;
		score += HP * scoreMultiplyer * 10;
		UpdateUI();
	}
	
	public void BallHitWall()
	{
		scoreMultiplyer = 1;
		UpdateUI();
	}
	
	public void UpdateUI()
	{
		scoreFiled.text = score.ToString();
		scoreMultiplyerField.text = scoreMultiplyer.ToString();
	}
}
