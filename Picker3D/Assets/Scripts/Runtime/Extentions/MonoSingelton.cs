using UnityEngine;

namespace Runtime.Extentions
{
    public class MonoSingelton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance{
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if(_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
        protected void Awake()
        {
            _instance = this as T;
        }
    }
}