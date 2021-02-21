using MoqAssist.Core.Dictionary;

namespace MoqAssist.UnitTests.Tests.MockDictionary
{
    public static class DictionaryBuilder
    {
        public static MoqAssistDictionary Default => new DefaultMockDictionary();
        public static MoqAssistDictionary MockWithSetup => new MockWithSetupDictionary();
    }
}