using UnityEngine;
using System.Collections;

namespace AM.Visualization
{
public class TempBuilder {

		public TTerrain[,] TerrainMap;

		public TempBuilder ()
		{
			TerrainMap = new TTerrain[10,10];
			for (int y = 0; y < TerrainMap.GetLength(0);y++){
				for (int x = 0; x < TerrainMap.GetLength(1);x++){
					if (x >= 5)
						TerrainMap[y,x] = TTerrain.Desert;
					else
						TerrainMap[y,x] = TTerrain.Grass;
				}
			}
		}

}
}