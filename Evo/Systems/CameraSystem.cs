using System.Numerics;
using Arch.Core;
using Arch.System;
using Arch.System.SourceGenerator;
using Evo.Components;
using Raylib_cs;

namespace Evo.Systems;

public partial class CameraSystem(World world) : BaseSystem<World, float>(world)
{
    [Query]
    [All<Head, Position>]
    private void HeadMovement(ref Position pos)
    {
        ref var camera = ref World.GetSingleton<Camera2D>();

        camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2f, Raylib.GetScreenHeight() / 2f);
        camera.Target = pos.V;
    }
}