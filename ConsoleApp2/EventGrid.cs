using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AZ204.MessageEventDemo
{
    public static class EventGrid
    {
        public static Task Run(ICollection<Invoice> invoices)
        {
            var topicUri = "https://qinvoiceeventgrid.southeastasia-1.eventgrid.azure.net/api/events";
            var topicHost = new Uri(topicUri).Host;

            const string topicKey = "9dG9XyhdwIldO9P1fvw8lL7FnQWiuN7XJr16t+7YPC4=";
            var topicCredentials = new TopicCredentials(topicKey);

            var client = new EventGridClient(topicCredentials);

            return client.PublishEventsAsync(topicHost, GetEvents(invoices));
        }

        static IList<EventGridEvent> GetEvents(ICollection<Invoice> invoices)
        {
            return invoices.Select(invoice => new EventGridEvent()
            {
                Id = invoice.id.ToString(),
                EventType = "Arbago.Invoice.Created",
                Data = invoice,
                EventTime = DateTime.Now,
                Subject = "Invoice",
                DataVersion = "0.1"
            })
            .ToList();
        }
    }
}
