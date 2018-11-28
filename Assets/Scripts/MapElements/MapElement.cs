using UnityEngine;

namespace dmdspirit.Tactical
{
    public enum MapElementType
    {
        UNDEFINED,
        Terrain,
        Character
    }

    // Base class for every element on map (character, terrain, object).
    [System.Serializable]
    public class MapElement
    {
        public int x;
        public int y;
        public int height;
        public MapElementType elementType;

        public MapElement(int x, int y, int height, MapElementType elementType)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.elementType = elementType;
        }
    }
}