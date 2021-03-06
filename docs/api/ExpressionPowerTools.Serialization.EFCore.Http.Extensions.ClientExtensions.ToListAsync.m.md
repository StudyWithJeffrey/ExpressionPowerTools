﻿# ClientExtensions.ToListAsync Method

[Index](../index.md) > [ExpressionPowerTools.Serialization.EFCore.Http](ExpressionPowerTools.Serialization.EFCore.Http.a.md) > [ExpressionPowerTools.Serialization.EFCore.Http.Extensions](ExpressionPowerTools.Serialization.EFCore.Http.Extensions.n.md) > [ClientExtensions](ExpressionPowerTools.Serialization.EFCore.Http.Extensions.ClientExtensions.cs.md) > **ToListAsync**

Grabs the list.

## Overloads

| Overload | Description |
| :-- | :-- |
| [ToListAsync&lt;T>(IRemoteQueryable&lt;T> query)](#tolistasynctiremotequeryablet-query) | Grabs the list. |
## ToListAsync&lt;T>(IRemoteQueryable&lt;T> query)

Grabs the list.

```csharp
public static Task<IList<T>> ToListAsync<T>(IRemoteQueryable<T> query)
```

### Return Type

 [Task&lt;IList&lt;T>>](https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1)  - The result.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `query` | [IRemoteQueryable&lt;T>](ExpressionPowerTools.Serialization.EFCore.Http.Signatures.IRemoteQueryable`1.i.md) | The query. |



---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
