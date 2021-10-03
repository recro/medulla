
sudo su root
cd ~
cd medulla/medulla
git pull

./build-docker.sh

docker stop medulla
docker run --rm -d -p 80:80 --name=medulla medulla 



