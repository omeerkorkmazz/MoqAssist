using Moq;

namespace MoqAssist.Core.Dictionary
{
    internal interface IMockDictionary
    {
        void Register<T>(Mock<T> mockedObject) where T : class;
        bool IsMockExist<T>() where T : class;
    }
}