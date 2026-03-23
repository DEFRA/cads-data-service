#!/usr/bin/env bash
set -euo pipefail

# Linux make this executable via: chmod +x platform.sh

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

TOOLS_DIR="$ROOT_DIR/../../cads-tools"
BACKEND_DIR="$ROOT_DIR"
UI_DIR="$ROOT_DIR/../../cads-mis"

COMMAND="${1:-help}"

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
  docker compose up -d
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
    echo "  ./platform.sh tools     # Start shared infra only"
    echo "  ./platform.sh backend   # Start backend + tools"
    echo "  ./platform.sh ui        # Start UI + tools"
    echo "  ./platform.sh all       # Start UI + backend + tools"
    echo "  ./platform.sh down      # Stop everything"
    ;;
esac