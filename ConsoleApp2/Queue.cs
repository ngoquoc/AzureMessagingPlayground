using Azure.Storage.Queues;
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
    // Chỉ gởi được 1 message đồng thời
    public static class Queue
    {
        public static Task Run(ICollection<Invoice> invoices)
        {
            const string connString = "DefaultEndpointsProtocol=https;AccountName=qinvoice;AccountKey=sI+AeC9kPqQLNjIAj4NpGob+gF5YRlcu9apEz7qEgI1CMnT6KXn0aWCACwYuMBWjOtgNuHHgGlxk4YNmG7ixHQ==;EndpointSuffix=core.windows.net";
            const string queueName = "invoices";

            var client = new QueueClient(connString, queueName);
            foreach (var invoice in invoices)
            {
                var serializedInvoice = JsonSerializer.Serialize(invoice);
                // Azure Function queue binding expects base64 message -> WTF?
                var message = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedInvoice));
                client.SendMessageAsync(message).Wait();
            }

            return Task.FromResult(0);
        }
    }
}
