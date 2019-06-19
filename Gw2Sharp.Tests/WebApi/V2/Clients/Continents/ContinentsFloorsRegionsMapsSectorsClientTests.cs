using System.Threading.Tasks;
using Gw2Sharp.Tests.Helpers;
using Gw2Sharp.WebApi;
using Gw2Sharp.WebApi.Caching;
using Gw2Sharp.WebApi.Http;
using Gw2Sharp.WebApi.V2.Clients;
using NSubstitute;
using Xunit;

namespace Gw2Sharp.Tests.WebApi.V2.Clients
{
    public class ContinentsFloorsRegionsMapsSectorsClientTests : BaseEndpointClientTests
    {
        public ContinentsFloorsRegionsMapsSectorsClientTests()
        {
            var connection = new Connection(string.Empty, Locale.English, Substitute.For<IHttpClient>(), new NullCacheMethod());
            this.client = new Gw2WebApiClient(connection).V2.Continents[1].Floors[0].Regions[1].Maps[1].Sectors;
            this.Client = this.client;
        }

        private readonly IContinentsFloorsRegionsMapsSectorsClient client;

        [Theory]
        [InlineData("TestFiles.Continents.ContinentsFloorsRegionsMapsSectors.bulk.json")]
        public Task PaginatedTestAsync(string file) => this.AssertPaginatedDataAsync(this.client, file);

        [Theory]
        [InlineData("TestFiles.Continents.ContinentsFloorsRegionsMapsSectors.single.json")]
        public Task GetTestAsync(string file) => this.AssertGetDataAsync(this.client, file);

        [Theory]
        [InlineData("TestFiles.Continents.ContinentsFloorsRegionsMapsSectors.bulk.json")]
        public Task BulkTestAsync(string file) => this.AssertBulkDataAsync(this.client, file);

        [Theory]
        [InlineData("TestFiles.Continents.ContinentsFloorsRegionsMapsSectors.bulk.json")]
        public Task AllTestAsync(string file) => this.AssertAllDataAsync(this.client, file);

        [Theory]
        [InlineData("TestFiles.Continents.ContinentsFloorsRegionsMapsSectors.ids.json")]
        public Task IdsTestAsync(string file) => this.AssertIdsDataAsync(this.client, file);


        #region ArgumentNullException tests

        [Fact]
        public override void ArgumentNullConstructorTest()
        {
            AssertArguments.ThrowsWhenNullConstructor(
                this.Client.GetType(),
                new[] { typeof(IConnection), typeof(int), typeof(int), typeof(int), typeof(int) },
                new object[] { new Connection(), 1, 0, 1, 1 },
                new[] { true, false, false, false, false });
        }

        #endregion
    }
}