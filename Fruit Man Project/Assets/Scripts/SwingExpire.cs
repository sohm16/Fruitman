using UnityEngine;
using System.Collections;

public class SwingExpire : MonoBehaviour {
	
	private IEnumerator KillOnAnimationEnd() {
		
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
	}
	
	void Update () {
		
		StartCoroutine (KillOnAnimationEnd ());
	}
	/* controlled by enemycontrollers
	void OnTriggerEnter2D (Collider2D other) {
		
		if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spawned Enemy") && other.gameObject.name != "Pumpking")
			Destroy (other.gameObject);
	
	}*/
}