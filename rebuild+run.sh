#!/bin/bash

docker build -t sex .
docker run -p "8080:8080" -it --rm sex