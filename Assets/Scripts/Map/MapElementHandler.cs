using UnityEngine;
using System.Collections;
using System;

namespace dmdspirit.Tactical
{
    [SelectionBase]
    public class MapElementHandler : MonoBehaviour
    {
        public event Action<MapElement> OnInitialized;
        public event Action<MapElement> OnElementUpdated;

        private MapElement element;
        private GameObject model;

        public void Initialize(MapElement element)
        {
            this.element = element;
            transform.position = new Vector3(element.x * transform.localScale.x, element.height * transform.localScale.y, element.y * transform.localScale.z);
            gameObject.name = $"({element.x},{element.y},{element.height}){element.elementType.ToString()}";
            LoadModel();
            if (OnInitialized != null)
                OnInitialized(element);
        }

        public void UpdateMapElement()
        {
            if (element == null)
            {
                Debug.LogWarning($"Map element is not initialized.", gameObject);
                return;
            }
            element.x = (int)(transform.position.x / transform.localScale.x);
            element.y = (int)(transform.position.z / transform.localScale.z);
            element.height = (int)(transform.position.y / transform.localScale.y);
            // Some snapping action here.
            // TODO: (dmdspirit) Snapping seems to behave strangely. Should check this.
            Vector3 snappedPosition = new Vector3(
                element.x * transform.localScale.x,
                element.height * transform.localScale.y,
                element.y * transform.localScale.z);
            if (snappedPosition != transform.position)
            {
                Debug.LogWarning($"Map Element was snapped to map grid (from {transform.position} to {snappedPosition}).", gameObject);
                transform.position = snappedPosition;
            }
            if (OnElementUpdated != null)
                OnElementUpdated(element);
        }

        public void ElementChanged(MapElement element, bool reloadModel = false)
        {
            this.element = element;
            if (reloadModel)
                LoadModel();
        }

        public void LoadModel()
        {
            if (model != null)
                StartCoroutine(DeleteOldModel(model));
            model = ModelController.Instance.LoadModel(element, transform);
        }

        private IEnumerator DeleteOldModel(GameObject oldModel)
        {
            yield return new WaitForEndOfFrame();
            DestroyImmediate(oldModel);
        }
    }
}