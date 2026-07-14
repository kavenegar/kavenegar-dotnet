using System.Threading.Tasks;
using Kavenegar.Models;
using Kavenegar.Models.Enums;

namespace Kavenegar.Test
{
    public class VerifyTests
    {
        private const string RealApiKey = "<put your apikey here>";
        private const string RealReceptor = "<put your receptor here>";

        // API Tested: POST /v1/{api-key}/verify/lookup.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_Verify_Lookup_Overloads(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);

            
            var res1 = api.VerifyLookup(receptor, "123456", "otp-template");
            Assert.NotNull(res1);
            var res2 = api.VerifyLookup(receptor, "123456", "otp-template", VerifyLookupType.Sms);
            Assert.NotNull(res2);


            var res2Token1 = api.VerifyLookup(receptor, "123", "456", "otp-template-1");
            Assert.NotNull(res2Token1);
            var res2Token2 = api.VerifyLookup(receptor, "123", "456", "otp-template-1", VerifyLookupType.Sms);
            Assert.NotNull(res2Token2);


            var res3 = api.VerifyLookup(receptor, "123", "456", "789", "otp-template-2");
            Assert.NotNull(res3);
            var res4 = api.VerifyLookup(receptor, "123", "456", "789", "otp-template-2", VerifyLookupType.Sms);
            Assert.NotNull(res4);
            

            var res5 = api.VerifyLookup(receptor, "123", "456", "789", "token10", "otp-template-3");
            Assert.NotNull(res5);
            var res6 = api.VerifyLookup(receptor, "123", "456", "789", "token10", "otp-template-3", VerifyLookupType.Sms);
            Assert.NotNull(res6);
            
            
            var res7 = api.VerifyLookup(receptor, "123", "456", "789", "token10", "token20","otp-template-4", VerifyLookupType.Sms);
            Assert.NotNull(res7);
            var res8 = api.VerifyLookup(receptor, "123", "456", "789", "token10", "token20","otp-template-4", VerifyLookupType.Sms);
            Assert.NotNull(res8);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_Verify_Lookup_Overloads_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);

            
            var res1 = await api.VerifyLookupAsync(receptor, "123456", "otp-template");
            Assert.NotNull(res1);
            var res2 = await api.VerifyLookupAsync(receptor, "123456", "otp-template", VerifyLookupType.Sms);
            Assert.NotNull(res2);


            var res2Token1 = await api.VerifyLookupAsync(receptor, "123", "456", "otp-template-1");
            Assert.NotNull(res2Token1);
            var res2Token2 = await api.VerifyLookupAsync(receptor, "123", "456", "otp-template-1", VerifyLookupType.Sms);
            Assert.NotNull(res2Token2);


            var res3 = await api.VerifyLookupAsync(receptor, "123", "456", "789", "otp-template-2");
            Assert.NotNull(res3);
            var res4 = await api.VerifyLookupAsync(receptor, "123", "456", "789", "otp-template-2", VerifyLookupType.Sms);
            Assert.NotNull(res4);
            

            var res5 = await api.VerifyLookupAsync(receptor, "123", "456", "789", "token10", "otp-template-3");
            Assert.NotNull(res5);
            var res6 = await api.VerifyLookupAsync(receptor, "123", "456", "789", "token10", "otp-template-3", VerifyLookupType.Sms);
            Assert.NotNull(res6);
            
            
            var res7 = await api.VerifyLookupAsync(receptor, "123", "456", "789", "token10", "token20","otp-template-4", VerifyLookupType.Sms);
            Assert.NotNull(res7);
            var res8 = await api.VerifyLookupAsync(receptor, "123", "456", "789", "token10", "token20","otp-template-4", VerifyLookupType.Sms);
            Assert.NotNull(res8);
        }
#endif


        // API Tested: POST /v1/{api-key}/verify/lookup.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor, "123456", "otp-template", "test")]
        public void Test_VerifyLookup_Extended(string apiKey, string receptor, string token, string template, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var result = api.VerifyLookup(receptor, token, null, null, null, null, template, VerifyLookupType.Sms, tag: tag);
            Assert.NotNull(result);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, "123456", "otp-template", "test")]
        public async System.Threading.Tasks.Task Test_VerifyLookup_Extended_Async(string apiKey, string receptor, string token, string template, string tag)
        {
            var api = new KavenegarApi(apiKey);
            var result = await api.VerifyLookupAsync(receptor, token, null, null, null, null, template, VerifyLookupType.Sms, tag: tag);
            Assert.NotNull(result);
        }
