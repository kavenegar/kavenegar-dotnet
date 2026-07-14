using System.Threading.Tasks;
namespace Kavenegar.Test
{
    public class LineTests
    {
        private const string RealApiKey = "<put your apikey here>";
        private const string RealReceptor = "<put your receptor here>";
        private const string RealSender = "<put your sender here>";

        //new
        // API Tested: GET /v1/{api-key}/line/blocked/exists.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public void Test_BlockedLineExists(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            var check = api.BlockedLineExists(sender, receptor);
            Assert.NotNull(check);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public async System.Threading.Tasks.Task Test_BlockedLineExists_Async(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            var check = await api.BlockedLineExistsAsync(sender, receptor);
            Assert.NotNull(check);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/line/blocked/add.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public void Test_AddBlockedLine(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            var addedBlock = api.AddBlockedLine(receptor, sender);
            Assert.NotNull(addedBlock);

            // Clean up
            api.RemoveBlockedLine(receptor, sender);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public async System.Threading.Tasks.Task Test_AddBlockedLine_Async(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            var addedBlock = await api.AddBlockedLineAsync(receptor, sender);
            Assert.NotNull(addedBlock);

            // Clean up
            await api.RemoveBlockedLineAsync(receptor, sender);
        }
#endif

        //new
        // API Tested: DELETE /v1/{api-key}/line/blocked/remove.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public void Test_RemoveBlockedLine(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            // Ensure blocked first
            api.AddBlockedLine(receptor, sender);

            var removedBlock = api.RemoveBlockedLine(receptor, sender);
            Assert.NotNull(removedBlock);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender)]
        public async System.Threading.Tasks.Task Test_RemoveBlockedLine_Async(string apiKey, string receptor, string sender)
        {
            var api = new KavenegarApi(apiKey);
            // Ensure blocked first
            await api.AddBlockedLineAsync(receptor, sender);

            var removedBlock = await api.RemoveBlockedLineAsync(receptor, sender);
            Assert.NotNull(removedBlock);
        }
#endif

        //new
        // API Tested: GET /v1/{api-key}/line/blocked/list.json
        [Theory]
        [InlineData(RealApiKey, RealSender)]
        public void Test_ListBlockedLines(string apiKey, string sender)
        {
            var api = new KavenegarApi(apiKey);
            var blockedList = api.ListBlockedLines(sender, 1, Kavenegar.Utils.DateHelper.DateTimeToUnixTimestamp(DateTime.Now.AddDays(-1)));
            Assert.NotNull(blockedList);
            Assert.NotNull(blockedList.Entries);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealSender)]
        public async System.Threading.Tasks.Task Test_ListBlockedLines_Async(string apiKey, string sender)
        {
            var api = new KavenegarApi(apiKey);
            var blockedList = await api.ListBlockedLinesAsync(sender, 1, Kavenegar.Utils.DateHelper.DateTimeToUnixTimestamp(DateTime.Now.AddDays(-1)));
            Assert.NotNull(blockedList);
            Assert.NotNull(blockedList.Entries);
        }
#endif
    }
}

