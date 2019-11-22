#!/bin/bash
$(aws ecr get-login --no-include-email --region us-east-1)
docker build -t gift-circle .
docker tag gift-circle:latest 540629508292.dkr.ecr.us-east-1.amazonaws.com/gift-circle:latest
docker push 540629508292.dkr.ecr.us-east-1.amazonaws.com/gift-circle:latest

add-apt-repository ppa:eugenesan/ppa
apt-get update
apt-get install jq -y

TASK=$(aws ecs describe-task-definition --task-definition gift-circle --output json | jq '.taskDefinition.containerDefinitions')
aws ecs register-task-definition --family gift-circle --container-definitions "$TASK" --task-role-arn arn:aws:iam::540629508292:role/ecsTaskExecutionRole --execution-role-arn arn:aws:iam::540629508292:role/ecsTaskExecutionRole

REVISION=$(aws ecs describe-task-definition --task-definition gift-circle --output json  | jq -r '.taskDefinition.revision')
aws ecs update-service --cluster default-ec2 --service gift-circle --task-definition gift-circle:$REVISION

#run chmod +x deploy.sh first if you have permissions issues

