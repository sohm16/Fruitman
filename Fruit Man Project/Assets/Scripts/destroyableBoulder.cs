using UnityEngine;
using System.Collections;

public class destroyableBoulder : MonoBehaviour {
	private SpriteRenderer sr;
	public Sprite blownUpSprite;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Apple Bomb") {
			StartCoroutine(KillBoulder(this.gameObject));
		}
	}

	private IEnumerator KillBoulder(GameObject bldr) {
		yield return new WaitForSeconds (5.332f);
		sr.sprite = blownUpSprite;
		Destroy(this.gameObject);
	}
}
