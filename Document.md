## **Setting Up Docker for My .NET Web API Application**  
I containerized my **FileUploadAPI** by creating a **Dockerfile** and setting up **Docker Compose** to run both the API and MongoDB together.  

---

### **Step 1: Creating the Dockerfile**  
I placed a `Dockerfile` inside my `FileUploadAPI` project to define how the application should be built and run in a container.  

- **Build Stage:** Uses the .NET SDK to restore dependencies and compile the project.  
- **Runtime Stage:** Uses the .NET runtime to execute the compiled application.  
- **Exposes Port 5000:** Ensures my API is accessible at `http://localhost:5000`.  

---

### **Step 2: Setting Up Docker Compose**  
I created a `docker-compose.yml` file to define the services required for my application:  

- **MongoDB Service:**  
  - Uses the official MongoDB image.  
  - Exposes port `27017` for database connections.  
  - Creates a volume (`mongodb_data`) to persist data.  
  - Configured admin credentials (`admin/password`).  

- **FileUploadAPI Service:**  
  - Builds from the `Dockerfile`.  
  - Exposes port `5000` to match my API’s configuration.  
  - Uses an **environment variable** to set the MongoDB connection string.  
  - `depends_on` ensures MongoDB starts before my API.  

---

### **Step 3: Running the Docker Containers**  
After setting up everything, I ran my services using:  

```sh
docker-compose up --build
```  

This command:  
- Built and started both the API and MongoDB containers.  
- Showed real-time logs in the terminal to confirm successful startup.  

---

## **Verifying My API and MongoDB**  
Once the services were up and running, I checked if everything was working correctly.  

### **Checking Running Containers**  
I ran:  
```sh
docker ps
```  
This listed:  
```
CONTAINER ID   IMAGE           PORTS                    NAMES  
123abc456def   fileuploadapi   0.0.0.0:5000->5000/tcp   fileuploadapi_container  
789xyz123uvw   mongo           0.0.0.0:27017->27017/tcp mongo_container  
```  

### **Verifying MongoDB Connection Inside Docker**  
To confirm MongoDB was running properly, I opened a shell inside the MongoDB container:  
```sh
docker exec -it mongo_container mongosh -u admin -p password
```  
Then, I ran:  
```sh
show dbs
```  
This showed `FileUploadDB`, meaning my database was set up correctly.  

### **Accessing Swagger to Test My API**  
I opened **Swagger UI** in the browser at:  
```
http://localhost:5000/swagger
```  
This allowed me to test my API endpoints.  

---

## **Stopping and Cleaning Up Docker**  
If I needed to stop only the API container, I ran:  
```sh
docker stop fileuploadapi_container
```  
To stop all running containers at once, I used:  
```sh
docker stop $(docker ps -q)
```  

---

## **Summary of What I Did**  

| **Step** | **What I Did** |  
|----------|----------------|  
| 1 | Created a **Dockerfile** to containerize my .NET Web API |  
| 2 | Created a **docker-compose.yml** to run MongoDB and the API together |  
| 3 | Used `docker-compose up --build` to start both services |  
| 4 | Verified MongoDB connection using `docker exec -it mongo_container mongosh` |  
| 5 | Accessed my API at `http://localhost:5000/swagger` |  
| 6 | Stopped containers using `docker stop` or `docker-compose down` |  

