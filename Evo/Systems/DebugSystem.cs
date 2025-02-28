using System.Text;
using Arch.Core;
using Arch.System;
using Raylib_cs;

namespace Evo.Systems;

public class DebugSystem(World world) : BaseSystem<World, float>(world)
{
    public override void Update(in float t)
    {
        base.Update(in t);
        var s = new StringBuilder();
        foreach (var arch in World.Archetypes)
        {
            var types = string.Join(",", arch.Types.Select(p => p.Type.Name).ToArray());
            s.Append($"Types: {types}.  Entities: {arch.EntityCount}\n");
        }

        Raylib.DrawText(s.ToString(), 10, 10, 10, Color.Black);
    }
}