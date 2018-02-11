using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float wheelspinSpeed;
	private int highestscore;
	private int currentScore;
	public int maximumLife;
	public Vector2 gapSize;
	private int lifeLeft = 0;
    public float delayToDisableUI = 0.5f;
	[SerializeField]
	private int maximunHandsToGenerate = 2;
	private int minimunGapBetweenHands =25;
	private int maximuHands; 

	private GameObject handsWheel;
	public float maximumSpinSpeed;  
	public float minimunSpinSpeed;
	public float TimeGaptoRandomlizeTheSpeed = 5f;
	public Text text_Score;
    [SerializeField]
    private GameObject[] lifeArray;
    [SerializeField]
    private GameObject EndGameMenu;
    [SerializeField]
    private Text EndGameText;
    public AudioSource shakehandSound;
    public AudioSource SlapSound;
    public AudioSource correctSound;
    public AudioSource wrongSound;
    public AudioSource EndSound;
    public GameObject prefebs;

	void Awake(){
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;
		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		//Sets this to not be destroyed when reloading scene
		//DontDestroyOnLoad(gameObject);

	}

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        lifeLeft = maximumLife;
		handsWheel = GameObject.FindGameObjectWithTag ("Wheel");
		maximuHands = 165/minimunGapBetweenHands;
        EndGameMenu.SetActive(false);
		InvokeRepeating ("RandomTheSpinSpeed", TimeGaptoRandomlizeTheSpeed, TimeGaptoRandomlizeTheSpeed);
		GenerateHandsAroundWheel ();
	}
	
	// Update is called once per frame
	void Update () {
		text_Score.text = currentScore.ToString();

		if (lifeLeft <= 0)
			GameEnd ();
	}

	private void RandomTheSpinSpeed(){
		wheelspinSpeed = Random.Range (minimunSpinSpeed,maximumSpinSpeed);
	}
		
	private void GameEnd(){
		Debug.Log ("Lose");
        UpdateHighestScore(currentScore);
        var temp = highestscore - currentScore;
        EndGameText.text = "You Made " + currentScore + " Deals\n" + temp +" 250 Deals Away From our Top Dealer ";
        EndGameMenu.SetActive(true);
        EndSound.Play();
        Time.timeScale = 0;
	}

	public void AddScore(){
		currentScore++;
	}

	public void MinusLife(){
        if (lifeLeft > 0)
        {
            lifeLeft--;
            lifeArray[lifeLeft].SetActive(false);
        }

	}

	public void AddLife(){
		if (lifeLeft < maximumLife)
        {
            lifeLeft++;
            lifeArray[lifeLeft-1].SetActive(true);
        }

	}

	public void UpdateHighestScore(int score){
        if (score > highestscore)
        {
            highestscore = score;
        }
	}

	public void OnDestroyRespawnHands(){
		if (handsWheel != null) {
			int number =  Random.Range (1, maximunHandsToGenerate);
			for(int i = 0; i<number;i++){
				GenerateHandsAroundWheel ();
			}
		}
	}

	private void GenerateHandsAroundWheel(){
		GameObject handHolder = Instantiate (prefebs, handsWheel.transform.position, Quaternion.identity);
		int handType = Random.Range (0, 2);// 1 means Bad Hand
		int goodhandStyle = 100;
		int badhandStyle = 100;
		if (handType == 1) {
			badhandStyle = Random.Range (0, (int)BadHandStyle.BadHand6+1);
		} else {
			goodhandStyle = Random.Range (0, (int)GoodHandStyle.GoodHand2+1);
		}
		SelectionHands selectionHandsScript = handHolder.GetComponent<SelectionHands> ();
		selectionHandsScript.handType = (HandsType)handType;
		selectionHandsScript.goodHandStyle = (GoodHandStyle)goodhandStyle;
		selectionHandsScript.badHandStyle = (BadHandStyle)badhandStyle;
		//move up 
		handHolder.transform.Translate(Vector3.right*1.5f);
		//reset to the start positions
		handHolder.transform.RotateAround (handsWheel.transform.position, Vector3.forward, -75f);
		handHolder.transform.parent = handsWheel.transform;
		int degreeMulitplier = Random.Range (0, maximuHands);
		handHolder.transform.RotateAround (handsWheel.transform.position, Vector3.forward, minimunGapBetweenHands* degreeMulitplier);
	}

    public void RestartGame()
    {
        SceneManager.LoadScene("MainGamePlay");
        Time.timeScale = 1;
        currentScore = 0;
        EndGameMenu.SetActive(false);
        foreach (var item in lifeArray)
        {
            item.SetActive(true);
        }
    }
}
