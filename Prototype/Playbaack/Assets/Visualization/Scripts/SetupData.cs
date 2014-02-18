using UnityEngine;
using System.Collections;
using AM;
using System.Collections.Generic;

public class SetupData : MonoBehaviour {

	public TTerrain[,] TerrainMap;
	public List<CharController> Characters = new List<CharController>();


	// Use this for initialization
	void Start () {
		TestFill();
		((EventController)GameObject.Find("Event Manager").GetComponent("EventController")).Setup(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TestFill(){
		TerrainMap = new TTerrain[10,10];
		for (int y = 0; y < TerrainMap.GetLength(0);y++){
			for (int x = 0; x < TerrainMap.GetLength(1);x++){
				if (Random.Range(0,2) == 1)
					TerrainMap[y,x] = TTerrain.Desert;
				else
					TerrainMap[y,x] = TTerrain.Grass;
			}
		}
		Characters.Add(new CharController("Jim","Jim1", CharClass.Knight,new Vector2(0,0)));
		Characters.Add(new CharController("John", "John2", CharClass.Knight,new Vector2(9,9)));
	}
}
