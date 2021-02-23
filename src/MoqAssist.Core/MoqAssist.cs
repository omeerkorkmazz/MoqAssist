using System;
using System.Linq;
using System.Collections.Generic;
using Moq;
using MoqAssist.Core.Dictionary;
using MoqAssist.Core.Exceptions;

// CREATED BY OMER KORKMAZ
// 14.02.2021 14:20

namespace MoqAssist.Core
{
    ///<summary>A lightweight Mocking Assistant for Moq Library.</summary>
    public class MoqAssist<T> where T : class
    {
        private List<T> _instances { get; set; }
        private Dictionary<string, Lazy<object>> _constructorMocks { get; set; }
        private MoqAssistDictionary _mockObjectsDictionary { get; set; }


        #region Constructor
        internal MoqAssist(MoqAssistDictionary dictionary)
        {
            _constructorMocks = new Dictionary<string, Lazy<object>>();
            _mockObjectsDictionary = dictionary;
            _instances = getConstructors();
        }
        #endregion

        #region Methods
        private List<T> getConstructors()
        {
            var constructorList = new List<T>();

            var type = typeof(T);
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                var constructorParams = constructor.GetParameters();
                object[] args = new object[constructorParams.Length];
                for (int i = 0; i < constructorParams.Length; i++)
                {
                    var ctxParam = _mockObjectsDictionary.GetByKey(constructorParams[i].ParameterType.FullName);
                    var isCtxParamExist = _constructorMocks.Any(x => x.Key == ctxParam.Key);
                    if (!isCtxParamExist) _constructorMocks.Add(ctxParam.Key, ctxParam.Value);
                    args[i] = ((Mock)ctxParam.Value.Value).Object;
                }
                constructorList.Add((T)Activator.CreateInstance(type, args));
            }
            return constructorList;
        }

        ///<summary>Gets all constructors of a given T object with mocked dependencies sequentially</summary>
        ///<returns>List of T with the sequential constructors</returns>
        public List<T> GetConstructors() => _instances;

        ///<summary>Gets a given TObject as mocked by checking from the dictionary</summary>
        ///<exception cref="MockObjectNotFoundException">Thrown when mocked object not found in the dictionary.</exception>
        ///<returns>Mock of a given TObject where TObject is a class</returns>
        public Mock<TObject> GetMock<TObject>() where TObject : class
        {
            var fullName = typeof(TObject).FullName;
            if (!_constructorMocks.Any(x => x.Key == fullName)) throw new MockObjectNotFoundException($"{fullName} could not found in the mock dictionary!");

            var mockedObject = _constructorMocks.FirstOrDefault(x => x.Key == fullName);
            return (Mock<TObject>)mockedObject.Value.Value;
        }

        ///<summary>Gives the dictionary including all mocked objects registered by MoqAssistDictionary</summary>
        public MoqAssistDictionary MockDictionary() => _mockObjectsDictionary;

        #endregion

        #region Creators
        ///<summary>Provides an instance of MoqAssist with a given MoqAssistDictionary</summary>
        public static MoqAssist<T> Construct(MoqAssistDictionary dictionary) => new MoqAssist<T>(dictionary);

        #endregion
    }
}