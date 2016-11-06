using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResetOnRespawn : MonoBehaviour {

	private Vector3 startPosition;
	private Quaternion startRotation;
	private Vector3 startLocalScale;
	private Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		startRotation = transform.rotation;
		startLocalScale = transform.localScale;

		if (GetComponent<Rigidbody2D> () != null) {
			myRigidBody = GetComponent<Rigidbody2D> ();
		}

		if (GetComponent<Image> () != null) {
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetObject(){
		transform.position = startPosition;
		transform.rotation = startRotation;
		transform.localScale = startLocalScale;

		if (myRigidBody != null) {
			myRigidBody.velocity = Vector3.zero;
		}
	}
}
