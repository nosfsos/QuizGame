using UnityEngine;

public static class Extensions
{
    public static Vector3 Vector2ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y);
    }

    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
}