# docker volume create dynamodb-data

# docker-compose pull && \
# docker-compose \
#     -f docker-compose.yml \
#     up -d --build --remove-orphans

docker run -d -p 5006:8000 --name gift-circle-dynamo amazon/dynamodb-local -jar DynamoDBLocal.jar -inMemory -sharedDb 

aws dynamodb create-table --endpoint-url http://localhost:5006 --table-name GiftCircle-Circles --attribute-definitions AttributeName=Id,AttributeType=S --key-schema AttributeName=Id,KeyType=HASH --provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1

aws dynamodb list-tables --endpoint-url http://localhost:5006 
