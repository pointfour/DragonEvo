using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep : MonoBehaviour {

	public Renderer body_render;
	public GameObject food;
	public float speed = 0.5f;
	public float movespeed = 1;
	public Rigidbody rb;
	public GameObject hunter;
	public float boostmax = 1.5f;
	public float boost = 1.5f;


	// Use this for initialization
	void Start () {
		boost = boostmax;
		rb = gameObject.GetComponent<Rigidbody> ();
		Color body_color = body_render.material.color;
		body_color = new Color (body_color.r + Random.Range (-0.1f, 0.1f), body_color.g + Random.Range (-0.1f, 0.1f), body_color.b + Random.Range (-0.1f, 0.1f));
		body_render.material.color = body_color;
		InvokeRepeating ("clearfood", 2.0f, 0.3f);
		InvokeRepeating ("clearhunter", 2.0f, 0.3f);
		InvokeRepeating("jump", 5.0f, 5f);
		movespeed = movespeed + Random.Range (-0.1f, 0.1f);
		boostmax = boostmax + Random.Range (-0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		if (boostmax > boost) {
			boost += 0.01f;
		}
		if (hunter == null) {
			GameObject[] hunters = GameObject.FindGameObjectsWithTag ("hunter");
			float distance1 = 1000f;
			int id1 = 4000;
			for (int i = 0; i < hunters.Length; i++) {
				if (distance1 > Vector3.Distance (transform.position, hunters [i].transform.position) && Vector3.Distance (transform.position, hunters [i].transform.position) < 5) {
					distance1 = Vector3.Distance (transform.position, hunters [i].transform.position);
					id1 = i;
				}
			}
			if (id1 != 4000) {
				hunter = hunters [id1];
			}
			//normal behavior
			if (food == null) {
				GameObject[] foods = GameObject.FindGameObjectsWithTag ("grass");
				float distance = 1000f;
				int id = 4000;
				for (int i = 0; i < foods.Length; i++) {
					if (distance > Vector3.Distance (transform.position, foods [i].transform.position)) {
						distance = Vector3.Distance (transform.position, foods [i].transform.position);
						id = i;
					}
				}
				if (id != 4000) {
					food = foods [id];
				}
			} else {
				movetowards (food.transform.position);

				if (Vector3.Distance (transform.position, food.transform.position) < 2) {
					Destroy (food);
					Instantiate (gameObject, new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z - 2), transform.rotation);
					var lifepointscript = GameObject.FindGameObjectsWithTag ("Player") [0].GetComponent<lifepoints> ();
					lifepointscript.lp += 1;
				}
			}
		} else {
			//hunted behavior
			runaway(hunter.transform.position);
		}
	}
	void clearfood(){
		food = null;
	}
	void clearhunter(){
		hunter = null;
	}
	void jump(){
		Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
		if (hunter == null) {
			rb.AddForce (transform.up * movespeed * 10);
		} else {
			rb.AddForce (transform.up * movespeed * 10 * boost);	
		}
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

	void runaway(Vector3 hunter){
		Vector3 targetDir = -hunter + transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay (transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation (newDir);

		gameObject.transform.position += transform.forward / 30 * movespeed*boost;
		boost -= 0.03f;
		boost = Mathf.Clamp (boost, 0.5f, boostmax);
		//rb.AddForce(transform.forward*5);
		Debug.DrawRay (transform.position, targetDir);
		Debug.DrawRay (transform.position, (transform.position - transform.forward), Color.red);
	}
}
