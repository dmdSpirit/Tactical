using UnityEngine;

namespace dmdspirit.Tactical
{
    [RequireComponent(typeof(MapElementHandler))]
    public class CharacterHandler : MonoBehaviour
    {
        public CharacterElement characterElement;
        [SerializeField]
        private CharacterModelType currentModelType;
        [SerializeField]
        private DirectionEnum currentFacingDirection;

        public Vector3 moveToVector;
        public float speed = 1;
        public bool isMoving = false;

        private MapElementHandler mapElementHandler;
        private bool isInitialized = false;


        private void Awake()
        {
            mapElementHandler = GetComponent<MapElementHandler>();
        }

        private void Start()
        {
            mapElementHandler.OnInitialized += Initialize;
            mapElementHandler.OnElementUpdated += UpdateElement;
        }

        public void Initialize(MapElement element)
        {
            this.characterElement = (CharacterElement)element;
            gameObject.name = $"({characterElement.x},{characterElement.y},{characterElement.height}){characterElement.modelType.ToString()} {characterElement.name}";
            transform.localRotation = Quaternion.Euler(0, 90 * (int)characterElement.facingDirection, 0);
            isInitialized = true;
            currentModelType = characterElement.modelType;
        }

        public void UpdateElement(MapElement element)
        {
            this.characterElement = (CharacterElement)element;
            gameObject.name = $"({characterElement.x},{characterElement.y},{characterElement.height}){characterElement.modelType.ToString()} {characterElement.name}";
        }

        private void OnValidate()
        {
            if (isInitialized == false || isMoving)
                return;
            if (currentModelType != characterElement.modelType)
            {
                characterElement.modelType = currentModelType;
                mapElementHandler.ElementChanged(characterElement, true);
            }

            // TODO: (dmdspirit) Changing facing direction in Inspector does not change anything.
            if (currentFacingDirection != characterElement.facingDirection)
            {
                characterElement.facingDirection = currentFacingDirection;
                mapElementHandler.ElementChanged(characterElement);
            }
        }

        [ContextMenu("Move character")]
        public void MoveCharacter()
        {
            isMoving = true;
            StartCoroutine(AnimationController.Instance.MoveAnimation(transform, transform.position, moveToVector, speed, FinishedMovement));
        }

        public void FinishedMovement()
        {
            isMoving = false;
            mapElementHandler.UpdateMapElement();
        }
    }
}