﻿using UnityEngine;
using System.Collections;

/**
 * This script is involved into the ship destruction when the game has to start.
 * Some objects in the screen have to be destroyed and smany others have to be created 
 * and moved.
 */
public class ShipDemolition : MonoBehaviour {

	// asteroid that will collides the ship
	public GameObject asteroid;
	// asteroid's speed
	public float speed = 1f;

	public GameObject explosionPrefab;

	// objects list to destroy 
	public GameObject[] toDestroy;

	// the trigger ables to start the game
	public GameObject starter;

	private bool startAnim = false;

	public AudioClip explosion;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		//use the CLICK cource to play explosion sound
		source = GameObject.FindGameObjectWithTag (GameTags.camera).GetComponents<AudioSource> ()[1];
		//starter.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// if the ship is clicked
		if (Input.GetMouseButtonDown(0)) {
			if(Utility.checkInput(gameObject)) {
				startAnim = true;
			}
		}
		startInitialAnimation(startAnim);
	}


	private string checkInput() {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			return hit.collider.gameObject.name;
		} else {
			return null;
		}
	}

	/**
	 * Move the asteroid in direction of the ship to destry it.
	 */
	private void startInitialAnimation(bool canStart) {
		if (canStart) {
			if (asteroid != null) {
				asteroid.transform.position = Vector3.Lerp (asteroid.transform.position, transform.position, speed * 0.1f);// * Time.deltaTime);
			}
		}
	}

	/**
	 * When the asteroid object collide with the ship:
	 *  - UI objects are destroyed (included the ship)
	 *  - Explosion sound and animation are performed
	 *  - Player sprite is animated.
	 */ 
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.name == asteroid.name){
			//Debug.Log("Collision with " + coll.gameObject.name);

			// animate the player
			Instantiate(starter, new Vector3(0, 0, 1), Quaternion.identity);
			Vector3 v = transform.position;
			v.z = -10; //bring it on the front
			Instantiate(explosionPrefab, v, transform.rotation);
			Utility.playSoundOnSource(source, explosion, true, 0.7f);

			destroyObjects();
			Destroy(gameObject, 0.1f);
			Destroy(asteroid, 0.1f);
		}
	}
	
	private void destroyObjects() {
		for (int i = 0; i < toDestroy.Length; i++) {
			//Debug.Log("Destroying " + toDestroy[i].name);
			if(toDestroy[i] != null) {
				Destroy(toDestroy[i], 0);
			}
		}
	}

}