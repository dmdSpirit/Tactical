using UnityEngine;
using System.Collections;
using System;

namespace dmdspirit.Tactical
{
    [SelectionBase]
    public abstract class MapElementHandler : MonoBehaviour
    {
        public event Action<MapElement> OnInitialized;
        public event Action<MapElement> OnElementUpdated;

        protected GameObject model;

        protected void OnValidate()
        {
            // TODO: (dmdspirit) Write data validation.
        }

        public void Initialize(MapElement element)
        {
            transform.position = new Vector3(element.x * transform.localScale.x, element.height * transform.localScale.y, element.y * transform.localScale.z);
            LoadModel();
            if (OnInitialized != null)
                OnInitialized(element);
        }

        public void UpdateMapElement(ref MapElement element)
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

        public abstract void ElementChanged(MapElement element, bool reloadModel = false);

        public abstract void LoadModel();
    }
}