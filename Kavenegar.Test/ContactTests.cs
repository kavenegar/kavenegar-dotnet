using System.Threading.Tasks;
namespace Kavenegar.Test
{
    public class ContactTests
    {
        private const string RealApiKey = "53732F653245324C4651484E5A4F3166354672636A706B564D666D79587566574573344E687A4F65716F733D";
        private const string RealReceptor = "09912064992";

        //new
        // API Tested: GET /v1/{api-key}/group/listgroups.json
        [Theory]
        [InlineData(RealApiKey)]
        public void Test_ListGroups_Real(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var groups = api.ListGroups();
            Assert.NotNull(groups);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_ListGroups_Real_Async(string apiKey)
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
        public void Test_AddGroup_Real(string apiKey)
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
        public async System.Threading.Tasks.Task Test_AddGroup_Real_Async(string apiKey)
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
        public void Test_EditGroup_Real(string apiKey)
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
        public async System.Threading.Tasks.Task Test_EditGroup_Real_Async(string apiKey)
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
        public void Test_SearchGroups_Real(string apiKey)
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
        public async System.Threading.Tasks.Task Test_SearchGroups_Real_Async(string apiKey)
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
        public void Test_RemoveGroup_Real(string apiKey)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("RemoveGroupTest", "SaaS-Remove")[0];

            var remainingGroups = api.RemoveGroup(group.GroupId);
            Assert.NotNull(remainingGroups);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey)]
        public async System.Threading.Tasks.Task Test_RemoveGroup_Real_Async(string apiKey)
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
        public void Test_AddContact_Real(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("AddContactTest", "SaaS-Contact")[0];

            var contacts = api.AddContact(group.GroupId, receptor, "John Doe Add Contact Test");
            Assert.NotNull(contacts);

            // Clean up
            api.RemoveGroup(group.GroupId);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_AddContact_Real_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = (await api.AddGroupAsync("AddContactTest", "SaaS-Contact"))[0];

            var contacts = await api.AddContactAsync(group.GroupId, receptor, "John Doe Add Contact Test");
            Assert.NotNull(contacts);

            // Clean up
            await api.RemoveGroupAsync(group.GroupId);
        }
#endif

        //new
        // API Tested: DELETE /v1/{api-key}/contact/remove.json
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public void Test_RemoveContact_Real(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = api.AddGroup("RemoveContactTest", "SaaS-RemoveContact")[0];
            api.AddContact(group.GroupId, receptor, "John Doe Remove Contact Test");

            var remainingContacts = api.RemoveContact(null, receptor, group.GroupId);
            Assert.NotNull(remainingContacts);

            // Clean up
            api.RemoveGroup(group.GroupId);
        }
#if !NET35
        [Theory]
        [InlineData(RealApiKey, RealReceptor)]
        public async System.Threading.Tasks.Task Test_RemoveContact_Real_Async(string apiKey, string receptor)
        {
            var api = new KavenegarApi(apiKey);
            var group = (await api.AddGroupAsync("RemoveContactTest", "SaaS-RemoveContact"))[0];
            await api.AddContactAsync(group.GroupId, receptor, "John Doe Remove Contact Test");

            var remainingContacts = await api.RemoveContactAsync(null, receptor, group.GroupId);
            Assert.NotNull(remainingContacts);

            // Clean up
            await api.RemoveGroupAsync(group.GroupId);
        }
#endif
    }
}

