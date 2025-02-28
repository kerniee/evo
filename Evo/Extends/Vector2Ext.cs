using System.Numerics;

namespace Evo.Extends;

public static class Vector2Ext
{
    public static Vector2 Norm(this Vector2 v)
    {
        if (v.Length() <= 0.00000001f) return Vector2.Zero;
        return v / v.Length();
    }

    public static Vector2 ToClosestInt(this Vector2 v)
    {
        return new Vector2((int)Math.Round(v.X), (int)Math.Round(v.Y));
    }
}