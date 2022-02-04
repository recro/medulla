aws ecr --profile=recro get-login-password --region us-east-2 | docker login --username AWS --password-stdin 369840493607.dkr.ecr.us-east-2.amazonaws.com
docker build -t medulla-crd-kubernetes-controller .
docker tag medulla-crd-kubernetes-controller:latest 369840493607.dkr.ecr.us-east-2.amazonaws.com/medulla-crd-kubernetes-controller:latest
docker push 369840493607.dkr.ecr.us-east-2.amazonaws.com/medulla-crd-kubernetes-controller:latest