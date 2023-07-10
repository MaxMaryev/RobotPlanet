using UnityEngine;

public static class RandomUtils
{
    public static Vector3 RandomInCirclePlane(float minRadius, float maxRadius)
    {
        var radius = Random.Range(minRadius, maxRadius);
        var angle = Random.Range(0f, 2f * Mathf.PI);

        return new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
    }
}
