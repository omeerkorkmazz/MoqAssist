using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moq;
using MoqAssist.Core.Exceptions;

// CREATED BY OMER KORKMAZ
// 14.02.2021 14:20

namespace MoqAssist.Core.Dictionary
{
    ///<summary>Dictionary Assistant to register mock objects once and use them in the unit tests</summary>
    public abstract class MoqAssistDictionary : IMockDictionary
    {
        private Dictionary<string, Lazy<object>> _mockObjectsDictionary { get; set; }

        #region Constructor
        protected MoqAssistDictionary()
        {
            _mockObjectsDictionary = new Dictionary<string, Lazy<object>>();
            RegisterMocks();
        }
        #endregion

        ///<summary>Provides to register mock objects into dictionary</summary>
        public abstract void RegisterMocks();

        #region Methods

        ///<summary>Registers a given T object as mocked into dictionary</summary>
        public void Register<T>(Mock<T> mockedObject) where T : class
        {
            if (IsMockExist<T>()) throw new MockObjectAlreadyRegisteredException($"{typeof(T).FullName} has already registered in the mock dictionary!");
            _mockObjectsDictionary.Add(typeof(T).FullName, new Lazy<object>(() => mockedObject, LazyThreadSafetyMode.PublicationOnly));
        }

        ///<summary>Checks whether a given T object as mocked exists in the dictionary or not</summary>
        ///<returns>Boolean</returns>
        public bool IsMockExist<T>() where T : class => _mockObjectsDictionary.Any(x => x.Key == typeof(T).FullName);
        internal bool IsMockExist(string key) => _mockObjectsDictionary.Any(x => x.Key == key);
        internal KeyValuePair<string, Lazy<object>> GetByKey(string key)
        {
            if (!IsMockExist(key)) throw new MockObjectNotFoundException($"{key} could not found in the mock dictionary!");
            return _mockObjectsDictionary.FirstOrDefault(x => x.Key == key);
        }

        #endregion

    }
}