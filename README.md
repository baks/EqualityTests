# A way for performing suite of tests for equality in declarative manner

[![Build status](https://ci.appveyor.com/api/projects/status/34cbe6bp2k33yond?svg=true)](https://ci.appveyor.com/project/baks/equalitytests)

You can find more information [here](http://baks.github.io/2015/10/26/testing-for-equality).

### Equality Test assertions

- [x] `Equals` and `GetHashCode` method are overridden [EqualsOverrideAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/EqualsOverrideAssertion.cs) and [GetHashCodeOverrideAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/GetHashCodeOverrideAssertion.cs)
- [x] Check if implementation is reflexive (**[EqualsSelfAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsSelfAssertion.cs) class from** `AutoFixture.Idioms`)
- [x] Check if implementation is symmetric [EqualsSymmetricAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/EqualsSymmetricAssertion.cs)
- [x] Check if implementation is transitive [EqualsTransitiveAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/EqualsTransitiveAssertion.cs)
- [x] Check if implementation is consistent (**[EqualsSuccessiveAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsSuccessiveAssertion.cs) class from** `AutoFixture.Idioms`)
- [x] Check if returns false when compare to null (**[EqualsNullAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsNullAssertion.cs) class from** `AutoFixture.Idioms`)
- [x] Check whether implementation of equality works according to the comparison logic [EqualsValueCheckAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/EqualsValueCheckAssertion.cs)
- [x] `GetHashCode` implementation produces correct results [GetHashCodeValueCheckAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/GetHashCodeValueCheckAssertion.cs)
- [x] `GetHashCode` implementation is consistent (**[GetHashCodeSuccessiveAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/GetHashCodeSuccessiveAssertion.cs) class from** `AutoFixture.Idioms`)
- [x] `==` and `!=` operators are overloaded [EqualityOperatorOverloadAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/EqualityOperatorOverloadAssertion.cs) and [InequalityOperatorOverloadAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/InequalityOperatorOverloadAssertion.cs)
- [x] `==` and `!=` operators produces correct results [EqualityOperatorValueCheckAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/EqualityOperatorValueCheckAssertion.cs) and [InequalityOperatorValueCheckAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/InequalityOperatorValueCheckAssertion.cs)
- [x] `IEquatable<T>` is implemented [IEquatableImplementedAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/IEquatableImplementedAssertion.cs)
- [x] Using `IEquatable<T>` produces correct results [IEquatableValueCheckAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/Assertions/IEquatableValueCheckAssertion.cs)
