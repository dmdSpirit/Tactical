
namespace dmdspirit.Tactical
{
    public enum DirectionEnum
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public enum CharacterModelType
    {
        Warrior,
        Mage
    }

    [System.Serializable]
    public class CharacterElement : MapElement
    {
        public string name;
        public int speed;
        public int jumpHeight;
        public CharacterModelType modelType;
        public DirectionEnum facingDirection;
        public bool hasMoved;
        public bool hasActed;

        public CharacterElement(int x, int y, int height, string name, int speed, int jumpHeight,
            CharacterModelType modelType, DirectionEnum facingDirection = DirectionEnum.North, bool hasMoved = false, bool hasActed = false) : base(x, y, height, MapElementType.Character)
        {
            this.name = name;
            this.speed = speed;
            this.jumpHeight = jumpHeight;
            this.modelType = modelType;
            this.facingDirection = facingDirection;
            this.hasMoved = hasMoved;
            this.hasActed = hasActed;
        }
    }
}