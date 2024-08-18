using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ScaleFromHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    public AnimationCurve tweenCurve;
    public float tweenSpeed;
    public float scale;
    public bool useMaxScale = false;
    [ShowIf("useMaxScale")] public float maxScale = 15f;
    float baseScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        scale = baseScale * (healthSystem.currentHealth / healthSystem.maxHealth);
        if (useMaxScale ) { scale = Mathf.Clamp(scale, 0, maxScale); }

        StartCoroutine(Scale(gameObject, scale));
    }
    public IEnumerator Scale(GameObject objectToScale, float scale)
    {
        float t = 0;
        while (objectToScale.transform.localScale != new Vector3(scale, scale, 1f))
        {
            t += Time.deltaTime;
            float tween = tweenCurve.Evaluate(Mathf.Lerp(0, 1, t));
            objectToScale.transform.localScale = Vector3.Lerp(objectToScale.transform.localScale, new Vector3(scale, scale, 1f), tween);
            yield return null;
        }
    }
}
