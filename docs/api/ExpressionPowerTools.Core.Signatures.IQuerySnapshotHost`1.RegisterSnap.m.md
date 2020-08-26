﻿# IQuerySnapshotHost&lt;T>.RegisterSnap Method

[Index](../index.md) > [ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Signatures](ExpressionPowerTools.Core.Signatures.n.md) > [IQuerySnapshotHost<T>](ExpressionPowerTools.Core.Signatures.IQuerySnapshotHost`1.i.md) > **RegisterSnap**

Register a callback to receive the [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) when snapped.

## Overloads

| Overload | Description |
| :-- | :-- |
| [RegisterSnap(Action&lt;Expression> callback)](#registersnapactionexpression-callback) | Register a callback to receive the [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) when snapped. |
## RegisterSnap(Action&lt;Expression> callback)

Register a callback to receive the [Expression](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression) when snapped.

```csharp
public virtual String RegisterSnap(Action<Expression> callback)
```

### Return Type

 [String](https://docs.microsoft.com/dotnet/api/system.string)  - A unique identifier used to unregister.

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `callback` | [Action&lt;Expression>](https://docs.microsoft.com/dotnet/api/system.action-1) | The callback to use. |



---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 8/26/2020 6:58:17 PM | (c) Copyright 2020 Jeremy Likness. | 0.8.2-alpha |