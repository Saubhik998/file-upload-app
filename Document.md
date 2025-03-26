## **1. Setting Up Docker for the .NET Web API Application**
To containerize the **FileUploadAPI**, we created a **Dockerfile** and set up **Docker Compose** for both the API and MongoDB.

### **Step 1: Creating a Dockerfile**
We created a `Dockerfile` inside the `FileUploadAPI` project to define how our .NET Web API should be built and run inside a container.

---
- **Build stage:** Uses the .NET SDK to restore dependencies and compile the project.  
- **Runtime stage:** Uses the .NET runtime to execute the compiled application.  
- **Expose port 5000:** Ensures the app is accessible on `http://localhost:5000`.  

```
- **MongoDB Service:**
  - Uses the official MongoDB image.
  - Exposes port `27017` for database connections.
  - Creates a volume (`mongodb_data`) to persist data.
  - Sets up admin credentials (`admin/password`).
- **FileUploadAPI Service:**
  - Builds from the `Dockerfile`.
  - Exposes port `5000` to match the API's configuration.
  - Uses an **environment variable** to set the MongoDB connection string.
  - `depends_on` ensures MongoDB starts before the API.

---

### **Step 3: Running the Docker Containers**
After setting up the `Dockerfile` and `docker-compose.yml`, we ran the services using:

```sh
docker-compose up --build
```
This command:
- Builds and starts the API and MongoDB containers.
- Shows logs in the terminal to check if services start correctly.

---

## **2. Verifying the API and MongoDB**
Once the services were up and running, we tested if the API was accessible.

### **Check Running Containers**
```sh
docker ps
```
This listed:
```
CONTAINER ID   IMAGE           PORTS                    NAMES
123abc456def   fileuploadapi   0.0.0.0:5000->5000/tcp   fileuploadapi_container
789xyz123uvw   mongo           0.0.0.0:27017->27017/tcp mongo_container
```

### **Check MongoDB Connection Inside Docker**
To verify MongoDB was running, we opened a shell inside the MongoDB container:
```sh
docker exec -it mongo_container mongosh -u admin -p password
```
Then, we ran:
```sh
show dbs
```
This confirmed `FileUploadDB` existed.

### **Accessing Swagger**
We opened **Swagger UI** in the browser:
```
http://localhost:5000/swagger
```

---

## **3. Stopping and Cleaning Up Docker**
### **Stop a Running Container**
To stop only the API container:
```sh
docker stop fileuploadapi_container
```
To stop all running containers:
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
