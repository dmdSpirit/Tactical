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

        public event Action OnMapLoaded;

        private List<MapElement> map;

        private void Start()
        {
            map = new List<MapElement>();
            map.Add(new MapElement(0, 0));
            map.Add(new MapElement(0, 1, elementType: MapElementType.Block, canStandOn: true));
            map.Add(new MapElement(1, 0, elementType: MapElementType.Block, canStandOn: true));

        }

        public List<MapElement> GetElement(int x, int y)
        {
            List<MapElement> result = new List<MapElement>();
            if (map == null)
                Debug.LogError("Trying to get element from non initialized map.");
            else
                foreach (var elemtent in map)
                    if (elemtent.x == x && elemtent.y == y)
                        result.Add(elemtent);
            return result;
        }

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
            map = new List<MapElement>(loadedMap.mapArray);
            if (OnMapLoaded != null)
                OnMapLoaded();
        }

        private void BuildMap()
        {

        }

        private void ClearMap()
        {

        }
    }
}