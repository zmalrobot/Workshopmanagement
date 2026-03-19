#!/bin/bash
set -e

# ============================================================
# WorkshopManagement - Deployment Script
# Run this script from the repository root directory.
# Usage: chmod +x deploy.sh && ./deploy.sh
# ============================================================

APP_NAME="workshopmanagement"
INSTALL_DIR="/opt/workshopmanagement"
DATA_DIR="${INSTALL_DIR}/data"

echo "============================================"
echo " WorkshopManagement - Deployment"
echo "============================================"

# --- Check prerequisites ---
echo ""
echo "[1/5] Checking prerequisites..."

if ! command -v docker &> /dev/null; then
    echo "ERROR: Docker is not installed. Install it first:"
    echo "  curl -fsSL https://get.docker.com | sh"
    exit 1
fi

if ! docker compose version &> /dev/null; then
    echo "ERROR: Docker Compose (v2) is not available."
    echo "  It should be included with Docker. Update Docker or install the compose plugin."
    exit 1
fi

echo "  Docker:         $(docker --version)"
echo "  Docker Compose: $(docker compose version)"

# --- Create data directories ---
echo ""
echo "[2/5] Creating data directories..."

sudo mkdir -p "${DATA_DIR}/mongodb"
sudo mkdir -p "${DATA_DIR}/images"
sudo mkdir -p "${DATA_DIR}/templates"

# Set ownership so the containers can write (app runs as non-root UID 1654 in .NET 8 images)
sudo chown -R 1654:1654 "${DATA_DIR}/images"
sudo chown -R 1654:1654 "${DATA_DIR}/templates"
# MongoDB container runs as UID 999
sudo chown -R 999:999 "${DATA_DIR}/mongodb"

sudo chmod -R 755 "${DATA_DIR}"

echo "  Data directory: ${DATA_DIR}"

# --- Copy project files ---
echo ""
echo "[3/5] Copying project files..."

sudo mkdir -p "${INSTALL_DIR}/src"
sudo cp -r . "${INSTALL_DIR}/src/"
sudo chown -R "$(id -u):$(id -g)" "${INSTALL_DIR}/src"

# --- Write production docker-compose with host-bound volumes ---
echo ""
echo "[4/5] Generating production compose file..."

cat > "${INSTALL_DIR}/src/docker-compose.prod.yml" <<EOF
services:
  workshopmanagement:
    build:
      context: .
      dockerfile: Workshopmanagement/Dockerfile
    container_name: workshopmanagement-app
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:8080
      - WebClientCulture=it-IT
      - MongoServerAddress=mongodb
      - MongoServerPort=27017
      - MongoDatabase=WorkshopManager
      - ApplicationName=WorkshopManagement
      - AllowInsecureTls=false
      - IsBeta=false
      - ImageBasePath=/app/data/images
      - TemplateBasePath=Templates
      - TemplateSavePath=/app/data/templates
    volumes:
      - ${DATA_DIR}/images:/app/data/images
      - ${DATA_DIR}/templates:/app/data/templates
    depends_on:
      mongodb:
        condition: service_healthy
    restart: unless-stopped

  mongodb:
    image: mongo:7
    container_name: workshopmanagement-mongo
    ports:
      - "127.0.0.1:27017:27017"
    volumes:
      - ${DATA_DIR}/mongodb:/data/db
    healthcheck:
      test: ["CMD", "mongosh", "--eval", "db.adminCommand('ping')"]
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 20s
    restart: unless-stopped
EOF

# --- Build and deploy ---
echo ""
echo "[5/5] Building and starting containers..."

cd "${INSTALL_DIR}/src"
docker compose -f docker-compose.prod.yml build --no-cache
docker compose -f docker-compose.prod.yml up -d

echo ""
echo "============================================"
echo " Deployment complete!"
echo "============================================"
echo ""
echo " Application: http://$(hostname -I | awk '{print $1}'):8080"
echo " Data stored in: ${DATA_DIR}"
echo ""
echo " Useful commands:"
echo "   View logs:     cd ${INSTALL_DIR}/src && docker compose -f docker-compose.prod.yml logs -f"
echo "   Stop:          cd ${INSTALL_DIR}/src && docker compose -f docker-compose.prod.yml down"
echo "   Restart:       cd ${INSTALL_DIR}/src && docker compose -f docker-compose.prod.yml restart"
echo "   Update:        cd ${INSTALL_DIR}/src && docker compose -f docker-compose.prod.yml up -d --build"
echo ""
