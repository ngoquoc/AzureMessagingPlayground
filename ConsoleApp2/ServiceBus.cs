using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AZ204.MessageEventDemo
{
    class ServiceBus
    {
        public static Task Run(ICollection<Invoice> invoices)
        {
            const string connString = "Endpoint=sb://qinvoicesbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YvcFecwuc+Ezke+z7Ys08LAz4WfK3Ni+Udmy9aIMbZs=";
            const string entityPath = "invoices";

            var client = new QueueClient(connString, entityPath);
            return client.SendAsync(GetMessages(invoices));
        }

        private static IList<Message> GetMessages(ICollection<Invoice> invoices)
        {
            return invoices.Select(i => new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(i))))
                .ToList();
        }
    }
}
