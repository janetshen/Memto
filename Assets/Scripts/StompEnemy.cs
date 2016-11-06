using UnityEngine;
using System.Collections;

public class StompEnemy : MonoBehaviour {

	public Rigidbody2D playerRigidBody;
	public float bounce;
	public GameObject deathSplosion;

	// Use this for initialization
	void Start () {
		playerRigidBody = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
			Instantiate (deathSplosion, other.transform.position, other.transform.rotation);
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, bounce, 0f);
		}

		if (other.tag == "Boss") {
			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, bounce, 0f);
			other.transform.parent.GetComponent<Boss> ().takeDamage = true;
		}
	}
}
