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
        pos.Value += vel.Value * deltaTime * Speed;
    }

    [Query]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void FollowEntity([Data] in float deltaTime, ref Position pos, ref Follow follow)
    {
        if (!World.TryGet(follow.Previous, out Position previousPos)) return;
        var p0 = previousPos.Value; // Leader's position
        var p1 = pos.Value; // Current part's position
        var v = p0 - p1; // Vector from current to leader
        var dist = v.Length();

        if (!(dist > 1f)) return; // Avoid division by zero or tiny vectors
        var direction = v / dist;
        var target = p0 - direction * follow.Distance;
        var delta = (target - p1) * Stiffness * deltaTime;
        pos.Value += delta;
    }
}