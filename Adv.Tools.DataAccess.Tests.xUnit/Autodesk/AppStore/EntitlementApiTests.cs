using Adv.Tools.DataAccess.Autodesk.AppStore;
using System;
using System.Linq;
using Xunit;

namespace Adv.Tools.DataAccess.Tests.xUnit.Autodesk.AppStore
{
    public class EntitlementApiTests
    {
        static readonly string baseUrl = @"https://apps.exchange.autodesk.com/";
        static readonly string endPoint = @"webservices/checkentitlement";
        static readonly string appId = @"D8EF9571-6E04-4BAE-8ABA-B847117E6541";
        static readonly string userId = @"XJ8709FK9";

        [Fact]
        public void CheckEntitlementModelResponse_Successful()
        {
            var entitlementApi = new EntitlementApi(baseUrl, endPoint);
            entitlementApi.CheckEntitlement(appId, userId);

            Assert.Equal(appId, entitlementApi.EntitlementResposne.AppId);
            Assert.Equal(userId, entitlementApi.EntitlementResposne.UserId);
            Assert.Equal("Ok", entitlementApi.EntitlementResposne.Message);
        }

        [Fact]
        public void CheckEntitlementApiResponse_Successful()
        {
            var entitlementApi = new EntitlementApi(baseUrl, endPoint);
            bool Entitlement = entitlementApi.CheckEntitlement(appId, userId);

            Assert.False(Entitlement);
        }


    }
}
