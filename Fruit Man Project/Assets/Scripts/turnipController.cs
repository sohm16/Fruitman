using UnityEngine;
using System.Collections;

public class turnipController : MonoBehaviour {
	public float width;
	public float height;
	public float speed;
	public float dmgFlashTime;
	public AudioClip enemyDeath;

	private AudioSource audioSource;
	private Rigidbody2D rb2d;
	private float timerCounter;
	// Use this for initialization

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timerCounter += Time.deltaTime*speed;

		float x = Mathf.Cos (timerCounter) * width;
		float y = Mathf.Sin (timerCounter) * height;
		rb2d.MovePosition (rb2d.position + new Vector2(x, y) * Time.fixedDeltaTime);
	
	}

	private IEnumerator waitForSound() {

		this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		this.gameObject.GetComponent<Animator> ().enabled = false;
		this.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy(this.gameObject);


	}
	

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "Bananarang" || other.gameObject.tag == "Cherry Swing" || other.gameObject.tag == "AppleExploding") {	
			audioSource.clip = enemyDeath;
			audioSource.Play ();
			StartCoroutine (waitForSound());

		}
	}
}
