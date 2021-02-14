using System.Collections.Generic;
using Moq;

namespace MoqAssist.Core.Dictionary
{
    internal interface IMockDictionary
    {
        void AddToDictionary<T>(Mock<T> mockedObject) where T : class;
        bool IsMockExist<T>() where T : class;
        KeyValuePair<string, object> GetMockPair<T>() where T : class;
    }
}