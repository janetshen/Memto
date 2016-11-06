using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController thePlayer;

	public GameObject deathSplosion;
	public bool respawning = false;
	public bool invincible;
	public AudioSource levelMusic;
	public bool respawnCoActive;

	private MusicalBlock[] blocksToReset;

	public bool isLastLevel;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
		//levelMusic = FindObjectOfType<AudioSource> ();
		blocksToReset = FindObjectsOfType<MusicalBlock>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.R) && !respawning) {
			Respawn();
			respawning = true;
		}
	}

	public void Respawn(){
			StartCoroutine ("RespawnCo");
	}

	public IEnumerator RespawnCo(){

		respawnCoActive = true;

		thePlayer.gameObject.SetActive(false);

		Instantiate( deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds (waitToRespawn);

		respawnCoActive = false;
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive(true);

		respawning = false;
		for (int i = 0; i < blocksToReset.Length; i++) {
			blocksToReset[i].gameObject.SetActive(true);
			blocksToReset[i].ResetBlock();
		}
	}
}