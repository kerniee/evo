using System.Numerics;
using Arch.Core;
using Arch.System;
using Evo.Components;
using Evo.Systems;
using Raylib_cs;

namespace Evo;

internal static class Program
{
    private const float DistanceBetweenParts = 10f;

    private static readonly QueryDescription GameCloseQuery =
        new QueryDescription().WithAll<GameClose>();

    private static void CreateSnake(World world, Vector2 startPos)
    {
        var head = world.Create(
            new Position { Value = startPos },
            new Velocity { Value = new Vector2(1, 0) },
            new Head()
        );

        var previous = head;

        for (var i = 1; i < 10; i++)
        {
            var body = world.Create(
                new Position { Value = new Vector2(-i * DistanceBetweenParts, 0) },
                new Follow { Previous = previous, Distance = DistanceBetweenParts }
            );
            previous = body;
        }
    }

    private static bool IsRunning(World world)
    {
        return world.CountEntities(GameCloseQuery) <= 0;
    }

    public static void Main()
    {
        var world = World.Create();
        var systems = new Group<float>(
            "Systems",
            new InputSystem(world),
            new MovementSystem(world),
            new CameraSystem(world),
            new RenderSystem(world),
            new UiSystem(world),
            new DebugSystem(world)
        );

        world.CreateSingleton(new Camera2D(
            Vector2.Zero,
            new Vector2(),
            0,
            1
        ));
        systems.Initialize();

        CreateSnake(world, new Vector2(100, 100));

        while (IsRunning(world))
        {
            var dt = Raylib.GetFrameTime();
            systems.BeforeUpdate(in dt);
            systems.Update(in dt);
            systems.AfterUpdate(in dt);
        }

        systems.Dispose();
    }
}