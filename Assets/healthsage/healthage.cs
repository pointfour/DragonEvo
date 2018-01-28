using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class healthage : MonoBehaviour {

	public int deathage = 1000;
	public int age = 0;
	public int maxhp = 100;
	public float hp = 100;
	public float factor = 1;
	public GameObject carcas;
	public GameObject ui;
	private Transform player;

	public GameObject hp_ui;
	public GameObject maxhp_ui;
	public GameObject age_ui;
	public GameObject maxage_ui;
	private TextMeshPro hp_tmp;
	private TextMeshPro maxhp_tmp;
	private TextMeshPro age_tmp;
	private TextMeshPro maxage_tmp;


	// Use this for initialization
	void Start () {
		hp_tmp = hp_ui.GetComponent<TextMeshPro> ();
		maxhp_tmp = maxhp_ui.GetComponent<TextMeshPro> ();
		age_tmp = age_ui.GetComponent<TextMeshPro> ();
		maxage_tmp = maxage_ui.GetComponent<TextMeshPro> ();
		player = GameObject.FindGameObjectsWithTag ("Player")[0].transform;
		age = 0;
		maxhp = Random.Range (maxhp - 10, maxhp + 10);
		hp = maxhp;
		deathage += Random.Range (-10, 10);
	}
	
	// Update is called once per frame
	void Update () {
		hp_tmp.SetText ("HP:" + hp);
		maxhp_tmp.SetText ("Max HP:" + maxhp);
		age_tmp.SetText ("Age:" + age);
		maxage_tmp.SetText ("Death Age:" + deathage);

		age++;
		if (age < 500) {
			float scale = Mathf.Clamp (((float)age / (float)500), 0.4f, 1f)/factor;
			transform.localScale = new Vector3 (scale, scale, scale);
		}
		if (hp < 0) {
			Destroy (gameObject);
			Instantiate (carcas, transform.position, transform.rotation);
		} else if (age > deathage) {
			Destroy (gameObject);
			if (Random.Range (0f, 1f) < 0.5f) {
				Instantiate (carcas, transform.position, transform.rotation);
			}
		}
		RotateUi ();
		if (Input.GetKey (KeyCode.Tab)) {
			ui.SetActive (true);
		} else {
			ui.SetActive (false);
		}

	}
	void RotateUi(){
		ui.transform.LookAt (player.position);
	}
}
