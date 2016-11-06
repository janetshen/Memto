using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string firstLevel;
	public string levelSelect;

	public int startingLives;

	public string[] levelNames;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame(){
		SceneManager.LoadScene (firstLevel);

		for (int i = 0; i < levelNames.Length; i++) {
			PlayerPrefs.SetInt (levelNames [i], 0);
			Debug.Log (levelNames [i] + " set to 0");

		}

//		PlayerPrefs.SetInt ("CoinCount", 0);
//		PlayerPrefs.SetInt ("PlayerLives", sadffasdf); 
//		PlayerPrefs.SetInt ("CurrentLives", startingLives); 
	}

	public void Continue(){
		SceneManager.LoadScene (levelSelect);
	}

	public void QuitGame(){
		Application.Quit();
	}
}
