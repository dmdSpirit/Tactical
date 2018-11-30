using UnityEngine;

namespace dmdspirit.Tactical
{
    [SelectionBase]
    [ExecuteInEditMode]
    public class CharacterHandler : MapElementHandler
    {
        public CharacterElement characterElement;

        [SerializeField]
        private CharacterModelType currentModelType;
        [SerializeField]
        private FacingDirection currentFacingDirection;

        // For testing.
        public Vector3 moveToVector;
        // I'm not sure if all units should have same movement animation speed.
        public float speed = 1;

        // I think all input will be locked while unit is moving. Should test this later.
        private bool isMoving = false;
        private bool isInitialized = false;

        private void Awake()
        {
            if (characterElement != null)
                InitializeCharacter(characterElement);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (isInitialized == false)
                return;
            if (transform.position != characterElement.mapElement.GetWorldPosition())
                UpdateMapElement(ref characterElement.mapElement);
            if (currentFacingDirection != characterElement.facingDirection)
                RotateCharacter();
            if(currentModelType != characterElement.modelType)
            {
                characterElement.modelType = currentModelType;
                UnityEditor.EditorApplication.delayCall += () => LoadModel();
            }
        }
#endif

        public void InitializeCharacter(CharacterElement characterElement)
        {
            this.characterElement = characterElement;
            transform.localRotation = Quaternion.Euler(0, 90 * (int)characterElement.facingDirection, 0);
            currentModelType = characterElement.modelType;
            InitializeMapElement(characterElement.mapElement);
            isInitialized = true;
        }

        // For testing only.
        [ContextMenu("Move character")]
        public void MoveCharacter()
        {
            isMoving = true;
            StartCoroutine(AnimationController.Instance.WalkHorizontally(transform, transform.position, moveToVector, speed, FinishedMovement));
        }

        public void RotateCharacter()
        {
            characterElement.facingDirection = currentFacingDirection;
            transform.localRotation = Quaternion.Euler(0, 90 * (int)characterElement.facingDirection, 0);
        }

        public void UpdateElement()
        {
            UpdateMapElement(ref characterElement.mapElement);
            gameObject.name = characterElement.ToString();
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            model = ModelController.Instance.LoadCharacterModel(characterElement, transform);
            gameObject.name = characterElement.ToString();
        }

        private void FinishedMovement()
        {
            isMoving = false;
            UpdateElement();
        }
    }
}