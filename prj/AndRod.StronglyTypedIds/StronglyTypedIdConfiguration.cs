using System.Reflection;

namespace AndRod.StronglyTypedIds;

public sealed class StronglyTypedIdConfiguration
{
    public StronglyTypedIdConfiguration()
    {
    }

    private readonly HashSet<Assembly> _configuredAssemblies = [];

    private readonly HashSet<Type> _types = [];
    /// <summary>
    /// Gets all registered strongly-typed ID types that implements the <see cref="StronglyTypedId{TSelf, TValue}" /> abstract class.
    /// </summary>
    public IReadOnlyCollection<Type> Types => _types;

    private readonly Dictionary<Type, (Type ValueType, object DefaultValue)> _typeMap = [];
    private bool _build;

    /// <summary>
    /// Gets the type map of strongly-typed IDs, mapping each strongly-typed ID type to its value type and default value.
    /// </summary>
    public IReadOnlyDictionary<Type, (Type ValueType, object DefaultValue)> TypeMap => _typeMap;

    private StronglyTypedIdConfiguration Add(Assembly? assembly)
    {
        if (assembly is null)
        {
            return this;
        }

        _configuredAssemblies.Add(assembly);
        return this;
    }

    private StronglyTypedIdConfiguration Add(Type type) => Add(Assembly.GetAssembly(type));

    public StronglyTypedIdConfiguration Add<T>() where T : IStronglyTypedId => Add(typeof(T));

    public void Build()
    {
        if (_build)
        {
            return;
        }

        _build = true;

        // Create the cache of strongly-typed ID value types including their default values
        foreach (var stronglyTypedIdType in GetAllStronglyTypedIds())
        {
            _types.Add(stronglyTypedIdType);
        }

        var stronglyTypedIdValueTypes = GetStronglyTypedIdValueTypes();

        foreach (var (stronglyTypedIdType, valueType) in stronglyTypedIdValueTypes)
        {
            _typeMap[stronglyTypedIdType] = (valueType, getDefaultValue(valueType));
        }

        static object getDefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type)! : default!;
    }

    private IEnumerable<Type> GetAllStronglyTypedIds()
    {
        return _configuredAssemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IStronglyTypedId).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
    }

    private IEnumerable<(Type StronglyTypedIdType, Type ValueType)> GetStronglyTypedIdValueTypes()
    {
        return GetAllStronglyTypedIds()
            .Select(t => (
                StronglyTypedIdType: t.BaseType!.GetGenericArguments()[0],
                ValueType: t.BaseType!.GetGenericArguments()[1]
            ));
    }

}
