#!/usr/bin/env bash
set -euo pipefail

# Linux make this executable via: chmod +x platform.sh

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

BACKEND_DIR="$ROOT_DIR/.."
TOOLS_DIR="$BACKEND_DIR/../cads-tools"
UI_DIR="$BACKEND_DIR/../cads-mis"

COMMAND="${1:-help}"
MAC_OVERRIDE="${2:-}"

# Determine which override file to use
compose_override() {
  case "$MAC_OVERRIDE" in
    --mac-intel)
      echo "docker-compose.override.mac.intel.yml"
      ;;
    --mac-arm)
      echo "docker-compose.override.mac.arm.yml"
      ;;
    *)
      echo "docker-compose.override.yml"
      ;;
  esac
}

start_tools() {
  echo "[platform] Starting shared infra..."
  "$TOOLS_DIR/harness/run-harness.sh" up
  return $?
}

stop_tools() {
  echo "[platform] Stopping shared infra..."
  "$TOOLS_DIR/harness/run-harness.sh" down
  return $?
}

start_backend() {
  echo "[platform] Starting backend..."
  cd "$BACKEND_DIR"

  OVERRIDE_FILE=$(compose_override)
  echo "[platform] Using override: $OVERRIDE_FILE"

  docker compose \
    -f docker-compose.yml \
    -f "$OVERRIDE_FILE" \
    up -d

  return $?
}

stop_backend() {
  echo "[platform] Stopping backend..."
  cd "$BACKEND_DIR"
  docker compose down || true
  return $?
}

start_ui() {
  echo "[platform] Starting UI..."
  cd "$UI_DIR"
  docker compose up -d
  return $?
}

stop_ui() {
  echo "[platform] Stopping UI..."
  cd "$UI_DIR"
  docker compose down || true
  return $?
}

case "$COMMAND" in
  tools)
    start_tools
    ;;
  backend)
    start_tools
    start_backend
    ;;
  ui)
    start_tools
    start_ui
    ;;
  all)
    start_tools
    start_backend
    start_ui
    ;;
  down)
    stop_ui
    stop_backend
    stop_tools
    ;;
  *)
    echo "Usage:"
    echo "  ./platform.sh tools                # Start shared infra only"
    echo "  ./platform.sh backend [override]   # Start backend + tools"
    echo "  ./platform.sh ui                   # Start UI + tools"
    echo "  ./platform.sh all [override]       # Start UI + backend + tools"
    echo "  ./platform.sh down                 # Stop everything"
    echo ""
    echo "Overrides:"
    echo "  --mac-intel   Use docker-compose.override.mac.intel.yml"
    echo "  --mac-arm     Use docker-compose.override.mac.arm.yml"
    echo ""
    echo "Examples:"
    echo "  ./platform.sh backend"
    echo "  ./platform.sh backend --mac-intel"
    echo "  ./platform.sh all --mac-arm"
    ;;
esac