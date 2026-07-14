using System.Threading.Tasks;
using Kavenegar.Models.Enums;

namespace Kavenegar.Test
{
    public class CallTests
    {
        private const string RealApiKey = "<put your apikey here>";
        private const string RealReceptor = "<put your receptor here>";
        private const string RealSender = "<put your sender here>";

        // API Tested: POST /v1/{api-key}/call/maketts.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public void Test_Call_MakeTTS_Overloads(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            // Overload 1: SendResult CallMakeTTS(string message, string receptor)
            var res1 = api.CallMakeTTS("تست تماس صوتی ۱", receptor);
            Assert.NotNull(res1);

            // Overload 2: List<SendResult> CallMakeTTS(string message, List<string> receptor)
            var res2 = api.CallMakeTTS("تست تماس صوتی ۲", new List<string> { receptor });
            Assert.NotNull(res2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public async System.Threading.Tasks.Task Test_Call_MakeTTS_Overloads_Async(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            // Overload 1: SendResult CallMakeTTS(string message, string receptor)
            var res1 = await api.CallMakeTTSAsync("تست تماس صوتی ۱", receptor);
            Assert.NotNull(res1);

            // Overload 2: List<SendResult> CallMakeTTS(string message, List<string> receptor)
            var res2 = await api.CallMakeTTSAsync("تست تماس صوتی ۲", new List<string> { receptor });
            Assert.NotNull(res2);
        }
#endif

        // API Tested: POST /v1/{api-key}/call/maketts.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست تماس تبدیل متن به گفتار", "tts-tag")]
        public void Test_CallMakeTTS_Extended(string apiKey, string receptor, string sender, string message, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var receptors = new List<string> { receptor };
            var result = api.CallMakeTTS(message, receptors, null, null, sender: sender, tag: tag);
            Assert.NotNull(result);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست تماس تبدیل متن به گفتار", "tts-tag")]
        public async System.Threading.Tasks.Task Test_CallMakeTTS_Extended_Async(string apiKey, string receptor, string sender, string message, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var receptors = new List<string> { receptor };
            var result = await api.CallMakeTTSAsync(message, receptors, null, null, sender: sender, tag: tag);
            Assert.NotNull(result);
        }
#endif
    }
}

