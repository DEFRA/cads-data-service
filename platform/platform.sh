#!/usr/bin/env bash
set -euo pipefail

# Linux make this executable via: chmod +x platform.sh

# ------------------------------------------------------------
# Resolve ROOT_DIR to the real cads-tools folder
# Works locally AND in GitHub Actions
# ------------------------------------------------------------
if [[ -z "${ROOT_DIR:-}" ]]; then
  export ROOT_DIR="$(cd "$(dirname "$0")/../../cads-tools" && pwd)"
fi

# ------------------------------------------------------------
# Resolve other repo paths relative to cads-tools
# ------------------------------------------------------------
CDS_DIR="$ROOT_DIR/../cads-data-service"
CADS_BRIDGE_DIR="$ROOT_DIR/../cads-bridge"
TOOLS_DIR="$ROOT_DIR"
CADS_MIS_DIR="$ROOT_DIR/../cads-mis"
CADS_ADMIN_FRONTEND_DIR="$ROOT_DIR/../cads-admin-frontend"

COMMAND="${1:-help}"
MAC_OVERRIDE=""
SYNC_DATA_SEED=""
CLEAN=""

# Process additional optional arguments
for arg in "${@:2}"; do
  case "$arg" in
    --mac-intel|--mac-arm) MAC_OVERRIDE="$arg" ;;
    --sync-data-seed)      SYNC_DATA_SEED="$arg" ;;
    --clean)           CLEAN="$arg" ;;
    *)                     ;;
  esac
done

ensure_network() {
  if ! docker network inspect cads-network >/dev/null 2>&1; then
    echo "[platform] Creating cads-network network..."
    docker network create cads-network
  fi
  return $?
}

# Determine which override file to use
compose_override() {
  case "$MAC_OVERRIDE" in
    --mac-intel) echo "docker-compose.override.mac.intel.yml" ;;
    --mac-arm)  echo "docker-compose.override.mac.arm.yml" ;;
    *)
      if [[ "${CI:-}" = "true" ]]; then
        echo "docker-compose.ci-override.yml"
      else
        echo "docker-compose.override.yml"
      fi
      ;;
  esac
  return 0
}

start_tools() {
  echo "[platform] Starting shared infra..."
  ensure_network
  "$TOOLS_DIR/harness/run-harness.sh" up ${SYNC_DATA_SEED:+"$SYNC_DATA_SEED"}
  return $?
}

stop_tools() {
  echo "[platform] Stopping shared infra..."
  "$TOOLS_DIR/harness/run-harness.sh" down ${CLEAN:+"$CLEAN"}
  return $?
}

start_cds() {
  echo "[platform] Starting cds..."
  cd "$CDS_DIR"

  OVERRIDE_FILE=$(compose_override)
  echo "[platform] Using cds override: $OVERRIDE_FILE"

  docker compose -p cads \
    -f docker-compose.yml \
    -f "$OVERRIDE_FILE" \
    up --build -d

  return $?
}

stop_cds() {
  echo "[platform] Stopping cds..."
  cd "$CDS_DIR"
  docker compose -p cads down || true
  return $?
}

start_mis() {
  echo "[platform] Starting mis..."
  cd "$CADS_MIS_DIR"
  docker compose -p cads -f docker-compose.yml up --build -d
  return $?
}

stop_mis() {
  echo "[platform] Stopping mis..."
  cd "$CADS_MIS_DIR"
  docker compose -p cads -f docker-compose.yml down || true
  return $?
}

start_admin() {
  echo "[platform] Starting admin..."
  cd "$CADS_ADMIN_FRONTEND_DIR"
  docker compose -p cads -f docker-compose.yml up --build -d
  return $?
}

stop_admin() {
  echo "[platform] Stopping admin..."
  cd "$CADS_ADMIN_FRONTEND_DIR"
  docker compose -p cads -f docker-compose.yml down || true
  return $?
}

start_bridge() {
  echo "[platform] starting bridge..."

  cd "$CADS_BRIDGE_DIR"
  OVERRIDE_FILE=$(compose_override)
  echo "[platform] Using bridge override: $OVERRIDE_FILE"

  docker compose -p cads \
    -f docker-compose.yml \
    -f "$OVERRIDE_FILE" \
    up --build -d

  return $?
}

stop_bridge() {
  echo "[platform] Stopping bridge..."
  cd "$CADS_BRIDGE_DIR"
  docker compose -p cads -f docker-compose.yml down || true
  return $?
}

case "$COMMAND" in
  tools)
    start_tools
    ;;
  cds)
    start_tools
    start_cds
    ;;
  bridge)
    start_tools
    start_bridge
    ;;
  mis)
    start_tools
    start_mis
    ;;
  admin)
    start_tools
    start_admin
    ;;
  all)
    start_tools
    start_cds
    start_bridge
    start_mis
    start_admin
    ;;
  down)
    stop_mis
    stop_admin
    stop_bridge
    stop_cds
    stop_tools
    ;;
  *)
    echo "Usage:"
    echo "  ./platform.sh tools                # Start shared infra only"
    echo "  ./platform.sh cds [override]       # Start cds + tools"
    echo "  ./platform.sh bridge [override]    # Start bridge + tools"
    echo "  ./platform.sh mis                  # Start mis + tools"
    echo "  ./platform.sh admin                # Start admin + tools"
    echo "  ./platform.sh all [override]       # Start cds + bridge + mis + admin + tools"
    echo "  ./platform.sh down                 # Stop everything"
    echo ""
    echo "Overrides:"
    echo "  --mac-intel   Use docker-compose.override.mac.intel.yml"
    echo "  --mac-arm     Use docker-compose.override.mac.arm.yml"
    echo ""
    echo "Options:"
    echo "  --sync-data-seed  Pass --sync-data-seed to the harness on start"
    echo ""
    echo "Examples:"
    echo "  ./platform.sh cds"
    echo "  ./platform.sh cds --mac-intel"
    echo "  ./platform.sh all --mac-arm"
    echo "  ./platform.sh cds --mac-arm --sync-data-seed"
    ;;
esac