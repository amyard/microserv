
-- check available docker images and checking the running containers  
`docker ps`


-- downloading mongo image  
`docker pull mongo`

-- run docker  
`docker run -d -p 27017:27017 --name shopping-mongo mongo`   
-d  detached mode  
-p  port number 

-- checking logs for mongo image  
`docker logs -f shopping-mongo`  


-- run bash command from inside the image  
`docker exec -it shopping-mongo /bin/bash`  
-it  interactive terminal   
