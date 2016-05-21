using UnityEngine;
using System.Collections;

public class PumpkingController : MonoBehaviour {

	private Rigidbody2D rb2d;
	private SpriteRenderer sRenderer;
	private Animator anim;
	public Sprite deathSprite;
	public GameManager gameManager;
	public GameObject fireball;

	public GameObject enemySpawnpoint;
	public GameObject carrotEnemy;
	public GameObject broccoliEnemy;
	public GameObject cornEnemy;
	public GameObject turnipEnemy;

	private float startingPosition;
	public float endingPosition;

	private Vector2 shotPosition;

	private string lastTrigger;

	private bool attacking;
	private bool attemptDash;
	private bool endDash;
	private bool dying;
	private bool damageable;
	
	private GameObject projectile;

	public int health;
	public int cherrySwordDmg;
	public int fireBallBackDmg;
	public int appleBombDmg;
	public int bananarangDmg;

	public float dashSpeed;
	public float timeBetweenAttacks;
	public float dmgPeriod;
	
	private AudioSource aSource;
	public AudioClip pumpkingLaugh;

	private int lastAttack;

	// Use this for initialization
	void Start () {
	
		rb2d = GetComponent<Rigidbody2D> ();
		sRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		aSource = GetComponent<AudioSource> ();

		startingPosition = transform.position.y;

		anim.SetTrigger ("Warning");
		lastTrigger = "Warning";

		shotPosition = new Vector2(transform.position.x, startingPosition - 1f);

		attacking = false;
		damageable = true;
	}

	void Update () {

		gameManager.bossHealth = "Pumpking Health: " + health;

		if (health <= 0 && !dying)
			StartCoroutine (isKilled ());

		if (!attacking)
			StartCoroutine (attack ());

		if (attemptDash && transform.position.y > endingPosition) {
			triggerer ("slash");
			rb2d.velocity = new Vector2 (0, -dashSpeed);
		}

		if (transform.position.y <= endingPosition) {
			triggerer ("idle");
			endDash = true;
			attemptDash = false;
			rb2d.velocity = new Vector2 (0, dashSpeed / 2);
		}

		if (endDash && transform.position.y >= startingPosition) {
			rb2d.velocity = new Vector2 (0, 0);
			endDash = false;
		}

		if (dying)
			damageable = false;
	}
	
	private IEnumerator attack() {

		attacking = true;

		int attack = Random.Range (0, 4);

		if (attack == lastAttack) {			// ensure you dont do the same attack 2x
			if (attack == 3)
				attack = Random.Range (0, 3);
			else if (attack == 2) {
				attack = Random.Range (0,3);
				if (attack == 2) attack = 3;
			}
			else if (attack == 1) {
				attack = Random.Range (0,3);
				if (attack == 1) attack = 3;
			}
			else
				attack = Random.Range (1,4);
		}

		if (attack == 2 && gameManager.enemyExists) {

			attack = Random.Range (0, 3);
			if (attack == 2) attack = 3;
		}

		if (attack == 0) {	// do nothing

			triggerer ("idle");

			if (gameManager.playBossSounds) {
				aSource.clip = pumpkingLaugh;
				aSource.Play();
			}
		}
		else if (attack == 1) {
			attemptDash = true;
		}
		else if (attack == 2 && gameManager.enemyExists)
			spawnEnemy ();
		else {
			triggerer ("spawn");
			projectile = (GameObject)Instantiate (fireball, shotPosition, transform.rotation);
			projectile.GetComponent <Rigidbody2D>().velocity = new Vector2 (0, -10);
		}

		lastAttack = attack;

		yield return new WaitForSeconds (timeBetweenAttacks);
		triggerer ("idle");
		yield return new WaitForSeconds (0.5f);
		attacking = false;
	}

	void triggerer(string trigger) {

		anim.ResetTrigger (lastTrigger);
		anim.SetTrigger (trigger);
		lastTrigger = trigger;
	}

	private IEnumerator isKilled() {

		dying = true;
		triggerer ("death");
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);	
	}

	private IEnumerator flashDmg() {

		sRenderer.color = Color.red;
		yield return new WaitForSeconds(.2f);
		sRenderer.color = Color.white;
	}

	private IEnumerator invincibility() {
		
		damageable = false;
		yield return new WaitForSeconds (dmgPeriod);
		damageable = true;
	}

	void OnTriggerEnter2D (Collider2D other) {

		int healthStart = health;

		if (other.gameObject.tag == "Bananarang" && damageable) {
			health -= bananarangDmg;
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "Cherry Swing" && damageable)
			health -= cherrySwordDmg;
		else if (other.gameObject.tag == "Fireball Good" && damageable) {
			health -= fireBallBackDmg;
			Destroy (other.gameObject);
		} else if (other.gameObject.tag == "Enemy")
			Destroy (other.gameObject);
		else if (other.gameObject.tag == "AppleExploding")
				health -= appleBombDmg;

		if (health < healthStart) {
			StartCoroutine (flashDmg ());
			StartCoroutine (invincibility ());
		}
	}

	void spawnEnemy () {

		triggerer ("spawn");
		GameObject spawnedEnemy;

		int enemyToSpawn = Random.Range (0, 3);
		if (enemyToSpawn == 0)
			spawnedEnemy = (GameObject)Instantiate (carrotEnemy, enemySpawnpoint.GetComponent<Transform>().position, enemySpawnpoint.GetComponent<Transform>().rotation);
		else if (enemyToSpawn == 1)
			spawnedEnemy = (GameObject)Instantiate (turnipEnemy, enemySpawnpoint.GetComponent<Transform>().position, enemySpawnpoint.GetComponent<Transform>().rotation);
		else
			spawnedEnemy = (GameObject)Instantiate (broccoliEnemy, enemySpawnpoint.GetComponent<Transform>().position, enemySpawnpoint.GetComponent<Transform>().rotation);

		spawnedEnemy.tag = "Spawned Enemy";

		gameManager.enemyExists = true;
	}
}
