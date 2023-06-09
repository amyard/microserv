
-- check available docker images and checking the running containers  
`docker ps`   
`docker ps -a` - shows stopped container

-- check the available images
`docker images`

-- downloading mongo image  
`docker pull mongo`

-- run docker  
`docker run -d -p 27017:27017 --name shopping-mongo mongo`   
-d  detached mode (working background)   
-p  port number 

-- checking logs for mongo image  
`docker logs -f shopping-mongo`  


-- run bash command from inside the image  
`docker exec -it shopping-mongo /bin/bash`  
-it  interactive terminal   


-- run images  
`docker start shopping-mongo`

-- docker stop  
``docker stop f7fb``  
f7fb is containerID 

-- remove image  
``docker rm f7fb``  or/AND  ``docker rmi f7fb``     
f7fb is containerID

-- volumes will save all mongo data/tables/scheme even after closing the db
``volumes:
    mongo_data:``

-- run docker compose   
``docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d``  

-- shut down all images   
``docker-compose -f docker-compose.yml -f docker-compose.override.yml down``

-- stop all images
``docker rmi $(docker ps -aq)``

-- remove unnamed images  
``docker system remove``

-- check logs(aspnetrun-redis  - my image)   
``docker logs -f aspnetrun-redis``
``docker exec -it aspnetrun-redis /bin/bash``

-- run docker  
`docker run -d -p 27017:27017 --name shopping-mongo mongo`  
``docker run -d -p 6379:6379 --name aspnetrun-redis redis``


-- Portainer - container manager