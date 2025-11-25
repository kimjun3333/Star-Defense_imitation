using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 매니저 클래스에 사용하는 제네릭 싱글톤 베이스 클래스.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = (T)FindAnyObjectByType(typeof(T));

                if(instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);

                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if(transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
