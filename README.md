# Azure messaging services playground

## How to run

```bash
Usage:
  AZ204.MessageEventDemo [options]

Options:
  --types <types>    types
  --num <num>        num [default: 10]
  --version          Show version information
  -?, -h, --help     Show help and usage information
```

## Key takeaways

1. Service bus

- Package `Microsoft.Azure.ServiceBus`

- Usage

  ```csharp
  const string connString = "";
  const string entityPath = "invoices";

  var client = new QueueClient(connString, entityPath);

  var messages = new List<Message>()
  {
    new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(i)
  };

  return client.SendAsync(messages);
  ```

2. Queue

- Package `Azure.Storage.Queues`
- Azure function Queue binding expects always Base64 string.

3. Event grid

Package `Microsoft.Azure.EventGrid`

4. Event hub

Package `Azure.Messaging.EventHubs`

## Necessary tools and references

1. Azure storage explorer
https://azure.microsoft.com/en-us/features/storage-explorer/