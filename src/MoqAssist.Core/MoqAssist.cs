using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace MoqAssist.Core
{
    public class MoqAssist<T> where T : class
    {
        private List<T> Instances { get; set; }
        private Dictionary<string, object> ConstructorMocks { get; set; }
        private BaseMockDictionary MockObjectsDictionary { get; set; }


        #region Constructor
        public MoqAssist(BaseMockDictionary dictionary)
        {
            ConstructorMocks = new Dictionary<string, object>();
            MockObjectsDictionary = dictionary;
            Instances = getInstances();
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
                    var ctxParam = MockObjectsDictionary.GetObjectByName(constructorParams[i].ParameterType.FullName);
                    var isCtxParamExist = ConstructorMocks.Any(x => x.Key == ctxParam.Key);
                    if (!isCtxParamExist) ConstructorMocks.Add(ctxParam.Key, ctxParam.Value);
                    args[i] = ((Mock)ctxParam.Value).Object;
                }
                constructorList.Add((T)Activator.CreateInstance(type, args));
            }
            return constructorList;
        }

        public List<T> GetInstances()
        {
            return Instances;
        }

        public Mock<TObject> GetMockObject<TObject>() where TObject : class
        {
            var fullName = typeof(TObject).FullName;
            var mockedObj = ConstructorMocks.FirstOrDefault(x => x.Key == fullName);
            if (mockedObj.Key == null && mockedObj.Value == null) return null;
            return (Mock<TObject>)mockedObj.Value;
        }
        #endregion
    }
}