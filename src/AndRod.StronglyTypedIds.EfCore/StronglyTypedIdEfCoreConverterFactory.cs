using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AndRod.StronglyTypedIds.EfCore;

/// <summary>
/// A factory class for creating Entity Framework Core value converters for strongly-typed IDs.
/// </summary>
public static class StronglyTypedIdEfCoreConverterFactory
{
    public static ValueConverter Create<TStronglyTypedId>()
        where TStronglyTypedId : IStronglyTypedId
    {
        var stronglyTypedIdType = typeof(TStronglyTypedId);
        var valueType = StronglyTypedIdFactory.GetValueType<TStronglyTypedId>();

        var converterType = typeof(StronglyTypedIdEfCoreConverter<,>).MakeGenericType(stronglyTypedIdType, valueType);

        return (ValueConverter)Activator.CreateInstance(converterType)!;
    }
}