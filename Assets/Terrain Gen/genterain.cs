using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genterain : MonoBehaviour {

	public int debth = 60;
	public int lenght = 256;
	public int width = 256;
	public float scale = 4f;

	public float offsetX = 0f;
	public float offsetY = 0f;

	// Use this for initialization
	void Start () {
		Terrain terain = GetComponent<Terrain> ();
		terain.terrainData = GenerateTerrain (terain.terrainData);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	TerrainData GenerateTerrain(TerrainData terData){
		terData.heightmapResolution = width;
		terData.size = new Vector3 (width, debth, lenght);
		float[,] heightss = GenHights ();
		terData.SetHeights (0, 0, heightss);
		//terData.SetAlphamaps (0, 0, GenColor (terData.alphamapWidth, terData.alphamapHeight, heightss));
		Debug.Log (terData.alphamapWidth+"---"+ terData.alphamapHeight);
		return terData;
	}

	float[,] GenHights(){
		float[,] heights = new float[width, lenght];
		for (int x = 0; x < width; x++) {
			for (int z = 0; z < lenght; z++) {
				heights [x, z] = CalcHeight (x,z);
			}
		}
		return heights;
	}
	float[,,] GenColor(int width, int length,float[,] heights){
		float[,,] map = new float[width, lenght, 2];

		// For each point on the alphamap...
		for (int y = 0; y < lenght; y++) {
			for (var x = 0; x < width; x++) {
				// Get the normalized terrain coordinate that
				// corresponds to the the point.
				var normX = (int)(x * 1.0 / (width - 1));
				var normY = (int)(y * 1.0 / (lenght - 1));
				if (heights [normX, normY] < 0.25f) {
					map [x, y, 0] = 0;
					map [x, y, 1] = 1;
				} else {
					map [x, y, 0] = 1;
					map [x, y, 1] = 0;
				}
				Debug.Log (heights [normX, normY]);
			}
		}

		return map;
	}
	float CalcHeight(int x, int z){
		float xCoord = (float)x / width * scale + offsetX;
		float yCoord = (float)z / lenght * scale + offsetY;
		return Mathf.PerlinNoise (xCoord, yCoord);
	}
}
