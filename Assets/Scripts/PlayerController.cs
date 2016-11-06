using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float activeMoveSpeed;
	public Rigidbody2D myRigidBody;

	public bool canMove;

	public float jumpSpeed;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	private Animator myAnim;

	public Vector3 respawnPosition;

	public LevelManager theLevelManager;

	public GameObject stompBox;

	public float knockbackForce;
	public float knockbackLength;
	private float knockbackCounter;

	public float invincibilityLength;
	private float invincibilityCounter;

	public AudioSource jumpSound;
	public AudioSource hurtSound;

	private bool onPlatform;
	public float onPlatformSpeedModifier;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		respawnPosition = transform.position;
		theLevelManager = FindObjectOfType<LevelManager>();
		activeMoveSpeed = moveSpeed;
		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (knockbackCounter <= 0 && canMove) {

			if (onPlatform) {
				activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
			} else {
				activeMoveSpeed = moveSpeed;
			}

			if (Input.GetAxisRaw ("Horizontal") > 0f) {
				myRigidBody.velocity = new Vector3 (activeMoveSpeed, myRigidBody.velocity.y, 0f);
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
				myRigidBody.velocity = new Vector3 (-activeMoveSpeed, myRigidBody.velocity.y, 0f);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			} else {
				myRigidBody.velocity = new Vector3 (0f, myRigidBody.velocity.y, 0f);
			}

			if (Input.GetButton ("Jump") && isGrounded && !theLevelManager.isLastLevel) {
				//if (Input.GetButton("Jump")) {
				myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, jumpSpeed, 0f);
				jumpSound.Play();
			}
		}

		if (knockbackCounter > 0) {
			knockbackCounter -= Time.deltaTime;

			if (transform.localScale.x > 0) {
				myRigidBody.velocity = new Vector3 (-knockbackForce, knockbackForce, 0f);
			} else {
				myRigidBody.velocity = new Vector3 (knockbackForce, knockbackForce, 0f);
			}
		}

		myAnim.SetFloat( "Speed", Mathf.Abs( myRigidBody.velocity.x ) );
		myAnim.SetBool ("isGrounded", isGrounded);

		if (invincibilityCounter > 0) {
			invincibilityCounter -= Time.deltaTime;
		}

	
		if (invincibilityCounter <= 0) {
			theLevelManager.invincible = false;
		}

		if (myRigidBody.velocity.y < 0) {
			stompBox.SetActive(true);
		} else {
			stompBox.SetActive(false);
		}
	}

	public void Knockback(){
		knockbackCounter = knockbackLength;
		invincibilityCounter = invincibilityLength;
		theLevelManager.invincible = true;
	}

	void OnTriggerEnter2D( Collider2D other){
		if (other.tag == "KillPlane") {
			//gameObject.SetActive(false);
			//transform.position = respawnPosition;
			theLevelManager.Respawn();
		}

		if (other.tag == "Checkpoint") {
			respawnPosition = other.transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "MovingPlatform") {
			transform.parent = other.transform;
			onPlatform = true;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "MovingPlatform") {
			transform.parent = null;
			onPlatform = false;
		}
	}

}
