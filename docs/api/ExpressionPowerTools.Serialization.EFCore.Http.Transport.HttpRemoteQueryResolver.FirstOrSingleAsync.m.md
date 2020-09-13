﻿# HttpRemoteQueryResolver.FirstOrSingleAsync Method

[Index](../index.md) > [ExpressionPowerTools.Serialization.EFCore.Http](ExpressionPowerTools.Serialization.EFCore.Http.a.md) > [ExpressionPowerTools.Serialization.EFCore.Http.Transport](ExpressionPowerTools.Serialization.EFCore.Http.Transport.n.md) > [HttpRemoteQueryResolver](ExpressionPowerTools.Serialization.EFCore.Http.Transport.HttpRemoteQueryResolver.cs.md) > **FirstOrSingleAsync**

Execute the remote query and materialize the single item.

## Overloads

| Overload | Description |
| :-- | :-- |
| [FirstOrSingleAsync&lt;T>(IQueryable&lt;T> query)](#firstorsingleasynctiqueryablet-query) | Execute the remote query and materialize the single item. |
## FirstOrSingleAsync&lt;T>(IQueryable&lt;T> query)

Execute the remote query and materialize the single item.

```csharp
public virtual Task<T> FirstOrSingleAsync<T>(IQueryable<T> query)
```

### Return Type

 [Task&lt;T>](https://docs.microsoft.com/dotnet/api/system.threading.tasks.task-1)  - The result.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `query` | [IQueryable&lt;T>](https://docs.microsoft.com/dotnet/api/system.linq.iqueryable-1) | The base query to run. |



---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 9/13/2020 7:35:36 AM | (c) Copyright 2020 Jeremy Likness. | 0.8.8-alpha |