using UnityEngine;
using System.Collections;

namespace dmdspirit.Tactical
{
    [RequireComponent(typeof(MapElementHandler))]
    public class TerrainElementHandler : MonoBehaviour
    {
        public TerrainElement terrainElement;
        [SerializeField]
        private TerrainType currentTerrainType;

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
            this.terrainElement = (TerrainElement)element;
            gameObject.name = $"({terrainElement.x},{terrainElement.y},{terrainElement.height}){terrainElement.terrainType.ToString()} Terrain";
            isInitialized = true;
            currentTerrainType = terrainElement.terrainType;
        }

        public void UpdateElement(MapElement element)
        {
            this.terrainElement = (TerrainElement)element;
            gameObject.name = $"({terrainElement.x},{terrainElement.y},{terrainElement.height}){terrainElement.terrainType.ToString()} Terrain";
        }

        private void OnValidate()
        {
            if (isInitialized == false)
                return;
            if (currentTerrainType != terrainElement.terrainType)
            {
                terrainElement.terrainType = currentTerrainType;
                mapElementHandler.ElementChanged(terrainElement, true);
            }
        }
    }
}