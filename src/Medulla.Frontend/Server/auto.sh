
docker stop medulla
docker rmi medulla -f


cd ~
cd medulla/medulla
git pull

cd ~/medulla/medulla/src/Medulla.Frontend/Server
docker build -t medulla -f ./Dockerfile ../..

docker stop medulla
docker run --rm -d -p 80:80 --name=medulla medulla 



