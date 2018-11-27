using UnityEngine;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class Character
    {
        public string name;
        public int movementSpeed;
        public int jumpHeight;
        public bool hasMoved;
        public bool hasDoneAction;
    }
}