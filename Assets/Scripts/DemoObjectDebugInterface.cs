using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoObjectDebugInterface : MonoBehaviour
{
    public static DemoObjectDebugInterface Instance { get; private set; } = null;

    public List<DemoObject> AllDemoObjects { get; private set; } = new List<DemoObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"DemoObjectDebugInterface already exists. Destroying {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Register(DemoObject other)
    {
        AllDemoObjects.Add(other);
    }

    public void Deregister(DemoObject other)
    {
        AllDemoObjects.Remove(other);
    }
}
