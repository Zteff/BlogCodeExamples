using FakeItEasy;
using NUnit.Framework;

namespace UnitTest.ValidateArgumentToDependency
{
    [TestFixture]
    public class FooTests
    {
        private Foo _sut;
        private IBar _fakeBar;

        [SetUp]
        public void SetUp()
        {
            _fakeBar = A.Fake<IBar>();
            _sut = new Foo(_fakeBar);
        }

        [Test]
        public void MethodToTest_ValidInput_ValidateInputToDependency()
        {
            A.CallTo(() => _fakeBar.SomeMethod(1)).Returns(1);
            var expectedInputToDependency = 1;
            int actualInputToDependency = 0;
            A.CallTo(() => _fakeBar.SomeMethod(A<int>.Ignored)).Invokes((int i) => actualInputToDependency = i);
            var result = _sut.MethodToTest(1);;
            Assert.AreEqual(expectedInputToDependency, actualInputToDependency);
        }
    }

    public class Foo
    {
        private readonly IBar _bar;

        public Foo(IBar bar)
        {
            _bar = bar;
        }

        public int MethodToTest(int someArg)
        {
            var valueFromBar = _bar.SomeMethod(someArg);
            return valueFromBar + 1;
        }
    }

    public interface IBar
    {
        int SomeMethod(int someArg);
    }
}