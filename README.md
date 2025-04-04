# Fullstack Todo App

This project is a fullstack Todo application built with:

- **Backend**: ASP.NET Core Web API (C#)
- **Frontend**: React (Vite)
- **Database**: MongoDB Atlas (NoSQL)
- **State Management**: Redux Toolkit

It supports full CRUD operations for managing tasks, including form validation, modals for create/edit, and API communication.

---

## ✅ 1. Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- A MongoDB Atlas cluster (free tier works fine)

---

### Backend Setup

```bash
cd TodoApp.Server
dotnet restore
```

## ✅ 2. Configuration:

### Setting up Mongo connection

Add this to TodoApp.Server folder in a file named appsettings.json:

``` json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "TodoDatabaseSettings": {
    "ConnectionString": "mongodb+srv://<username>:<password>@cluster.mongodb.net/?retryWrites=true&w=majority",
    "DatabaseName": "TodoDb",
    "TodoCollectionName": "todo"
  } 
}
```


## ✅ 3. Frontend Setup:

### Setting up Mongo connection

``` bash
cd todoapp.client
npm install
```

In App.js, set the API endpoint:

```javascript
const todoURL = "http://localhost:7282/todos"; // Backend URL, change if needed.
```

Run the frontend:

  ```bash
  npm run dev
  ```

## ✅ 4. Notable design choices

- **State Management**: Redux Toolkit is used for state management, making it easier to manage the global state of the application.

- **MongoDB Atlas Integration**: Chosen for its flexibility and easy cloud setup. Ideal for unstructured data like todo items.

- **RESTful API Design**: API follows REST principles with clear routes for GET, POST, PUT, and DELETE.

- **TypeScript Consideration**: During development, a typo in field casing (iscomplete vs isComplete, id vs Id) was encountered. TypeScript would have caught this — highlighting its usefulness in larger projects.
