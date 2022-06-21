using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleShip : MonoBehaviour
{
    public bool repeatable;
    public Vector3 minScale;
    public Vector3 maxScale;

    IEnumerator Start()
    {
        while (repeatable)
        {
            yield return RepeatLerp(minScale, maxScale, 1.4f);
            yield return RepeatLerp(maxScale, minScale, 1.4f);
        }
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * 2f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
