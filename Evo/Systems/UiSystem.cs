using Arch.Core;
using Arch.System;
using Evo.Extends;
using Raylib_cs;

namespace Evo.Systems;

public class UiSystem(World world) : BaseSystem<World, float>(world)
{
    public override void BeforeUpdate(in float t)
    {
        base.BeforeUpdate(in t);
        RaylibExt.DrawTextXCentered("Snaky Snake", 12, 20, Color.Black);
        Raylib.DrawFPS(Raylib.GetRenderWidth() - 100, 10);
    }
}