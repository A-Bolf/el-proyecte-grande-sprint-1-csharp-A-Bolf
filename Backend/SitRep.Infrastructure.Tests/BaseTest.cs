using System.Reflection;
using SiteRep.Infrastructure.Tests.Extensions;
using SitRep.Infrastructure.Tests.Common;

namespace SitRep.Infrastructure.Tests
{
    public abstract class BaseTest
    {
        protected HttpClient Client;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            TestingWebAppFactory<Program> factory = new();
            Client = factory.CreateClient();
        }
        public BaseTest()
        {
        }
        protected static string GetDataStructureFromJson(string resource)
        {
            var structure = Assembly.GetExecutingAssembly().GetEmbeddedResourceContent(resource);
            if (structure == null)
            {
                return String.Empty;
            }
            return structure;
        }
    }
}
