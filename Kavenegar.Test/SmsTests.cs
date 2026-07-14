using Kavenegar.Models;
using Kavenegar.Models.Enums;

namespace Kavenegar.Test
{
    public class SmsTests
    {
        private const string RealApiKey = "<put your apikey here>";
        private const string RealReceptor = "<put your receptor here>";
        private const string RealSender = "<put your sender here>";

        [Fact]
        public void Test_Sms_Send_Overload_1()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.Send(RealSender, new List<string> { RealReceptor }, "تست پیام ۱");
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Overload_1_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendAsync(RealSender, new List<string> { RealReceptor }, "تست پیام ۱");
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_Send_Overload_2()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.Send(RealSender, RealReceptor, "تست پیام ۲");
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Overload_2_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendAsync(RealSender, RealReceptor, "تست پیام ۲");
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_Send_Overload_3()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.Send(RealSender, RealReceptor, "تست پیام ۳", MessageType.MobileMemory, DateTime.MinValue);
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Overload_3_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendAsync(RealSender, RealReceptor, "تست پیام ۳", MessageType.MobileMemory, DateTime.MinValue);
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_Send_Overload_4()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.Send(RealSender, new List<string> { RealReceptor }, "تست پیام ۴", MessageType.MobileMemory, DateTime.MinValue);
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Overload_4_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendAsync(RealSender, new List<string> { RealReceptor }, "تست پیام ۴", MessageType.MobileMemory, DateTime.MinValue);
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_Send_Overload_5()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.Send(RealSender, RealReceptor, "تست پیام ۵", MessageType.MobileMemory, DateTime.MinValue, "localid123");
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Overload_5_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendAsync(RealSender, RealReceptor, "تست پیام ۵", MessageType.MobileMemory, DateTime.MinValue, "localid123");
            Assert.NotNull(res);
        }
#endif


        [Fact]
        public void Test_Sms_Send_Overload_6()
        {
            var api = new KavenegarApi(RealApiKey);
            var first = api.Send(RealSender, RealReceptor, "تست پیام ۶", "localid4564");
            var seconde = api.Send(RealSender, RealReceptor, "تست پیام ۶", "localid4564");
            Assert.NotNull(first);
            Assert.NotNull(seconde);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Overload_6_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var first = await api.SendAsync(RealSender, RealReceptor, "تست پیام ۶", "localid4564");
            var seconde = await api.SendAsync(RealSender, RealReceptor, "تست پیام ۶", "localid4564");
            Assert.NotNull(first);
            Assert.NotNull(seconde);
        }
#endif

        [Fact]
        public void Test_Sms_Send_Overload_7()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.Send(RealSender, new List<string> { RealReceptor }, "تست پیام ۷", "localid789");
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Overload_7_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendAsync(RealSender, new List<string> { RealReceptor }, "تست پیام ۷", "localid789");
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_Send_Extend()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.Send(RealSender, new List<string> { RealReceptor }, "تست پیام پیشرفته", MessageType.MobileMemory, DateTime.MinValue, null, null, null, null, null, null, null, null, null, null);
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Send_Extend_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendAsync(RealSender, new List<string> { RealReceptor }, "تست پیام پیشرفته", MessageType.MobileMemory, DateTime.MinValue, null, null, null, null, null, null, null, null, null, null);
            Assert.NotNull(res);
        }
