using UnityEngine;
using System.Collections;

namespace dmdspirit.Tactical
{
    [SelectionBase]
    public class TerrainElementHandler : MonoBehaviour
    {
        public TerrainElement element;

        [SerializeField]
        private TerrainType currentTerrainType;

        private GameObject model;

        public void Initialize(TerrainElement element)
        {
            this.element = element;
            transform.position = new Vector3(element.x * transform.localScale.x, element.height * transform.localScale.y, element.y * transform.localScale.z);
            gameObject.name = $"({element.x},{element.y},{element.height}){element.terrainType.ToString()}-Terrain";
            currentTerrainType = element.terrainType;
            LoadModel();
        }

        // Update mapElement. Used for creating map from unity editor.
        public void UpdateMapElement()
        {
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
            if (element.terrainType != currentTerrainType)
            {
                Debug.LogError($"Something went wrong, {nameof(element.terrainType)} is not equal to {nameof(currentTerrainType)}", gameObject);
                element.terrainType = currentTerrainType;
                LoadModel();
            }
            gameObject.name = $"({element.x},{element.y},{element.height}){element.elementType.ToString()}";
        }

        private void LoadModel()
        {
            if (model != null)
                StartCoroutine(DeleteOldModel(model));
            model = ModelController.Instance.LoadModel(element, transform);
        }

        private void OnValidate()
        {
            if (element != null && element.terrainType != currentTerrainType)
            {
                element.terrainType = currentTerrainType;
                LoadModel();
            }
        }

        private IEnumerator DeleteOldModel(GameObject oldModel)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log($"Destroying {oldModel.name}.");
            DestroyImmediate(oldModel);
        }
    }
}