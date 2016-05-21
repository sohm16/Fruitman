using UnityEngine;
using System.Collections;

public class cornController : MonoBehaviour {
	public GameObject cornBomb;
	public float fireRate;
	public float distance;

	private GameObject target;
	private float nextFire;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player");
	}
	
	void Update (){
		if (target == null) {
			target = GameObject.Find ("Player");
		}
		if (target != null) {
			target = GameObject.Find ("Player");
		
			float temp = Vector2.Distance (transform.position, target.transform.position);
			while (Time.time > nextFire && temp < distance) {
			
				nextFire = Time.time + fireRate;
				Instantiate (cornBomb, transform.position, Quaternion.Euler (0, 0, 0));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Bananarang") {
			Destroy (this.gameObject);
			Destroy(other.gameObject);
		}
	}
}
