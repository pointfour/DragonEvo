using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifepoints : MonoBehaviour {

	public int lp = 0;
	public Text lp_ui;
	public GameObject sheep;
	public GameObject wolf;
	public GameObject vulture;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1) && lp > 10) {
			Debug.Log ("sheep");
			lp -= 10;
			Instantiate (sheep, transform.position + transform.forward, transform.rotation);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2) && lp > 10) {
			Debug.Log ("wolf");
			lp -= 10;
			Instantiate (wolf, transform.position + transform.forward, transform.rotation);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3) && lp > 10) {
			Debug.Log ("vulture");
			lp -= 10;
			Instantiate (vulture, transform.position + transform.forward, transform.rotation);
		}
		lp_ui.text = "Life Points: " + lp;
	}
}
