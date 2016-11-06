using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;
	private LevelManager theLevelManager;
	public GameObject thePauseScreen;
	private PlayerController thePlayer;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager>();
		thePlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale == 0f) {
				ResumeGame();
			} else {
				PauseGame();
			}
		}
	}

	public void PauseGame(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Time.timeScale = 0f;
			thePlayer.canMove = false;
			theLevelManager.levelMusic.Pause();
			thePauseScreen.SetActive (true);
		}
	}

	public void ResumeGame(){
		thePauseScreen.SetActive (false);
		Time.timeScale = 1f;
		theLevelManager.levelMusic.UnPause();
		thePlayer.canMove = true;
	}

	public void LevelSelect(){
	//	PlayerPrefs.SetInt ("CurrentLives", theLevelManager.currentLives);
	//	PlayerPrefs.SetInt ("CoinCount", theLevelManager.coinCount);
		Time.timeScale = 1f;
		SceneManager.LoadScene(levelSelect);
	}

	public void QuitToMainMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(mainMenu);
	}

}

