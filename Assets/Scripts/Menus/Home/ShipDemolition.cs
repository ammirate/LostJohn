﻿using UnityEngine;
using System.Collections;

public class ShipDemolition : MonoBehaviour {

	public GameObject asteroid;
	public float speed = 1f;
	public GameObject explosionPrefab;
	public AudioClip explosionSound;
	public GameObject[] toDestroy;

	public GameObject starterTrigger;

	private bool startAnim = false;
	private Vector3 asteroidPos;

	// Use this for initialization
	void Start () {
		asteroidPos = asteroid.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if(checkInput() == this.name) {
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

	private void startInitialAnimation(bool canStart) {
		if (canStart) {
			if (asteroid != null) {
				asteroid.transform.position = Vector3.Lerp (asteroid.transform.position, transform.position, speed * 0.1f);// * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.name == asteroid.name){
			//Debug.Log("Collision with " + coll.gameObject.name);

			starterTrigger.GetComponent<Player_MoveOut>().anim();

			Destroy(asteroid);
			playSound(explosionSound);
			Instantiate(explosionPrefab, transform.position, transform.rotation);
			destroyObjects();


			Destroy(gameObject,0.2f);

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


	private void playSound(AudioClip clip) {
		// FIXME: can't use local audio source
		// because this object will be destroyed
	}

	private void instantiateStarterPlayer() {

	}

}







