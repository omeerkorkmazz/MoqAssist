using System.Collections.Generic;
using System.Linq;

namespace MoqAssist.Core
{
    public abstract class BaseMockDictionary : IBaseMockDictionary
    {
        protected Dictionary<string, object> MockObjectDictionary { get; set; }

        #region Constructor
        protected BaseMockDictionary()
        {
            MockObjectDictionary = new Dictionary<string, object>();
            LoadMockObjects();
        }
        #endregion

        #region Methods

        //abstract method to load mock objects into dictionary
        public abstract void LoadMockObjects();

        public KeyValuePair<string, object> GetObjectByName(string key)
        {
            var obj = MockObjectDictionary.FirstOrDefault(x => x.Key == key);
            return obj;
        }

        public void AddToDictionary(string objName, object obj)
        {
            MockObjectDictionary.Add(objName, obj);
        }

        public Dictionary<string, object> GetMockDictionary()
        {
            return MockObjectDictionary;
        }

        #endregion
    }
}