using UnityEngine;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class Character
    {
        public string name;
        public int movementSpeed;
        public int jumpHeight;
        public bool canFly = false;
        public bool hasMoved;
        public bool hasDoneAction;
        // TODO: (dmdspirit) I think I should move this to some kind of Vector3 class.
        public int x;
        public int y;
        public int height;
        public CharacterModelType characterModelType;

        public Character(string name, int movementSpeed, int jumpHeight, bool canFly, bool hasMoved,
            bool hasDoneAction, int x, int y, int height, CharacterModelType characterModelType)
        {
            this.name = name;
            this.movementSpeed = movementSpeed;
            this.jumpHeight = jumpHeight;
            this.canFly = canFly;
            this.hasMoved = hasMoved;
            this.hasDoneAction = hasDoneAction;
            this.x = x;
            this.y = y;
            this.height = height;
            this.characterModelType = characterModelType;
        }
    }
}