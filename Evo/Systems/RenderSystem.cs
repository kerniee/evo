using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.System;
using Evo.Components;
using Evo.Extends;
using Raylib_cs;

namespace Evo.Systems;

public partial class RenderSystem(World world) : BaseSystem<World, float>(world)
{
    private readonly Color _bodyColor = new(10, 10, 50, 50);

    [Query]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DrawEntities(ref Position pos)
    {
        var x = (int)pos.Value.X;
        var y = (int)pos.Value.Y;
        Raylib.DrawCircle(x, y, 10, _bodyColor);
        Raylib.DrawCircleLines(x, y, 11, Color.Black);
    }

    public override void Initialize()
    {
        base.Initialize();
        Raylib.InitWindow(800, 600, "Snake Game");
        Raylib.SetTargetFPS(60);
    }

    public override void BeforeUpdate(in float t)
    {
        base.BeforeUpdate(in t);
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        RaylibExt.DrawTextXCentered("Snaky Snake", 12, 20, Color.Black);
    }

    public override void AfterUpdate(in float t)
    {
        base.AfterUpdate(in t);
        Raylib.EndDrawing();
    }

    public override void Dispose()
    {
        base.Dispose();
        Raylib.CloseWindow();
    }
}