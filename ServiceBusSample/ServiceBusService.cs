using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;

namespace ServiceBusSample
{
    public class ServiceBusService
    {

        private IServiceBusNamespace serviceBusNamespace { get; set; }

        public ServiceBusService()
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(Constant.CLIENT_ID, Constant.CLIENT_SECRET, Constant.TENANT_ID,
                AzureEnvironment.AzureGlobalCloud);
            var serviceBusManager = ServiceBusManager.Authenticate(credentials, Constant.SUBSCRIPTION_ID);
            serviceBusNamespace = serviceBusManager.Namespaces.GetByResourceGroup(Constant.RESOURCE_GROUP, Constant.RESOURCE_NAME);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicPath"></param>
        /// <returns></returns>
        public async Task<List<Microsoft.Azure.Management.ServiceBus.Fluent.ISubscription>> GetSubscriptions(string topicName)
        {
            var topics = await serviceBusNamespace.Topics.ListAsync();
            return topics.FirstOrDefault(t => t.Name == topicName)?
                .Subscriptions
                .List()
                .ToList();
        }
    }
}
