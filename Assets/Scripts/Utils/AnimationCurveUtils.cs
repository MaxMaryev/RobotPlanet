using UnityEngine;

public static class AnimationCurveUtils
{
    public static void Normalize(ref AnimationCurve curve)
    {
        if (curve.length <= 0)
            return;

        var keys = curve.keys;
        for (int i = 0; i < keys.Length; i++)
        {
            var time = keys[i].time;
            var value = keys[i].value;

            if (time < 0f || time > 1f)
                time = Mathf.Clamp01(time);
            if (value < 0f || value > 1f)
                value = Mathf.Clamp01(value);

            keys[i].time = time;
            keys[i].value = value;
        }

        curve.keys = keys;
    }
}