namespace EqualityTests
{
    public static class EqualityTestsConfigurer<T> where T : class
    {
        public static EqualityTestsConfiguration<T> Instance(T instance)
        {
            return new EqualityTestsConfiguration<T>(instance);
        }
    }
}