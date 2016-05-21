using UnityEngine;
using System.Collections;

public class broccoliController : MonoBehaviour {
	public float velocity;
	private Rigidbody2D rb2d;
	public bool sideToSide; //false = up and down, true = side to side

	public AudioClip enemyDeath;
	private AudioSource audioSource;

	Vector2 leftAndRight;
	Vector2 upAndDown;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		rb2d = GetComponent<Rigidbody2D> ();
		sideToSide = true;
		velocity = 2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		upAndDown = new Vector2(0,velocity) * Time.fixedDeltaTime;
		leftAndRight = new Vector2 (velocity, 0) * Time.fixedDeltaTime;
		if (sideToSide) {
			rb2d.MovePosition (rb2d.position + leftAndRight);
		} else {//rb2d.AddForce (new Vector2 (0, velocity), ForceMode2D.Force);
			rb2d.MovePosition (rb2d.position + upAndDown);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Bananarang" || other.gameObject.tag == "Cherry Swing" || other.gameObject.tag == "AppleExploding") {
			audioSource.clip = enemyDeath;
			audioSource.Play ();
			StartCoroutine (waitForSound());

		}
			velocity = -velocity;
	}

	private IEnumerator waitForSound() {
		
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		this.gameObject.GetComponent<Animator> ().enabled = false;
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy(gameObject);
		
		
	}
}
