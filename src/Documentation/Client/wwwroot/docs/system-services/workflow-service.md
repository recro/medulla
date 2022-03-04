---
title: Workflow Service
shortTitle: Workflow Service
intro: The workflow services goal is to abstract away the workflow execution environment from the frontend through an API.
---
## Overview
### Goals
Create a service that can manage workflows for the platform.

### Requirements
* Perform basic CRUD actions on workflows
* Execute workflows
* Check workflow statuses
* Stop workflows

### Background
The workflow engine to be used is [Argo Worflows](https://argoproj.github.io/argo-workflows/), it stores workflows within Kubernetes as custom resources.

### Design Overview
The service consists of a GraphQL api that can manipulate and work with Argo Workflows.

## Usage
### Use Cases
The primary use case is to be able to handle workflows that are executed within Medulla. These workflows are user defined so over time we will be iterating this to add more functionality.

### User Experience
The service should be provided as a GraphQL api, for the time being details like authentication are not set in stone. These details will be finalized and this document updated when the time comes.

## Design
### Detailed Design

This design will evolve over time as the service is implemented, we will start with just creating and deleting workflows.

Here is an example workflow body to be submitted to the service:

```json
{
    "name": "my-workflow",
    "tasks": [
        {
            "name": "A",
            "action": "echo",
            "parameters": [{"name": "message", "value": "Hello"}]
        },
        {
            "name": "B",
            "action": "echo",
            "parameters": [{"name": "message", "value": "World"}],
            "dependsOn": [ "A" ]
        }
    ]
}
```
And here is the expected kubernetes output for that body:

Note: The echo template shown below can be used for an initial implementation, we will iterate on this in the future to add pre-made templates based on what containers are being used by the workflow. Medulla will have many for a large number of use cases.
```yaml
apiVersion: argoproj.io/v1alpha1
kind: Workflow
metadata:
  generateName: my-workflow
spec:
  entrypoint: wf
  templates:
  - name: echo
    inputs:
      parameters:
      - name: message
    container:
      image: alpine:3.7
      command: [echo, "{{inputs.parameters.message}}"]
  - name: wf
    dag:
      tasks:
      - name: A
        template: echo
        arguments:
          parameters: [{name: message, value: "Hello"}]
      - name: B
        dependencies: [A]
        template: echo
        arguments:
          parameters: [{name: message, value: "World"}]
```

Here is the GraphQL schema to handle the workflows:
```graphql
schema {
  query: Query
  mutation: Mutation
}

type Query {
  workflows(name: String): [Workflow]
}

type Mutation {
  createWorkflow(workflow: WorkflowInput!): Workflow
}

type Workflow {
  name: String
  tasks: [Tasks]
}

input WorkflowInput {
  name: String
  tasks: [Tasks]
}

type Tasks {
  name: String
  action: String
  parameters: [Parameters]
}

type Parameters {
  name: String
  value: String
}
```

### Alternatives
One thing originally considered was designing entirely from scratch, this was scrapped due to complexity and time investment. We want to use Argo Workflows to give a good execution environment for Medulla with minimal design/development overhead.

### Dependencies
* [Argo Worflows](https://argoproj.github.io/argo-workflows/)

### Rollout Plan
N/A: New service

### Migration Plan
N/A: New service

## Testing

The only primary use cases right now are creating workflows and getting workflows. Following the schemas specified in the detailed design, when creating a workflow, the Kubernetes custom resource should be applied inside the cluster Argo Workflows is running in (it is assumed that Argo Workflows and the workflow service are both running in the same cluster).

A success is determined by being able to submit the JSON data as the body to the GraphQL api and seeing the custom resource in Kubernetes with the respective values submitted.
