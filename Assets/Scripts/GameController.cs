using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Text scoreFiled;
	public Text scoreMultiplyerField;
	public int score = 0;
	public int scoreMultiplyer = 1;
	public GameObject OponentGroup;
	private int oponentsLeft = 1;
	public GameObject Boss;
	public RectTransform BossPanel;
	private Animator bossPanelAnimator;
	private Animator canvasAnimator;
	private Slider bossHPSlider;
	public Canvas canvas;
	
	
	public static GameController instance;
	// Use this for initialization
	
	void Awake ()
	{
		instance = this;
	}
	void Start () {
		UpdateUI();
		//oponentsLeft = OponentGroup.transform.childCount;
		bossPanelAnimator = BossPanel.GetComponent<Animator>();
		bossHPSlider = BossPanel.GetComponentInChildren<Slider>();
		canvasAnimator = canvas.GetComponent<Animator>();
	}
	
	// Update is called once per fram
	
	public void OponentGlamorized(int HP)
	{
		scoreMultiplyer += HP;
		score += HP * scoreMultiplyer * 10;
		UpdateUI();
		oponentsLeft--;
		if(oponentsLeft == 0)
		{
			StartBossRound();
		}
	}
	
	private void StartBossRound()
	{
		//bossPanelAnimator.SetTrigger("Active");
		canvasAnimator.SetTrigger("BossRoundStart");
		Boss.SetActive(true);
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
	
	public void BossHit(float percentHP)
	{
		Debug.Log ("PercentHP: " + percentHP);
		if(percentHP > 0)
		{
			bossHPSlider.value = percentHP;
		}
		else
		{
			bossHPSlider.value = 0f;
			VoctoryState();
		}
	}
	private void VoctoryState()
	{
		Boss.SetActive(false);
	}
}
