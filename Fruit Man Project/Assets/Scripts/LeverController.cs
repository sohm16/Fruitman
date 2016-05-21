using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour {
	public GameObject objectAffected;
	public Sprite otherSprite;
	public Sprite otherSprite2;
	public bool toggleable;
	private SpriteRenderer sr;
	private bool spriteBool;
	// Use this for initialization
	void Start () {
		toggleable = true;
		spriteBool = true;
		sr = this.GetComponent<SpriteRenderer> ();

		objectAffected.SetActive(true);
		if (this.name == "revealAppleButton") {
			objectAffected.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (toggleable) {
				if (objectAffected.activeSelf == true) {
					objectAffected.SetActive (false);
				} else {
					objectAffected.SetActive (true);
				}
				spriteBool = !spriteBool;
				if (spriteBool == true) {
					sr.sprite = otherSprite2;

				} else if (spriteBool == false) {
					sr.sprite = otherSprite;
				}

			
			}
			toggleable = false;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		toggleable = true;

	}
}
