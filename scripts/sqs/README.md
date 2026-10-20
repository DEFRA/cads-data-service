# SQS Test Scripts

Helper scripts for manually sending test messages to SQS queues (e.g. the
`cads-cds-queue-deadletter.fifo` DLQ) running in the local LocalStack
container, without having to hand-craft `aws sqs send-message` commands and
unique GUIDs yourself.

Each message sent gets a fresh, unique:
- `id` (message body)
- `correlationId` (message body + `CorrelationId` message attribute)
- `MessageDeduplicationId` (FIFO queue dedup id)

Two equivalent scripts are provided so you can use whichever shell you have
available:

| Script | Shell |
|---|---|
| `send-dlq-test-messages.sh` | bash (macOS, Linux, WSL, Git Bash, Cygwin) |
| `send-dlq-test-messages.ps1` | PowerShell (Windows, or `pwsh` cross-platform) |

## Prerequisites

- [AWS CLI](https://aws.amazon.com/cli/) installed and available on `PATH`.
- LocalStack running locally (e.g. via `./platform/platform.sh cds` or
  `docker-compose up`), reachable at `http://localhost:4566` by default.

## bash usage

```bash
# Send 1 message, group "manual-test" (the default)
./scripts/sqs/send-dlq-test-messages.sh

# Send 5 messages, all group "manual-test"
./scripts/sqs/send-dlq-test-messages.sh 5

# Send 6 messages, cycling through two different message group ids
./scripts/sqs/send-dlq-test-messages.sh 6 groupA,groupB
```

Make it executable once (if needed):

```bash
chmod +x ./scripts/sqs/send-dlq-test-messages.sh
```

### Overriding defaults (env vars)

| Variable | Default | Description |
|---|---|---|
| `QUEUE_URL` | `http://cads-localstack:4566/000000000000/cads-cds-queue-deadletter.fifo` | Full queue URL to send to |
| `ENDPOINT_URL` | `http://localhost:4566` | LocalStack endpoint |
| `REGION` | `eu-west-2` | AWS region |
| `SUBJECT` | `S3ToPostgresCopy` | `Subject` message attribute value |

```bash
QUEUE_URL="http://cads-localstack:4566/000000000000/some-other-queue.fifo" \
  ./scripts/sqs/send-dlq-test-messages.sh 10
```

## PowerShell usage

```powershell
# Send 1 message, group "manual-test" (the default)
./scripts/sqs/send-dlq-test-messages.ps1

# Send 5 messages, all group "manual-test"
./scripts/sqs/send-dlq-test-messages.ps1 -Count 5

# Send 6 messages, cycling through two different message group ids
./scripts/sqs/send-dlq-test-messages.ps1 -Count 6 -GroupIds "groupA,groupB"
```

### Overriding defaults (parameters)

| Parameter | Default | Description |
|---|---|---|
| `-QueueUrl` | `http://cads-localstack:4566/000000000000/cads-cds-queue-deadletter.fifo` | Full queue URL to send to |
| `-EndpointUrl` | `http://localhost:4566` | LocalStack endpoint |
| `-Region` | `eu-west-2` | AWS region |
| `-Subject` | `S3ToPostgresCopy` | `Subject` message attribute value |

```powershell
./scripts/sqs/send-dlq-test-messages.ps1 -Count 10 -QueueUrl "http://cads-localstack:4566/000000000000/some-other-queue.fifo"
```

If PowerShell's execution policy blocks running the script, either run it
with:

```powershell
pwsh -File ./scripts/sqs/send-dlq-test-messages.ps1 -Count 5
```

or temporarily relax the policy for the current process:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
./scripts/sqs/send-dlq-test-messages.ps1 -Count 5
```

## Which one should I use?

- **macOS / Linux** → use the `.sh` script directly.
- **Windows with WSL or Git Bash** → use the `.sh` script directly (GUIDs
  fall back to `python3` if `uuidgen` isn't available).
- **Windows (native PowerShell, no WSL/Git Bash)** → use the `.ps1` script.

