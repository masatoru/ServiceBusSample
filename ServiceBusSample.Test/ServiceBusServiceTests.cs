using System.Threading.Tasks;
using NUnit.Framework;
using ServiceBusSample;

namespace Tests
{
    public class ServiceBusServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetSubscriptionList()
        {
            var service = new ServiceBusService();
            var subscriptionList = await service.GetSubscriptions("topic1");

            Assert.AreEqual(3, subscriptionList.Count);
            Assert.AreEqual("sub1", subscriptionList[0].Name);
            Assert.AreEqual("sub2", subscriptionList[1].Name);
            Assert.AreEqual("sub3", subscriptionList[2].Name);
        }
    }
}