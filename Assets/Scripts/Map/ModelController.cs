using UnityEngine;
using System.Collections.Generic;

namespace dmdspirit.Tactical
{
    public enum ModelTypeEnum
    {
        MapElement,
        Character
    }

    [ExecuteInEditMode]
    public class ModelController : MonoSingleton<ModelController>
    {
        private Dictionary<string, GameObject> mapElementModelDictionary;
        private Dictionary<string, GameObject> characterModelDictionary;

        public bool modelsInitialized { get; protected set; } = false;

        private void Awake()
        {
            InitializeModels();
        }

        public GameObject GetModel(ModelTypeEnum modelType, string modelName, GameObject loader)
        {
            switch (modelType)
            {
                case ModelTypeEnum.Character:
                    if (characterModelDictionary.ContainsKey(modelName))
                        return characterModelDictionary[modelName];
                    else
                    {
                        Debug.LogError($"Character model {modelName} was not loaded. Probably it does not exist.", loader);
                        return null;
                    }
                case ModelTypeEnum.MapElement:
                    if (mapElementModelDictionary.ContainsKey(modelName))
                        return mapElementModelDictionary[modelName];
                    else
                    {
                        Debug.LogError($"MapElement model {modelName} was not loaded. Probably it does not exist.", loader);
                        return null;
                    }
                default:
                    Debug.LogError($"Wrong {nameof(ModelTypeEnum)} {modelType}.", loader);
                    return null;
            }
        }

        public void InitializeModels()
        {
            var mapElementModels = Resources.LoadAll<GameObject>("Map Element Models");
            mapElementModelDictionary = new Dictionary<string, GameObject>();
            foreach(var mapElementModel in mapElementModels)
                mapElementModelDictionary.Add(mapElementModel.name, mapElementModel);
            var characterModels = Resources.LoadAll<GameObject>("Character Models");
            characterModelDictionary = new Dictionary<string, GameObject>();
            foreach (var characterModel in characterModels)
                characterModelDictionary.Add(characterModel.name, characterModel);
            modelsInitialized = true;
        }
    }
}
