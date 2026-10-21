#!/usr/bin/env bash
# Sends one or more test messages to the CADS DLQ (or any SQS FIFO queue) via
# the LocalStack endpoint, generating unique GUIDs for the message id,
# correlation id and deduplication id on every message.
#
# Usage:
#   ./scripts/sqs/send-dlq-test-messages.sh [count] [groupId[,groupId2,...]]
#
# Examples:
#   ./scripts/sqs/send-dlq-test-messages.sh                # sends 1 message, group "manual-test"
#   ./scripts/sqs/send-dlq-test-messages.sh 5               # sends 5 messages, all group "manual-test"
#   ./scripts/sqs/send-dlq-test-messages.sh 5 groupA,groupB # sends 5 messages, cycling through the two groups
#
# Environment overrides:
#   QUEUE_URL     - full queue URL (default: cads-cds DLQ)
#   ENDPOINT_URL  - localstack endpoint (default: http://localhost:4566)
#   REGION        - aws region (default: eu-west-2)
#   SUBJECT       - message attribute "Subject" value (default: S3ToPostgresCopy)

set -euo pipefail

QUEUE_URL="${QUEUE_URL:-http://cads-localstack:4566/000000000000/cads-cds-queue-deadletter.fifo}"
ENDPOINT_URL="${ENDPOINT_URL:-http://localhost:4566}"
REGION="${REGION:-eu-west-2}"
SUBJECT="${SUBJECT:-S3ToPostgresCopy}"

COUNT="${1:-1}"
IFS=',' read -r -a GROUP_IDS <<< "${2:-manual-test}"

new_guid() {
  if command -v uuidgen >/dev/null 2>&1; then
    uuidgen | tr '[:upper:]' '[:lower:]'
  else
    python3 -c 'import uuid; print(uuid.uuid4())'
  fi
}

for ((i = 1; i <= COUNT; i++)); do
  GROUP_ID="${GROUP_IDS[$(((i - 1) % ${#GROUP_IDS[@]}))]}"
  MESSAGE_ID=$(new_guid)
  CORRELATION_ID=$(new_guid)
  DEDUP_ID=$(new_guid)
  TIMESTAMP=$(date -u +%Y-%m-%dT%H:%M:%S.000Z)

  BODY="{\"id\":\"$MESSAGE_ID\",\"fileImportId\":$i,\"objectKey\":\"manual-test/test-$i.csv\",\"correlationId\":\"$CORRELATION_ID\"}"

  echo "[$i/$COUNT] group=$GROUP_ID dedup=$DEDUP_ID id=$MESSAGE_ID correlationId=$CORRELATION_ID"

  aws --endpoint-url "$ENDPOINT_URL" --region "$REGION" sqs send-message \
    --queue-url "$QUEUE_URL" \
    --message-group-id "$GROUP_ID" \
    --message-deduplication-id "$DEDUP_ID" \
    --message-body "$BODY" \
    --message-attributes "{
      \"Subject\":{\"DataType\":\"String\",\"StringValue\":\"$SUBJECT\"},
      \"CorrelationId\":{\"DataType\":\"String\",\"StringValue\":\"$CORRELATION_ID\"},
      \"EventTimeUtc\":{\"DataType\":\"String\",\"StringValue\":\"$TIMESTAMP\"},
      \"MessageGroupId\":{\"DataType\":\"String\",\"StringValue\":\"$GROUP_ID\"},
      \"MessageDeduplicationId\":{\"DataType\":\"String\",\"StringValue\":\"$DEDUP_ID\"}
    }" > /dev/null
done

echo "Done. Sent $COUNT message(s)."

