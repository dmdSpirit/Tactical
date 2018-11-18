using UnityEngine;

namespace dmdspirit.Tactical
{
    public class MapElementHandler : MonoBehaviour
    {
        [SerializeField]
        public MapElement element { get; protected set; }

        private GameObject model;

        public void Initialize(MapElement element)
        {
            this.element = element;
            transform.position = new Vector3(element.x, element.height, element.y);
            gameObject.name = $"({element.x},{element.y},{element.height}){element.elementType.ToString()}";
            LoadModel();
        }

        private void LoadModel()
        {
            GameObject modelPrefab = ModelController.Instance.GetModel(element.elementType.ToString());
            if (modelPrefab == null)
                return;
            model = Instantiate(modelPrefab, transform);
        }
    }
}