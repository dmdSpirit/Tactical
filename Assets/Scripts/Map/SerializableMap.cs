using System.Collections.Generic;
using UnityEngine;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class SerializableMap
    {
        public MapElement[] mapArray;

        public SerializableMap(List<MapElementHandler> mapToSerialize)
        {
            if (mapToSerialize == null)
            {
                Debug.LogError("Trying to serialize non initialized list of map elements.");
                return;
            }
            mapArray = new MapElement[mapToSerialize.Count];
            for (int i = 0; i < mapToSerialize.Count; i++)
                mapArray[i] = mapToSerialize[i].element;
        }

        public bool IsInitialized()
        {
            return mapArray != null && mapArray.Length > 0;
        }
    }
}