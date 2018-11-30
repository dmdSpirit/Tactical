using UnityEngine;

namespace dmdspirit.Tactical
{
    [SelectionBase]
    [ExecuteInEditMode]
    public class TerrainElementHandler : MapElementHandler
    {
        public TerrainElement terrainElement;

        [SerializeField]
        private TerrainType currentTerrainType;

        private bool isInitialized = false;

        private void Awake()
        {
            if (terrainElement != null)
                InitializeTerrain(terrainElement);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (isInitialized == false)
                return;
            if (transform.position != terrainElement.mapElement.GetWorldPosition())
                UpdateMapElement(ref terrainElement.mapElement);
            if (currentTerrainType != terrainElement.terrainType)
            {
                terrainElement.terrainType = currentTerrainType;
                UnityEditor.EditorApplication.delayCall += () => LoadModel();
            }
        }
#endif

        public void InitializeTerrain(TerrainElement terrainElement)
        {
            this.terrainElement = terrainElement;
            currentTerrainType = terrainElement.terrainType;
            InitializeMapElement(terrainElement.mapElement);
            isInitialized = true;
        }

        public void UpdateElement()
        {
            UpdateMapElement(ref terrainElement.mapElement);
            name = terrainElement.ToString();
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            model = ModelController.Instance.LoadTerrainModel(terrainElement, transform);
            gameObject.name = terrainElement.ToString();
        }
    }
}