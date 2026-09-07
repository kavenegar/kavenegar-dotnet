using System.Threading.Tasks;
namespace Kavenegar.Test
{
    public class UtilityTests
    {
        private const string RealApiKey = "<put your apikey here>";

        // APIs Tested:
        // - GET /v1/{api-key}/utils/ping.json
        // - GET /v1/{api-key}/utils/getdate.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_Utils(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            // Ping
            var status = api.Ping();
            Assert.Equal("pong", status);

            // Get Date
            var serverDate = api.GetServerDate();
            Assert.NotNull(serverDate);
            Assert.NotNull(serverDate.Datetime);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_Utils_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            // Ping
            var status = await api.PingAsync();
            Assert.Equal("pong", status);

            // Get Date
            var serverDate = await api.GetServerDateAsync();
            Assert.NotNull(serverDate);
            Assert.NotNull(serverDate.Datetime);
        }
#endif

        // APIs Tested:
        // - GET /v1/{api-key}/account/info.json
        // - POST /v1/{api-key}/account/config.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_Account_Info_And_Config(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            // AccountInfo: AccountInfoResult AccountInfo()
            var accountInfo = api.AccountInfo();
            Assert.NotNull(accountInfo);

            // AccountConfig: AccountConfigResult AccountConfig(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed)
            var accountConfig = api.AccountConfig("enabled", "enabled", "enabled", "<put your sender here>", 10000, "enabled");
            Assert.NotNull(accountConfig);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_Account_Info_And_Config_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            // AccountInfo: AccountInfoResult AccountInfo()
            var accountInfo = await api.AccountInfoAsync();
            Assert.NotNull(accountInfo);

            // AccountConfig: AccountConfigResult AccountConfig(string apilogs, string dailyreport, string debugmode, string defaultsender, int? mincreditalarm, string resendfailed)
            var accountConfig = await api.AccountConfigAsync("enabled", "enabled", "enabled", "<put your sender here>", 10000, "enabled");
            Assert.NotNull(accountConfig);
        }
#endif

        // Unit Test: HttpClient Dependency Injection constructor verification
        [Fact]
        public void Test_HttpClient_Constructor_Injection()
        {
            using (var httpClient = new HttpClient())
            {
                var api = new KavenegarApi("MOCK_API_KEY", httpClient);
                Assert.NotNull(api);
                Assert.Equal("MOCK_API_KEY", api.ApiKey);
            }
        }
#if !NET35
        [Fact]
        public async System.Threading.Tasks.Task Test_HttpClient_Constructor_Injection_Async()
        {
            using (var httpClient = new HttpClient())
            {
                var api = new KavenegarApi("MOCK_API_KEY", httpClient);
                Assert.NotNull(api);
                Assert.Equal("MOCK_API_KEY", api.ApiKey);
            }
        }
#endif
    }
}

