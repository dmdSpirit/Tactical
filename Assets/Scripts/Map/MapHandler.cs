using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

namespace dmdspirit.Tactical
{
    public class MapHandler : MonoSingleton<MapHandler>
    {
        public string mapFileFolder = "Maps";
        public string mapName = "TestMap";

        public TerrainElementHandler terrainElementPrefab;

        public event Action OnMapLoaded;

        private List<TerrainElementHandler> terrainMap;

        // TODO: (dmdspirit) Add character save/load.
        // TODO: (dmdspirit) I think i should create base class 'MapElement' and generalize all this saving/loading.

        private void Start()
        {
            UpdateMap();
        }

        public bool CheckIsEmpty()
        {
            if (terrainMap == null)
            {
                Debug.Log("Trying to save non initialized map.");
                return true;
            }
            return false;
        }

        public void SaveMap(string mapFilePath)
        {
            UpdateMap();
            string mapJSON = JsonUtility.ToJson(new SerializableMap(terrainMap));
            File.WriteAllText(mapFilePath, mapJSON);
            Debug.Log($"Map was saved to {mapFilePath}");
        }

        public void LoadMap(string mapFilePath)
        {
            
            string mapJSON = File.ReadAllText(mapFilePath);
            SerializableMap loadedMap = JsonUtility.FromJson<SerializableMap>(mapJSON);
            if(loadedMap.IsInitialized() == false)
            {
                Debug.LogError($"Map file has wrong data format or loaded map was empty. {mapFilePath}");
                return;
            }
            ClearMap();
            BuildMap(loadedMap);
            if (OnMapLoaded != null)
                OnMapLoaded();
        }

        public void ClearMap()
        {
            UpdateMap();
            if (terrainMap == null)
                return;
            foreach (var mapElement in terrainMap)
                DestroyImmediate(mapElement.gameObject);
            terrainMap.Clear();
        }

        public void UpdateMap()
        {
            terrainMap = new List<TerrainElementHandler>();
            foreach(TerrainElementHandler mapElementHandler in FindObjectsOfType<TerrainElementHandler>())
            {
                if (mapElementHandler.transform.parent != transform)
                    Debug.LogError($"{nameof(TerrainElementHandler)} object is not child of {gameObject.name} and will not be saved with the rest map elements.", mapElementHandler.gameObject);
                else
                {
                    mapElementHandler.UpdateMapElement();
                    terrainMap.Add(mapElementHandler);
                }
            }
        }

        private void BuildMap(SerializableMap loadedMap)
        {
            if (terrainElementPrefab == null)
            {
                Debug.LogError($"{gameObject.name}: {nameof(terrainElementPrefab)} is not set. Could not build the map.");
                return;
            }
            terrainMap = new List<TerrainElementHandler>();
            foreach(var mapElement in loadedMap.terrainArray)
            {
                TerrainElementHandler loadedElement = Instantiate<TerrainElementHandler>(terrainElementPrefab, transform);
                loadedElement.Initialize(mapElement);
                terrainMap.Add(loadedElement);
            }
        }
    }
}