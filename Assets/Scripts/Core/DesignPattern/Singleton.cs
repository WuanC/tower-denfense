using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected bool canDontDestroyOnLoad = true;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                //if (instance == null)
                //{
                //    GameObject singletonObject = new GameObject(typeof(T).Name);
                //    instance = singletonObject.AddComponent<T>();
                //}
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this as T;

            if (canDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
