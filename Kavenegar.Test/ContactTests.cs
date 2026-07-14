using System.Threading.Tasks;
namespace Kavenegar.Test
{
    public class ContactTests
    {
        private const string RealApiKey = "<put your apikey here>";
        private const string RealReceptor = "<put your receptor here>";

        //new
        // API Tested: GET /v1/{api-key}/group/listgroups.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_ListGroups(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var groups = api.ListGroups();
            Assert.NotNull(groups);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_ListGroups_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var groups = await api.ListGroupsAsync();
            Assert.NotNull(groups);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/group/addgroup.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_AddGroup(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var newGroups = api.AddGroup("AddGroupTest1", "SaaS");
            Assert.Single(newGroups);
            var group = newGroups[0];
            Assert.NotNull(group);

            // Clean up
            api.RemoveGroup(group.GroupId);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_AddGroup_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var newGroups = await api.AddGroupAsync("AddGroupTest1", "SaaS");
            Assert.Single(newGroups);
            var group = newGroups[0];
            Assert.NotNull(group);

            // Clean up
            await api.RemoveGroupAsync(group.GroupId);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/group/editgroup.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_EditGroup(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("EditGroupTest", "SaaS-Edit")[0];
            
            var edited = api.EditGroup(group.GroupId, "EditGroupTestUpdated", null, "SaaS-Edit-Updated");
            Assert.Equal("EditGroupTestUpdated", edited[0].Name);

            // Clean up
            api.RemoveGroup(group.GroupId);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_EditGroup_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var group = (await api.AddGroupAsync("EditGroupTest", "SaaS-Edit"))[0];
            
            var edited = await api.EditGroupAsync(group.GroupId, "EditGroupTestUpdated", null, "SaaS-Edit-Updated");
            Assert.Equal("EditGroupTestUpdated", edited[0].Name);

            // Clean up
            await api.RemoveGroupAsync(group.GroupId);
        }
#endif

        //new
        // API Tested: GET /v1/{api-key}/group/searchgroups.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_SearchGroups(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("SearchGroupTest", "SaaS-Search")[0];

            var search = api.SearchGroups("SaaS-Search");
            Assert.NotNull(search);

            // Clean up
            api.RemoveGroup(group.GroupId);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_SearchGroups_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var group = (await api.AddGroupAsync("SearchGroupTest", "SaaS-Search"))[0];

            var search = await api.SearchGroupsAsync("SaaS-Search");
            Assert.NotNull(search);

            // Clean up
            await api.RemoveGroupAsync(group.GroupId);
        }
#endif

        //new
        // API Tested: DELETE /v1/{api-key}/group/removegroup.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_RemoveGroup(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("RemoveGroupTest", "SaaS-Remove")[0];

            var remainingGroups = api.RemoveGroup(group.GroupId);
            Assert.NotNull(remainingGroups);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_RemoveGroup_Async(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var group = (await api.AddGroupAsync("RemoveGroupTest", "SaaS-Remove"))[0];

            var remainingGroups = await api.RemoveGroupAsync(group.GroupId);
            Assert.NotNull(remainingGroups);
        }
#endif

        //new
        // API Tested: POST /v1/{api-key}/contact/add.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_AddContact(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("AddContactTest", "SaaS-Contact")[0];

            var contacts = api.AddContact(group.GroupId, receptor, "تست افزودن مخاطب");
            Assert.NotNull(contacts);

            // Clean up
            api.RemoveGroup(group.GroupId);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_AddContact_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = (await api.AddGroupAsync("AddContactTest", "SaaS-Contact"))[0];

            var contacts = await api.AddContactAsync(group.GroupId, receptor, "تست افزودن مخاطب");
            Assert.NotNull(contacts);

            // Clean up
            await api.RemoveGroupAsync(group.GroupId);
        }
#endif

        //new
        // API Tested: DELETE /v1/{api-key}/contact/remove.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_RemoveContact(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("RemoveContactTest", "SaaS-RemoveContact")[0];
            api.AddContact(group.GroupId, receptor, "تست حذف مخاطب");

            var remainingContacts = api.RemoveContact(null, receptor, group.GroupId);
            Assert.NotNull(remainingContacts);

            // Clean up
            api.RemoveGroup(group.GroupId);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_RemoveContact_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = (await api.AddGroupAsync("RemoveContactTest", "SaaS-RemoveContact"))[0];
            await api.AddContactAsync(group.GroupId, receptor, "تست حذف مخاطب");

            var remainingContacts = await api.RemoveContactAsync(null, receptor, group.GroupId);
            Assert.NotNull(remainingContacts);

            // Clean up
            await api.RemoveGroupAsync(group.GroupId);
        }
#endif
    }
}

