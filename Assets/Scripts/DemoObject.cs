using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoObject : MonoBehaviour
{
    [SerializeField] float BounceHeight = 1f;
    [SerializeField] float BounceInterval = 1f;
    [SerializeField] AnimationCurve BounceCurve;

    float BounceProgress;
    Vector3 StartPos;

#if UNITY_EDITOR
    public bool IsExpanded { get; set; } = false;
#endif

    // Start is called before the first frame update
    void Start()
    {
        BounceProgress = Random.Range(0f, 1f);
        StartPos = transform.position;

        if (DemoObjectDebugInterface.Instance != null)
            DemoObjectDebugInterface.Instance.Register(this);
    }

    private void OnDestroy()
    {
        if (DemoObjectDebugInterface.Instance != null)
            DemoObjectDebugInterface.Instance.Deregister(this);
    }

    // Update is called once per frame
    void Update()
    {
        BounceProgress = (BounceProgress + Time.deltaTime / BounceInterval) % 1f;

        transform.position = StartPos + Vector3.up * BounceHeight * BounceCurve.Evaluate(BounceProgress);
    }

#if UNITY_EDITOR
    public string GetDebugInfo()
    {
        return $"Position: {transform.position}{System.Environment.NewLine}Progress: {BounceProgress}";
    }

    public void GoSlower()
    {
        BounceInterval *= 1.5f;
    }

    public void GoFaster()
    {
        BounceInterval *= 0.5f;
    }
#endif
}
