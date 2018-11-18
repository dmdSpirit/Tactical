using UnityEngine;
using UnityEditor;
using System.IO;

namespace dmdspirit.Tactical
{
    [CustomEditor(typeof(MapHandler))]
    public class MapHandlerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MapHandler mapHanlder = (MapHandler)target;
            string fullFolderPath = $"{Application.dataPath}/{mapHanlder.mapFileFolder}";
            if (GUILayout.Button("Save Map"))
            {
                if (Directory.Exists(fullFolderPath) == false)
                    Directory.CreateDirectory(fullFolderPath);
                if (mapHanlder.CheckIsEmpty() == false)
                {
                    string mapPath = EditorUtility.SaveFilePanel("Save current map", fullFolderPath, "map_1", "map");
                    if (string.IsNullOrEmpty(mapPath) == false)
                        mapHanlder.SaveMap(mapPath);
                }
            }
            if(GUILayout.Button("Load Map"))
            {
                if (Directory.Exists(fullFolderPath) == false)
                    Directory.CreateDirectory(fullFolderPath);
                string mapPath = EditorUtility.OpenFilePanel("Load map", fullFolderPath, "map");
                if (string.IsNullOrEmpty(mapPath) == false)
                    mapHanlder.LoadMap(mapPath);
            }
        }
    }
}