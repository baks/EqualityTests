# A way for performing suite of tests for equality in declarative manner

[![Build status](https://ci.appveyor.com/api/projects/status/34cbe6bp2k33yond?svg=true)](https://ci.appveyor.com/project/baks/equalitytests)

### TODO list

- [ ] Check whether implementation of equality works according to the comparison logic<br/>
- [ ] `==` and `!=` operators are overloaded <br/>
- [ ] `==` and `!=` operators produces correct results<br/>
- [ ] Using `IEquatable<T>` produces correct results<br/>
- [x] Check if implementation is reflexive **thanks to [EqualsSelfAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsSelfAssertion.cs) class from** `AutoFixture.Idioms`<br/>
- [x] Check if implementation is symmetric<br/>
- [x] Check if implementation is transitive<br/>
- [x] Check if implementation is consistent **thanks to [EqualsSuccessiveAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsSuccessiveAssertion.cs) class from** `AutoFixture.Idioms`<br/>
- [x] Check if returns false when compare to null~~ **thanks to [EqualsNullAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/EqualsNullAssertion.cs) class from** `AutoFixture.Idioms`<br/>
- [x] `GetHashCode` implementation is consistent~~ **thanks to [GetHashCodeSuccessiveAssertion](https://github.com/AutoFixture/AutoFixture/blob/master/Src/Idioms/GetHashCodeSuccessiveAssertion.cs) class from** `AutoFixture.Idioms`<br/>