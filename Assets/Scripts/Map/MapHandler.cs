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
        public CharacterHandler characterPrefab;

        public event Action OnMapLoaded;

        private List<TerrainElementHandler> terrainList;
        private List<CharacterHandler> charactersList;

        // TODO: (dmdspirit) Add character save/load.
        // TODO: (dmdspirit) I think i should create base class 'MapElement' and generalize all this saving/loading.

        private void Start()
        {
            UpdateMap();
        }

        public bool CheckIsEmpty()
        {
            if (terrainList == null && charactersList == null)
            {
                Debug.Log("Trying to save non initialized map.");
                return true;
            }
            return false;
        }

        public void SaveMap(string mapFilePath)
        {
            UpdateMap();
            string mapJSON = JsonUtility.ToJson(new SerializableMap(terrainList, charactersList));
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
            if (terrainList == null)
                return;
            foreach (var mapElement in terrainList)
                DestroyImmediate(mapElement.gameObject);
            foreach (var character in charactersList)
                DestroyImmediate(character.gameObject);
            terrainList.Clear();
            charactersList.Clear();
        }

        public void UpdateMap()
        {
            terrainList = new List<TerrainElementHandler>();
            charactersList = new List<CharacterHandler>();
            foreach(TerrainElementHandler mapElementHandler in FindObjectsOfType<TerrainElementHandler>())
            {
                if (mapElementHandler.transform.parent != transform)
                    Debug.LogError($"{nameof(TerrainElementHandler)} object is not child of {gameObject.name} and will not be saved with the rest map elements.", mapElementHandler.gameObject);
                else
                {
                    mapElementHandler.GetComponent<MapElementHandler>().UpdateMapElement();
                    terrainList.Add(mapElementHandler);
                }
            }
            foreach (CharacterHandler characterHandler in FindObjectsOfType<CharacterHandler>())
            {
                if (characterHandler.transform.parent != transform)
                    Debug.LogError($"{nameof(CharacterHandler)} object is not child of {gameObject.name} and will not be saved with the rest map elements.", characterHandler.gameObject);
                else
                {
                    characterHandler.GetComponent<MapElementHandler>().UpdateMapElement();
                    charactersList.Add(characterHandler);
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
            if (characterPrefab == null)
            {
                Debug.LogError($"{gameObject.name}: {nameof(characterPrefab)} is not set. Could not build the map.");
                return;
            }
            terrainList = new List<TerrainElementHandler>();
            foreach(var mapElement in loadedMap.terrainArray)
            {
                TerrainElementHandler loadedElement = Instantiate<TerrainElementHandler>(terrainElementPrefab, transform);
                loadedElement.Initialize(mapElement);
                terrainList.Add(loadedElement);
            }
            charactersList = new List<CharacterHandler>();
            foreach (var character in loadedMap.characterArray)
            {
                CharacterHandler loadedElement = Instantiate<CharacterHandler>(characterPrefab, transform);
                loadedElement.Initialize(character);
                charactersList.Add(loadedElement);
            }
        }

        // TODO: (dmdspirit) New Elements do not get loaded models.
        public void GenerateTerrainElement()
        {
            TerrainElementHandler loadedElement = Instantiate<TerrainElementHandler>(terrainElementPrefab, transform);
            loadedElement.GetComponent<MapElementHandler>().Initialize(new TerrainElement(0,0,0,true,TerrainType.Rock));
            if (terrainList == null)
                terrainList = new List<TerrainElementHandler>();
            terrainList.Add(loadedElement);
        }

        public void GenerateCharacterElement()
        {
            CharacterHandler loadedElement = Instantiate<CharacterHandler>(characterPrefab, transform);
            loadedElement.GetComponent<MapElementHandler>().Initialize(new CharacterElement(0, 0, 0, "Character", 1, 1, CharacterModelType.Warrior));
            //loadedElement.Initialize();
            if (charactersList == null)
                charactersList = new List<CharacterHandler>();
            charactersList.Add(loadedElement);
        }
    }
}