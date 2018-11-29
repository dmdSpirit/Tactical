using UnityEngine;
using System.Collections.Generic;

namespace dmdspirit.Tactical
{
    // TODO: (dmdspirit) Write calculation of surfaces to have seamless textures like in Disgaea 5.

    [ExecuteInEditMode]
    public class ModelController : MonoSingleton<ModelController>
    {
        // TODO: (dmdspirit) Move away from string to enums. We can parse then on LoadModel.
        private Dictionary<string, Material> terrainMaterialsDictionary;
        private Dictionary<string, GameObject> characterModelDictionary;
        private GameObject terrainModel;

        private void Awake()
        {
            InitializeModels();
        }

        public void InitializeModels()
        {
            var mapElementModels = Resources.LoadAll<Material>("Terrain Materials");
            terrainMaterialsDictionary = new Dictionary<string, Material>();
            foreach (var mapElementModel in mapElementModels)
                terrainMaterialsDictionary.Add(mapElementModel.name, mapElementModel);
            var characterModels = Resources.LoadAll<GameObject>("Character Models");
            characterModelDictionary = new Dictionary<string, GameObject>();
            foreach (var characterModel in characterModels)
                characterModelDictionary.Add(characterModel.name, characterModel);
            if (characterModelDictionary.Count == 0)
                Debug.LogError("No character models were loaded.", gameObject);
            if (terrainMaterialsDictionary.Count == 0)
                Debug.LogError("No terrain materials were loaded.", gameObject);
            terrainModel = Resources.Load<GameObject>("TerrainModel");
            if (terrainModel == null)
                Debug.LogError("Could not load terrain model asset.", gameObject);
        }

        public GameObject LoadModel(CharacterElement characterElement)
        {
            // TODO: (dmdspirit) Implement loading character model.
            return null;
        }

        public GameObject LoadModel(TerrainElement terrainElement)
        {
            // TODO: (dmdspirit) Implement loading terrain model.
            return null;
        }

        public GameObject LoadModel(MapElement mapElement, Transform callerTransform)
        {
            if (terrainMaterialsDictionary == null || characterModelDictionary == null||terrainModel==null)
                InitializeModels();
            switch (mapElement.elementType)
            {
                case MapElementType.Character:
                    {
                        CharacterElement element = (CharacterElement)mapElement;
                        if (characterModelDictionary.ContainsKey(element.modelType.ToString()))
                            return Instantiate(characterModelDictionary[element.modelType.ToString()], callerTransform);
                        else
                        {
                            Debug.LogError($"Character model {element.modelType.ToString()} was not found in models dictionary. Seems like model asset is missing", callerTransform.gameObject);
                            return null;
                        }
                    }
                case MapElementType.Terrain:
                    {
                        TerrainElement element = (TerrainElement)mapElement;
                        if (element.terrainType == TerrainType.Empty)
                            return null;
                        if (terrainMaterialsDictionary.ContainsKey(element.terrainType.ToString())==false)
                        {
                            Debug.LogError($"Terrain material {element.terrainType.ToString()} was not found in models dictionary. Seems like model asset is missing", callerTransform.gameObject);
                            return null;

                        }
                        GameObject newTerrainModel = Instantiate(terrainModel, callerTransform);
                        MeshRenderer meshRenderer = newTerrainModel.GetComponent<MeshRenderer>();
                        if (meshRenderer == null)
                        {
                            Debug.LogError("Terrain model prefab is missing MeshRenderer component.", gameObject);
                            return newTerrainModel;
                        }
                        meshRenderer.material = terrainMaterialsDictionary[element.terrainType.ToString()];
                        return newTerrainModel;
                    }
                default:
                    Debug.LogError($"Model loading is not implemented for this type of mapElement {mapElement.elementType}", callerTransform.gameObject);
                    return null;
            }
        }
    }
}