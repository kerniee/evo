﻿global using Camera2D = Raylib_cs.Camera2D;
using System.Numerics;
using Arch.Core;

namespace Evo.Components;

public record struct GameClose;

public record struct Head;

public record struct Position
{
    public Vector2 V;
}

public record struct Velocity
{
    public Vector2 V;
}

public record struct Follow
{
    public float Distance;
    public Entity Previous;
}