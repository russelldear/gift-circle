using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;

namespace GiftCircle.Models
{
    public static class ModelExtensions
    {
        public static Circle ToCircle(this Document document)
        {
            return new Circle
            {
                Id = Guid.Parse(document["Id"]),
                UserId = document["UserId"],
                Name = document["Name"]
            };
        }

        public static List<Circle> ToCircles(this List<Document> documents)
        {
            var result = new List<Circle>();

            foreach (var document in documents)
            {
                result.Add(document.ToCircle());
            }
            
            return result;
        }
    }
}