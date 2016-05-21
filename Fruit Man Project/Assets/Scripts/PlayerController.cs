using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private AudioSource playerAudioSource;

	//misc
	public GameManager gameManager;
	public GUIText scoreText;
	private Animator animator;
	private SpriteRenderer sRenderer;
	private Rigidbody2D rb2d;

	//vectors
	private Vector2 movement;
	private Vector2 shotVelocity;
	private Vector2 shotPosition;
	public Vector2 cherryOffsetRight;
	public Vector2 cherryOffsetLeft;
	public Vector2 cherryOffsetUp;
	public Vector2 cherryOffsetDown;

	//sounds
	public AudioClip seedPickUp;
	public AudioClip hpPickUp;
	public AudioClip appleExplosion;
	public AudioClip bananaSwoosh;
	public AudioClip cherrySlash;
	public AudioClip playerHit;


	//ints
	private int health;
	public int shotSpeed;

	//GameObjects
	private GameObject projectile;
	private GameObject shot;
	public GameObject appleShot;
	public GameObject bananaShot;
	public GameObject cherryShot;
	public GameObject cherrySwing;

	//floats
	public float bananaFireRate;
	public float appleFireRate;
	public float cherryFireRate;
	private float nextBananaFire;
	private float nextAppleFire;
	private float nextCherryFire;
	public float spriteScale;
	public float playerSpeed;
	private float shotRotation;
	public float invincibilityPeriod;
	public float dmgFlashTime;
	public float rollingSpeed;
	public float nuxSpeed;

	//booleans
	private bool rolling;
	private bool allowMovement;
	private bool attacking;
	private bool damageable;
	private bool inEnemy;
	private bool bossAtk;

	//strings
	private string lastTrigger;

	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(100,40);
	public Vector2 size = new Vector2(150,20);
	public Texture2D emptyTex;
	public Texture2D fullTex;

	void Start ()
	{		

		playerAudioSource = GetComponent<AudioSource>();
		sRenderer = GetComponent<SpriteRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();
		allowMovement = true;
		damageable = true;

		health = 30;
		//healthText.text = "Health: " + health;

		gameManager.hasCherryPower = true;
		gameManager.activeCherryPower = true;
		
		animator = GetComponent<Animator> ();
		animator.SetTrigger ("WarningTrigger");
		lastTrigger = "WarningTrigger";
		animTrigger ("CherryActiveDown");

		DontDestroyOnLoad (this);
	}
	
	void FixedUpdate ()
	{
		//barDisplay = health;
		if (this.health <= 0 && !gameManager.nuxMode) {	// check for gameover
			Destroy(this.gameObject);
			Application.LoadLevel(0);
		}

		if (allowMovement) {
			if (Input.GetKey (KeyCode.W)) {

				if (gameManager.activeCoconutPower) 
					StartCoroutine (checkRolling ());

				directionAnim ("Up");
			}

			else if (Input.GetKey (KeyCode.S)) {

				if (gameManager.activeCoconutPower) 
					StartCoroutine (checkRolling ());
			
				directionAnim ("Down");
			} 

			else if (Input.GetKey (KeyCode.A)) {

				if (gameManager.activeCoconutPower) 
					StartCoroutine (checkRolling ());
			
				directionAnim ("Left");
			} 

			else if (Input.GetKey (KeyCode.D)) {

				if (gameManager.activeCoconutPower) 
					StartCoroutine (checkRolling ());
			
				directionAnim ("Right");
			}

			if (Input.GetKey (KeyCode.Keypad3) || Input.GetKey (KeyCode.Alpha3)) {	// player changes powers (apple)
				
				if (gameManager.hasApplePower) {
					rolling = false;
					gameManager.activeBananaPower = false;
					gameManager.activeApplePower = true;
					gameManager.activeCoconutPower = false;
					gameManager.activeCherryPower = false;

					animTrigger ("AppleActiveDown");
				}
			} 
			
			else if (Input.GetKey (KeyCode.Keypad2) || Input.GetKey (KeyCode.Alpha2)) {	// banana
				
				if (gameManager.hasBananaPower) {
					rolling = false;
					gameManager.activeApplePower = false;
					gameManager.activeBananaPower = true;
					gameManager.activeCoconutPower = false;
					gameManager.activeCherryPower = false;
					
					animTrigger ("BananaActiveDown");
				}
			}
			
			else if (Input.GetKey (KeyCode.Keypad4) || Input.GetKey (KeyCode.Alpha4)) {	// coconut
				
				if (gameManager.hasCoconutPower) {
					gameManager.activeApplePower = false;
					gameManager.activeBananaPower = false;
					gameManager.activeCoconutPower = true;
					gameManager.activeCherryPower = false;
					
					if (!rolling)
						animTrigger ("CoconutActiveDown");
				}
			}
			
			else if (Input.GetKey (KeyCode.Keypad1) || Input.GetKey (KeyCode.Alpha1)) {	// cherry
				
				if (gameManager.hasCherryPower) {
					rolling = false;
					gameManager.activeApplePower = false;
					gameManager.activeBananaPower = false;
					gameManager.activeCoconutPower = false;
					gameManager.activeCherryPower = true;
					
					animTrigger ("CherryActiveDown");
				}
			}
			
			if (Input.GetKey (KeyCode.UpArrow)) { // shoot up

				if (gameManager.activeCherryPower) {
					attacking = true;
					StartCoroutine(swordSwing("Up"));
				}
				
				directionAnim ("Up");
				
				shootProjectile (0f, 1f);
			} 
			
			else if (Input.GetKey (KeyCode.DownArrow)) { // shoot down

				if (gameManager.activeCherryPower) {
					attacking = true;
					StartCoroutine(swordSwing("Down"));
				}
				
				directionAnim("Down");
				
				shootProjectile (0f, -1f);
			} 
			
			else if (Input.GetKey (KeyCode.LeftArrow)) { // shoot left

				if (gameManager.activeCherryPower) {
					attacking = true;
					StartCoroutine(swordSwing("Left"));
				}

				directionAnim ("Left");

				shootProjectile (-1f, 0f);
			} 
			
			else if (Input.GetKey (KeyCode.RightArrow)) { // shoot right

				if (gameManager.activeCherryPower) {
					attacking = true;
					StartCoroutine(swordSwing("Right"));
				}
				directionAnim ("Right");

				shootProjectile (1f, 0f);
			}

			else if (Input.GetKey (KeyCode.R)) { // shoot right
				Destroy(this.gameObject);
				Application.LoadLevel(0);
			}
			
			float moveHorizontal = Input.GetAxis ("Horizontal");	// player movement
			float moveVertical = Input.GetAxis ("Vertical");


			Vector2 movement = new Vector2 (moveHorizontal, moveVertical);	

			if (allowMovement) {

				rb2d.velocity = movement * playerSpeed;

				if (rolling)
					rb2d.velocity = movement * rollingSpeed;

				if (gameManager.nuxMode)
					rb2d.velocity = movement * nuxSpeed;
			}
		}
		
		if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.D)) {
			rolling = false;
			if (gameManager.activeCoconutPower)
				animTrigger ("CoconutActiveDown");
		}

		if (inEnemy && damageable) {
			StartCoroutine (damageDelay ());
			playerAudioSource.clip = playerHit;
			playerAudioSource.Play ();
			inEnemy = false;
		}

		if (health <= 0)
			health = 0;
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Seed") { // If player picks up apple upgrade
			
			gameManager.score = gameManager.score + 1;
			scoreText.text = "Seeds: " + gameManager.score;
			playerAudioSource.clip = seedPickUp;
			playerAudioSource.Play ();
			Destroy (other.gameObject);
		} 
		if (other.gameObject.tag == "Golden Seed") { // If player picks up apple upgrade
			gameManager.score = gameManager.score + 10;
			scoreText.text = "Seeds: " + gameManager.score;
			playerAudioSource.clip = seedPickUp;
			playerAudioSource.Play ();
			Destroy (other.gameObject);
		} 

		if (other.gameObject.tag == "Apple Upgrade") { // If player picks up apple upgrade
			
			gameManager.hasApplePower = true;
			playerAudioSource.clip = seedPickUp;
			playerAudioSource.Play ();
			Destroy (other.gameObject);
		} 

		else if (other.gameObject.tag == "Banana Upgrade") { // player picks up banana upgrade
			gameManager.hasBananaPower = true;
			playerAudioSource.clip = seedPickUp;
			playerAudioSource.Play ();
			Destroy (other.gameObject);
		} 

		else if (other.gameObject.tag == "Coconut Upgrade") { // player picks up banana upgrade
			gameManager.hasCoconutPower = true;
			playerAudioSource.clip = seedPickUp;
			playerAudioSource.Play ();
			Destroy (other.gameObject);
		} 

		else if (other.gameObject.tag == "Cherry Upgrade") { // player picks up banana upgrade
			gameManager.hasCherryPower = true;
			playerAudioSource.clip = seedPickUp;
			playerAudioSource.Play ();
			Destroy (other.gameObject);
		} 
		else if (other.gameObject.tag == "Health Pack") { // player picks up banana upgrade
			health = health + 5;
			if(health > 30)
				health = 30;
			playerAudioSource.clip = hpPickUp;
			playerAudioSource.Play ();
			Destroy (other.gameObject);
		} 

		else if (other.gameObject.tag == "Enemy Projectile") { // player is hit by enemy shot
			//play sounds
			playerAudioSource.clip = playerHit;
			playerAudioSource.Play();

			if (gameManager.activeCherryPower){
				health = health - 1;
				Destroy (other.gameObject);
				StartCoroutine(damageFlash());

				return;
			}

			else if (!rolling){
				health = health - 2;
				Destroy (other.gameObject);
				StartCoroutine(damageFlash());

				return;
			}
			//healthText.text = "Health: " + health;
			Destroy (other.gameObject);
			if (!rolling)
				StartCoroutine(damageFlash());
		} 
		else if (other.gameObject.tag == "Fireball") { // player is hit by fireball that hasn't been sent back
			
			if (gameManager.activeCherryPower || rolling){
				health -= 5;

			}
			else{
				health -= 10;

			}
		} else if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spawned Enemy") { // player is hit by enemy
			inEnemy = true;
			if (other.gameObject.name == "Pumpking")
				bossAtk = true;
		}
		else if (other.gameObject.tag == "Spikes" && !rolling) { // player is hit by enemy

			inEnemy = true;
		}
	}


	void OnTriggerExit2D(Collider2D other) {
		
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spikes" || other.gameObject.tag == "Spawned Enemy") {
			inEnemy = false;
			bossAtk = false;
		}
	}

	void shootProjectile(float x, float y) {	// calculate velocity, position, etc. for shots

		shotPosition = new Vector2 (transform.localPosition.x + (x / 1.3f), transform.localPosition.y + (y / 1.3f));
		shotVelocity = new Vector2 (0f, 0f);
		shotRotation = 0f;
		
		if (gameManager.activeApplePower && Time.time > nextAppleFire) {
			shot = appleShot;
			nextAppleFire = Time.time + appleFireRate;
			playerAudioSource.clip = appleExplosion;
			//playerAudioSource.Play();
			fireProjectile ();
		}
		
		else if (gameManager.activeBananaPower && Time.time > nextBananaFire) {
			shot = bananaShot;
			nextBananaFire = Time.time + bananaFireRate;
			shotVelocity = new Vector2 (x * shotSpeed, y * shotSpeed);
			playerAudioSource.clip = bananaSwoosh;
			playerAudioSource.Play();
			fireProjectile ();
		}

		else if (gameManager.activeCherryPower && Time.time > nextCherryFire) {
			shot = cherryShot;
			nextCherryFire = Time.time + cherryFireRate;

			playerAudioSource.clip = cherrySlash;
			playerAudioSource.Play();

			if (x > 0) {
				shotPosition = new Vector2 (transform.position.x + cherryOffsetRight.x, transform.position.y + cherryOffsetRight.y);
				shotRotation = 90f;
			}

			else if (x < 0) {
				shotPosition = new Vector2 (transform.position.x + cherryOffsetLeft.x, transform.position.y + cherryOffsetLeft.y);
				shotRotation = -90f;
			}

			else if (y < 0) 
				shotPosition = new Vector2 (transform.position.x + cherryOffsetDown.x, transform.position.y + cherryOffsetDown.y);

			else {
				shotPosition = new Vector2 (transform.position.x + cherryOffsetUp.x, transform.position.y + cherryOffsetUp.y);
				shotRotation = 180f;
			}

			fireProjectile ();
		}
	}

	void fireProjectile() {	// actually fire projectile
		if (gameManager.activeCherryPower) { // cherry swing along with shot
			projectile = (GameObject)Instantiate (cherrySwing, shotPosition, transform.rotation);
			projectile.GetComponent <Rigidbody2D> ().rotation = shotRotation;
		}
		projectile = (GameObject)Instantiate (shot, shotPosition, transform.rotation);
		projectile.GetComponent <Rigidbody2D> ().velocity = shotVelocity;
		projectile.GetComponent <Rigidbody2D> ().rotation = shotRotation;
	}

	private IEnumerator checkRolling() {
		
		if (!rolling) {
			animTrigger ("CoconutStartRolling");
			yield return new WaitForSeconds (0.333f);
		}

		rolling = true;
		animTrigger ("CoconutRolling");
	}
	
	private IEnumerator swordSwing(string direction) {
		
		allowMovement = false;
		rb2d.velocity = new Vector2 (0f, 0f);
		
		animTrigger ("CherryAttack" + direction);
		yield return new WaitForSeconds (cherryFireRate);	
		animTrigger ("CherryActive" + direction);

		attacking = false;
		allowMovement = true;
	}

	private IEnumerator damageDelay() {
		
		damageable = false;
		int healthLoss = 1;

		if ((rolling || gameManager.activeCherryPower) && !bossAtk)
			healthLoss = 1;
		else if ((rolling || gameManager.activeCherryPower) && bossAtk)
			healthLoss = 3;
		else if (!rolling && !gameManager.activeCherryPower && !bossAtk)
			healthLoss = 2;
		else if (!rolling && !gameManager.activeCherryPower && bossAtk)
			healthLoss = 5;

		health -= healthLoss;

		//healthText.text = "Health: " + health;
		
		StartCoroutine (damageFlash ());
		
		yield return new WaitForSeconds (invincibilityPeriod);	
		
		damageable = true;
	}
	private IEnumerator damageFlash() {
		
		sRenderer.color = Color.red;
		yield return new WaitForSeconds (dmgFlashTime);	
		sRenderer.color = Color.white;
	}
	
	void animTrigger (string trigger) { //cleanly set a new animation trigger on player

		animator.ResetTrigger (lastTrigger);
		animator.SetTrigger (trigger);
		lastTrigger = trigger;
	}

	void directionAnim (string direction) {	// set animation triggers for given direction & transformation

		if (gameManager.activeCherryPower && !attacking) 
			animTrigger ("CherryActive" + direction);

		else if (gameManager.activeApplePower)
			animTrigger ("AppleActive" + direction);
		
		else if (gameManager.activeBananaPower)
			animTrigger ("BananaActive" + direction);
	}



	
	void OnGUI() {
		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), emptyTex);
		
		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0,0, size.x * health/30, size.y));
		GUI.Box(new Rect(0,0, size.x, size.y), fullTex);
		GUI.EndGroup();
		GUI.EndGroup();
	}

}