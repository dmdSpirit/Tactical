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

        public MapElementHandler mapElementPrefab;

        public event Action OnMapLoaded;

        private List<MapElementHandler> map;

        public bool CheckIsEmpty()
        {
            if (map == null)
            {
                Debug.Log("Trying to save non initialized map.");
                return true;
            }
            return false;
        }

        public void SaveMap(string mapFilePath)
        {
            string mapJSON = JsonUtility.ToJson(new SerializableMap(map));
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

        private void BuildMap(SerializableMap loadedMap)
        {
            if (mapElementPrefab == null)
            {
                Debug.LogError($"{gameObject.name}: {nameof(mapElementPrefab)} is not set. Could not build the map.");
                return;
            }
            map = new List<MapElementHandler>();
            foreach(var mapElement in loadedMap.mapArray)
            {
                MapElementHandler loadedElement = Instantiate<MapElementHandler>(mapElementPrefab, transform);
                loadedElement.Initialize(mapElement);
                map.Add(loadedElement);
            }
        }

        public void ClearMap()
        {
            if (map == null)
                return;
            foreach (var mapElement in map)
                DestroyImmediate(mapElement.gameObject);
            map.Clear();
        }
    }
}