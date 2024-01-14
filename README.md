If you are not using Visual Studio, to build the docker image

`docker buildx build -t letemknow:latest -f .\Letemknow\Server\Dockerfile .`

To run it for testing. 

`docker run --rm -d -p 80:80/tcp letemknow:latest`

Now you can hit it via http://localhost:80