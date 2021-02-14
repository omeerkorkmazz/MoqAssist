using System.Collections.Generic;
using System.Linq;
using Moq;
using MoqAssist.Core.Exceptions;

// CREATED BY OMER KORKMAZ
// 14.02.2021 14:20

namespace MoqAssist.Core.Dictionary
{
    ///<summary>Dictionary Assistant to register mock objects once and use them in the unit tests</summary>
    public abstract class MoqAssistDictionary : IMockDictionary
    {
        private Dictionary<string, object> _mockObjectsDictionary { get; set; }

        #region Constructor
        protected MoqAssistDictionary()
        {
            _mockObjectsDictionary = new Dictionary<string, object>();
            RegisterMocks();
        }
        #endregion

        ///<summary>Provides to register mock objects into dictionary</summary>
        public abstract void RegisterMocks();

        #region Methods

        ///<summary>Registers a given T object as mocked into dictionary</summary>
        public void Register<T>(Mock<T> mockedObject) where T : class => _mockObjectsDictionary.Add(typeof(T).FullName, mockedObject);
        
        ///<summary>Checks whether a given T object as mocked exists in the dictionary or not</summary>
        ///<returns>Boolean</returns>
        public bool IsMockExist<T>() where T : class => _mockObjectsDictionary.Any(x => x.Key == typeof(T).FullName);
        internal bool IsMockExist(string key) => _mockObjectsDictionary.Any(x => x.Key == key);

        ///<summary>Gets a given T object as mocked by checking from the dictionary</summary>
        ///<exception cref="MockObjectNotFoundException">Thrown when mocked object not found in the dictionary.</exception>
        ///<returns>Key-Value Pair (Key = Full Name of T, Value = Mock of T)</returns>
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