using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour {

	public string levelToLoad;
	public string nextLevel;

	public bool unlocked;

	public Sprite doorBottomOpen;
	public Sprite doorTopOpen;
	public Sprite doorBottomClosed;
	public Sprite doorTopClosed;

	public SpriteRenderer doorTop;
	public SpriteRenderer doorBottom;

	private bool inFrontOfDoor;
	private bool playing;

	public AudioSource wholeMusic;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Level1", 1);

		if (PlayerPrefs.GetInt (levelToLoad) == 1) {
			unlocked = true;
		} else {
			unlocked = false;
		}

		if (unlocked) {
			doorTop.sprite = doorTopOpen;
			doorBottom.sprite = doorBottomOpen;
		} else {
			doorTop.sprite = doorTopClosed;
			doorBottom.sprite = doorBottomClosed;
		}

		//wholeMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (inFrontOfDoor && Input.GetKeyDown(KeyCode.UpArrow) && unlocked) {
				SceneManager.LoadScene (levelToLoad);
		}
		if (inFrontOfDoor && Input.GetKeyDown(KeyCode.DownArrow) && unlocked) {
			wholeMusic.Play ();
		}

	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			inFrontOfDoor = true;
			//wholeMusic.Play ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			inFrontOfDoor = false;
			wholeMusic.Stop ();
		}
	}

}
