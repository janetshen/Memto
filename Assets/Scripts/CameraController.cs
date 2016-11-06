using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float followAhead;
	public float floatAbove;

	private Vector3 targetPosition;
	public float smoothing;

	public bool followTarget;

	// Use this for initialization
	void Start () {
		followTarget = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (followTarget) {
			//targetPosition = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);
			targetPosition = new Vector3 (target.transform.position.x, target.transform.position.y + floatAbove, transform.position.z);

			if (target.transform.localScale.x > 0) { //facing right
				targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
			} else {
				targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
			}

			//transform.position = targetPosition;
			transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing);
		}
	}
}
