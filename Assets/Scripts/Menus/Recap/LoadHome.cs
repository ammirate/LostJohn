﻿using UnityEngine;
using System.Collections;

/**
 * This script bring the user back to the Home screen.
 */
public class LoadHome : MonoBehaviour {

	GameLevelManager level;
	// Use this for initialization
	void Start () {
		level = GameLevelManager.getInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if(checkInput() == gameObject.name) {
				Time.timeScale = 1;
				level.loadHome();
			}
		}
	}

	private string checkInput() {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
//			Debug.Log(hit.collider.gameObject.name);
			return hit.collider.gameObject.name;
		} else {
			return null;
		}
	}
}
