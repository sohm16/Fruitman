using UnityEngine;
using System.Collections;

public class BananaSwitchController : MonoBehaviour {
	public GameObject objectAffected;
	public Sprite otherSprite;
	private SpriteRenderer sr;


	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Cherry Swing") {
			if(objectAffected.activeSelf == true){
				objectAffected.SetActive(false);
				sr.sprite = otherSprite;
			}

		}
	}


}
