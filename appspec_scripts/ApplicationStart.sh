

echo "running"

docker stop medulla
docker rm medulla
docker rmi 369840493607.dkr.ecr.us-east-2.amazonaws.com/medulla-frontend:latest
docker pull 369840493607.dkr.ecr.us-east-2.amazonaws.com/medulla-frontend:latest
docker run --rm -d --name=medulla -p 80:5000 369840493607.dkr.ecr.us-east-2.amazonaws.com/medulla-frontend:latest

