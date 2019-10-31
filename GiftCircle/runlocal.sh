#!/bin/bash

docker build -t "gift-circle" . 

docker run -p 5005:5005 --name gift-circle gift-circle