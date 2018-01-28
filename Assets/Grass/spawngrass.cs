using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawngrass : MonoBehaviour {

	public GameObject grass;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (0f, 1f) < 0.01f) {
			Instantiate (grass, new Vector3 (Random.Range (transform.position.x + 18, transform.position.x - 18),	transform.position.y+5, Random.Range (transform.position.z + 18, transform.position.z - 18)), transform.rotation);
		}
	}
}
