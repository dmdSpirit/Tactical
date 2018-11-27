using UnityEngine;
using System.Collections;

namespace dmdspirit.Tactical
{
    public class CharacterHandler : MonoBehaviour
    {
        private GameObject model;
        private Character character;

        public void Initialize(Character character)
        {
            this.character = character;
            LoadModel();
        }

        private void LoadModel()
        {
            if (model != null)
                StartCoroutine(ClearOldModel(model));
            // TODO: (Stas) Finish character initialization.
        }

        private IEnumerator ClearOldModel(GameObject oldModel)
        {
            yield return new WaitForEndOfFrame();
            DestroyImmediate(oldModel);
        }
    }
}