#endif

        [Theory]
        [InlineData(RealApiKey, RealReceptor, null, "تست ارسال پیشرفته با خط‌مشی", (int)MessageType.MobileMemory, null, null, "local-id-test-12", "mix", null)]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته با خط‌مشی و رسانه", (int)MessageType.MobileMemory, null, null, "local-id-test-media", "mix", "8c90c180-7b7f-f111-81cb-005056a53592")]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته بدون خط‌مشی", (int)MessageType.MobileMemory, null, null, "local-id-test-without-policy", null, null)]
        public void Test_Sms_Send_Extended(
            string apiKey, 
            string receptor, 
            string sender, 
            string message, 
            int type, 
            string tag, 
            string hide, 
            string localMessageId, 
            string policy, 
            string mediaIdString)
        {
            var api = new KavenegarApi(apiKey);
            var receptors = new List<string> { receptor };
            Guid? mediaId = string.IsNullOrEmpty(mediaIdString) || mediaIdString == Guid.Empty.ToString() ? (Guid?)null : Guid.Parse(mediaIdString);
            
            string? finalTag = string.IsNullOrEmpty(tag) ? null : tag;
            string? finalHide = string.IsNullOrEmpty(hide) ? null : hide;
            string? finalLocalMsgId = string.IsNullOrEmpty(localMessageId) ? null : localMessageId;
            string? finalPolicy = string.IsNullOrEmpty(policy) ? null : policy;

            var request = new SendRequest
            {
                Sender = sender,
                Receptor = receptors,
                Message = message,
                Type = (MessageType)type,
                Date = DateTime.MinValue,
                LocalIds = null,
                Tag = finalTag,
                Hide = finalHide,
                LocalMessageId = finalLocalMsgId,
                Policy = finalPolicy,
                MediaId = mediaId
            };

            var result = api.Send(request);
            Assert.NotNull(result);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, null, "تست ارسال پیشرفته با خط‌مشی", (int)MessageType.MobileMemory, null, null, "local-id-test-12", "mix", null)]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته با خط‌مشی و رسانه", (int)MessageType.MobileMemory, null, null, "local-id-test-media", "mix", "8c90c180-7b7f-f111-81cb-005056a53592")]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته بدون خط‌مشی", (int)MessageType.MobileMemory, null, null, "local-id-test-without-policy", null, null)]
        public async Task Test_Sms_Send_Extended_Async(
            string apiKey, 
            string receptor, 
            string sender, 
            string message, 
            int type, 
            string tag, 
            string hide, 
            string localMessageId, 
            string policy, 
            string mediaIdString)
        {
            var api = new KavenegarApi(apiKey);
            var receptors = new List<string> { receptor };
            Guid? mediaId = string.IsNullOrEmpty(mediaIdString) || mediaIdString == Guid.Empty.ToString() ? (Guid?)null : Guid.Parse(mediaIdString);
            
            string? finalTag = string.IsNullOrEmpty(tag) ? null : tag;
            string? finalHide = string.IsNullOrEmpty(hide) ? null : hide;
            string? finalLocalMsgId = string.IsNullOrEmpty(localMessageId) ? null : localMessageId;
            string? finalPolicy = string.IsNullOrEmpty(policy) ? null : policy;

            var request = new SendRequest
            {
                Sender = sender,
                Receptor = receptors,
                Message = message,
                Type = (MessageType)type,
                Date = DateTime.MinValue,
                LocalIds = null,
                Tag = finalTag,
                Hide = finalHide,
                LocalMessageId = finalLocalMsgId,
                Policy = finalPolicy,
                MediaId = mediaId
            };

            var result = await api.SendAsync(request);
            Assert.NotNull(result);
        }
