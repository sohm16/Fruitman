using UnityEngine;
using System.Collections;

public class movableController : MonoBehaviour {

	private Rigidbody2D body;
	void Start()
	{
		body = GetComponent<Rigidbody2D> ();
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player")
			body.isKinematic = false;        
	}
}