﻿# TypeBase Class

[Index](../index.md) > [ExpressionPowerTools.Serialization](ExpressionPowerTools.Serialization.a.md) > [ExpressionPowerTools.Serialization.Serializers](ExpressionPowerTools.Serialization.Serializers.n.md) > **TypeBase**

Simple class to represent type information. Never serialized, but
            used for key calculation.

```csharp
public class TypeBase : MemberBase
```

Inheritance [Object](https://docs.microsoft.com/dotnet/api/system.object) → [MemberBase](ExpressionPowerTools.Serialization.Serializers.MemberBase.cs.md) → **TypeBase**

## Constructors

| Ctor | Description |
| :-- | :-- |
| [TypeBase(Type type)](ExpressionPowerTools.Serialization.Serializers.TypeBase.ctor.md#typebasetype-type) | Initializes a new instance of the [TypeBase](ExpressionPowerTools.Serialization.Serializers.TypeBase.cs.md) class initialized            with a type. |
## Properties

| Property | Type | Description |
| :-- | :-- | :-- |
| [`MemberType`](ExpressionPowerTools.Serialization.Serializers.TypeBase.MemberType.prop.md) | [Int32](https://docs.microsoft.com/dotnet/api/system.int32) | Gets or sets the [MemberTypes](https://docs.microsoft.com/dotnet/api/system.reflection.membertypes) value. |

## Methods

| Method | Description |
| :-- | :-- |
| [String CalculateKey()](ExpressionPowerTools.Serialization.Serializers.TypeBase.CalculateKey.m.md) | Calculates the unique key. |

---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 9/13/2020 7:35:36 AM | (c) Copyright 2020 Jeremy Likness. | 0.8.8-alpha |