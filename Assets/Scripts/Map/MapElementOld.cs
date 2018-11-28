using UnityEngine;

namespace dmdspirit.Tactical
{
    public enum MapElementOldType
    {
        Empty,
        Block
    }

    [System.Serializable]
    public class MapElementOld
    {
        public int x;
        public int y;
        public int height;
        public MapElementOldType elementType = MapElementOldType.Empty;

        public bool canStandOn = false;

        public MapElementOld(int x, int y, int height = 0, MapElementOldType elementType = MapElementOldType.Empty, bool canStandOn = false)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.elementType = elementType;
            this.canStandOn = canStandOn;
        }
    }
}