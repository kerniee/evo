using System.Numerics;
using Arch.Core;
using Arch.System;
using Arch.System.SourceGenerator;
using Evo.Components;
using Raylib_cs;

namespace Evo.Systems;

public partial class InputSystem(World world) : BaseSystem<World, float>(world)
{
    [Query]
    private void HandleGameClose()
    {
        if (!Raylib.WindowShouldClose()) return;
        var gameCloseQuery = new QueryDescription().WithAll<GameClose>();
        if (World.CountEntities(gameCloseQuery) > 0) return;
        World.Create<GameClose>();
    }

    [Query]
    [All<Head, Position, Velocity>]
    private void HeadMovement(ref Position pos, ref Velocity vel)
    {
        ref var camera = ref World.GetSingleton<Camera2D>();
        var mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), camera);
        var direction = mousePos - pos.V;

        if (!(direction.Length() > 1f))
        {
            vel.V = Vector2.Zero;
            return;
        }

        vel.V = direction / direction.Length();

        if (Raylib.IsMouseButtonDown(MouseButton.Left)) vel.V *= 3;
    }
}