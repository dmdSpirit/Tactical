using UnityEngine;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class CharacterElement : MapElement
    {
        public string name;
        public int speed;
        public int jumpHeight;
        public CharacterModelType modelType;
        public bool hasMoved;
        public bool hasActed;

        public CharacterElement(int x, int y, int height, string name, int speed, int jumpHeight,
            CharacterModelType modelType, bool hasMoved = false, bool hasActed = false) : base(x, y, height, MapElementType.Character)
        {
            this.name = name;
            this.speed = speed;
            this.jumpHeight = jumpHeight;
            this.modelType = modelType;
            this.hasMoved = hasMoved;
            this.hasActed = hasActed;
        }
    }
}