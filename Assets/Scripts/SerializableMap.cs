using System.Collections.Generic;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class SerializableMap
    {
        public MapElement[] mapArray;

        public SerializableMap(List<MapElement> newMap)
        {
            mapArray = newMap.ToArray();
        }

        public bool IsInitialized()
        {
            return mapArray != null && mapArray.Length > 0;
        }
    }
}