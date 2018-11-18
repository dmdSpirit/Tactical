using UnityEngine;

namespace dmdspirit.Tactical
{
    public class MapElementHandler : MonoBehaviour
    {
        MapElement element;

        public void Initialize(MapElement element)
        {
            this.element = element;
            transform.position = new Vector3(element.x, element.y, element.height);
            LoadModel();
        }

        private void LoadModel()
        {
            
        }
    }
}