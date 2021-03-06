﻿# ExpressionEvaluator.IsPartOf Method

[Index](../index.md) > [ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Comparisons](ExpressionPowerTools.Core.Comparisons.n.md) > [ExpressionEvaluator](ExpressionPowerTools.Core.Comparisons.ExpressionEvaluator.cs.md) > **IsPartOf**

Determines whether an [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) is part of another expression.

## Overloads

| Overload | Description |
| :-- | :-- |
| [IsPartOf(Expression source, Expression target)](#ispartofexpression-source-expression-target) | Determines whether an [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) is part of another expression. |
| [IsPartOf&lt;T>(IQueryable&lt;T> source, IQueryable&lt;T> target)](#ispartoftiqueryablet-source-iqueryablet-target) | Determines whether an [IQueryable&lt;out T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) is part of another query. |
## IsPartOf(Expression source, Expression target)

Determines whether an [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) is part of another expression.

```csharp
public virtual Boolean IsPartOf(Expression source, Expression target)
```

### Return Type

 [Boolean](https://docs.microsoft.com/dotnet/api/system.boolean)  - A flag indicating whether the source is part of the target.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `source` | [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) | The source [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) . |
| `target` | [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) | The target [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) to parse. |


## Remarks

A source is part of a target if an [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) exists in the
            target's tree that is similar to the source.

## IsPartOf&lt;T>(IQueryable&lt;T> source, IQueryable&lt;T> target)

Determines whether an [IQueryable&lt;out T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) is part of another query.

```csharp
public virtual Boolean IsPartOf<T>(IQueryable<T> source, IQueryable<T> target)
```

### Return Type

 [Boolean](https://docs.microsoft.com/dotnet/api/system.boolean)  - A flag indicating whether the source is part of the target.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `source` | [IQueryable&lt;T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) | The source [IQueryable&lt;out T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) . |
| `target` | [IQueryable&lt;T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) | The target [IQueryable&lt;out T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) to parse. |


## Remarks

A source is part of a target if an [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) exists in the
            target's tree that is similar to the source.


---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
