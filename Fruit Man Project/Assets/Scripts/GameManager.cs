using UnityEngine;
using System.Collections;

using System.Collections.Generic;		//Allows us to use Lists. 
using UnityEngine.UI;					//Allows us to use UI.

public class GameManager : MonoBehaviour
{
	
	public static GameManager gameManager = null;	//Static gameController of GameManager which allows it to be accessed by any other script.

	public int score;

	public GameObject player;
	public Vector3 playerSpawnPoint;
	public Camera cameraMainRoom;				

	public bool hasApplePower;
	public bool hasBananaPower;
	public bool hasCherryPower;
	public bool hasCoconutPower;

	public bool activeCoconutPower;
	public bool activeCherryPower;
	public bool activeApplePower;
	public bool activeBananaPower;

	public bool hitByEnemy;
	
	public Texture appleIcon;
	public Texture bananaIcon;
	public Texture coconutIcon;
	public Texture cherryIcon;
	//public GUIText healthText;

	public float scaleGUI;
	public string bossHealth;
	public bool enemyExists;
	
	public bool nuxMode;
	public bool playBossSounds;


	//Awake is always called before any Start functions
	void Start()
	{	
		//		Instantiate (player, playerSpawnPoint.transform.position, playerSpawnPoint.transform.rotation);
		//Check if gameController already exists
		playBossSounds = false;
		if (gameManager == null) {
			
			//if not, set gameManager to this
			gameManager = this;
		}
		//If gameManager already exists and it's not this:
		else if (gameManager != this) {
			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one gameManager of a GameManager.
			Destroy (gameManager);
			gameManager = this;
			cameraMainRoom.backgroundColor = Color.black;

		}

		gameManager.hitByEnemy = false;
		gameManager.hasApplePower = false;
		gameManager.hasBananaPower = false;
		gameManager.hitByEnemy = false;
		gameManager.activeApplePower = false;
		gameManager.activeBananaPower = false;
		gameManager.score = 0;


		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad (gameManager);

	}

	void Update() {
		if (player != null) {
			cameraMainRoom.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10f);
		}
		if (GameObject.FindGameObjectWithTag ("Spawned Enemy") != null)
			enemyExists = false;
		
		if (Input.GetKeyUp (KeyCode.N)) {
			if (nuxMode)
				nuxMode = false;
			else
				nuxMode = true;
		}
		
		if (nuxMode) {
			hasApplePower = true;
			hasBananaPower = true;
			hasCoconutPower = true;
		}
		if (player != null) {
			player.GetComponent<BoxCollider2D> ().isTrigger = nuxMode;
		}
		/*
		if (gameManager.hasApplePower)
			appleIcon.enabled = true;
		if (gameManager.hasBananaPower)
			bananaIcon.enabled = true;
		if (gameManager.hasCoconutPower)
			coconutIcon.enabled = true;
		if (Input.GetKey (KeyCode.R)) { // shoot right
			Destroy(player.gameObject);
			Application.LoadLevel(0);
			player = GameObject.Find("Player");
		}
		*/
	}

	void OnGUI () {
		
		if (nuxMode)
			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "NUXMODE ENABLED");
		
		GUI.DrawTexture(new Rect(0, Screen.height - Screen.height / 9, scaleGUI, scaleGUI), cherryIcon);
		
		if (gameManager.hasApplePower)
			GUI.DrawTexture(new Rect(scaleGUI * 2, Screen.height - Screen.height / 9, scaleGUI, scaleGUI), appleIcon);
		
		if (gameManager.hasBananaPower)
			GUI.DrawTexture(new Rect(scaleGUI, Screen.height - Screen.height / 9, scaleGUI, scaleGUI), bananaIcon);
		
		if (gameManager.hasCoconutPower)
			GUI.DrawTexture(new Rect(scaleGUI * 3, Screen.height - Screen.height / 9, scaleGUI, scaleGUI), coconutIcon);
		
		if (!gameManager.hasApplePower && !gameManager.hasBananaPower && !gameManager.hasCoconutPower)
			GUI.Label (new Rect(scaleGUI, Screen.height - Screen.height / 9, scaleGUI * 10, scaleGUI), " Move: WASD \n Attack: Arrow Keys \n Switch fruit: Number Keys");
	}
}