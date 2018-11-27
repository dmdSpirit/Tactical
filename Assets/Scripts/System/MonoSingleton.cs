using UnityEngine;

namespace dmdspirit.Tactical
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static object lockObject = new object();
        private static bool applicationIsQuiting = false;

        public static T Instance
        {
            get
            {
                if (applicationIsQuiting)
                {
                    Debug.LogWarning($"[MonoSingleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again - returning null");
                }

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            string objectNames = "";
                            foreach (var o in FindObjectsOfType(typeof(T)))
                                objectNames += o.name + ", ";
                            Debug.LogError($"[MonoSingleton] Something went really wrong - found more than 1 instance of singletone ({objectNames})!  Reopening the scene might fix it.");
                            return instance;
                        }
                        if (instance == null)
                        {
                            GameObject singleton = new GameObject();
                            instance = singleton.AddComponent<T>();
                            singleton.name = $"(singleton) {typeof(T)}";
                            //DontDestroyOnLoad(singleton);
                            Debug.Log($"[MonoSingleton] An instance of {typeof(T)} is needed in the scene so '{singleton}' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            //                        Debug.Log($"[MonoSingleton] Using instance already created: {_instance.gameObject.name}");
                        }
                    }
                    return instance;
                }
            }
        }

        private void OnDestroy()
        {
            //applicationIsQuiting = true;
        }
    }
}