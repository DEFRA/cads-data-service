#!/bin/bash
sed -i 's/\r$//' "$0"
export AWS_REGION=eu-west-2
export AWS_DEFAULT_REGION=eu-west-2
export AWS_ACCESS_KEY_ID=test
export AWS_SECRET_ACCESS_KEY=test
export ENDPOINT_URL=http://localhost:4566
set -e

# S3
echo "Creating S3 resources..."

## S3: Create 'cads-ingest-bucket' bucket
existing_bucket=$(awslocal s3api list-buckets \
  --query "Buckets[?Name=='cads-ingest-bucket'].Name" \
  --output text)

if [[ "$existing_bucket" == "cads-ingest-bucket" ]]; then
  echo "S3 bucket already exists: cads-ingest-bucket"
else
  awslocal s3api create-bucket --bucket cads-ingest-bucket --region eu-west-2 \
    --create-bucket-configuration LocationConstraint=eu-west-2 \
    --endpoint-url=http://localhost:4566
  echo "S3 bucket created: cads-ingest-bucket"
fi

# SQS
echo "Creating SQS resources..."

## S3: Create 'cads_ingest_queue' DLQ
dlq_url=$(awslocal sqs create-queue \
  --queue-name cads_ingest_queue-deadletter \
  --endpoint-url=http://localhost:4566 \
  --output text \
  --query 'QueueUrl')
echo "'cads_ingest_queue' DLQ created: $dlq_url"

# Get the ARN of the 'cads_ingest_queue' DLQ, which is needed for the redrive policy
dlq_arn=$(awslocal sqs get-queue-attributes \
  --queue-url "$dlq_url" \
  --attribute-names QueueArn \
  --endpoint-url=http://localhost:4566 \
  --output text \
  --query 'Attributes.QueueArn')
echo "'cads_ingest_queue' DLQ ARN: $dlq_arn"

# Create 'cads_ingest_queue' queue
queue_url=$(awslocal sqs create-queue \
  --queue-name cads_ingest_queue \
  --endpoint-url=http://localhost:4566 \
  --output text \
  --query 'QueueUrl')
echo "'cads_ingest_queue' queue created: $queue_url"

# Define the Redrive Policy, linking the main queue to the DLQ.
redrive_policy_json=$(cat <<EOF
{
  "deadLetterTargetArn": "$dlq_arn",
  "maxReceiveCount": "3"
}
EOF
)

# Set redrive policy for 'cads_ingest_queue' DLQ
echo "Set redrive policy for 'cads_ingest_queue' DLQ..."
awslocal sqs set-queue-attributes \
  --queue-url "$queue_url" \
  --attributes "{\"RedrivePolicy\":\"{\\\"deadLetterTargetArn\\\":\\\"$dlq_arn\\\",\\\"maxReceiveCount\\\":\\\"3\\\"}\"}" \
  --endpoint-url=$ENDPOINT_URL
# =================================================================

# Get the 'cads_ingest_queue' queue ARN
queue_arn=$(awslocal sqs get-queue-attributes \
  --queue-url "$queue_url" \
  --attribute-name QueueArn \
  --endpoint-url=http://localhost:4566 \
  --output text \
  --query 'Attributes.QueueArn')
echo "'cads_ingest_queue' queue ARN: $queue_arn"

echo "Bootstrapping Complete"