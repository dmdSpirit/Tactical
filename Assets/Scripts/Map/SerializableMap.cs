using System.Collections.Generic;
using UnityEngine;

namespace dmdspirit.Tactical
{
    [System.Serializable]
    public class SerializableMap
    {
        public TerrainElement[] terrainArray;
        public CharacterElement[] characterArray;

        public SerializableMap(List<TerrainElementHandler> terrainList, List<CharacterHandler> characterList)
        {
            if (terrainList == null)
            {
                Debug.LogError("Trying to serialize non initialized list of map elements.");
                return;
            }
            if (characterList == null)
            {
                Debug.LogError("Trying to serialize non initialized list of characters.");
                return;
            }
            this.terrainArray = new TerrainElement[terrainList.Count];
            for (int i = 0; i < terrainList.Count; i++)
                this.terrainArray[i] = terrainList[i].terrainElement;
            this.characterArray = new CharacterElement[characterList.Count];
            for (int i = 0; i < characterList.Count; i++)
                this.characterArray[i] = characterList[i].characterElement;
        }

        public bool IsInitialized()
        {
            return (terrainArray != null && characterArray != null) && (terrainArray.Length > 0 || characterArray.Length>0);
        }
    }
}