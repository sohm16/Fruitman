using UnityEngine;
using System.Collections;

public class DestroyOnImpact : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Wall")
			Destroy (gameObject);
	}
}