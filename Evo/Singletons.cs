using Arch.Core;

namespace Evo;

public static class Singletons
{
    private static readonly Dictionary<World, Dictionary<Type, Entity>> WorldSingletons = new();

    public static ref T CreateSingleton<T>(this World world, in T component = default)
    {
        if (!WorldSingletons.TryGetValue(world, out var singletons))
        {
            singletons = new Dictionary<Type, Entity>();
            WorldSingletons.Add(world, singletons);
        }

        var type = typeof(T);

        if (singletons.TryGetValue(type, out var existingEntity))
        {
            world.Set(existingEntity, component);
            return ref world.Get<T>(existingEntity);
        }

        var entity = world.Create(component);
        singletons.Add(type, entity);
        return ref world.Get<T>(entity);
    }

    public static ref T GetSingleton<T>(this World world)
    {
        var type = typeof(T);
        if (!WorldSingletons.TryGetValue(world, out var singletons))
            throw new Exception($"No singleton of type {type} found in world.");

        if (!singletons.TryGetValue(type, out var singleton))
            throw new Exception($"No singleton of type {type} found in world.");

        return ref world.Get<T>(singleton);
    }
}