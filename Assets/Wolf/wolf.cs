using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf : MonoBehaviour {

	public GameObject hunt;
	public GameObject meat;
	public Transform forward;
	public float damage = 1f;
	public float speed = 1.5f;
	public float movespeed = 2;
	public int eaten = 0;

	// Use this for initialization
	void Start () {
		eaten = 0;
		damage = Random.Range (damage - 0.01f, damage + 0.01f);
		InvokeRepeating("clearhunt", 2.0f, 2f);
		InvokeRepeating("jump", 2.0f, 2f);
		InvokeRepeating("clearmeat", 2.0f, 2f);
		movespeed = movespeed + Random.Range (-0.1f, 0.1f);
		speed = speed + Random.Range (-0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		if (meat == null) {
			GameObject[] meats = GameObject.FindGameObjectsWithTag ("meat");
			float distance1 = 1000f;
			int id1 = 4000;
			for (int i = 0; i < meats.Length; i++) {
				if (distance1 > Vector3.Distance (transform.position, meats [i].transform.position)) {
					distance1 = Vector3.Distance (transform.position, meats [i].transform.position);
					id1 = i;
				}
			}
			if (id1 != 4000) {
				meat = meats [id1];
			}
			if (hunt == null) {
				GameObject[] hunts = GameObject.FindGameObjectsWithTag ("prey");
				float distance = 1000f;
				int id = 4000;
				for (int i = 0; i < hunts.Length; i++) {
					if (distance > Vector3.Distance (transform.position, hunts [i].transform.position)) {
						distance = Vector3.Distance (transform.position, hunts [i].transform.position);
						id = i;
					}
				}
				if (id != 4000) {
					hunt = hunts [id];
				}
			} else {
				movetowards (hunt.transform.position);

				if (Vector3.Distance (transform.position, hunt.transform.position) < 2) {
					var healthage = hunt.GetComponent<healthage> ();
					healthage.hp -= damage;
				}
			}
		} else {
			movetowards (meat.transform.position);

			if (Vector3.Distance (transform.position, meat.transform.position) < 2) {
				Destroy (meat);
				eaten++;
				if (eaten > 3) {
					eaten = 0;
					Instantiate (gameObject, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z - 2), transform.rotation);
					var lifepointscript = GameObject.FindGameObjectsWithTag ("Player") [0].GetComponent<lifepoints> ();
					lifepointscript.lp += 1;
				}
			}
		}
	}
	void clearhunt(){
		hunt = null;
	}
	void clearmeat(){
		meat = null;
	}
	void jump(){
		Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
		rb.AddForce (transform.up * movespeed * 150);
	}
	void movetowards(Vector3 target){
		target = new Vector3 (target.x, target.y + 0.5f, target.z);
		Vector3 targetDir = target - transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
		transform.position += transform.forward / 30 * movespeed;
		Debug.DrawRay (transform.position, targetDir);
		Debug.DrawRay (transform.position, (transform.position - transform.forward),Color.red);
	}
}
