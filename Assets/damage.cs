﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour {

	public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 fwd = cam.transform.TransformDirection (Vector3.forward);

			RaycastHit hit;

			if (Physics.Raycast (transform.position, fwd, out hit)) {
				GameObject objectHit = hit.transform.root.gameObject;


				if (objectHit.GetComponent<healthage> () != null) {
					var health = objectHit.gameObject.GetComponent<healthage> ();
					health.hp -= 50;
					Debug.Log (health.age);
				}
			}


		}
	}
}
