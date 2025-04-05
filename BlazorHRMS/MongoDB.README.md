# MongoDB Connection Configuration

## Overview
This document explains how the MongoDB connection string is configured in the BlazorHRMS application. The connection string contains sensitive credentials and should never be committed to version control.

## Configuration Approach
The application uses a separate configuration file for storing sensitive information:

- `appsettings.secrets.json` - Contains the MongoDB connection string and other sensitive configuration
- This file is excluded from Git via the `.gitignore` file

## Setup Instructions for Developers

1. Create a file named `appsettings.secrets.json` in the BlazorHRMS project directory
2. Add the following content to the file, replacing with your actual MongoDB connection details:

```json
{
  "MongoDB": {
    "ConnectionString": "mongodb+srv://username:password@cluster.example.mongodb.net/?retryWrites=true&w=majority&appName=yourAppName"
  }
}
```

3. The application will automatically load this file if it exists (see `Program.cs`)

## Usage in Code

The MongoDB connection string is accessed through the `IConfiguration` interface. See the `MongoDBService` class for an example of how to use it:

```csharp
// Retrieve the connection string from the secrets configuration
var connectionString = configuration["MongoDB:ConnectionString"];

// Create a MongoClient with the connection string
var client = new MongoClient(connectionString);
```

## Security Best Practices

- Never commit `appsettings.secrets.json` to version control
- Consider using environment variables or a secure secret manager for production environments
- Rotate MongoDB credentials periodically
- Use the principle of least privilege when creating MongoDB users