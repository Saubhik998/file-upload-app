
# 📁 File Upload App

A simple and efficient file upload application built using **.NET Web API** for the backend and **React** for the frontend. This project leverages **MongoDB GridFS** for storing uploaded files, including images. It provides APIs for uploading, fetching, and deleting files, and is thoroughly tested with **xUnit** for high code coverage.

---

## ✨ Features

- Upload images with descriptive text.
- View all uploaded files in a list.
- View individual file details.
- Delete files when no longer needed.
- Uses **MongoDB GridFS** for file storage.
- API documentation with **Swagger**.
- High code coverage with **xUnit** tests.
- Frontend built using **React** for a smooth user experience.

---

## 🛠️ Technologies Used

### Backend
- **.NET 8.0 (Web API)**
- **MongoDB** with **GridFS**
- **Swagger** for API documentation
- **xUnit** for unit testing

### Frontend
- **React** (with Hooks and functional components)
- **Axios** for API communication
- **Bootstrap** for styling

---

## 🚀 Getting Started

### Prerequisites
- **Node.js** and **npm**
- **MongoDB** (locally or via cloud)
- **.NET SDK (8.0 or higher)**
- **Visual Studio** for backend
- **VS Code** for frontend

---

## 🗃️ Project Structure

```
FileUploadSolution
├── FileUploadAPI                # .NET Web API backend
│   ├── Controllers               # API controllers
│   ├── Models                    # Data models
│   ├── Services                  # Service logic
│   ├── Tests                     # xUnit test cases
│   └── Program.cs                # Entry point for the backend
└── FileUploadFrontend            # React frontend
    ├── src
    │   ├── components             # React components
    │   ├── api                    # API functions
    │   └── App.js                 # Main React file
    └── public                     # Public assets
```

---

## ⚙️ Configuration

### Backend (FileUploadAPI)

1. Update the MongoDB connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "MongoDbConnection": "mongodb://localhost:27017"
     }
   }
   ```
2. Run the backend server:
   ```
   dotnet run
   ```
3. The API will be available at:
   ```
   https://localhost:7295/swagger/index.html
   ```

### Frontend (FileUploadFrontend)

1. Navigate to the frontend folder:
   ```
   cd FileUploadFrontend/my-react-app
   ```
2. Install dependencies:
   ```
   npm install
   ```
3. Run the React development server:
   ```
   npm start
   ```
4. The frontend will be available at:
   ```
   http://localhost:3000
   ```
---

## 📝 Usage

1. Open the application in your browser.
2. Upload an image with.
3. View the list of uploaded files.
4. Click on a file to view its details.
5. Delete unwanted files.

---

## 🧪 Testing

To run the xUnit tests for the backend:
```
cd FileUploadAPI.Tests
dotnet test
```

