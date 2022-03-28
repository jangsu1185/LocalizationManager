using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = (T)FindObjectOfType(typeof(T));
            if (_instance == null)
            {
                string objName = typeof(T).ToString();

                GameObject obj = GameObject.Find(objName);
                if (obj == null)
                {
                    obj = new GameObject();
                    obj.name = objName;
                }
                _instance = obj.AddComponent<T>();
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance != null)
        {
            Object[] obj = FindObjectsOfType(typeof(T));
            if (obj.Length > 1) 
            {
                Destroy(gameObject);
                return;
            }
        }
        DontDestroyOnLoad(this);
    }

    public virtual void OnApplicationQuit()
    {
        _instance = null;
    }
}
