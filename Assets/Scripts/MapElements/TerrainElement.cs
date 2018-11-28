using UnityEngine;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class TerrainElement : MapElement
    {
        public bool canStandOn;
        public TerrainType terrainType;

        public TerrainElement(int x, int y, int height, bool canStandOn,
            TerrainType terrainType) : base(x, y, height, MapElementType.Terrain)
        {
            this.canStandOn = canStandOn;
            this.terrainType = terrainType;
        }
    }
}