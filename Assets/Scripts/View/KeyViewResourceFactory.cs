using UnityEngine;
using Zenject;

namespace View
{
    public interface IResource
    {
        string ResourceId { get; }
    }

    public class KeyViewResourceFactory<TKey, TView> : IFactory<TKey, TView>
        where TView : MonoBehaviour
        where TKey : IResource
    {
        private readonly DiContainer _container;
        private readonly string _path;

        protected KeyViewResourceFactory(DiContainer container, string path)
        {
            _container = container;
            _path = path;
        }

        public TView Create(TKey key)
        {
            var go = _container.InstantiatePrefabResourceForComponent<TView>(string.Format(_path, key.ResourceId), new [] { (object) key });
            return go;
        }
    }
}