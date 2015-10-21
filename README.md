# A way for performing suite of tests for equality in declarative manner

[![Build status](https://ci.appveyor.com/api/projects/status/34cbe6bp2k33yond?svg=true)](https://ci.appveyor.com/project/baks/equalitytests)

### TODO list

- [x] `Equals` and `GetHashCode` method are overridden [EqualsOverrideAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/EqualsOverrideAssertion.cs) and [GetHashCodeOverrideAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/GetHashCodeOverrideAssertion.cs)
- [ ] Check whether implementation of equality works according to the comparison logic
- [x] `==` and `!=` operators are overloaded [EqualityOperatorOverloadAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/EqualityOperatorOverloadAssertion.cs) and [InequalityOperatorOverloadAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/InequalityOperatorOverloadAssertion.cs)
- [ ] `==` and `!=` operators produces correct results
- [x] `IEquatable<T>` is implemented [IEquatableImplementedAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/IEquatableImplementedAssertion.cs)
- [ ] Using `IEquatable<T>` produces correct results
- [x] Check if implementation is reflexive (**thanks to [EqualsSelfAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsSelfAssertion.cs) class from** `AutoFixture.Idioms`)
- [x] Check if implementation is symmetric [EqualsSymmetricAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/EqualsSymmetricAssertion.cs)
- [x] Check if implementation is transitive [EqualsTransitiveAssertion](https://github.com/baks/EqualityTests/blob/master/EqualityTests/EqualsTransitiveAssertion.cs)
- [x] Check if implementation is consistent (**thanks to [EqualsSuccessiveAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsSuccessiveAssertion.cs) class from** `AutoFixture.Idioms`)
- [x] Check if returns false when compare to null (**thanks to [EqualsNullAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsNullAssertion.cs) class from** `AutoFixture.Idioms`)
- [x] `GetHashCode` implementation is consistent (**thanks to [GetHashCodeSuccessiveAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/GetHashCodeSuccessiveAssertion.cs) class from** `AutoFixture.Idioms`)