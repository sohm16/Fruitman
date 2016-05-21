using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	public Sprite bombSprite;
	public Sprite explosionSprite;

	private GameObject target;
	private Vector3 endingPosition;
	private SpriteRenderer sr;


	void Start() {
		target = GameObject.Find ("Player"); 
		endingPosition = target.transform.position;
		sr = GetComponent<SpriteRenderer> ();
	}

	private IEnumerator destoryBomb(GameObject bmb) {
		yield return new WaitForSeconds (0.5f);
		Destroy(bmb);
	}
	
	void Update () {
		// The step size is equal to speed times frame time.
		float step = speed * Time.deltaTime;

		// Move our position a step closer to the target.
		transform.position = Vector2.MoveTowards (transform.position, endingPosition, step);

		if ( transform.position == endingPosition) {
			sr.sprite = explosionSprite;
			StartCoroutine (destoryBomb(this.gameObject));

		}

	}
}
