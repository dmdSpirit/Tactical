
namespace dmdspirit.Tactical
{
    public enum CharacterModelType
    {
        Warrior,
        Mage
    }

    [System.Serializable]
    public class CharacterElement
    {
        public MapElement mapElement;
        public string name;
        public int speed;
        public int jumpHeight;
        public CharacterModelType modelType;
        public FacingDirection facingDirection;
        public bool hasMoved;
        public bool hasActed;

        public CharacterElement() : this(new MapElement(MapElementType.Character), "Character", 1, 1, CharacterModelType.Warrior) { }

        public CharacterElement(int x, int y, int height, string name, int speed, int jumpHeight,
            CharacterModelType modelType, FacingDirection facingDirection = FacingDirection.North, bool hasMoved = false, bool hasActed = false)
            : this(new MapElement(x, y, height, MapElementType.Character), name, speed, jumpHeight, modelType, facingDirection, hasMoved, hasActed) { }

        public CharacterElement(MapElement mapElement, string name, int speed, int jumpHeight,
            CharacterModelType modelType, FacingDirection facingDirection = FacingDirection.North, bool hasMoved = false, bool hasActed = false)
        {
            this.mapElement = mapElement;
            this.name = name;
            this.speed = speed;
            this.jumpHeight = jumpHeight;
            this.modelType = modelType;
            this.facingDirection = facingDirection;
            this.hasMoved = hasMoved;
            this.hasActed = hasActed;
        }

        public override string ToString()
        {
            return $"({mapElement.x},{mapElement.y},{mapElement.height}) {name} - {modelType.ToString()}";
        }
    }
}