#endif

        [Fact]
        public void Test_Sms_SendArray_Overload_1()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.SendArray(new List<string> { RealSender }, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۱" });
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SendArray_Overload_1_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendArrayAsync(new List<string> { RealSender }, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۱" });
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_SendArray_Overload_2()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.SendArray(RealSender, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۲" }, MessageType.MobileMemory, DateTime.MinValue);
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SendArray_Overload_2_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendArrayAsync(RealSender, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۲" }, MessageType.MobileMemory, DateTime.MinValue);
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_SendArray_Overload_3()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.SendArray(RealSender, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۳" }, MessageType.MobileMemory, DateTime.MinValue, "localmsgid123");
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SendArray_Overload_3_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendArrayAsync(RealSender, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۳" }, MessageType.MobileMemory, DateTime.MinValue, "localmsgid123");
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_SendArray_Overload_4()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.SendArray(RealSender, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۴" }, "localmsgid456");
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SendArray_Overload_4_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendArrayAsync(RealSender, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۴" }, "localmsgid456");
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_SendArray_Overload_5()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.SendArray(new List<string> { RealSender }, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۵" }, "localmsgid789");
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SendArray_Overload_5_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendArrayAsync(new List<string> { RealSender }, new List<string> { RealReceptor }, new List<string> { "پیام آرایه ۵" }, "localmsgid789");
            Assert.NotNull(res);
        }
#endif

        [Fact]
        public void Test_Sms_SendArray_11Params()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = api.SendArray(new List<string> { RealSender }, new List<string> { RealReceptor }, new List<string> { "پیام آرایه قدیمی" }, new List<MessageType> { MessageType.MobileMemory }, DateTime.MinValue, null, null, null, null, null);
            Assert.NotNull(res);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SendArray_11Params_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var res = await api.SendArrayAsync(new List<string> { RealSender }, new List<string> { RealReceptor }, new List<string> { "پیام آرایه قدیمی" }, new List<MessageType> { MessageType.MobileMemory }, DateTime.MinValue, null, null, null, null, null);
            Assert.NotNull(res);
        }
#endif

        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته با خط‌مشی", (int)MessageType.MobileMemory, "local-id-test", "", "", "mix", null)]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته با خط‌مشی و رسانه", (int)MessageType.MobileMemory, "local-id-test-media", "", "", "mix", "8c90c180-7b7f-f111-81cb-005056a53592")]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته بدون خط‌مشی", (int)MessageType.MobileMemory, "local-id-test-without-policy", "", "", null, null)]
        public void Test_Sms_SendArray_Extended(
            string apiKey, 
            string receptor, 
            string sender, 
            string message, 
            int type, 
            string localmessageids, 
            string tag, 
            string hide, 
            string policy, 
            string mediaIdString)
        {
            var api = new KavenegarApi(apiKey);
            var senders = new List<string> { sender };
            var receptors = new List<string> { receptor };
            var messages = new List<string> { message };
            var types = new List<MessageType> { (MessageType)type };
            Guid? mediaId = string.IsNullOrEmpty(mediaIdString) || mediaIdString == Guid.Empty.ToString() ? (Guid?)null : Guid.Parse(mediaIdString);
            
            List<string>? finalLocalMessageIds = string.IsNullOrEmpty(localmessageids) ? null : new List<string> { localmessageids };
            string? finalTag = string.IsNullOrEmpty(tag) ? null : tag;
            string? finalHide = string.IsNullOrEmpty(hide) ? null : hide;
            string? finalPolicy = string.IsNullOrEmpty(policy) ? null : policy;

            var request = new SendArrayRequest
            {
                Senders = senders,
                Receptors = receptors,
                Messages = messages,
                Types = types,
                Date = DateTime.MinValue,
                LocalMessageIds = finalLocalMessageIds,
                Tag = finalTag,
                Hide = finalHide,
                Policy = finalPolicy,
                MediaId = mediaId
            };

            var result = api.SendArray(request);
            Assert.NotNull(result);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته با خط‌مشی", (int)MessageType.MobileMemory, "local-id-test", "", "", "mix", null)]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته با خط‌مشی و رسانه", (int)MessageType.MobileMemory, "local-id-test-media", "", "", "mix", "8c90c180-7b7f-f111-81cb-005056a53592")]
        [InlineData(RealApiKey, RealReceptor, RealSender, "تست ارسال پیشرفته بدون خط‌مشی", (int)MessageType.MobileMemory, "local-id-test-without-policy", "", "", null, null)]
        public async Task Test_Sms_SendArray_Extended_Async(
            string apiKey, 
            string receptor, 
            string sender, 
            string message, 
            int type, 
            string localmessageids, 
            string tag, 
            string hide, 
            string policy, 
            string mediaIdString)
        {
            var api = new KavenegarApi(apiKey);
            var senders = new List<string> { sender };
            var receptors = new List<string> { receptor };
            var messages = new List<string> { message };
            var types = new List<MessageType> { (MessageType)type };
            Guid? mediaId = string.IsNullOrEmpty(mediaIdString) || mediaIdString == Guid.Empty.ToString() ? (Guid?)null : Guid.Parse(mediaIdString);
            
            List<string>? finalLocalMessageIds = string.IsNullOrEmpty(localmessageids) ? null : new List<string> { localmessageids };
            string? finalTag = string.IsNullOrEmpty(tag) ? null : tag;
            string? finalHide = string.IsNullOrEmpty(hide) ? null : hide;
            string? finalPolicy = string.IsNullOrEmpty(policy) ? null : policy;

            var request = new SendArrayRequest
            {
                Senders = senders,
                Receptors = receptors,
                Messages = messages,
                Types = types,
                Date = DateTime.MinValue,
                LocalMessageIds = finalLocalMessageIds,
                Tag = finalTag,
                Hide = finalHide,
                Policy = finalPolicy,
                MediaId = mediaId
            };

            var result = await api.SendArrayAsync(request);
            Assert.NotNull(result);
        }
#endif

        [Fact]
        public void Test_Sms_Status_Single()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "تست بررسی وضعیت تکی");
            Assert.NotNull(sendRes);
            var status = api.Status(sendRes.Messageid.ToString());
            Assert.NotNull(status);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Status_Single_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست بررسی وضعیت تکی");
            Assert.NotNull(sendRes);
            var status = await api.StatusAsync(sendRes.Messageid.ToString());
            Assert.NotNull(status);
        }
#endif

        [Fact]
        public void Test_Sms_Status_List()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "تست بررسی وضعیت لیست");
            Assert.NotNull(sendRes);
            var statusList = api.Status(new List<string> { sendRes.Messageid.ToString() });
            Assert.NotNull(statusList);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Status_List_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست بررسی وضعیت لیست");
            Assert.NotNull(sendRes);
            var statusList = await api.StatusAsync(new List<string> { sendRes.Messageid.ToString() });
            Assert.NotNull(statusList);
        }
