using DoubleDCore.Service;
using UnityEngine;

namespace Game.WorldMap
{
    public class WorldMapController : MonoService
    {
        [SerializeField] private GameObject[] _objects;

        private void Awake()
        {
            Close();
        }

        public void Open()
        {
            foreach (var o in _objects)
                o.SetActive(true);
        }

        public void Close()
        {
            foreach (var o in _objects)
                o.SetActive(false);
        }
    }
}