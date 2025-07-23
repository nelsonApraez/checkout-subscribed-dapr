# Checkout Subscribed DAPR

A .NET 7 Web API microservice that demonstrates event-driven architecture using DAPR (Distributed Application Runtime) with Azure Service Bus as the message broker.

## Overview

This project implements a subscriber service that listens to checkout events from a pub/sub messaging system. It's designed to handle order processing notifications in a distributed microservices architecture.

## Features

- **Event-Driven Architecture**: Uses DAPR pub/sub components for decoupled communication
- **Azure Service Bus Integration**: Configured to work with Azure Service Bus Topics
- **Containerized**: Includes Docker support for easy deployment
- **RESTful API**: Exposes HTTP endpoints with Swagger documentation
- **Cloud Events**: Supports CloudEvents specification for event handling

## Architecture Components

### DAPR Components
- **Pub/Sub Component**: Configured to use Azure Service Bus Topics
- **Topic Subscription**: Listens to `topicCompany` for checkout events
- **Consumer ID**: Uses `subscriptionCompany` as the consumer identifier

### API Endpoints
- `POST /checkout`: Receives and processes checkout events from the pub/sub system

## Project Structure

```
CheckoutSubscribed/
├── Controllers/
│   └── CheckoutServiceController.cs    # Main controller handling checkout events
├── components/
│   └── pubsub.yaml                     # DAPR pub/sub configuration
├── Program.cs                          # Application entry point and configuration
├── CheckoutSubscribed.csproj          # Project dependencies and configuration
├── Dockerfile                         # Container image definition
└── appsettings.json                   # Application configuration
```

## Getting Started

### Prerequisites

- .NET 7.0 SDK
- DAPR CLI
- Docker (optional, for containerized deployment)
- Azure Service Bus (for production use)

### Configuration

1. **Configure Azure Service Bus Connection**:
   Update the connection string in `components/pubsub.yaml`:
   ```yaml
   metadata:
   - name: connectionString
     value: "YOUR-CONNECTION-STRING-HERE"
   ```

2. **Environment Variables**:
   Configure your application settings in `appsettings.json` or environment variables as needed.

### Running the Application

#### Using DAPR CLI (Recommended)

```bash
# Run with DAPR
dapr run --app-id checkout-subscribed --app-port 5000 --dapr-http-port 3500 --components-path ./components -- dotnet run
```

#### Using Docker

```bash
# Build the image
docker build -t checkout-subscribed .

# Run the container
docker run -p 8080:80 checkout-subscribed
```

#### Direct .NET Run

```bash
dotnet run
```

## Event Processing

The service subscribes to the `topicCompany` topic and processes `Order` events with the following structure:

```json
{
  "environment": "string",
  "quantity": 0,
  "priority": "string"
}
```

When an event is received, the service logs the environment and priority information to the console.

## API Documentation

Once running, you can access the Swagger UI at:
- `http://localhost:5000/swagger` (when running with dotnet)
- `http://localhost:8080/swagger` (when running with Docker)

## Dependencies

- **Dapr.AspNetCore** (1.11.0): DAPR integration for ASP.NET Core
- **Dapr.Client** (1.11.0): DAPR client SDK
- **Swashbuckle.AspNetCore** (6.5.0): Swagger/OpenAPI documentation

## Development

This service is part of a larger microservices ecosystem and is designed to:

1. Receive checkout events from other services via DAPR pub/sub
2. Process order information (environment, quantity, priority)
3. Log processing results for monitoring and debugging

## Deployment

The service can be deployed to various platforms:

- **Kubernetes**: Use DAPR sidecar injection
- **Azure Container Apps**: Native DAPR support
- **Docker**: Standalone container deployment
- **Local Development**: Direct .NET execution with DAPR CLI

## Contributing

When contributing to this project:

1. Ensure DAPR components are properly configured
2. Test with both local and Azure Service Bus configurations
3. Maintain backward compatibility for event schemas
4. Update documentation for any new endpoints or configurations

## License

This project is licensed under the terms specified in the LICENSE file.