#endif

        [Fact]
        public void Test_Sms_StatusLocalMessageId_Single()
        {
            var api = new KavenegarApi(RealApiKey);
            string localId = "loc_" + DateTime.Now.Ticks;
            var sendRes = api.Send(RealSender, RealReceptor, "تست بررسی وضعیت محلی تکی", localId);
            Assert.NotNull(sendRes);
            var localStatus = api.StatusLocalMessageId(localId);
            Assert.NotNull(localStatus);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_StatusLocalMessageId_Single_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            string localId = "loc_" + DateTime.Now.Ticks;
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست بررسی وضعیت محلی تکی", localId);
            Assert.NotNull(sendRes);
            var localStatus = await api.StatusLocalMessageIdAsync(localId);
            Assert.NotNull(localStatus);
        }
#endif

        [Fact]
        public void Test_Sms_StatusLocalMessageId_List()
        {
            var api = new KavenegarApi(RealApiKey);
            string localId = "loc_list_" + DateTime.Now.Ticks;
            var sendRes = api.Send(RealSender, RealReceptor, "تست بررسی وضعیت محلی لیست", localId);
            Assert.NotNull(sendRes);
            var localStatusList = api.StatusLocalMessageId(new List<string> { localId });
            Assert.NotNull(localStatusList);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_StatusLocalMessageId_List_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            string localId = "loc_list_" + DateTime.Now.Ticks;
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست بررسی وضعیت محلی لیست", localId);
            Assert.NotNull(sendRes);
            var localStatusList = await api.StatusLocalMessageIdAsync(new List<string> { localId });
            Assert.NotNull(localStatusList);
        }
#endif

        [Fact]
        public void Test_Sms_Cancel_Single()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, new List<string> { RealReceptor }, "تست لغو پیام زمان‌بندی شده تکی", MessageType.MobileMemory, DateTime.Now.AddHours(2));
            Assert.NotNull(sendRes);
            var cancel = api.Cancel(sendRes[0].Messageid.ToString());
            Assert.NotNull(cancel);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Cancel_Single_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, new List<string> { RealReceptor }, "تست لغو پیام زمان‌بندی شده تکی", MessageType.MobileMemory, DateTime.Now.AddHours(2));
            Assert.NotNull(sendRes);
            var cancel = await api.CancelAsync(sendRes[0].Messageid.ToString());
            Assert.NotNull(cancel);
        }
#endif

        [Fact]
        public void Test_Sms_Cancel_List()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, new List<string> { RealReceptor }, "تست لغو پیام زمان‌بندی شده لیست", MessageType.MobileMemory, DateTime.Now.AddHours(2));
            Assert.NotNull(sendRes);
            var cancelList = api.Cancel(new List<string> { sendRes[0].Messageid.ToString() });
            Assert.NotNull(cancelList);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Cancel_List_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, new List<string> { RealReceptor }, "تست لغو پیام زمان‌بندی شده لیست", MessageType.MobileMemory, DateTime.Now.AddHours(2));
            Assert.NotNull(sendRes);
            var cancelList = await api.CancelAsync(new List<string> { sendRes[0].Messageid.ToString() });
            Assert.NotNull(cancelList);
        }
