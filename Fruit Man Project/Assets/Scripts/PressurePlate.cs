using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

	//public GUIText appliedPressure;
	public bool isActive;
	public bool hiddenDoorOpen;
	public GameObject hiddenDoor;
	private Collider2D bombCollider;


	void Start(){
		isActive = false;
		hiddenDoorOpen = false;
		if(this.name == "Pressure Plate Puzzle Room" ){
			hiddenDoor.SetActive(true);
			hiddenDoorOpen = false;
			}
	}

	void Update() {
		if (hiddenDoorOpen == true) {
			if(this.name == "Pressure Plate Puzzle Room" ){
				//hiddenDoorOpen = true;
				this.hiddenDoor.SetActive(false);
				return;
			}
			hiddenDoor.SetActive(true);
		} 
		else {
			if(this.name == "Pressure Plate Puzzle Room"){
				//hiddenDoorOpen = false;
				this.hiddenDoor.SetActive(true);
				return;
			}
			hiddenDoor.SetActive(false);
		}

	}


	void OnTriggerExit2D(Collider2D other) {
		if ((other.tag == "Player" || other.tag == "Moveable Box")) {
			if(this.name == "Pressure Plate Puzzle Room"){
				hiddenDoorOpen = false;
				return;
			}
				hiddenDoorOpen = false;

		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((other.tag == "Player" || other.tag == "Moveable Box")) {
			if(this.name == "Pressure Plate Puzzle Room"){
				hiddenDoorOpen = true;
				return;
			}
			hiddenDoorOpen = true;
		}
	}
}
