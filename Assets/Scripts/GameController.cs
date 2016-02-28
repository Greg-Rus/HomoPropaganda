using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public Text scoreFiled;
	public Text scoreMultiplyerField;
	public int score = 0;
	public int scoreMultiplyer = 1;
	public GameObject OponentGroupPrefab;
	public GameObject OponentGroupInstance;
	private int oponentsLeft = 1;
	public GameObject Boss;
	public GameObject BossPrefab;
	public GameObject JustKrycha;
	public GameObject JustKrychaPrefab;
	public GameObject BigExplosion;
	public RectTransform BossPanel;
	//private Animator bossPanelAnimator;
	private Animator canvasAnimator;
	private Slider bossHPSlider;
	public Canvas canvas;
	public GameObject MainMenuBackgroundPanel;
	public GameObject MainMenu;
	public GameObject VictoryPanel;
	public bool gamePaused = false;
	public bool gameStarted = false;
	public Ball ball;
	private MusicHandler music;
	//public GameObject ballPrefab;
	//private GameObject ballInstance;
	public Platform Player;

	
	
	
	public static GameController instance;
	// Use this for initialization
	
	void Awake ()
	{
		instance = this;
	}
	void Start () {
		UpdateUI();
		
		//bossPanelAnimator = BossPanel.GetComponent<Animator>();
		bossHPSlider = BossPanel.GetComponentInChildren<Slider>();
		canvasAnimator = canvas.GetComponent<Animator>();
		Time.timeScale = 0f;
		music = GetComponentInChildren<MusicHandler> ();
	}
	
	// Update is called once per fram
	void Update ()
	{

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!gamePaused && gameStarted)
			{
				MainMenu.SetActive(true);
				Time.timeScale = 0f;
				gamePaused = true;
			}
			else if(gamePaused && gameStarted)
			{
				MainMenu.SetActive(false);
				Time.timeScale = 1f;
				gamePaused = false;
			}
			
		}
	}
	
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
		music.PlayBossMusic ();
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
	
	public void PlayerInerceptedBossProjectile()
	{
		//score += 10 * scoreMultiplyer;
		//scoreMultiplyer++;
		//UpdateUI();
	}
	
	public void BossHitFlag()
	{
		scoreMultiplyer = 1;
		score -= 1000;
		if(score < 0)
		{
		 score = 0;
		}
		UpdateUI();
	}
	private void VoctoryState()
	{
		canvasAnimator.SetTrigger("BossRoundEnd");
		Boss.SetActive(false);
		GameObject explosion = Instantiate (BigExplosion, Boss.transform.position, Quaternion.identity) as GameObject;
		Destroy (explosion, 2f);
		JustKrycha = Instantiate(JustKrychaPrefab, Boss.transform.position, Quaternion.identity) as GameObject;
		
	}
	
	public void UI_NewGameStart()
	{
		bossHPSlider.value = 1f;
		MainMenuBackgroundPanel.SetActive(false);
		MainMenu.SetActive(false);
		gameStarted = true;
		gamePaused = false;
		Time.timeScale = 1f;
		VictoryPanel.SetActive(false);
		PlaceOponents();
		oponentsLeft = OponentGroupInstance.transform.childCount;
		//oponentsLeft = 1; //TODO use thoe one above
		ClearScore();
		ball.Reset();
		//PlaceBall();
		Player.Reset();
		music.PlayStandardMusic ();
	}
	
	public void OutroFinished()
	{
		VictoryPanel.SetActive(true);
		MainMenu.SetActive(true);
		Time.timeScale = 0f;
		gameStarted = false;
	}
/*	
	private void PlaceBall()
	{
		if(ballInstance != null)
		{
			Destroy(ballInstance);
		}
		ballInstance = Instantiate(ballPrefab, new Vector3 (9f,0f,0f), Quaternion.identity) as GameObject;
	}
*/	
	private void ClearScore()
	{
		score = 0;
		scoreMultiplyer = 1;
		UpdateUI();
	}
	
	private void PlaceOponents()
	{
		if (OponentGroupInstance != null)
		{
			Destroy(OponentGroupInstance);
		}
		OponentGroupInstance = Instantiate(OponentGroupPrefab, new Vector3 (9f,0f,0f), Quaternion.identity) as GameObject;
		
		//if(Boss != null)
	//	{
	//		Destroy(Boss);
	//	}
	//	Boss = Instantiate(BossPrefab, new Vector3 (-30f, 0f,0f), Quaternion.identity) as GameObject;
		
		if(JustKrycha != null)
		{
			Destroy(JustKrycha);
		}
	}
	
	private void toggleMainMenu()
	{
		if(!gamePaused && gameStarted)
		{
			MainMenu.SetActive(true);
			Time.timeScale = 0f;
			gamePaused = true;
		}
		else if(gamePaused && gameStarted)
		{
			MainMenu.SetActive(false);
			Time.timeScale = 1f;
			gamePaused = false;
		}
	}
	
	public void UI_QuitGame()
	{
		#if UNITY_WEBPLAYER
		Application.OpenURL("http://powerfantasygames.com/?p=51");
		#endif 

		Application.Quit();
	}
}
