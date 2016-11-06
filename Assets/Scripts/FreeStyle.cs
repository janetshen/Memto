using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FreeStyle : MonoBehaviour {

	public AudioSource blockSound;
	public AudioSource metronome;

	public InputField entry;
	public InputField saveLocation;

	public float waitBetweenNotes;

	public string enterMusicChallengeHere;
	public string[] notesArray;

	private bool playing;

	//Animation
	public Sprite switchOff;
	public Sprite switchOn;
	private SpriteRenderer switchCurrent;

	private bool standingOnSwitch;

	private float[] equalTemperamentCM = {1f //c equaltemperament
		, Mathf.Pow(2f, 1f/6f) //d
		, Mathf.Pow(2f, 1f/3f) //e
		, Mathf.Pow(2f, 5f/12f) //f
		, Mathf.Pow(2f, 7f/12f) //g
		, Mathf.Pow(2f, 3f/4f) //a
		, Mathf.Pow(2f, 11f/12f) //b
	};

	// Use this for initialization
	void Start () {
		notesArray = entry.text.Split(' ');
		Debug.Log ("Entry split complete");
		switchCurrent = GetComponent<SpriteRenderer>();
		Debug.Log ("GetComponentSpriteRenderer complete");
		saveLocation.text = "C:\\MyExportedMusic\\" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + ".txt";
		Debug.Log ("Savelocation default entered for user");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow) && !playing && standingOnSwitch & entry.text.Length > 0) {
			playing = true;
			Debug.Log ("Attempting to PlayEntry");
			StartCoroutine("PlayEntry");
			Debug.Log ("PlayEntry Compelte");
		}	
	}

	void OnTriggerStay2D( Collider2D other ){
		if (other.tag == "Player") {
			standingOnSwitch = true;
			//Debug.Log ("Standing on Switch");
		}
	}

	void OnTriggerExit2D( Collider2D other) {
		if (other.tag == "Player") {
			standingOnSwitch = false;
			//Debug.Log ("No longer standing on switch");
		}
	}

	public IEnumerator PlayEntry(){
		Debug.Log ("PlayEntryStarted");
		int octave = 0;
		int letterPosition = 7;

		switchCurrent.sprite = switchOn;
		Debug.Log ("Switch image changed");

		for (int i = 0; i < notesArray.Length; i++) {
			octave = 0;
			letterPosition = 7;

			if (notesArray [i] != "_") {

				Debug.Log ("Looping through notesArray");

				//Parse Octave
				if (int.TryParse (notesArray [i].Substring (notesArray [i].Length - 1, 1), out octave)) {
					octave = int.Parse (notesArray [i].Substring (notesArray [i].Length - 1, 1));
				} else {
					octave = 0;
				}

				//Parse Note
				if (int.TryParse (notesArray [i].Substring (0, 1), out letterPosition)) {
					letterPosition = int.Parse (notesArray [i].Substring (0, 1));
				} else {
					letterPosition = 7;
				}

				//Play Note
				if (letterPosition < 7) {
					blockSound.pitch = equalTemperamentCM [letterPosition - 1] * Mathf.Pow (2, octave - 4);
					blockSound.PlayOneShot (blockSound.clip, .5f);
				} 
			} else {
				metronome.PlayOneShot (metronome.clip, .3f);
			}

			yield return new WaitForSeconds (waitBetweenNotes);
		}

		playing = false;
		switchCurrent.sprite = switchOff;
	}

}
