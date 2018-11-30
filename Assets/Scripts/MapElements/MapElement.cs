using UnityEngine;

namespace dmdspirit.Tactical
{
    public enum MapElementType
    {
        UNDEFINED,
        Terrain,
        Character
    }

    [System.Serializable]
    public class MapElement
    {
        public int x;
        public int y;
        public int height;
        public MapElementType elementType;

        public MapElement() : this(0, 0, 0, MapElementType.UNDEFINED) { }

        public MapElement(MapElementType mapElementType) : this(0, 0, 0, mapElementType) { }

        public MapElement(int x, int y, int height, MapElementType elementType)
        {
            this.x = x;
            this.y = y;
            this.height = height;
            this.elementType = elementType;
        }

        public override string ToString()
        {
            return $"({x},{y},{height}) {elementType.ToString()}";
        }

        public Vector3 GetWorldPosition()
        {
            return new Vector3(x, height, y);
        }
    }
}