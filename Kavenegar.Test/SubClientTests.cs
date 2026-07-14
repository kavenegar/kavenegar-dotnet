using System.Threading.Tasks;
using Kavenegar.Models;

namespace Kavenegar.Test
{
    public class SubClientTests
    {
        private const string RealApiKey = "<put your apikey here>";
        private const string RealReceptor = "<put your receptor here>";

        //new
        // API Tested: GET /v1/{api-key}/client/list.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_ListClients(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var list = api.ListClients();
            Assert.NotNull(list);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_ListClients_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var list = await api.ListClientsAsync();
            Assert.NotNull(list);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/client/add.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_AddClient(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var clientName = "Add Test " + Guid.NewGuid().ToString().Substring(0, 8);
            var dto = new SubClientDto
            {
                FullName = clientName,
                UserName = "user_" + Guid.NewGuid().ToString().Substring(0, 8),
                Password = "Password123!",
                Mobile = receptor,
                Credit = 1000
            };
            var newClient = api.AddClient(dto);
            Assert.NotNull(newClient);
            Assert.Equal(clientName, newClient.FullName);

            // Clean up (suspend)
            api.SetClientStatus(newClient.ApiKey, 2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_AddClient_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var clientName = "Add Test " + Guid.NewGuid().ToString().Substring(0, 8);
            var dto = new SubClientDto
            {
                FullName = clientName,
                UserName = "user_" + Guid.NewGuid().ToString().Substring(0, 8),
                Password = "Password123!",
                Mobile = receptor,
                Credit = 1000
            };
            var newClient = await api.AddClientAsync(dto);
            Assert.NotNull(newClient);
            Assert.Equal(clientName, newClient.FullName);

            // Clean up (suspend)
            await api.SetClientStatusAsync(newClient.ApiKey, 2);
        }
#endif

        //new
        // API Tested: GET /v1/{api-key}/client/fetch.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_FetchClient(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = CreateTempClient(api, receptor);
            var fetched = api.FetchClient(tempClient.ApiKey);
            Assert.NotNull(fetched);

            // Clean up (suspend)
            api.SetClientStatus(tempClient.ApiKey, 2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_FetchClient_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = await CreateTempClientAsync(api, receptor);
            var fetched = await api.FetchClientAsync(tempClient.ApiKey);
            Assert.NotNull(fetched);

            // Clean up (suspend)
            await api.SetClientStatusAsync(tempClient.ApiKey, 2);
        }
#endif

        //new
        // API Tested: GET /v1/{api-key}/client/fetchbylocalid.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_FetchClientByLocalId(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = CreateTempClient(api, receptor);
            var fetched = api.FetchClientByLocalId(tempClient.LocalId);
            Assert.NotNull(fetched);

            // Clean up (suspend)
            api.SetClientStatus(tempClient.ApiKey, 2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_FetchClientByLocalId_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = await CreateTempClientAsync(api, receptor);
            var fetched = await api.FetchClientByLocalIdAsync(tempClient.LocalId);
            Assert.NotNull(fetched);

            // Clean up (suspend)
            await api.SetClientStatusAsync(tempClient.ApiKey, 2);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/client/renewkey.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_RenewClientKey(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = CreateTempClient(api, receptor);
            var renewed = api.RenewClientKey(tempClient.ApiKey);
            Assert.NotNull(renewed);

            // Clean up (suspend)
            api.SetClientStatus(renewed.ApiKey, 2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_RenewClientKey_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = await CreateTempClientAsync(api, receptor);
            var renewed = await api.RenewClientKeyAsync(tempClient.ApiKey);
            Assert.NotNull(renewed);

            // Clean up (suspend)
            await api.SetClientStatusAsync(renewed.ApiKey, 2);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/client/update.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_UpdateClient(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = CreateTempClient(api, receptor);
            var dto = new SubClientDto
            {
                ApiKey = tempClient.ApiKey,
                FullName = "Updated Sub Name",
                UserName = tempClient.UserName,
                Mobile = receptor
            };
            var updated = api.UpdateClient(dto);
            Assert.Equal("Updated Sub Name", updated.FullName);

            // Clean up (suspend)
            api.SetClientStatus(tempClient.ApiKey, 2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_UpdateClient_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = await CreateTempClientAsync(api, receptor);
            var dto = new SubClientDto
            {
                ApiKey = tempClient.ApiKey,
                FullName = "Updated Sub Name",
                UserName = tempClient.UserName,
                Mobile = receptor
            };
            var updated = await api.UpdateClientAsync(dto);
            Assert.Equal("Updated Sub Name", updated.FullName);

            // Clean up (suspend)
            await api.SetClientStatusAsync(tempClient.ApiKey, 2);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/client/chargecredit.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_ChargeClientCredit(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = CreateTempClient(api, receptor);
            var charged = api.ChargeClientCredit(tempClient.ApiKey, 500, "Refill Test");
            Assert.NotNull(charged);

            // Clean up (suspend)
            api.SetClientStatus(tempClient.ApiKey, 2);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_ChargeClientCredit_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = await CreateTempClientAsync(api, receptor);
            var charged = await api.ChargeClientCreditAsync(tempClient.ApiKey, 500, "Refill Test");
            Assert.NotNull(charged);

            // Clean up (suspend)
            await api.SetClientStatusAsync(tempClient.ApiKey, 2);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/client/setstatus.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_SetClientStatus(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = CreateTempClient(api, receptor);
            var suspended = api.SetClientStatus(tempClient.ApiKey, 2);
            Assert.NotNull(suspended);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_SetClientStatus_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var tempClient = await CreateTempClientAsync(api, receptor);
            var suspended = await api.SetClientStatusAsync(tempClient.ApiKey, 2);
            Assert.NotNull(suspended);
        }
#endif

        private SubClientResult CreateTempClient(KavenegarApi api, string receptor)
        {
            var clientName = "Temp " + Guid.NewGuid().ToString().Substring(0, 8);
            var dto = new SubClientDto
            {
                FullName = clientName,
                UserName = "user_" + Guid.NewGuid().ToString().Substring(0, 8),
                Password = "Password123!",
                Mobile = receptor,
                Credit = 100
            };
            return api.AddClient(dto);
        }
#if !NET35
        private async System.Threading.Tasks.Task<SubClientResult> CreateTempClientAsync(KavenegarApi api, string receptor)
        {
            var clientName = "Temp " + Guid.NewGuid().ToString().Substring(0, 8);
            var dto = new SubClientDto
            {
                FullName = clientName,
                UserName = "user_" + Guid.NewGuid().ToString().Substring(0, 8),
                Password = "Password123!",
                Mobile = receptor,
                Credit = 100
            };
            return await api.AddClientAsync(dto);
        }
#endif
    }
}

