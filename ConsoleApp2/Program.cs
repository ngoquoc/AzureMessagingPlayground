using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AZ204.MessageEventDemo
{
    class Program
    {
        static readonly Dictionary<string, Action<ICollection<Invoice>>> _functions = 
            new Dictionary<string, Action<ICollection<Invoice>>>
            {
                ["ehub"] = (invoices) => EventHub.Run(invoices).Wait(),
                ["egrid"] = (invoices) => EventGrid.Run(invoices).Wait(),
                ["sbus"] = (invoices) => ServiceBus.Run(invoices).Wait(),
                ["queue"] = (invoices) => Queue.Run(invoices).Wait()
            };

        static void Main(string types, int num = 10)
        {
            var runTypes = (types ?? "ehub|egrid|sbus|queue").Split("|");
            Console.WriteLine($@"Num = {num}, types = ""{string.Join(",", runTypes)}""");

            var invoicesJson = File.ReadAllText("fake_invoices.json");
            var invoices = JsonSerializer.Deserialize<IReadOnlyCollection<Invoice>>(invoicesJson)
                .Skip(1000).Take(num).ToArray();
            foreach (var type in runTypes)
            {
                Console.WriteLine($"Publishing {invoices.Length} invoices to {type} ...");
                _functions[type].Invoke(invoices);
            }
        }
    }
}
