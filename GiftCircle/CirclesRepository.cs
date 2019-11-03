using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace GiftCircle
{
    public class CirclesRepository
    {
        private const string CirclesTableName = "GiftCircle-Circles";

        private readonly Table _circlesTable;

        public CirclesRepository(DynamoDbSettings dynamoDbSettings)
        {
            AmazonDynamoDBConfig clientConfig;

            if (string.IsNullOrWhiteSpace(dynamoDbSettings.ServiceUrl))
            {
                clientConfig = new AmazonDynamoDBConfig
                    {RegionEndpoint = RegionEndpoint.GetBySystemName(dynamoDbSettings.RegionEndpoint)};
            }
            else
            {
                clientConfig = new AmazonDynamoDBConfig {RegionEndpoint = RegionEndpoint.USEast1};
                clientConfig.ServiceURL = dynamoDbSettings.ServiceUrl;
            }

            var dynamoDbClient = new AmazonDynamoDBClient(clientConfig);

            _circlesTable = Table.LoadTable(dynamoDbClient, CirclesTableName);
        }

        public async Task<Guid> CreateCircle(string userId, string name)
        {
            var circle = new Circle {Id = Guid.NewGuid(), UserId = userId, Name = name};

            var circleAsJson = JsonConvert.SerializeObject(circle);
            
            var item = Document.FromJson(circleAsJson);

            await _circlesTable.PutItemAsync(item);

            return circle.Id;
        }

        public async Task<Circle> GetCircle(Guid circleId)
        {
            var config = new GetItemOperationConfig
            {
                AttributesToGet = new List<string> { "Id", "UserId", "Name" },
                ConsistentRead = true
            };

            var document = await _circlesTable.GetItemAsync(circleId.ToString(), config);

            return Map(document);
        }

        private Circle Map(Document document)
        {
            return new Circle
            {
                Id = Guid.Parse(document["Id"]),
                UserId = document["UserId"],
                Name = document["Name"]
            };
        }
    }
}