#endif

        [Fact]
        public void Test_Sms_Select_Single()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "تست انتخاب تکی");
            Assert.NotNull(sendRes);
            var selectSingle = api.Select(sendRes.Messageid.ToString());
            Assert.NotNull(selectSingle);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Select_Single_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست انتخاب تکی");
            Assert.NotNull(sendRes);
            var selectSingle = await api.SelectAsync(sendRes.Messageid.ToString());
            Assert.NotNull(selectSingle);
        }
#endif

        [Fact]
        public void Test_Sms_Select_List()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "تست انتخاب لیست");
            Assert.NotNull(sendRes);
            var selectList = api.Select(new List<string> { sendRes.Messageid.ToString() });
            Assert.NotNull(selectList);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Select_List_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست انتخاب لیست");
            Assert.NotNull(sendRes);
            var selectList = await api.SelectAsync(new List<string> { sendRes.Messageid.ToString() });
            Assert.NotNull(selectList);
        }
#endif

        [Fact]
        public void Test_Sms_SelectOutbox_1()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "تست انتخاب صندوق خروجی ۱");
            Assert.NotNull(sendRes);
            var outbox = api.SelectOutbox(DateTime.Now.AddDays(-1));
            Assert.NotNull(outbox);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SelectOutbox_1_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست انتخاب صندوق خروجی ۱");
            Assert.NotNull(sendRes);
            var outbox = await api.SelectOutboxAsync(DateTime.Now.AddHours(-1));
            Assert.NotNull(outbox);
        }
#endif

        [Fact]
        public void Test_Sms_SelectOutbox_2()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "تست انتخاب صندوق خروجی ۲");
            Assert.NotNull(sendRes);
            var outbox = api.SelectOutbox(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.NotNull(outbox);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SelectOutbox_2_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست انتخاب صندوق خروجی ۲");
            Assert.NotNull(sendRes);
            var outbox = await api.SelectOutboxAsync(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.NotNull(outbox);
        }
#endif

        [Fact]
        public void Test_Sms_SelectOutbox_3()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "تست انتخاب صندوق خروجی ۳");
            Assert.NotNull(sendRes);
            var outbox = api.SelectOutbox(DateTime.Now.AddDays(-1), DateTime.Now, RealSender);
            Assert.NotNull(outbox);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_SelectOutbox_3_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "تست انتخاب صندوق خروجی ۳");
            Assert.NotNull(sendRes);
            var outbox = await api.SelectOutboxAsync(DateTime.Now.AddDays(-1), DateTime.Now, RealSender);
            Assert.NotNull(outbox);
        }
#endif

        [Fact]
        public void Test_Sms_LatestOutbox_1()
        {
            var api = new KavenegarApi(RealApiKey);
            var latest = api.LatestOutbox(10);
            Assert.NotNull(latest);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_LatestOutbox_1_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var latest = await api.LatestOutboxAsync(10);
            Assert.NotNull(latest);
        }
#endif

        [Fact]
        public void Test_Sms_LatestOutbox_2()
        {
            var api = new KavenegarApi(RealApiKey);
            var latest = api.LatestOutbox(10, RealSender);
            Assert.NotNull(latest);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_LatestOutbox_2_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var latest = await api.LatestOutboxAsync(10, RealSender);
            Assert.NotNull(latest);
        }
#endif

        [Fact]
        public void Test_Sms_CountOutbox_1()
        {
            var api = new KavenegarApi(RealApiKey);
            var count = api.CountOutbox(DateTime.Now.AddDays(-1));
            Assert.NotNull(count);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_CountOutbox_1_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var count = await api.CountOutboxAsync(DateTime.Now.AddDays(-1));
            Assert.NotNull(count);
        }
