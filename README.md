# Order Management API

This RESTful API manages orders, including creation, retrieval, updates, and reporting. It's designed for containerization with Docker, includes unit tests, and provides interactive API documentation via Swagger. Data is persisted using MongoDB.

## Table of Contents

* [Prerequisites](#prerequisites)
* [Getting Started](#getting-started)
    * [Local Development](#local-development)
    * [Docker](#docker)
* [API Endpoints](#api-endpoints)
    * [Orders](#orders)
    * [Reports](#reports)
* [Testing](#testing)
    * [Unit Tests](#unit-tests)
    * [Endpoint Testing (Postman/curl)](#endpoint-testing-postmancurl)
* [API Documentation (Swagger)](#api-documentation-swagger)
* [Deployment](#deployment)

## Prerequisites

* **.NET 8 SDK (or latest LTS):** [Download](https://dotnet.microsoft.com/en-us/download)
* **IDE (Recommended):** Visual Studio, Rider, or VS Code
* **Docker (Optional):** [Install](https://www.docker.com/products/docker-desktop)
* **HTTP Client (Recommended):** Postman, curl, or similar

## Getting Started

### Local Development

1. **Clone:**
   ```bash
  git clone [repository URL]
  cd OrderManagement.API

2. **Restore Packages:**
	dotnet restore OrderManagement.API.csproj

3. **Build:**
	dotnet build OrderManagement.API.csproj

4. **Run:**
    dotnet run

        The API will typically run on http://localhost:5000 (or a similar port – check the console).  The project is configured to automatically open the Swagger UI in your default browser.

### Docker

This project is configured for containerization using Docker.

1. **Build Image:**
	docker build -t order-management-api .

2. **Run Container:**
	docker run -it --rm -p {port}:8080 --name OrderManagement.API order-management-api

	The API will be accessible on http://localhost:{port}.


## API Endpoints

### Orders
  GET /order: Retrieve all orders.
  GET /order/{id}: Retrieve a specific order by ID.
  POST /order: Create a new order. 
  PATCH /order/{id}/status: Update an order's status.

### Reports
  GET /report: Retrieve a report of completed orders.


## Testing

### Unit Tests

The project includes a unit test project (OrderManagement.Tests).  You can run the tests using the .NET CLI or your IDE's test runner.

#### Using the .NET CLI:

1. **Navigate to the test project directory:**
  cd OrderManagement.Tests

2. **Run the tests:**
  dotnet test

#### Using your IDE:

  Most IDEs (Visual Studio, Rider, VS Code with the appropriate extensions) have built-in test runners that allow you to discover and run tests. Consult your IDE's documentation for more information.
  
  
### Endpoint Testing (Postman/curl)

#### You can use an HTTP client like Postman or curl to test the API endpoints directly.

  Example (curl):
    curl http://localhost:5000/api/order
  
  Example (Postman):
    Open Postman.
    Create a new request.
    Select the HTTP method.
    Enter the URL (e.g., http://localhost:5000/api/order).
    Send the request.

## API Documentation (Swagger)

  The project includes Swagger for interactive API documentation.  When you run the API locally, the Swagger UI should automatically open in your default browser. Swagger allows you to explore the available endpoints, view request/response models, and even make test requests directly from the browser.


## Deployment
  
  This API is designed for deployment within a Docker container.  For production deployments, consider using a container registry (e.g., Docker Hub, AWS ECR) and a container orchestration platform (e.g., Kubernetes, Docker Swarm).  Environment variables should be used for configuration.
