using System.Numerics;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.System;
using Evo.Components;

namespace Evo.Systems;

public partial class MovementSystem(World world) : BaseSystem<World, float>(world)
{
    private const float Stiffness = 10f;
    private const float Speed = 80f;

    [Query]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void MoveEntity([Data] in float deltaTime, ref Position pos, ref Velocity vel)
    {
        pos.V += vel.V * deltaTime * Speed;
    }

    [Query]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void FollowEntity([Data] in float deltaTime, ref Position pos, ref Follow follow)
    {
        if (!World.TryGet(follow.Previous, out Position previousPos)) return;
        var v = previousPos.V - pos.V; // Vector from current to leader
        var dist = v.Length();

        if (!(dist > 1f)) return; // Avoid division by zero or tiny vectors
        var direction = v / dist;
        var target = previousPos.V - direction * follow.Distance;
        var delta = (target - pos.V) * Stiffness * deltaTime;
        pos.V += delta;
    }
}