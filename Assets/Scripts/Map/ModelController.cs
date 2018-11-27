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
        [SerializeField]
        private GameObject[] mapElementModels;
        [SerializeField]
        private GameObject[] characterModels;

        private void Awake()
        {
            InitializeModels();
        }

        public GameObject GetModel(ModelTypeEnum modelType, string modelName)
        {
            // TODO: (Stas) Finish GetModel.
            return null;
        }

        private void InitializeModels()
        {
            mapElementModels = Resources.LoadAll<GameObject>("Map Element Models");
            characterModels = Resources.LoadAll<GameObject>("Character Models");
        }
    }
}
