using UnityEngine;

namespace dmdspirit.Tactical
{
    // Basically this enum contains terrain material names for easier inEditor map editing.
    public enum TerrainType
    {
        Empty,
        Grass,
        Rock
    }

    [System.Serializable]
    public class TerrainElement
    {
        public MapElement mapElement;
        public bool canStandOn;
        public TerrainType terrainType;

        // I don't want to make TerrainType.Empty the default value here because it can lead to some confusion when new created
        // terrain elements will not be seen in editor.
        public TerrainElement() : this(new MapElement(MapElementType.Terrain), true, TerrainType.Rock) { }

        public TerrainElement(int x, int y, int height, bool canStandOn, TerrainType terrainType) : this(new MapElement(x, y, height, MapElementType.Terrain), canStandOn, terrainType) { }

        public TerrainElement(MapElement mapElement, bool canStandOn, TerrainType terrainType)
        {
            this.mapElement = mapElement;
            this.canStandOn = canStandOn;
            this.terrainType = terrainType;
        }
    }
}