using System.Collections.Generic;
using System.Linq;
using Moq;
using MoqAssist.Core.Exceptions;

namespace MoqAssist.Core.Dictionary
{
    public abstract class MoqAssistDictionary : IMockDictionary
    {
        private Dictionary<string, object> _mockObjectsDictionary { get; set; }

        #region Constructor
        protected MoqAssistDictionary()
        {
            _mockObjectsDictionary = new Dictionary<string, object>();
            RegisterMockObjects();
        }
        #endregion

        public abstract void RegisterMockObjects();

        #region Methods
        public void AddToDictionary<T>(Mock<T> mockedObject) where T : class => _mockObjectsDictionary.Add(typeof(T).FullName, mockedObject);
        public bool IsMockExist<T>() where T : class => _mockObjectsDictionary.Any(x => x.Key == typeof(T).FullName);
        internal bool IsMockExist(string key) => _mockObjectsDictionary.Any(x => x.Key == key);
        public KeyValuePair<string, object> GetMockPair<T>() where T : class
        {
            var fullName = typeof(T).FullName;
            if (!IsMockExist<T>()) throw new MockObjectNotFoundException($"{fullName} could not found in the mock dictionary!");
            return _mockObjectsDictionary.FirstOrDefault(x => x.Key == fullName);
        }
        internal KeyValuePair<string, object> GetByKey(string key)
        {
            if (!IsMockExist(key)) throw new MockObjectNotFoundException($"{key} could not found in the mock dictionary!");
            return _mockObjectsDictionary.FirstOrDefault(x => x.Key == key);
        }

        #endregion

    }
}