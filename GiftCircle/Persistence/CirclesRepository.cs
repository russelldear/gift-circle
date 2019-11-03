using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using GiftCircle.Models;
using Newtonsoft.Json;

namespace GiftCircle.Persistence
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
                clientConfig = new AmazonDynamoDBConfig {RegionEndpoint = RegionEndpoint.GetBySystemName(dynamoDbSettings.RegionEndpoint)};
            }
            else
            {
                clientConfig = new AmazonDynamoDBConfig {ServiceURL = dynamoDbSettings.ServiceUrl};
            }

            var dynamoDbClient = new AmazonDynamoDBClient(clientConfig);

            _circlesTable = Table.LoadTable(dynamoDbClient, CirclesTableName);
        }

        public async Task CreateCircle(Circle circle)
        {
            var circleAsJson = JsonConvert.SerializeObject(circle);
            
            var item = Document.FromJson(circleAsJson);

            await _circlesTable.PutItemAsync(item);
        }

        public async Task<Circle> GetCircle(Guid circleId)
        {
            var config = new GetItemOperationConfig
            {
                AttributesToGet = new List<string> { "Id", "UserId", "Name" },
                ConsistentRead = true
            };

            var document = await _circlesTable.GetItemAsync(circleId.ToString(), config);

            return document.ToCircle();
        }

        public async Task<List<Circle>> GetCircles(string userId)
        {
            var scanFilter = new ScanFilter();

            scanFilter.AddCondition("UserId", ScanOperator.Equal, userId);

            var search = _circlesTable.Scan(scanFilter);

            var documentList = new List<Document>();

            do
            {
                documentList.AddRange(await search.GetNextSetAsync());

            } while (!search.IsDone);

            return documentList.ToCircles();
        }
    }
}
