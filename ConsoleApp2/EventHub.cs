using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AZ204.MessageEventDemo
{
    /// <summary>
    /// Có vẻ không phù hợp cho message lớn - API không support gởi payload dạng object :D
    /// </summary>
    public static class EventHub
    {
        public static Task Run(ICollection<Invoice> invoices)
        {
            const string connString = "Endpoint=sb://qinvoiceeventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YbadorIAHmGcjK3uV1Ad5sOo9EVXAkhr+YIswD5hF6w=";
            const string eHubName = "invoices";

            var client = new EventHubProducerClient(connString, eHubName);
            return client.SendAsync(GetEventsBatch(invoices));
        }

        private static IEnumerable<EventData> GetEventsBatch(ICollection<Invoice> invoices)
        {
            return invoices.Select(i => new EventData(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(i))));
        }
    }
}
