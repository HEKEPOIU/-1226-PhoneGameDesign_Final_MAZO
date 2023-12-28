using System;
using UnityEngine;

namespace Singleton
{
    
    /// <summary>
    /// A static singleton class. it don't protect from multiple instantiation from the same scene.
    /// it's will be destroyed when the scene is unloaded.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StaticSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;
        protected virtual void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
    
    
    /// <summary>
    /// A singleton class. it protect from multiple instantiation from the same scene.
    /// it's will be destroyed when the scene is unloaded.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : StaticSingleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            base.Awake();
        }
    }
    
    /// <summary>
    /// it's won't be destroyed when the scene is unloaded.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }

}
