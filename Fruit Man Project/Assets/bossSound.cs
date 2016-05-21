using UnityEngine;
using System.Collections;

public class bossSound : MonoBehaviour {
	private AudioSource aSource;
	public AudioClip clip;
	private bool playOnce;

	// Use this for initialization
	void Start () {
		aSource = GetComponent<AudioSource> ();
		playOnce = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player" && playOnce) {

			aSource.clip = clip;
			aSource.Play ();
			playOnce = false;
		}
	}
}
