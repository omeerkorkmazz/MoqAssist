using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MoqAssist.Core.Dictionary;
using MoqAssist.Core.Exceptions;

namespace MoqAssist.Core
{
    public class MoqAssist<T> where T : class
    {
        private List<T> _instances { get; set; }
        private Dictionary<string, object> _constructorMocks { get; set; }
        private MoqAssistDictionary _mockObjectsDictionary { get; set; }


        #region Constructor
        internal MoqAssist(MoqAssistDictionary dictionary)
        {
            _constructorMocks = new Dictionary<string, object>();
            _mockObjectsDictionary = dictionary;
            _instances = getInstances();
        }
        #endregion

        #region Methods
        private List<T> getInstances()
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
                    args[i] = ((Mock)ctxParam.Value).Object;
                }
                constructorList.Add((T)Activator.CreateInstance(type, args));
            }
            return constructorList;
        }
        public List<T> GetInstances() => _instances;
        public Mock<TObject> GetMock<TObject>() where TObject : class
        {
            var fullName = typeof(TObject).FullName;
            if (!_constructorMocks.Any(x => x.Key == fullName)) throw new MockObjectNotFoundException($"{fullName} could not found in the mock dictionary!");

            var mockedObject = _constructorMocks.FirstOrDefault(x => x.Key == fullName);
            return (Mock<TObject>)mockedObject.Value;
        }
        public MoqAssistDictionary MockDictionary() => _mockObjectsDictionary;

        #endregion

        #region Creators
        public static MoqAssist<T> Construct(MoqAssistDictionary dictionary) => new MoqAssist<T>(dictionary);

        #endregion
    }
}