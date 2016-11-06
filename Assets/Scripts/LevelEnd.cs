using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour {

	public string levelToLoad;
	public string levelToUnlock;

	private PlayerController thePlayer;
	private CameraController theCamera;
	private LevelManager theLevelManager;

	private float waitToMove = .5f;
	public float waitToLoad;

	private bool movePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
		theCamera = FindObjectOfType<CameraController>();
		theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (movePlayer) {
			//old script: thePlayer.myRigidBody.velocity = new Vector3 (thePlayer.moveSpeed, thePlayer.myRigidBody.velocity.y, 0f);
			//new script: insert victory dance here	
		}
	}

	void OnTriggerEnter2D( Collider2D other ){
		if (other.tag == "Player") {
			//SceneManager.LoadScene (levelToLoad);

			StartCoroutine("LevelEndCo");
		}	
	}

	public IEnumerator LevelEndCo(){
		thePlayer.canMove = false;
		theCamera.followTarget = false;
		theLevelManager.invincible = true;
		thePlayer.myRigidBody.velocity = Vector3.zero;
		theLevelManager.levelMusic.Stop();

	//	theLevelManager.gameOverMusic.Play();

	//	PlayerPrefs.SetInt ("CoinCount", theLevelManager.coinCount);
	//	PlayerPrefs.SetInt ("CurrentLives", theLevelManager.currentLives);

		PlayerPrefs.SetInt (levelToUnlock, 1);

		yield return new WaitForSeconds(waitToMove);

		movePlayer = true;

		yield return new WaitForSeconds(waitToLoad);
		SceneManager.LoadScene (levelToLoad);
	}
}
