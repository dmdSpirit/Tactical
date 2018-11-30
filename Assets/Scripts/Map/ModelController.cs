using UnityEngine;
using System.Collections.Generic;
using System;

namespace dmdspirit.Tactical
{

    [ExecuteInEditMode]
    public class ModelController : MonoSingleton<ModelController>
    {
        [SerializeField]
        private GameObject terrainModel;
        [SerializeField]
        private string characterModesFolder = "Character Models";
        [SerializeField]
        private string terrainMaterialsFolder = "Terrain Materials";

        private Dictionary<CharacterModelType, GameObject> characterModels;
        private Dictionary<TerrainType, Material> terrainMaterials;

        public GameObject LoadCharacterModel(CharacterElement characterElement, Transform parentTransform)
        {
            if (characterModels == null)
                if (InitializeCharacterModels() == false)
                {
                    Debug.LogError("Could not initialize character models.", gameObject);
                    return null;
                }
            if (characterModels.ContainsKey(characterElement.modelType))
            {
                GameObject newCharacterModel = Instantiate(characterModels[characterElement.modelType], parentTransform);
                newCharacterModel.name = $"{characterElement.modelType.ToString()} Model";
                return newCharacterModel;
            }
            else
            {
                Debug.LogError($"Character model {characterElement.modelType.ToString()} could not be loaded.", parentTransform.gameObject);
                return null;
            }
        }

        public GameObject LoadTerrainModel(TerrainElement terrainElement, Transform parentTransform)
        {
            if (terrainMaterials == null || terrainModel == null)
                if (InitializeTerrainMaterials() == false)
                {
                    Debug.LogError("Could not initialize terrain materials.", gameObject);
                    return null;
                }
            if (terrainElement.terrainType == TerrainType.Empty)
                return null;
            GameObject newTerrainModel = Instantiate(terrainModel, parentTransform);
            if (terrainMaterials.ContainsKey(terrainElement.terrainType))
            {
                MeshRenderer meshRenderer = newTerrainModel.GetComponent<MeshRenderer>();
                if (meshRenderer == null)
                {
                    Debug.LogError("Terrain model prefab is missing MeshRenderer component.", gameObject);
                    return newTerrainModel;
                }
                meshRenderer.material = terrainMaterials[terrainElement.terrainType];
            }
            newTerrainModel.name = $"{terrainElement.terrainType.ToString()} Model";
            return newTerrainModel;
        }

        private bool InitializeCharacterModels()
        {
            if (terrainModel == null)
            {
                Debug.LogError($"{nameof(terrainModel)} is not set.", gameObject);
                return false;
            }
            characterModels = new Dictionary<CharacterModelType, GameObject>();
            var loadedCharacterModels = Resources.LoadAll<GameObject>(characterModesFolder);
            if (loadedCharacterModels.Length == 0)
            {
                Debug.LogError($"No character model prefabs found in Resources/{characterModesFolder} folder.", gameObject);
                return false;
            }
            foreach (var characterModel in loadedCharacterModels)
            {
                CharacterModelType modelType;
                if (Enum.TryParse<CharacterModelType>(characterModel.name, out modelType))
                    characterModels.Add(modelType, characterModel);
                else
                    Debug.LogError($"No model type found for Resources/{characterModesFolder}/{characterModel.name}. It will not be loaded", characterModel);
            }
            if (characterModels.Count == 0)
            {
                Debug.LogError($"No character models loaded.", gameObject);
                return false;
            }
            if (characterModels.Count != Enum.GetValues(typeof(CharacterModelType)).Length)
                foreach (CharacterModelType modelType in Enum.GetValues(typeof(CharacterModelType)))
                    if (characterModels.ContainsKey(modelType) == false)
                        Debug.LogError($"Missing character model prefab for {modelType} model type.", gameObject);
            return true;
        }

        private bool InitializeTerrainMaterials()
        {
            terrainMaterials = new Dictionary<TerrainType, Material>();
            var loadedTerrainMaterials = Resources.LoadAll<Material>(terrainMaterialsFolder);
            if (loadedTerrainMaterials.Length == 0)
            {
                Debug.LogError($"No material files found in Resources/{terrainMaterialsFolder} folder.", gameObject);
                return false;
            }
            foreach (var terrainMaterial in loadedTerrainMaterials)
            {
                TerrainType terrainType;
                if (Enum.TryParse<TerrainType>(terrainMaterial.name, out terrainType))
                    terrainMaterials.Add(terrainType, terrainMaterial);
                else
                    Debug.LogError($"No terrain type found for Resources/{terrainMaterialsFolder}/{terrainMaterial.name}. It will not be loaded", terrainMaterial);
            }
            if (terrainMaterials.Count == 0)
            {
                Debug.LogError($"No terrain type materials loaded.", gameObject);
                return false;
            }
            if (terrainMaterials.Count != Enum.GetValues(typeof(TerrainType)).Length)
                foreach (TerrainType terrainType in Enum.GetValues(typeof(TerrainType)))
                    if (terrainType != TerrainType.Empty && terrainMaterials.ContainsKey(terrainType) == false)
                        Debug.LogError($"Missing material file for {terrainType} terrain type.", gameObject);
            return true;
        }
    }
}