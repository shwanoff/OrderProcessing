# Order Processing System

## Project Overview
The Order Processing System is a .NET 9 microservice designed to manage orders and products. It follows Domain-Driven Design (DDD) principles and provides functionalities to add new orders, retrieve order details, and manage product information.

## Features
- Add new orders with associated products.
- Retrieve order details by order ID.
- Using .NET 9 (C#) with ASP.NET Core Web API.
- Domain-Driven Design (DDD) architecture.
- MS SQL Server database with Entity Framework Core hosting on Docker.
- Manage product information within orders.
- AutoMapper for object-to-object mapping.
- FluentValidation for request validation.
- Serilog for structured logging.
- NUnit for testing.
- MediatR for command/query handling.

## Installation
1. Clone the repository:
   
```
   git clone https://github.com/shwanoff/OrderProcessing.git
```

2. Open the solution with Visual Studio
3. Build the solution
   
## Usage
Open the project in Visual Studio.

### Option 1: Build and run the project OrderProcessing.API for HTTPS
- Ensure the profile `https` is selected for the OrderProcessing.API project.
- Press `F5` or click on the Start Debugging button.
- The API will be available at `https://localhost:6081/index.html`.

### Option 2: Run the project using Docker Compose
- Ensure the profile `docker-compose` is selected.
- Press `F5` or click on the Start Debugging button.
- The API will be available at `https://localhost:8081/index.html`.

You can use Swagger UI to execute requests.

## API Endpoints
### Create an Order
- **Endpoint:** `POST /api/orders`
- **Request Body:**
  
```
  {
    "invoiceAddress": "123 Main st",
    "invoiceEmailAddress": "test@example.com",
    "invoiceCreditCardNumber": "1234567890123452",
    "products": [
      {
        "productId": 1,
        "productName": "Laptop",
        "productAmount": 1,
        "productPrice": 1000
      }
    ]
  }
```
- **Responses:**
  - `201 Created` with the `orderNumber` (UUID) if successful
  - `400 Bad Request` if the email address is invalid
  - `400 Bad Request` if the credit card number is invalid
  - `400 Bad Request` if required fields are missing

### Get an Order by Order Number
- **Endpoint:** `GET /api/orders/{orderNumber}`
- **Request Parameter:** `orderNumber` (UUID)
- **Response Body:**
  
```
  {
    "orderNumber": "9d2c4464-f757-4f42-9c0f-7bece224793f",
    "invoiceAddress": "123 Main st",
    "invoiceEmailAddress": "test@example.com",
    "invoiceCreditCardNumber": "1234-5678-9012-3452",
    "products": [
      {
        "productId": 1,
        "productName": "Laptop",
        "productAmount": 1,
        "productPrice": 1000
      }
    ],
    "createdAt": "2025-03-13T04:56:24.9979462"
  }
```

## Testing
Run unit tests.
Integration tests will run using InMemoryDB.
