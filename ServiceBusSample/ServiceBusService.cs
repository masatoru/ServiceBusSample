using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus.Management;
using ISubscriptions = Microsoft.Azure.Management.ServiceBus.Fluent.ISubscriptions;

namespace ServiceBusSample
{
    public class ServiceBusService
    {
        private const string CLIENT_ID = "";
        private const string CLIENT_SECRET = "";
        private const string TENANT_ID = "";
        private const string SUBSCRIPTION_ID = "";
        private const string RESOURCE_GROUP = "";
        private const string RESOURCE_NAME = "";

        private IServiceBusNamespace serviceBusNamespace { get; set; }

        public ServiceBusService()
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(CLIENT_ID, CLIENT_SECRET, TENANT_ID,
                AzureEnvironment.AzureGlobalCloud);
            var serviceBusManager = ServiceBusManager.Authenticate(credentials, SUBSCRIPTION_ID);
            serviceBusNamespace = serviceBusManager.Namespaces.GetByResourceGroup(RESOURCE_GROUP, RESOURCE_NAME);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topicPath"></param>
        /// <returns></returns>
        public async Task<ISubscriptions> GetSubscriptions(string topicName)
        {
            var topics = await serviceBusNamespace.Topics.ListAsync();
            return topics.FirstOrDefault(t => t.Name == topicName)?
                .Subscriptions;
        }
    }
}
