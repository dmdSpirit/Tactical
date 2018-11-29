using UnityEngine;

namespace dmdspirit.Tactical
{
    [RequireComponent(typeof(MapElementHandler))]
    public class CharacterHandler : MapElementHandler
    {
        public CharacterElement characterElement;

        [SerializeField]
        private CharacterModelType currentModelType;
        [SerializeField]
        private FacingDirection currentFacingDirection;

        public Vector3 moveToVector;
        public float speed = 1;
        public bool isMoving = false;

        private bool isInitialized = false;

        public void InitializeCharacter(CharacterElement characterElement)
        {
            Initialize(characterElement.mapElement);
            this.characterElement = characterElement;
            gameObject.name = characterElement.ToString();
            transform.localRotation = Quaternion.Euler(0, 90 * (int)characterElement.facingDirection, 0);
            isInitialized = true;
            currentModelType = characterElement.modelType;
        }

        [ContextMenu("Move character")]
        public void MoveCharacter()
        {
            isMoving = true;
            StartCoroutine(AnimationController.Instance.WalkHorizontally(transform, transform.position, moveToVector, speed, FinishedMovement));
        }

        public void FinishedMovement()
        {
            isMoving = false;
            UpdateMapElement(ref characterElement.mapElement);
        }

        public override void ElementChanged(MapElement element, bool reloadModel = false)
        {
            throw new System.NotImplementedException();
        }

        public override void LoadModel()
        {
            throw new System.NotImplementedException();
        }
    }
}