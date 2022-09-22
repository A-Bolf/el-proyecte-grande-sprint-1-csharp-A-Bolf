using SiteRep.Infrastructure.Tests.Common;
using SiteRep.Infrastructure.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SiteRep.Infrastructure.Tests
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
