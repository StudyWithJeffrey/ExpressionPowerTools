﻿# RulesEngine.RuleForField Method

[Index](../index.md) > [ExpressionPowerTools.Serialization](ExpressionPowerTools.Serialization.a.md) > [ExpressionPowerTools.Serialization.Rules](ExpressionPowerTools.Serialization.Rules.n.md) > [RulesEngine](ExpressionPowerTools.Serialization.Rules.RulesEngine.cs.md) > **RuleForField**

Sets up a field rule.

## Overloads

| Overload | Description |
| :-- | :-- |
| [RuleForField(Action&lt;MemberSelector&lt;FieldInfo>> selector)](#ruleforfieldactionmemberselectorfieldinfo-selector) | Sets up a field rule. |
## RuleForField(Action&lt;MemberSelector&lt;FieldInfo>> selector)

Sets up a field rule.

```csharp
public virtual IRulesConfiguration RuleForField(Action<MemberSelector<FieldInfo>> selector)
```

### Return Type

 [IRulesConfiguration](ExpressionPowerTools.Serialization.Signatures.IRulesConfiguration.i.md)  - The [IRulesConfiguration](ExpressionPowerTools.Serialization.Signatures.IRulesConfiguration.i.md) .

### Parameters

| Parameter | Type | Description |
| :-- | :-- | :-- |
| `selector` | [Action&lt;MemberSelector&lt;FieldInfo>>](https://docs.microsoft.com/dotnet/api/system.action-1) | The selector. |



---

| Generated | Copyright | Version |
| :-- | :-: | --: |
| 9/13/2020 7:35:36 AM | (c) Copyright 2020 Jeremy Likness. | 0.8.8-alpha |