#endif

        [Fact]
        public void Test_Sms_CountOutbox_2()
        {
            var api = new KavenegarApi(RealApiKey);
            var count = api.CountOutbox(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.NotNull(count);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_CountOutbox_2_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var count = await api.CountOutboxAsync(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.NotNull(count);
        }
#endif

        [Fact]
        public void Test_Sms_CountOutbox_3()
        {
            var api = new KavenegarApi(RealApiKey);
            var count = api.CountOutbox(DateTime.Now.AddDays(-1), DateTime.Now, 10);
            Assert.NotNull(count);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_CountOutbox_3_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var count = await api.CountOutboxAsync(DateTime.Now.AddDays(-1), DateTime.Now, 10);
            Assert.NotNull(count);
        }
#endif

        [Fact]
        public void Test_Sms_Receive()
        {
            var api = new KavenegarApi(RealApiKey);
            var receive = api.Receive(RealSender, 0);
            Assert.NotNull(receive);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Receive_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var receive = await api.ReceiveAsync(RealSender, 0);
            Assert.NotNull(receive);
        }
#endif

        [Fact]
        public void Test_Sms_CountInbox_1()
        {
            var api = new KavenegarApi(RealApiKey);
            var inboxCount = api.CountInbox(DateTime.Now.AddDays(-1), RealSender);
            Assert.NotNull(inboxCount);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_CountInbox_1_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var inboxCount = await api.CountInboxAsync(DateTime.Now.AddDays(-1), RealSender);
            Assert.NotNull(inboxCount);
        }
#endif

        [Fact]
        public void Test_Sms_CountInbox_2()
        {
            var api = new KavenegarApi(RealApiKey);
            var inboxCount = api.CountInbox(DateTime.Now.AddDays(-1), DateTime.Now, RealSender);
            Assert.NotNull(inboxCount);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_CountInbox_2_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var inboxCount = await api.CountInboxAsync(DateTime.Now.AddDays(-1), DateTime.Now, RealSender);
            Assert.NotNull(inboxCount);
        }
#endif

        [Fact]
        public void Test_Sms_CountInbox_3()
        {
            var api = new KavenegarApi(RealApiKey);
            var inboxCount = api.CountInbox(DateTime.Now.AddDays(-1), DateTime.Now, RealSender, 0);
            Assert.NotNull(inboxCount);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_CountInbox_3_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var inboxCount = await api.CountInboxAsync(DateTime.Now.AddDays(-1), DateTime.Now, RealSender, 0);
            Assert.NotNull(inboxCount);
        }
#endif


        [Fact]
        public void Test_Sms_StatusByReceptor()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = api.StatusByReceptor(RealReceptor, DateTime.Now.AddDays(-1), DateTime.Now.AddMinutes(-5));
            Assert.NotNull(result);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_StatusByReceptor_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = await api.StatusByReceptorAsync(RealReceptor, DateTime.Now.AddDays(-1), DateTime.Now.AddMinutes(-5));
            Assert.NotNull(result);
        }
#endif

        [Fact]
        public void Test_Sms_MakeReceive()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = api.Send(RealSender, RealReceptor, "پیام تست ادغام");
            Assert.NotNull(sendRes);
            var result = api.MakeReceive(RealSender, RealReceptor, sendRes.Messageid.ToString(), "پیام تست ادغام", 1);
            Assert.NotNull(result);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_MakeReceive_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var sendRes = await api.SendAsync(RealSender, RealReceptor, "پیام تست ادغام");
            Assert.NotNull(sendRes);
            var result = await api.MakeReceiveAsync(RealSender, RealReceptor, sendRes.Messageid.ToString(), "پیام تست ادغام", 1);
            Assert.NotNull(result);
        }
#endif

        [Fact]
        public void Test_Sms_Unreads()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = api.Unreads(RealSender, 0);
            Assert.NotNull(result);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_Unreads_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = await api.UnreadsAsync(RealSender, 0);
            Assert.NotNull(result);
        }
#endif

        [Fact]
        public void Test_Sms_InboxPaged()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = api.InboxPaged(RealSender, 1, DateTime.Now.AddDays(-2), DateTime.Now, 0);
            Assert.NotNull(result);
            Assert.NotNull(result.Metadata);
            Assert.NotNull(result.Entries);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_InboxPaged_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = await api.InboxPagedAsync(RealSender, 1, DateTime.Now.AddDays(-1), DateTime.Now, 1);
            Assert.NotNull(result);
            Assert.NotNull(result.Metadata);
            Assert.NotNull(result.Entries);
        }
#endif

        [Fact]
        public void Test_Sms_GroupSendReport()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = api.GroupSendReport(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.NotNull(result);
        }
#if !NET35
        [Fact]
        public async Task Test_Sms_GroupSendReport_Async()
        {
            var api = new KavenegarApi(RealApiKey);
            var result = await api.GroupSendReportAsync(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.NotNull(result);
        }
#endif
    }
}

