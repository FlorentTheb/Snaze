public static class ServiceLocator
{
    private static Dictionary<Type, object> Services = new();

    public static void Register<T>(T service)
    {
        if (Services.ContainsKey(typeof(T)))
            throw new ArgumentException($"Cannot register {typeof(T)} service. This service is already registered");
        Services[typeof(T)] = service!;
    }

    public static T GetService<T>()
    {
        if (!Services.ContainsKey(typeof(T)))
            throw new ArgumentException($"{typeof(T)} service is not found. Make sure to register before uses.");
        return (T)Services[typeof(T)];
    }
}
