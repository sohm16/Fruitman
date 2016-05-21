using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {

	public float speed;
	public Sprite goodSprite;

	// Use this for initialization
	void Start () {

		speed = -speed;
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed);
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Cherry Swing") {
			speed = -speed;
			gameObject.tag = "Fireball Good";
			GetComponent<SpriteRenderer>().sprite = goodSprite;
		}
	}
}
