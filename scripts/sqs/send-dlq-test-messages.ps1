<#
.SYNOPSIS
    Sends one or more test messages to the CADS DLQ (or any SQS FIFO queue) via
    the LocalStack endpoint, generating unique GUIDs for the message id,
    correlation id and deduplication id on every message.

.DESCRIPTION
    PowerShell equivalent of scripts/sqs/send-dlq-test-messages.sh, for native
    Windows use (no WSL/Git Bash required). Requires the AWS CLI to be on PATH.

.PARAMETER Count
    Number of messages to send. Defaults to 1.

.PARAMETER GroupIds
    Comma-separated list of message group ids to cycle through. Defaults to "manual-test".

.PARAMETER QueueUrl
    Full queue URL. Defaults to the cads-cds DLQ.

.PARAMETER EndpointUrl
    LocalStack endpoint. Defaults to http://localhost:4566.

.PARAMETER Region
    AWS region. Defaults to eu-west-2.

.PARAMETER Subject
    Message attribute "Subject" value. Defaults to S3ToPostgresCopy.

.EXAMPLE
    ./scripts/sqs/send-dlq-test-messages.ps1
    Sends 1 message, group "manual-test"

.EXAMPLE
    ./scripts/sqs/send-dlq-test-messages.ps1 -Count 5
    Sends 5 messages, all group "manual-test"

.EXAMPLE
    ./scripts/sqs/send-dlq-test-messages.ps1 -Count 6 -GroupIds "groupA,groupB"
    Sends 6 messages, cycling through the two groups
#>

[CmdletBinding()]
param(
    [int]$Count = 1,
    [string]$GroupIds = "manual-test",
    [string]$QueueUrl = "http://cads-localstack:4566/000000000000/cads-cds-queue-deadletter.fifo",
    [string]$EndpointUrl = "http://localhost:4566",
    [string]$Region = "eu-west-2",
    [string]$Subject = "S3ToPostgresCopy"
)

$ErrorActionPreference = "Stop"

$groupIdList = $GroupIds -split ","

for ($i = 1; $i -le $Count; $i++) {
    $groupId = $groupIdList[($i - 1) % $groupIdList.Length]
    $messageId = [guid]::NewGuid().ToString()
    $correlationId = [guid]::NewGuid().ToString()
    $dedupId = [guid]::NewGuid().ToString()
    $timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.000Z")

    $body = "{`"id`":`"$messageId`",`"fileImportId`":$i,`"objectKey`":`"manual-test/test-$i.csv`",`"correlationId`":`"$correlationId`"}"

    $attributes = @"
{
  "Subject":{"DataType":"String","StringValue":"$Subject"},
  "CorrelationId":{"DataType":"String","StringValue":"$correlationId"},
  "EventTimeUtc":{"DataType":"String","StringValue":"$timestamp"},
  "MessageGroupId":{"DataType":"String","StringValue":"$groupId"},
  "MessageDeduplicationId":{"DataType":"String","StringValue":"$dedupId"}
}
"@

    Write-Host "[$i/$Count] group=$groupId dedup=$dedupId id=$messageId correlationId=$correlationId"

    aws --endpoint-url $EndpointUrl --region $Region sqs send-message `
        --queue-url $QueueUrl `
        --message-group-id $groupId `
        --message-deduplication-id $dedupId `
        --message-body $body `
        --message-attributes $attributes | Out-Null
}

Write-Host "Done. Sent $Count message(s)."