#endif

        //new
        // API Tested: GET /v1/{api-key}/verify/list.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_ListTemplates(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var templates = api.ListTemplates();
            Assert.NotNull(templates);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_ListTemplates_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var templates = await api.ListTemplatesAsync();
            Assert.NotNull(templates);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/verify/addtemplate.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_AddTemplate(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = CreateTempTemplate(api);
            Assert.NotNull(added);

            // Clean up
            api.DeleteTemplate(int.Parse(added.TemplateId));
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_AddTemplate_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = await CreateTempTemplateAsync(api);
            Assert.NotNull(added);

            // Clean up
            await api.DeleteTemplateAsync(int.Parse(added.TemplateId));
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/verify/updatetemplate.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_UpdateTemplate(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = CreateTempTemplate(api);
            var dto = new TemplateDto
            {
                Name = "otp-template",
                TextMessage = "Verification code updated: %token%",
                SourceType = VerificationUsageType.WebApplication,
                SendMethod = VerificationType.Message
            };

            var updated = api.UpdateTemplate(int.Parse(added.TemplateId), dto);
            Assert.NotNull(updated);

            // Clean up
            api.DeleteTemplate(int.Parse(added.TemplateId));
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_UpdateTemplate_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = await CreateTempTemplateAsync(api);
            var dto = new TemplateDto
            {
                Name = "otp-template",
                TextMessage = "Verification code updated: %token%",
                SourceType = VerificationUsageType.WebApplication,
                SendMethod = VerificationType.Message
            };

            var updated = await api.UpdateTemplateAsync(int.Parse(added.TemplateId), dto);
            Assert.NotNull(updated);

            // Clean up
            await api.DeleteTemplateAsync(int.Parse(added.TemplateId));
        }
#endif

        //new
        // API Tested: GET /v1/{api-key}/verify/get.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_GetTemplate(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = CreateTempTemplate(api);

            var detail = api.GetTemplate(int.Parse(added.TemplateId));
            Assert.NotNull(detail);

            // Clean up
            api.DeleteTemplate(int.Parse(added.TemplateId));
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_GetTemplate_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = await CreateTempTemplateAsync(api);

            var detail = await api.GetTemplateAsync(int.Parse(added.TemplateId));
            Assert.NotNull(detail);

            // Clean up
            await api.DeleteTemplateAsync(int.Parse(added.TemplateId));
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/verify/clone.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_CloneTemplate(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = CreateTempTemplate(api);

            var cloned = api.CloneTemplate(int.Parse(added.TemplateId), null, added.Name + "-copy");
            Assert.NotNull(cloned);

            // Clean up both
            api.DeleteTemplate(int.Parse(added.TemplateId));
            api.DeleteTemplate(cloned.Id);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_CloneTemplate_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = await CreateTempTemplateAsync(api);

            var cloned = await api.CloneTemplateAsync(int.Parse(added.TemplateId), null, added.Name + "-copy");
            Assert.NotNull(cloned);

            // Clean up both
            await api.DeleteTemplateAsync(int.Parse(added.TemplateId));
            await api.DeleteTemplateAsync(cloned.Id);
        }
#endif

        //new
        // API Tested: DELETE /v1/{api-key}/verify/deletetemplate.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_DeleteTemplate(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = CreateTempTemplate(api);

            var deleteResult = api.DeleteTemplate(int.Parse(added.TemplateId));
            Assert.NotNull(deleteResult);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_DeleteTemplate_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var added = await CreateTempTemplateAsync(api);

            var deleteResult = await api.DeleteTemplateAsync(int.Parse(added.TemplateId));
            Assert.NotNull(deleteResult);
        }
#endif

        private TemplateInfoResult CreateTempTemplate(KavenegarApi api)
        {
            var dto = new TemplateDto
            {
                Name = "temp" + new Random().Next(1,10000),
                TextMessage = "Verification code: %token%",
                SourceType = VerificationUsageType.WebApplication,
                SendMethod = VerificationType.Message
            };
            return api.AddTemplate(dto);
        }
#if !NET35
        private async System.Threading.Tasks.Task<TemplateInfoResult> CreateTempTemplateAsync(KavenegarApi api)
        {
            var dto = new TemplateDto
            {
                Name = "temp" + new Random().Next(1,10000),
                TextMessage = "Verification code: %token%",
                SourceType = VerificationUsageType.WebApplication,
                SendMethod = VerificationType.Message
            };
            return await api.AddTemplateAsync(dto);
        }
#endif
    }
}

