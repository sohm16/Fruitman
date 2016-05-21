using UnityEngine;
using System.Collections;

public class CarrotController : MonoBehaviour {
	
	public Sprite carrotShotUp;
	public Sprite carrotShotDown;
	public Sprite carrotShotLeft;
	public Sprite carrotShotRight;
	private SpriteRenderer spriteRenderer;
	public AudioClip enemyDeath;
	private AudioSource audioSource;
	public GameObject carrotShot;
	private Vector2 shotVelocity;
	private Vector2 shotPosition;
	public int shotSpeed;
	public float fireRate;
	private float nextFire;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	void Update () {

		if (Time.time > nextFire) {

			nextFire = Time.time + fireRate;

			calculateShot (-1f, 0f);	// shoot left, right, down, up on given time
			calculateShot (1f, 0f);
			calculateShot (0f, -1f);
			calculateShot (0f, 1f);
		}
	}

	void calculateShot (float x, float y) {	// calculate pos, shoot, calc sprite & use velocity

		shotVelocity = new Vector2 (x * shotSpeed, y * shotSpeed);
		shotPosition = new Vector2 (transform.position.x + (x / 1.3f), transform.position.y + (y / 1.3f));
		GameObject projectile = (GameObject)Instantiate (carrotShot, shotPosition, transform.rotation);
		projectile.GetComponent <Rigidbody2D> ().velocity = shotVelocity;
		
		spriteRenderer = projectile.GetComponent <SpriteRenderer> ();

		if (x > 0) 
			spriteRenderer.sprite = carrotShotRight;
		else if (x < 0)
			spriteRenderer.sprite = carrotShotLeft;
		else if (y > 0)
			spriteRenderer.sprite = carrotShotUp;
		else
			spriteRenderer.sprite = carrotShotDown;
	}

	void OnTriggerEnter2D(Collider2D other) {	// check to see if player or player weapon hits me

		if (other.gameObject.tag == "Bananarang" || other.gameObject.tag == "Cherry Swing" || other.gameObject.tag == "AppleExploding") {	
			audioSource.clip = enemyDeath;
			audioSource.Play ();
			StartCoroutine (waitForSound());
			
			
		}
	}

	private IEnumerator waitForSound() {
		
		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		this.gameObject.GetComponent<Animator> ().enabled = false;
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy(this.gameObject);
		
		
	}
}