public static class RvoHelper
{
    public static UnityEngine.Vector2 ToUnityVector2(this RVO.Vector2 vector2)
    {
        return new UnityEngine.Vector2(vector2.x(), vector2.y());
    }

    public static RVO.Vector2 ToRvoVector2(this UnityEngine.Vector2 vector2)
    {
        return new RVO.Vector2(vector2.x, vector2.y);
    }

    public static RVO.Vector2 ToRvoVector2(this UnityEngine.Vector3 vector3)
    {
        return new RVO.Vector2(vector3.x, vector3.y);
    }
}