

echo "running"

docker login --username AWS --password="$(cat /tmp/docker-password)" 369840493607.dkr.ecr.us-east-2.amazonaws.com
docker pull 369840493607.dkr.ecr.us-east-2.amazonaws.com/medulla-frontend:latest
docker stop medulla
docker rm medulla
docker run -d --name=medulla --restart=always -p 80:5000 369840493607.dkr.ecr.us-east-2.amazonaws.com/medulla-frontend:latest
