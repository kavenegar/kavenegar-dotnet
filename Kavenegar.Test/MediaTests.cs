using System.Threading.Tasks;
namespace Kavenegar.Test
{
    public class MediaTests
    {
        private const string RealApiKey = "<put your apikey here>";

        //new
        // API Tested: GET /v1/{api-key}/media/list.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_ListMedia(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var list = api.ListMedia(1, 10);
            Assert.NotNull(list);
            Assert.NotNull(list.List);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_ListMedia_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var list = await api.ListMediaAsync(1, 10);
            Assert.NotNull(list);
            Assert.NotNull(list.List);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/media/upload.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_UploadMedia(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            byte[] fileBytes;
            using (var httpClient = new HttpClient())
            {
                fileBytes = httpClient.GetByteArrayAsync("https://fastly.picsum.photos/id/168/200/200.jpg?hmac=VxnpUGg87Q47YRONmdsU2vNGSPjCs5vrwiAL-0hEIHM").GetAwaiter().GetResult();
            }
            var uploaded = api.UploadMedia("test_image.jpg", fileBytes);
            Assert.NotNull(uploaded);

            // Clean up
            api.DeleteMedia(uploaded.Id);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_UploadMedia_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            byte[] fileBytes;
            using (var httpClient = new HttpClient())
            {
                fileBytes = httpClient.GetByteArrayAsync("https://fastly.picsum.photos/id/168/200/200.jpg?hmac=VxnpUGg87Q47YRONmdsU2vNGSPjCs5vrwiAL-0hEIHM").GetAwaiter().GetResult();
            }
            var uploaded = await api.UploadMediaAsync("test_image.jpg", fileBytes);
            Assert.NotNull(uploaded);

            // Clean up
            await api.DeleteMediaAsync(uploaded.Id);
        }
#endif

        //new
        // API Tested: GET /v1/{api-key}/media/get.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_GetMedia(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            byte[] fileBytes;
            using (var httpClient = new HttpClient())
            {
                fileBytes = httpClient.GetByteArrayAsync("https://fastly.picsum.photos/id/168/200/200.jpg?hmac=VxnpUGg87Q47YRONmdsU2vNGSPjCs5vrwiAL-0hEIHM").GetAwaiter().GetResult();
            }
            var uploaded = api.UploadMedia("temp_get_image.jpg", fileBytes);

            var details = api.GetMedia(uploaded.Id, null);
            Assert.NotNull(details);

            // Clean up
            api.DeleteMedia(uploaded.Id);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_GetMedia_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            byte[] fileBytes;
            using (var httpClient = new HttpClient())
            {
                fileBytes = httpClient.GetByteArrayAsync("https://fastly.picsum.photos/id/168/200/200.jpg?hmac=VxnpUGg87Q47YRONmdsU2vNGSPjCs5vrwiAL-0hEIHM").GetAwaiter().GetResult();
            }
            var uploaded = await api.UploadMediaAsync("temp_get_image.jpg", fileBytes);

            var details = await api.GetMediaAsync(uploaded.Id, null);
            Assert.NotNull(details);

            // Clean up
            await api.DeleteMediaAsync(uploaded.Id);
        }
#endif

        //new
        // API Tested: DELETE /v1/{api-key}/media/delete.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_DeleteMedia(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            byte[] fileBytes;
            using (var httpClient = new HttpClient())
            {
                fileBytes = httpClient.GetByteArrayAsync("https://fastly.picsum.photos/id/168/200/200.jpg?hmac=VxnpUGg87Q47YRONmdsU2vNGSPjCs5vrwiAL-0hEIHM").GetAwaiter().GetResult();
            }
            var uploaded = api.UploadMedia("temp_delete_image.jpg", fileBytes);

            var deleteResult = api.DeleteMedia(uploaded.Id);
            Assert.True(deleteResult.Deleted);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_DeleteMedia_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            byte[] fileBytes;
            using (var httpClient = new HttpClient())
            {
                fileBytes = httpClient.GetByteArrayAsync("https://fastly.picsum.photos/id/168/200/200.jpg?hmac=VxnpUGg87Q47YRONmdsU2vNGSPjCs5vrwiAL-0hEIHM").GetAwaiter().GetResult();
            }
            var uploaded = await api.UploadMediaAsync("temp_delete_image.jpg", fileBytes);

            var deleteResult = await api.DeleteMediaAsync(uploaded.Id);
            Assert.True(deleteResult.Deleted);
        }
#endif
    }
}

