using Raylib_cs;

namespace Evo.Extends;

public static class RaylibExt
{
    public static void DrawTextXCentered(string text, int posY, int fontSize, Color color)
    {
        var posX = (Raylib.GetScreenWidth() - Raylib.MeasureText(text, fontSize)) / 2;
        Raylib.DrawText(text, posX, posY, fontSize, color);
    }
}