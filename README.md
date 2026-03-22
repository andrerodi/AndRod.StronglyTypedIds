# AndRod.StronglyTypedIds

A small, lightweight .NET library that provides a simple pattern for creating strongly-typed IDs. Instead of passing around raw primitive values (like `int`, `Guid`, etc.), define small wrapper types that are semantically meaningful and type-safe while still being comparable, equatable, and easy to use.

This project contains:
- `IStronglyTypedId<T>` and `IStronglyTypedId` — the interfaces that describe strongly-typed IDs.
- `StronglyTypedId<TSelf, TValue>` — an abstract base class that implements equality, comparison and common behavior.
- `StronglyTypedIdFactory` — a runtime factory that can create instances (and empty/default instances) of registered strongly-typed ID classes.

Why use strongly-typed IDs?
- Prevent mixing different ID types accidentally (for example, passing a `ProductId` where a `UserId` is expected).
- Improve code readability and maintainability.
- Preserve performance by wrapping value types (no extra heap allocations when using struct value types as the underlying ID).

## Key concepts

- `TValue` is the underlying primitive or value type (e.g. `int`, `Guid`, or even a compound struct). It must be a `struct` and implement `IEquatable<TValue>` and `IComparable<TValue>`.
- `TSelf` is the concrete strongly-typed ID type itself (used to preserve the strongly-typed identity).
- Instances are comparable and equatable based on the underlying `Value`.
- An ID is considered transient when its `Value` equals the default for `TValue` (useful for DB-generated IDs).

## How to define a strongly-typed ID

Create a small class that inherits from `StronglyTypedId<TSelf, TValue>` and forwards the value to the base constructor. Example (using the project's style):

- Define an integer-based ID:
`public sealed class UserId(int value) : StronglyTypedId<UserId, int>(value) { }`

- Define a `Guid`-based ID:
`public sealed class OrderId(Guid value) : StronglyTypedId<OrderId, Guid>(value) { }`

- Define a compound-value ID (value is a struct):
`public readonly struct Compound(int id1, int id2) : IEquatable<Compound>, IComparable<Compound> { ... }`  
`public sealed class CompoundId(Compound value) : StronglyTypedId<CompoundId, Compound>(value) { }`

Note: The code in this repository uses newer C# concise type/primary-constructor syntax. You can also use the more classic C# class syntax if preferred.

## Usage examples

- Create directly:
`var id = new UserId(42);`

- Create via factory (useful when you only have `Type` at runtime or want central creation):
`var id = StronglyTypedIdFactory.Create<UserId>(42);`  
`var idByType = StronglyTypedIdFactory.Create(typeof(UserId), 42);`

- Create an empty/default instance:
`var empty = StronglyTypedIdFactory.Empty<UserId>(); // Value == default(int)`

- Equality & comparison:
`var a = new UserId(1);`  
`var b = new UserId(2);`  
`bool equal = (a == b);`  
`int cmp = a.CompareTo(b);`

- Boxed access (useful for generic code):
`IStronglyTypedId boxed = a;`  
`object raw = boxed.Value;`

## Building and running tests

Requires the .NET SDK appropriate for the project (see `TargetFramework` in the `.csproj` files).

From the repository root:

- Build:
`dotnet build`

- Run tests:
`dotnet test`

These commands will build the library and run the included MSTest unit tests which exercise the `StronglyTypedIdFactory` and example strongly-typed IDs.

## Notes and limitations

- The factory auto-discovers strongly-typed ID classes by scanning loaded assemblies. If your type is not visible to the AppDomain at factory initialization, the factory may not know the type. In that case, direct construction or ensuring the assembly is loaded before factory use will help.
- `TValue` must be a value type that implements `IEquatable<TValue>` and `IComparable<TValue>` — the base class enforces these constraints.