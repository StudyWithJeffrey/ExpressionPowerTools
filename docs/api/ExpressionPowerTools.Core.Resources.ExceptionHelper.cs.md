﻿# ExceptionHelper Class

[Index](../index.md) > [ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Resources](ExpressionPowerTools.Core.Resources.n.md) > **ExceptionHelper**

Helper for localized exceptions.

```csharp
public static class ExceptionHelper
```

Inheritance [Object](https://docs.microsoft.com/dotnet/api/system.object) → **ExceptionHelper**

## Constructors

| Ctor | Description |
| :-- | :-- |
| [static ExceptionHelper()](ExpressionPowerTools.Core.Resources.ExceptionHelper.ctor.md#static-exceptionhelper) | Initializes a new instance of the [ExceptionHelper](ExpressionPowerTools.Core.Resources.ExceptionHelper.cs.md) class. |
## Methods

| Method | Description |
| :-- | :-- |
| [InvalidOperationException AsInvalidOperationException(String message, String[] parameters)](ExpressionPowerTools.Core.Resources.ExceptionHelper.AsInvalidOperationException.m.md) | Invalid operation messages. |
| [ArgumentException MethodCallOnTypeRequiredException(String parameterName)](ExpressionPowerTools.Core.Resources.ExceptionHelper.MethodCallOnTypeRequiredException.m.md) | Generates a [ArgumentException](https://docs.microsoft.com/dotnet/api/system.argumentexception) for invalid expression. |
| [NullReferenceException NullReferenceNotAllowedException(String memberName)](ExpressionPowerTools.Core.Resources.ExceptionHelper.NullReferenceNotAllowedException.m.md) | Null reference exception. |
| [ArgumentException WhitespaceNotAllowedException(String parameterName)](ExpressionPowerTools.Core.Resources.ExceptionHelper.WhitespaceNotAllowedException.m.md) | Generates a [ArgumentException](https://docs.microsoft.com/dotnet/api/system.argumentexception) for empty string. |

---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
