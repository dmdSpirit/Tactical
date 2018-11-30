using UnityEngine;
using System.Collections;
using System;

namespace dmdspirit.Tactical
{
    [SelectionBase]
    [ExecuteInEditMode]
    public abstract class MapElementHandler : MonoBehaviour
    {
        public event Action<MapElement> OnMapElementInitialized;
        public event Action<MapElement> OnMapElementUpdated;

        [SerializeField]
        [HideInInspector]
        protected GameObject model;

        protected void InitializeMapElement(MapElement element)
        {
            transform.position = new Vector3(element.x * transform.localScale.x, element.height * transform.localScale.y, element.y * transform.localScale.z);
            LoadModel();
            if (OnMapElementInitialized != null)
                OnMapElementInitialized(element);
        }

        // TODO: (dmdspirit) Fix positioning issues that occur on UpdateMapElement if element was rotated.
        protected void UpdateMapElement(ref MapElement element)
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
            if (OnMapElementUpdated != null)
                OnMapElementUpdated(element);
        }

        protected virtual void LoadModel()
        {
            if (model != null)
                DestroyImmediate(model);
        }
    }
}