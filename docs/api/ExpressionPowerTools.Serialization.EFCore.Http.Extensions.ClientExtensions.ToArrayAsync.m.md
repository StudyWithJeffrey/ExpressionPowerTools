﻿# ClientExtensions.ToArrayAsync Method

[Index](../index.md) > [ExpressionPowerTools.Serialization.EFCore.Http](ExpressionPowerTools.Serialization.EFCore.Http.a.md) > [ExpressionPowerTools.Serialization.EFCore.Http.Extensions](ExpressionPowerTools.Serialization.EFCore.Http.Extensions.n.md) > [ClientExtensions](ExpressionPowerTools.Serialization.EFCore.Http.Extensions.ClientExtensions.cs.md) > **ToArrayAsync**

Grabs the array.

## Overloads

| Overload | Description |
| :-- | :-- |
| [ToArrayAsync&lt;T>(IRemoteQueryable&lt;T> query)](#toarrayasynctiremotequeryablet-query) | Grabs the array. |
## ToArrayAsync&lt;T>(IRemoteQueryable&lt;T> query)

Grabs the array.

```csharp
public static Task<T[]> ToArrayAsync<T>(IRemoteQueryable<T> query)
```

### Return Type

 [Task&lt;T[]>](https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1)  - The result.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `query` | [IRemoteQueryable&lt;T>](ExpressionPowerTools.Serialization.EFCore.Http.Signatures.IRemoteQueryable`1.i.md) | The query. |



---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 9/13/2020 7:35:36 AM | (c) Copyright 2020 Jeremy Likness. | 0.8.8-alpha |