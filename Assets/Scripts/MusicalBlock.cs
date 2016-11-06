using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicalBlock : MonoBehaviour {

	/*
	public Sprite c; //Middle C 261.626
	public Sprite d; //Middle D 293.665
	public Sprite e; //Middle E 329.628
	public Sprite f; //Middle F 349.228
	public Sprite g; //Middle G 391.995
	public Sprite a; //Middle A 440.000
	public Sprite b; //Middle B 493.883
	public Sprite rest; */
	public Sprite[] spriteArray;
	public Sprite currentBlock;
	//private string[] currentLetter = {"c","d","e","f","g","a","b","blank"};
	public int letterPosition = 7; //0 for c -> 6 for g
	//private float frequency;
	public AudioSource blockSound;
	public AudioSource metronome;
	public int octave = 0;
	private bool brandNew = true;

	private float[] equalTemperamentCM = {1f //c equaltemperament
								, Mathf.Pow(2f, 1f/6f) //d
								, Mathf.Pow(2f, 1f/3f) //e
								, Mathf.Pow(2f, 5f/12f) //f
								, Mathf.Pow(2f, 7f/12f) //g
								, Mathf.Pow(2f, 3f/4f) //a
								, Mathf.Pow(2f, 11f/12f) //b
								};

	private bool playerOnBlock;

	// Use this for initialization
	void Start () {
		UpdateBlock();
		//blockSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {

		if (playerOnBlock){
		//basic tone change major scale
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				//Debug.Log ("Up arrow key pressed");
				if (letterPosition >= 6) {
					letterPosition = 0;
					if (brandNew) {
						octave = 0;
						Debug.Log ("BrandNew");
					} else {
						octave += 1;
						Debug.Log("Not BrandNew");
					}
				} else {
					letterPosition += 1;
				}
				UpdateBlock ();
				PlaySound ();
			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				//Debug.Log ("Down arrow key pressed");
				if (letterPosition == 0) {
					letterPosition = 6;
					if (brandNew) {
						octave = -1;
					} else {
						octave -= 1;
					}
				} else {
					letterPosition -= 1;
				}
				UpdateBlock ();
				PlaySound ();
			} else if (Input.GetKeyDown (KeyCode.Backspace) || Input.GetKeyDown (KeyCode.Delete)) {
				ResetBlock();
			} else if (Input.GetButtonDown ("Jump")) {
				PlaySound ();
			} else if (Input.GetKeyDown (KeyCode.PageUp) && letterPosition < 7) {	//octave changes
				octave += 1;
				UpdateBlock ();
				PlaySound ();
			} else if (Input.GetKeyDown (KeyCode.PageDown) && letterPosition < 7) {	//octave changes
				octave -= 1;
				UpdateBlock ();
				PlaySound ();
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			playerOnBlock = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			playerOnBlock = false;
		}
	}

	void UpdateBlock(){
		GetComponent<SpriteRenderer>().sprite = spriteArray [letterPosition];
		//brandNew = false;
	}

	void PlaySound(){
		if (letterPosition < 7) {
			blockSound.pitch = equalTemperamentCM [letterPosition] * Mathf.Pow(2, octave);
			blockSound.Play ();
			Debug.Log ("Octave: " + octave);
			Debug.Log ("Letter Position: " + letterPosition);
			brandNew = false;
		}
	}

	public void ResetBlock(){
		letterPosition = 7; //0 for c -> 6 for g
		octave = 0;
		brandNew = true;
		UpdateBlock();
	}
}
