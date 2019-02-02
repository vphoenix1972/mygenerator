using FluentAssertions;
using Moq;
using Xunit;

namespace <%= projectNamespace %>.Web.Tests
{
    public class ExampleTest
    {
        [Fact]
        public void Test()
        {
            var instance = new Example();

            var actual = instance.Method();

            actual.Should().BeTrue();
        }

        [Fact]
        public void TestMocked()
        {
            var instance = new Example();

            var mock = new Mock<IMocked>();

            instance.MethodMocked(mock.Object);

            mock.Verify(e => e.Do(), Times.Once);
        }
    }

    public sealed class Example
    {
        public bool Method()
        {
            return true;
        }

        public void MethodMocked(IMocked mocked)
        {
            mocked.Do();
        }
    }

    public interface IMocked
    {
        void Do();
    }
}
