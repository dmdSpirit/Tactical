using UnityEngine;
using System.Collections;

namespace dmdspirit.Tactical
{
    // TODO: (dmdspirit) Replace 'class' enum with scriptableObjects characters system.
    public enum CharacterModelType
    {
        Warrior,
        Mage
    }

    // TODO: (dmdspirit) Welp, I think I should move most of the functionality to base MapElement class
    // and create Character, Terrain, Obstacle classes.
    [SelectionBase]
    public class CharacterHandler : MonoBehaviour
    {
        /*
        [SerializeField]
        private CharacterModelType currentCharacterModelType;

        private GameObject model;

        public void Initialize(Character character)
        {
            this.character = character;
            transform.position = new Vector3(character.x * transform.localScale.x, character.height * transform.localScale.y, character.y * transform.localScale.z);
            currentCharacterModelType = character.characterModelType;
            LoadModel();
        }

        public void UpdateCharacterElement()
        {
            if (model)
                character.x = (int)(transform.position.x / transform.localScale.x);
            character.y = (int)(transform.position.z / transform.localScale.z);
            character.height = (int)(transform.position.y / transform.localScale.y);
            // Some snapping action here.
            Vector3 snappedPosition = new Vector3(
                character.x * transform.localScale.x,
                character.height * transform.localScale.y,
                character.y * transform.localScale.z);
            if (snappedPosition != transform.position)
            {
                Debug.LogWarning($"Map Element was snapped to map grid (from {transform.position} to {snappedPosition}).", gameObject);
                transform.position = snappedPosition;
            }
            if (character.characterModelType != currentCharacterModelType)
            {
                Debug.LogError($"Something went wrong, {nameof(character.characterModelType)} is not equal to {nameof(currentCharacterModelType)}", gameObject);
                character.characterModelType = currentCharacterModelType;
                LoadModel();
            }
            gameObject.name = character.name;
        }

        private void LoadModel()
        {
            if (model != null)
                StartCoroutine(ClearOldModel(model));
            GameObject modelPrefab = ModelController.Instance.GetModel(ModelTypeEnum.Character, character.characterModelType.ToString(), gameObject);
            if (modelPrefab == null)
                return;
            model = Instantiate(modelPrefab, transform);
        }

        private void OnValidate()
        {
            if(character != null && character.characterModelType != currentCharacterModelType)
            {
                character.characterModelType = currentCharacterModelType;
                LoadModel();
            }
        }

        private IEnumerator ClearOldModel(GameObject oldModel)
        {
            yield return new WaitForEndOfFrame();
            DestroyImmediate(oldModel);
        }*/
    }
}