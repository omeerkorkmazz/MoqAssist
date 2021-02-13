using System.Collections.Generic;

namespace MoqAssist.Core
{
    internal interface IBaseMockDictionary
    {
        void LoadMockObjects();
        KeyValuePair<string, object> GetObjectByName(string key);
        void AddToDictionary(string objName, object obj);
        Dictionary<string, object> GetMockDictionary();    
    }
}