using System.Threading.Tasks;
using Gw2Sharp.WebApi.V2.Clients;
using Xunit;

namespace Gw2Sharp.Tests.WebApi.V2.Clients
{
    public class AccountMasteryPointsClientTests : BaseEndpointClientTests<IAccountMasteryPointsClient>
    {
        protected override bool IsAuthenticated => true;

        protected override IAccountMasteryPointsClient CreateClient(IGw2Client gw2Client) =>
            gw2Client.WebApi.V2.Account.Mastery.Points;

        [Theory]
        [InlineData("TestFiles.Account.AccountMasteryPoints.json")]
        public Task BlobTest(string file) => this.AssertBlobDataAsync(this.Client, file);
    }
}
