using UnityEngine;
using System.Collections;

namespace dmdspirit.Tactical
{
    [SelectionBase]
    public class MapElementHandler : MonoBehaviour
    {
        public MapElement element;

        [SerializeField]
        private MapElementType currentElementType;

        private GameObject model;

        public void Initialize(MapElement element)
        {
            this.element = element;
            transform.position = new Vector3(element.x * transform.localScale.x, element.height * transform.localScale.y, element.y * transform.localScale.z);
            gameObject.name = $"({element.x},{element.y},{element.height}){element.elementType.ToString()}";
            currentElementType = element.elementType;
            LoadModel();
        }

        // Update mapElement. Used for creating map from unity editor.
        public void UpdateMapElement()
        {
            if (model)
                element.x = (int)(transform.position.x / transform.localScale.x);
            element.y = (int)(transform.position.z / transform.localScale.z);
            element.height = (int)(transform.position.y / transform.localScale.y);
            // Some snapping action here.
            Vector3 snappedPosition = new Vector3(
                element.x * transform.localScale.x,
                element.height * transform.localScale.y,
                element.y * transform.localScale.z);
            if (snappedPosition != transform.position)
            {
                Debug.LogWarning($"Map Element was snapped to map grid (from {transform.position} to {snappedPosition}).", gameObject);
                transform.position = snappedPosition;
            }
            if (element.elementType != currentElementType)
            {
                Debug.LogError($"Something went wrong, {nameof(element.elementType)} is not equal to {nameof(currentElementType)}", gameObject);
                element.elementType = currentElementType;
                LoadModel();
            }
            gameObject.name = $"({element.x},{element.y},{element.height}){element.elementType.ToString()}";
        }

        private void LoadModel()
        {
            if (model != null)
                StartCoroutine(DeleteOldModel(model));
            GameObject modelPrefab = ModelController.Instance.GetModel(ModelTypeEnum.MapElement, element.elementType.ToString(), gameObject);
            if (modelPrefab == null)
                return;
            model = Instantiate(modelPrefab, transform);
        }

        private void OnValidate()
        {
            if (element != null && element.elementType != currentElementType)
            {
                element.elementType = currentElementType;
                LoadModel();
            }
        }

        private IEnumerator DeleteOldModel(GameObject oldModel)
        {
            yield return new WaitForEndOfFrame();
            DestroyImmediate(oldModel);
        }
    }
}