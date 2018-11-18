using UnityEngine;

namespace dmdspirit.Tactical
{
    public class ModelController : MonoSingleton<ModelController>
    {
        [SerializeField]
        GameObject[] models;

        // TODO: (dmdspirit) Write models array initialization.

        public GameObject GetModel(string modelName)
        {
            if(models == null)
            {
                Debug.LogError($"{gameObject.name}: Models array is empty.");
                return null;
            }
            foreach(var model in models)
                if (model.name == modelName)
                    return model;
            Debug.LogWarning($"Model with name {modelName} was not found.");
            return null;
        }

        
    }
}
