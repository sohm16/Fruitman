using UnityEngine;
using System.Collections;

public class BananaPlate : MonoBehaviour {
	
	//public GUIText appliedPressure;
	public bool isActive;
	//public bool bananaMoved;
	public GameObject banana;
	private Collider2D bombCollider;
	
	
	void Start(){
		isActive = false;
		//bananaMoved = false;
	}
	
	void Update() {
		if (isActive && banana != null) {
			banana.transform.position = new Vector2(GameObject.Find("BananaSpot").transform.position.x, GameObject.Find("BananaSpot").transform.position.y);
		} 
		else if(banana != null && !isActive) {
			banana.transform.position = new Vector2(GameObject.Find("BananaOrigin").transform.position.x,GameObject.Find("BananaOrigin").transform.position.y);

		}
		
	}
	
	
	void OnTriggerExit2D(Collider2D other) {
		if ((other.tag == "Player" || other.tag == "Moveable Box")) {
			//appliedPressure.text = "Pressure Plate Deactivated";
			isActive = false;
			//bananaMoved = false;
			
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if ((other.tag == "Player" || other.tag == "Moveable Box")) {
			//appliedPressure.text = "Pressure Plate Activated";
			isActive = true;
			//bananaMoved = true;
		}
	}
}
