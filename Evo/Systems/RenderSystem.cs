using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.System;
using Arch.System.SourceGenerator;
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
        var camera = World.GetSingleton<Camera2D>();
        Raylib.BeginMode2D(camera);

        Raylib.DrawCircleV(pos.V, 10, _bodyColor);
        Raylib.DrawCircleLinesV(pos.V, 10, Color.Black);

        Raylib.EndMode2D();
    }

    [Query]
    [All<Position, Velocity, Head>]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DrawEntitiesHead(ref Position pos, ref Velocity vel)
    {
        var camera = World.GetSingleton<Camera2D>();
        Raylib.BeginMode2D(camera);

        var direction = vel.V.Norm() * 5;
        Raylib.DrawCircleV(pos.V + direction, 2, Color.Black);

        Raylib.EndMode2D();
    }

    public override void Initialize()
    {
        base.Initialize();
        Raylib.SetConfigFlags(ConfigFlags.UndecoratedWindow | ConfigFlags.HighDpiWindow);
        Raylib.InitWindow(800, 600, "Snake Game");
        // Raylib.SetTargetFPS(60);
    }

    public override void BeforeUpdate(in float t)
    {
        base.BeforeUpdate(in t);
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);
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