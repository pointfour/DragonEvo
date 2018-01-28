using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vulture : MonoBehaviour {

	public GameObject meat;
	public float speed = 0.5f;
	public float movespeed = 10;
	public Rigidbody rb;
	public GameObject hunter;
	public float boostmax = 1.5f;
	public float boost = 1.5f;
	public float dir = -1f;
	public int eaten = 0;
	public Vector3 flypoint;


	// Use this for initialization
	void Start () {
		boost = boostmax;
		rb = gameObject.GetComponent<Rigidbody> ();
		InvokeRepeating ("pickmeat", 2.0f, 0.3f);
		InvokeRepeating ("clearhunter", 2.0f, 0.3f);
		InvokeRepeating ("setdir", 2.0f, 0f);
		movespeed = movespeed + Random.Range (-0.1f, 0.1f);
		boostmax = boostmax + Random.Range (-0.01f, 0.01f);
		movespeed = movespeed + Random.Range (-0.1f, 0.1f);
		flypoint.y = 666;
	}

	// Update is called once per frame
	void Update () {
		if (boostmax > boost) {
			boost += 0.01f;
		}
		if (hunter == null) {
			stablize ();
			zclamp ();
			GameObject[] hunters = GameObject.FindGameObjectsWithTag ("hunter");
			float distance1 = 1000f;
			int id1 = 4000;
			for (int i = 0; i < hunters.Length; i++) {
				if (distance1 > Vector3.Distance (transform.position, hunters [i].transform.position) && Vector3.Distance (transform.position, hunters [i].transform.position) < 10) {
					distance1 = Vector3.Distance (transform.position, hunters [i].transform.position);
					id1 = i;
				}
			}
			if (id1 != 4000) {
				hunter = hunters [id1];
			}


			//normal behavior
			if (meat == null && transform.position.y > 8) {
				GameObject[] foods = GameObject.FindGameObjectsWithTag ("meat");
				float distance = 1000f;
				int id = 4000;
				for (int i = 0; i < foods.Length; i++) {
					if (distance > Vector3.Distance (transform.position, foods [i].transform.position)) {
						distance = Vector3.Distance (transform.position, foods [i].transform.position);
						id = i;
					}
				}
				if (id != 4000) {
					meat = foods [id];
				}
				normalizey ();
			} else if (transform.position.y < 10f && meat == null) {
				if (Vector3.Distance (flypoint, transform.position) < 2f) {
					flypoint.y = 666;
				}
				if (flypoint.y == 666f) {
					flypoint = new Vector3 (Random.Range (-128f, 128f), 15f, Random.Range (-128f, 128f));
				} else {
					movetowards (flypoint);
				}
			} else if (transform.position.y > 15f && meat == null) {
				if (Vector3.Distance (flypoint, transform.position) < 2f) {
					flypoint.y = 666;
				}
				if (flypoint.y == 666f) {
					flypoint = new Vector3 (Random.Range (-128f, 128f), 15f, Random.Range (-128f, 128f));
				} else {
					movetowards (flypoint);
				}
			} else {
				flypoint.y = 666;
				movetowards (meat.transform.position);
				if (Vector3.Distance (transform.position, meat.transform.position) < 2) {
					eaten++;
					Destroy (meat);
					if (eaten > 3) {
						eaten = 0;
						Instantiate (gameObject, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z - 2), transform.rotation);
					}
					add_lifepoints (1);
				}
			}
		} else {
			//hunted behavior
			normalizey();
			runawayfrom (hunter.transform.position);
		}

		gameObject.transform.position += transform.forward / 30 * movespeed;
		transform.Rotate (Vector3.up * dir);

	}
	void pickmeat(){
		GameObject[] foods = GameObject.FindGameObjectsWithTag ("meat");
		float distance = 1000f;
		int id = 4000;
		for (int i = 0; i < foods.Length; i++) {
			if (distance > Vector3.Distance (transform.position, foods [i].transform.position)) {
				distance = Vector3.Distance (transform.position, foods [i].transform.position);
				id = i;
			}
		}
		if (id != 4000) {
			meat = foods [id];
		}
	}
	void clearhunter(){
		hunter = null;
	}
	void setdir(){
		if (Random.Range (-1f, 1f) < 0f) {
			dir = -1f;
		} else {
			dir = 1f;
		}
	}
	void normalizey(){
		if (transform.position.y <11f) {
			rb.AddForce (transform.up * 4f);	
		} else {
			rb.AddForce (transform.up * -4f);	
		}
	}
	void stablize(){
		if (transform.eulerAngles.y < 0) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y + 1f, transform.eulerAngles.z);
		}else if (transform.eulerAngles.y < 360) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y - 1f, transform.eulerAngles.z);
		}
		//Debug.Log ("stablizing" + transform.eulerAngles.y);
	}

	void add_lifepoints(int addify){
		var lifepointscript = GameObject.FindGameObjectsWithTag ("Player") [0].GetComponent<lifepoints> ();
		lifepointscript.lp += addify;
	}

	void zclamp(){
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp (transform.eulerAngles.z, -20f, 20f));
	}

	void movetowards(Vector3 target){
		dir = 0;
		Vector3 targetDir = target - transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay (transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation (newDir);
		Debug.DrawRay (transform.position, targetDir);
		Debug.DrawRay (transform.position, (transform.position - transform.forward), Color.red);
	}

	void runawayfrom(Vector3 enemy){
		Vector3 target = new Vector3 (enemy.x, enemy.y - 10, enemy.z);
		Vector3 targetDir = -target + transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay (transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation (newDir);

		boost -= 0.03f;
		boost = Mathf.Clamp (boost, 0.5f, boostmax);
		//rb.AddForce(transform.forward*5);
		Debug.DrawRay (transform.position, targetDir);
		Debug.DrawRay (transform.position, (transform.position - transform.forward), Color.red);
	}
}
