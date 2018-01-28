using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	void Start(){
		AddAlphaNoise (GetComponent<Terrain> (), 1);
	}


	void AddAlphaNoise(Terrain t, float noiseScale)
	{
		float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);

		for (int y = 0; y < t.terrainData.alphamapHeight; y++)
		{
			for (int x = 0; x < t.terrainData.alphamapWidth; x++)
			{
				float a0 = maps[x, y, 0];
				float a1 = maps[x, y, 1];

				a0 += Random.value * noiseScale;
				a1 += Random.value * noiseScale;

				float total = a0 + a1;

				maps[x, y, 0] = a0 / total;
				maps[x, y, 1] = a1 / total;
			}
		}

		t.terrainData.SetAlphamaps(0, 0, maps);
	}
}

