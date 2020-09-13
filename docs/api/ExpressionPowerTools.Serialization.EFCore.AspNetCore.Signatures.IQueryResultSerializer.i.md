﻿# IQueryResultSerializer Interface

[Index](../index.md) > [ExpressionPowerTools.Serialization.EFCore.AspNetCore](ExpressionPowerTools.Serialization.EFCore.AspNetCore.a.md) > [ExpressionPowerTools.Serialization.EFCore.AspNetCore.Signatures](ExpressionPowerTools.Serialization.EFCore.AspNetCore.Signatures.n.md) > **IQueryResultSerializer**

Executes the query and serializes the result.

```csharp
public interface IQueryResultSerializer
```

Derived  [QueryResultSerializer](ExpressionPowerTools.Serialization.EFCore.AspNetCore.Middleware.QueryResultSerializer.cs.md) 

## Methods

| Method | Description |
| :-- | :-- |
| [Task SerializeAsync(Stream response, IQueryable query, PayloadType type)](ExpressionPowerTools.Serialization.EFCore.AspNetCore.Signatures.IQueryResultSerializer.SerializeAsync.m.md) | Serializes the query result. |

---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 9/13/2020 7:35:36 AM | (c) Copyright 2020 Jeremy Likness. | 0.8.8-alpha |