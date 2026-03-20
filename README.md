# WorkshopManagement

WorkshopManagement is a .NET 8 web application for workshop operations, built to manage customer records, vehicle records, intervention workflows, execution history, and manager-controlled master data.

The application is centered on a workshop flow:

1. Select or register the customer.
2. Select or register the vehicle.
3. Start a workflow for the selected intervention.
4. Execute the workflow step by step, optionally collecting signature, mileage, fuel level, components, notes, and images.
5. Close the intervention and keep the execution history available for review and reporting.

## What The Project Does

The current codebase provides these business areas:

- Login with employee roles.
- Dashboard with shortcuts to start a new workflow or resume an open one.
- Customer registry.
- Vehicle registry.
- Open workflow management.
- Workflow history grouped by intervention execution series.
- Manager-only administration for employees, workflow definitions, workflow steps, and components.

The application is designed for workshop teams that need operational traceability across recurring inspections, maintenance tasks, and structured intervention procedures.

## Functional Specification

### Main actors

- Employee: performs workshop operations and works on running workflows.
- Manager: has employee permissions plus access to administration screens.

### Main operational flow

1. An employee signs in.
2. From the dashboard, the employee starts a new workflow or resumes one already in progress.
3. When starting a workflow, the user selects:
  - workflow type
  - customer
  - vehicle
  - signature step, if the selected workflow requires a disclaimer
4. The workflow creates a sequence of execution steps tied to the selected vehicle and customer.
5. During execution, each step can capture business data depending on how the task is configured:
  - notes
  - images
  - outcome status
  - components used
  - vehicle mileage
  - fuel level
  - repeated task loops
6. Completed interventions are stored in history and can be reviewed together with related images, components, and generated report output.

### Administrative flow

Managers can maintain:

- employees
- workflow catalog
- workflow tasks and step configuration
- reusable components

## Technical Overview

### Stack

- .NET 8
- ASP.NET Core Blazor Server with interactive server components
- MudBlazor for UI components
- MongoDB for persistence
- AKSoftware.Localization.MultiLanguages for localization
- Docker and Docker Compose for container deployment

### Application type

This is a server-rendered Blazor web app. The UI runs through Razor components and the server talks directly to MongoDB through custom data access classes in the `DatabaseManagment` namespace.

### Persistence model

The application stores and correlates these main entities:

- Customer
- Veichle
- Employee
- Workflow
- WorkflowTask
- WorkflowExecution
- Component

MongoDB stores structured business data, while images and generated templates are stored on disk paths controlled by environment variables.

### Authentication model

- Authentication is implemented with employee credentials and browser protected local storage.
- The first login bootstraps the system: if no employees exist, the submitted credentials create the first manager user.
- The local login session is effectively daily, because the stored login date is validated against the current day.

## Project Structure

```text
Code/
  docker-compose.yml          Default container deployment
  deploy.sh                   Linux/macOS deployment helper
  Workshopmanagement/
   Program.cs                Application startup and service registration
   Components/               Blazor layout and pages
   DatabaseManagment/        MongoDB engine, data objects, data access
   Resources/                Localization resources
   Templates/                HTML template for workflow report generation
   Utility/                  Shared helpers, themes, dialog parameters
   wwwroot/                  Static assets
```

## Configuration

The project uses `appsettings.json` mainly for logging and host defaults. Business runtime settings are primarily read from environment variables.

### appsettings files

- `Workshopmanagement/appsettings.json`: logging defaults and `AllowedHosts`.
- `Workshopmanagement/appsettings.Development.json`: development logging overrides.

### Required environment variables

These variables are used directly by the code and are already provided in `docker-compose.yml`:

| Variable | Purpose | Example |
| --- | --- | --- |
| `ASPNETCORE_ENVIRONMENT` | ASP.NET runtime environment | `Production` |
| `ASPNETCORE_URLS` | Server binding | `http://+:8080` |
| `WebClientCulture` | Default UI culture | `it-IT` |
| `MongoServerAddress` | MongoDB host | `mongodb` |
| `MongoServerPort` | MongoDB port | `27017` |
| `MongoDatabase` | MongoDB database name | `WorkshopManager` |
| `ApplicationName` | MongoDB client application name | `WorkshopManagement` |
| `AllowInsecureTls` | MongoDB TLS setting | `false` |
| `ImageBasePath` | Root path for uploaded execution images | `/app/data/images` |
| `TemplateBasePath` | Path containing the base workflow template | `Templates` |
| `TemplateSavePath` | Path where generated templates or reports are saved | `/app/data/templates` |
| `IsBeta` | Deployment flag currently present in compose configuration | `false` |

### Optional environment variables

The generic data access layer also supports collection-specific page sizes through environment variables such as:

- `CustomerPageSize`
- `VeichlePageSize`
- `WorkflowPageSize`

If not set, the UI defaults to page sizes `10`, `20`, and `50`.

## Deployment

### Option 1: Docker Compose

From the `Code` folder:

```bash
docker compose up --build -d
```

This starts:

- the web application on port `8080`
- MongoDB on port `27017`

The default compose file creates named Docker volumes for MongoDB data, uploaded images, and saved templates.

### Option 2: Deployment script

The repository includes `deploy.sh`, which:

1. verifies Docker and Docker Compose
2. creates persistent data directories under `$HOME/Documents/workshopmanagement/data`
3. copies the source into an installation folder
4. generates `docker-compose.prod.yml` with host-bound volumes
5. builds and starts the containers

Run it from the repository root:

```bash
chmod +x deploy.sh
./deploy.sh
```

After deployment, the application is available at `http://localhost:8080`.

### Container details

- The Dockerfile builds and publishes the `Workshopmanagement` project with .NET 8.
- The runtime image exposes port `8080`.
- The container creates `/app/data/images` and `/app/data/templates` for persistent business files.

## Local Development

### Prerequisites

- .NET 8 SDK
- MongoDB instance reachable by the app

### Run without Docker

Set the required environment variables, then start the project from `Code/Workshopmanagement`:

```bash
dotnet restore
dotnet run
```

Because MongoDB settings are read directly from environment variables, a local run will fail unless those values are defined.

## Notes And Current Configuration Behavior

- The application defaults to Italian culture when `WebClientCulture` is not set.
- Localization resources are embedded in the application assembly.
- Role-based navigation is enforced in the UI, with manager-only administration pages.
- Workflow reports are based on `Templates/workflowTemplateBase.html`.
- Image uploads and template output rely on filesystem paths, so those directories must remain writable in the target environment.

## Related Documentation

- Business-process wiki: see the files in `Wiki/`
- Source project: `Code/Workshopmanagement/Workshopmanagement.sln`

