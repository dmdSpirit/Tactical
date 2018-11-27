using UnityEngine;

namespace dmdspirit.Tactical
{
    public enum MapElementType
    {
        Empty,
        Block
    }

    [System.Serializable]
    public class MapElement
    {
        public int x;
        public int y;
        public int height;
        public MapElementType elementType = MapElementType.Empty;

        public bool canStandOn = false;

        public MapElement(int x, int y, int height = 0, MapElementType elementType = MapElementType.Empty, bool canStandOn = false)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.elementType = elementType;
            this.canStandOn = canStandOn;
        }
    }
}