using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class CheckSwitch : MonoBehaviour {

	public float waitBetweenNotes;

	//public AudioSource challengeSound;
	//public string answer;
	private string[] notesArray;

	private MusicalBlock[] notesToCheck;
	private MusicalBlock[] sortedNotesToCheck;
	public string[] stringNotesToCheck;
	public ChallengeSwitch challengeSwitch;
	public string[] correctNotes;
	private int letterPositionTranslated;
	private int octaveTranslated;

	private bool playing;
	private bool completeMatch = true;
	private bool standingOnSwitch;

	//Animation
	public Sprite switchOff;
	public Sprite switchOn;
	private SpriteRenderer switchCurrent;

	public AudioSource victorySound;

	public LevelEnd levelEndFlag;

	// Use this for initialization
	void Start () {
		switchCurrent = GetComponent<SpriteRenderer>();
		//challengeSwitch = FindObjectOfType<ChallengeSwitch> ();
		correctNotes = challengeSwitch.enterMusicChallengeHere.Split (' ');
		//levelEndFlag = FindObjectOfType<LevelEnd> ();
		levelEndFlag.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && standingOnSwitch && !playing) {
			playing = true;
			StartCoroutine("CheckChallenge");
		}
	}

	void OnTriggerStay2D( Collider2D other ){
		if (other.tag == "Player") {
			standingOnSwitch = true;
		}
	}

	void OnTriggerExit2D( Collider2D other ){
		if (other.tag == "Player") {
			standingOnSwitch = false;
		}
	}

	public IEnumerator CheckChallenge(){
		switchCurrent.sprite = switchOn;
		Debug.Log ("1 Image switched");

		notesToCheck = FindObjectsOfType<MusicalBlock>();
		Debug.Log ("2 Blocks grabbed");

		sortedNotesToCheck = new MusicalBlock[notesToCheck.Length];
		Debug.Log ("3 sortedNotesToCheck set to correct size");

		stringNotesToCheck = new string[notesToCheck.Length];
		Debug.Log ("4 stringNotesToCheck set to correct size");

		Debug.Log ("notesToCheck.Length =" + notesToCheck.Length);

		completeMatch = true; //reset evaluation.  Innocent till proven guilty.

		//sort by x value
		//step 1 - store current block positions
		for (int x = 0; x < notesToCheck.Length; x++) {
			sortedNotesToCheck[(int) notesToCheck [x].transform.position.x - 1] = notesToCheck[x];
		}

		Debug.Log ("sortedNotesToCheck set to correct size");

		//player's answer entry is played and secretly converted into a string array to compare with correct answer
		for (int i = 0; i < sortedNotesToCheck.Length; i++) {

			if (sortedNotesToCheck [i].letterPosition == 7) {
				stringNotesToCheck [i] = "_";
				sortedNotesToCheck [i].metronome.Play ();
			} else {
				letterPositionTranslated = sortedNotesToCheck [i].letterPosition + 1;
				octaveTranslated = sortedNotesToCheck [i].octave + 4;
				stringNotesToCheck [i] = letterPositionTranslated.ToString () + octaveTranslated.ToString ();
				sortedNotesToCheck [i].blockSound.Play ();
			}
			Debug.Log (stringNotesToCheck [i]);
			yield return new WaitForSeconds(waitBetweenNotes);
		}


		//and now for the comparison...
		for (int y = 0; y < stringNotesToCheck.Length; y++) {
			if (stringNotesToCheck [y] != correctNotes [y]) {
				completeMatch = false;
				Debug.Log ("Mismatch found at position " + y);
			} else {
				Debug.Log ("Pass: " + stringNotesToCheck[y] + " matches " + correctNotes[y]);
			}
		}

		if (completeMatch) {
			Debug.Log ("Trying to play victory sound");
			victorySound.Play ();
			levelEndFlag.gameObject.SetActive (true);
			//gameObject.SetActive (false);
		} else {
			Debug.Log ("Incorrect");
		}

		playing = false;
		switchCurrent.sprite = switchOff;
	}
}
