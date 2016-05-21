using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.layer != 8) {
			Destroy (other.gameObject);
		}

	}
}