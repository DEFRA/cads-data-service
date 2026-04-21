#!/usr/bin/env bash
set -euo pipefail

# Linux make this executable via: chmod +x run-backend.sh

# Path to the platform root (backend repo)
PLATFORM_DIR="../cads-data-service"

COMMAND="${1:-up}"

case "$COMMAND" in
  up)
    echo "[backend] Starting tools + backend..."
    cd "$PLATFORM_DIR"
    ./platform/platform.sh backend
    ;;
  down)
    echo "[backend] Stopping tools + backend..."
    cd "$PLATFORM_DIR"
    ./platform/platform.sh down
    ;;
  restart)
    echo "[backend] Restarting tools + backend..."
    cd "$PLATFORM_DIR"
    ./platform/platform.sh down
    ./platform/platform.sh backend
    ;;
  *)
    echo "Usage:"
    echo "  ./run-backend.sh up       # Start tools + backend"
    echo "  ./run-backend.sh down     # Stop tools + backend"
    echo "  ./run-backend.sh restart  # Restart tools + backend"
    ;;
esac