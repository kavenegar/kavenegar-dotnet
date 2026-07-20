using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kavenegar.Test
{
    public class CallTests
    {
        private const string ApiKey = "<put your apikey here>";
        private const string Receptor = "<put your receptor here>";

        // API Tested: POST /v1/{api-key}/call/maketts.json
        [Theory]
        [InlineData(ApiKey, Receptor)]
        public void Test_CallMakeTTS_Overloads(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);

            Assert.NotNull(api.CallMakeTTS("Test voice call 1", receptor));
            Assert.NotNull(api.CallMakeTTS("Test voice call 2", new List<string> { receptor }));
        }

#if !NET35
        [Theory]
        [InlineData(ApiKey, Receptor)]
        public async Task Test_CallMakeTTS_Overloads_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);

            Assert.NotNull(await api.CallMakeTTSAsync("Test voice call 1", receptor));
            Assert.NotNull(await api.CallMakeTTSAsync("Test voice call 2", new List<string> { receptor }));
        }
#endif

        [Theory]
        [InlineData(ApiKey, Receptor, "Test text-to-speech call", "tts-tag")]
        public void Test_CallMakeTTS_WithTag(string apiKey, string receptor, string message, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var result = api.CallMakeTTS(message, new List<string> { receptor }, null, null, tag);

            Assert.NotNull(result);
        }

#if !NET35
        [Theory]
        [InlineData(ApiKey, Receptor, "Test text-to-speech call", "tts-tag")]
        public async Task Test_CallMakeTTS_WithTag_Async(string apiKey, string receptor, string message, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var result = await api.CallMakeTTSAsync(message, new List<string> { receptor }, null, null, tag);

            Assert.NotNull(result);
        }
#endif
    }
}
