using System.Collections.Generic;
using UnityEngine;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class SerializableMap
    {
        public TerrainElement[] terrainArray;

        public SerializableMap(List<TerrainElementHandler> terrainMap)
        {
            if (terrainMap == null)
            {
                Debug.LogError("Trying to serialize non initialized list of map elements.");
                return;
            }
            terrainArray = new TerrainElement[terrainMap.Count];
            for (int i = 0; i < terrainMap.Count; i++)
                terrainArray[i] = terrainMap[i].element;
        }

        public bool IsInitialized()
        {
            return terrainArray != null && terrainArray.Length > 0;
        }
    }
}