﻿using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public bool bossActive;

	public float timeBetweenDrops;
	private float timeBetweenDropsStore;
	private float dropCount;

	public float waitForPlatforms;
	private float platformCount;

	public Transform leftPoint;
	public Transform rightPoint;
	public Transform dropSawSpawnPoint;

	public GameObject dropSaw;
	public GameObject theBoss;
	public bool bossRight;

	public GameObject rightPlatforms;
	public GameObject leftPlatforms;

	public bool takeDamage;

	public int startingHealth;
	private int currentHealth;

	public GameObject levelExit;

	public CameraController theCamera;

	private LevelManager theLevelManager;

	public bool waitingForRespawn;

	// Use this for initialization
	void Start () {
		timeBetweenDropsStore = timeBetweenDrops;
		dropCount = timeBetweenDrops;
		platformCount = waitForPlatforms;
		theBoss.transform.position = rightPoint.position;
		bossRight = true;
		currentHealth = startingHealth;
		theCamera = FindObjectOfType<CameraController> ();
		theLevelManager = FindObjectOfType<LevelManager> ();
	}

	// Update is called once per frame
	void Update () {

		if (theLevelManager.respawnCoActive) {
			bossActive = false;
			waitingForRespawn = true;
		}

		if (waitingForRespawn && !theLevelManager.respawnCoActive) {
			theBoss.SetActive (false);
			leftPlatforms.SetActive (false);
			rightPlatforms.SetActive (false);

			platformCount = waitForPlatforms;

			timeBetweenDrops = timeBetweenDropsStore;
			dropCount = timeBetweenDrops;

			theBoss.transform.position = rightPoint.position;
			bossRight = true;
			currentHealth = startingHealth;

			theCamera.followTarget = true;
			
			waitingForRespawn = false;
		}

		if (bossActive) {

			theCamera.followTarget = false;
			theCamera.transform.position = Vector3.Lerp(theCamera.transform.position,
							new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.smoothing * Time.deltaTime);

			theBoss.SetActive (true);

			if (dropCount > 0) {
				dropCount -= Time.deltaTime;
			} else {
				dropSawSpawnPoint.position = new Vector3 (Random.Range (leftPoint.position.x, rightPoint.position.x), dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);
				Instantiate (dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);
				dropCount = timeBetweenDrops;
			}

			if (bossRight) {
				if (platformCount > 0) {
					platformCount -= Time.deltaTime;
				} else {
					rightPlatforms.SetActive (true);
					leftPlatforms.SetActive (false);
				}
			} else {
				if (platformCount > 0) {
					platformCount -= Time.deltaTime;
				} else {
					leftPlatforms.SetActive (true);
					rightPlatforms.SetActive (false);
				}
			}

			if (takeDamage) {
				currentHealth -= 1;

				if (currentHealth <= 0) {
					levelExit.SetActive (true);
					gameObject.SetActive (false);
					theCamera.followTarget = true;
				}

				if (bossRight) {
					theBoss.transform.position = leftPoint.position;
				} else {
					theBoss.transform.position = rightPoint.position;
				}

				bossRight = !bossRight;
				rightPlatforms.SetActive (false);
				leftPlatforms.SetActive (false);

				platformCount = waitForPlatforms;

				timeBetweenDrops = timeBetweenDrops * .75f;

				takeDamage = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			bossActive = true;
		}

	}
}
