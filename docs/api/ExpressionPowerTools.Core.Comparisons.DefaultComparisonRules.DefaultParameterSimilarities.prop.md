﻿# DefaultComparisonRules.DefaultParameterSimilarities Property

[Index](../index.md) > [ExpressionPowerTools.Core](ExpressionPowerTools.Core.a.md) > [ExpressionPowerTools.Core.Comparisons](ExpressionPowerTools.Core.Comparisons.n.md) > [DefaultComparisonRules](ExpressionPowerTools.Core.Comparisons.DefaultComparisonRules.cs.md) > **DefaultParameterSimilarities**

Gets the default rules for parameter similarities.

```csharp
public static Expression<Func<ParameterExpression, ParameterExpression, Boolean>> DefaultParameterSimilarities { get; }
```

## Remarks

The types must be similar.

### Property Value

 [Expression&lt;Func&lt;ParameterExpression, ParameterExpression, Boolean>>](https://docs.microsoft.com/dotnet/api/system.linq.expressions.expression-1) 


---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 09/21/2020 19:07:57 | (c) Copyright 2020 Jeremy Likness. | 0.9.2-alpha |
