## **Setting Up Docker for .NET Web API Application**  

To containerize **FileUploadAPI**, a **Dockerfile** was created, and **Docker Compose** was set up to run both the API and MongoDB together.  

---

### **Step 1: Creating the Dockerfile**  
A `Dockerfile` was placed inside the `FileUploadAPI` project to define how the application should be built and run in a container.  

- **Build Stage:** Uses the .NET SDK to restore dependencies and compile the project.  
- **Runtime Stage:** Uses the .NET runtime to execute the compiled application.  
- **Exposes Port 5000:** Ensures the API is accessible at `http://localhost:5000`.  

---

### **Step 2: Setting Up Docker Compose**  
A `docker-compose.yml` file was created to define the services required for the application.  

- **MongoDB Service:**  
  - Uses the official MongoDB image.  
  - Exposes port `27017` for database connections.  
  - Creates a volume (`mongodb_data`) to persist data.  
  - Configured admin credentials (`admin/password`).  

- **FileUploadAPI Service:**  
  - Builds from the `Dockerfile`.  
  - Exposes port `5000` to match the API’s configuration.  
  - Uses an **environment variable** to set the MongoDB connection string.  
  - `depends_on` ensures MongoDB starts before the API.  

---

### **Step 3: Running the Docker Containers**  
After setting up everything, services were started using:  

```sh
docker-compose up --build
```  

This command:  
- Built and started both the API and MongoDB containers.  
- Showed real-time logs in the terminal to confirm successful startup.  

---

## **Verifying API and MongoDB**  
Once the services were up and running, checks were performed to confirm everything was working correctly.  

### **Checking Running Containers**  
The following command was used:  
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
To confirm MongoDB was running properly, a shell was opened inside the MongoDB container using:  
```sh
docker exec -it mongo_container mongosh -u admin -p password
```  
Then, the following command was executed:  
```sh
show dbs
```  
The database `FileUploadDB` was listed, confirming the setup was correct.  

### **Accessing Swagger to Test API**  
Swagger UI was accessed in the browser at:  
```
http://localhost:5000/swagger
```  
This allowed testing of the API endpoints.  

---

## **Stopping and Cleaning Up Docker**  
To stop only the API container, the following command was used:  
```sh
docker stop fileuploadapi_container
```  
To stop all running containers at once, the following command was executed:  
```sh
docker stop $(docker ps -q)
```  

---

## **Summary of Steps**  

| **Step** | **Action Taken** |  
|----------|----------------|  
| 1 | Created a **Dockerfile** to containerize the .NET Web API |  
| 2 | Created a **docker-compose.yml** to run MongoDB and the API together |  
| 3 | Used `docker-compose up --build` to start both services |  
| 4 | Verified MongoDB connection using `docker exec -it mongo_container mongosh` |  
| 5 | Accessed API at `http://localhost:5000/swagger` |  
| 6 | Stopped containers using `docker stop` or `docker-compose down` |  
