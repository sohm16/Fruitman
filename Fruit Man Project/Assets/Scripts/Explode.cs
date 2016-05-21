using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	//public Sprite boulder;
	//public Sprite explodedBoulder;
	public AudioClip explosion;
	public GameObject messenger;


	private AudioSource audioSource;
	private SpriteRenderer sr;
	private bool explode;


	void Start() {
		audioSource = GetComponent<AudioSource>();
		StartCoroutine (KillOnAnimationEnd ());

	}

	private IEnumerator KillOnAnimationEnd() {

		yield return new WaitForSeconds (3.99975f);
		//transform.localScale = new Vector3 (6, 6);
		explode = true;
		Instantiate (messenger, transform.position, transform.rotation);
		yield return new WaitForSeconds (1.33225f);
		Destroy (gameObject);
	}


	void Update () {
		if (explode) {
			audioSource.clip = explosion;
			audioSource.Play ();
		}

	}
	/* might not need this because theres a explode messenger now
	void OnTriggerStay2D (Collider2D other) {

		if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spawned Enemy") && explode && other.gameObject.name != "Pumpking") {
			Destroy (other.gameObject);
		}

	}*/
}
