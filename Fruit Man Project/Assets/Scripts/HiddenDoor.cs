using UnityEngine;
using System.Collections;

public class HiddenDoor : MonoBehaviour {
	public GameManager gameManager;
	public GameObject thisItem;
	public GameObject connection;
	public Camera cameraMainRoom;
	private Collider2D player;
	private AudioSource cameraAudioSource;
	public AudioClip bossMusic;
	public AudioClip caveMusic;
	public AudioClip outsideMusic;
	//public AudioClip mapTransition;

	// Use this for initialization
	void Start () {
		cameraMainRoom.backgroundColor = Color.black;
		cameraAudioSource = cameraMainRoom.GetComponent<AudioSource>();
		if (player != null) {
			cameraMainRoom.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10f);
		}
	}
		
	// Update is called once per frame
	void Update () {
		if (player != null) {
			cameraMainRoom.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10f);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			player = other;
		}
		if (other.tag == "Player" && thisItem.name == "toMainFromBanana") {
			other.transform.position = new Vector2(connection.transform.position.x - .6f, connection.transform.position.y);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toBananaFromApple") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y - 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toBananaFromMain") {
			//cameraAudioSource.clip = mapTransition;
			//cameraAudioSource.loop = false;
			//cameraAudioSource.Play ();

			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y - 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toAppleFromBanana") {
			other.transform.position = new Vector2(connection.transform.position.x + 2f, connection.transform.position.y);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toCoconutFromApple") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y - 1.5f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = outsideMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toAppleFromCoco") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y + 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toPuzzleRoomFromCoconut") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y - 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = outsideMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toCoconutFromPuzzle") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y + 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toPumpkinFromPuzzle") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y + 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f); 
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toPuzzleFromPumpkin") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y - 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toPumpkin1FromPumpkin2") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y - 1.5f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toPumpkin2FromPumpkin1") {
			other.transform.position = new Vector2(connection.transform.position.x + 1f, connection.transform.position.y);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = bossMusic;
			cameraAudioSource.Play();

			//gameManager.playBossSounds = true;
		}
		if (other.tag == "Player" && thisItem.name == "toAgendaFromBlank") {
			other.transform.position = new Vector2(connection.transform.position.x - 1f, connection.transform.position.y);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f); 
			cameraAudioSource.clip = outsideMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toBlankFromAgenda") {
			other.transform.position = new Vector2(connection.transform.position.x + 1f, connection.transform.position.y);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f); 
			cameraAudioSource.clip = outsideMusic;
			cameraAudioSource.Play();
		}
		if (other.tag == "Player" && thisItem.name == "toCoconutFromAgenda") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y + 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = outsideMusic;
			cameraAudioSource.Play();
		}

		if (other.tag == "Player" && thisItem.name == "toAgendaFromCoconut") {
			other.transform.position = new Vector2(connection.transform.position.x, connection.transform.position.y - 1f);
			cameraMainRoom.transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -10f);
			cameraAudioSource.clip = caveMusic;
			cameraAudioSource.Play();
		}
	}
}
