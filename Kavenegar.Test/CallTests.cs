using System.Threading.Tasks;
using Kavenegar.Models.Enums;

namespace Kavenegar.Test
{
    public class CallTests
    {
        private const string RealApiKey = "53732F653245324C4651484E5A4F3166354672636A706B564D666D79587566574573344E687A4F65716F733D";
        private const string RealReceptor = "09912064992";
        private const string RealSender = "1000100055";

        // API Tested: POST /v1/{api-key}/call/maketts.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public void Test_Call_MakeTTS_Overloads(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            // Overload 1: SendResult CallMakeTTS(string message, string receptor)
            var res1 = api.CallMakeTTS("Test voice call 1", receptor);
            Assert.NotNull(res1);

            // Overload 2: List<SendResult> CallMakeTTS(string message, List<string> receptor)
            var res2 = api.CallMakeTTS("Test voice call 2", new List<string> { receptor });
            Assert.NotNull(res2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public async System.Threading.Tasks.Task Test_Call_MakeTTS_Overloads_Async(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            // Overload 1: SendResult CallMakeTTS(string message, string receptor)
            var res1 = await api.CallMakeTTSAsync("Test voice call 1", receptor);
            Assert.NotNull(res1);

            // Overload 2: List<SendResult> CallMakeTTS(string message, List<string> receptor)
            var res2 = await api.CallMakeTTSAsync("Test voice call 2", new List<string> { receptor });
            Assert.NotNull(res2);
        }
#endif

        // API Tested: POST /v1/{api-key}/call/maketts.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender, "Test TTS Call", "tts-tag")]
        public void Test_CallMakeTTS_Extended_Real(string apiKey, string receptor, string sender, string message, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var receptors = new List<string> { receptor };
            var result = api.CallMakeTTS(message, receptors, null, null, sender: sender, tag: tag);
            Assert.NotNull(result);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender, "Test TTS Call", "tts-tag")]
        public async System.Threading.Tasks.Task Test_CallMakeTTS_Extended_Real_Async(string apiKey, string receptor, string sender, string message, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var receptors = new List<string> { receptor };
            var result = await api.CallMakeTTSAsync(message, receptors, null, null, sender: sender, tag: tag);
            Assert.NotNull(result);
        }
#endif
    }
}

