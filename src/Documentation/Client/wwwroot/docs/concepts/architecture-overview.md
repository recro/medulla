---
title: Medulla Architecture Overview
shortTitle: Architecture Overview
intro: An overview of what Medulla is made up of architectually.
---
## General Thoughts and Considerations
This document is written in a way that is more generic in terms of the specific technologies being used. (ex. MySQL vs Postgres, Istio vs Linkerd, etc.)
Separate pages will be written documenting details of the pros/cons of each technology, as well as the final choice on what technology was chosen with any other details on its use within Medulla.

Medulla will have a set of 3rd party services (open source) that will be tested against and directly supported during deployment, a deployment using our provided offerings will have the best support and we will do our best to support other offerings. Adding new offerings will be partially up to the community, and we want to offer deployment templates for both a fully contained deployment using all of our preferred and disconnected options as well as templates for hosting on all of the major cloud providers using their standard solutions.

We are always open to pull requests on all of the projects, and if you want to add support for specific deployment scenarios we highly recommend that you open a pull request. They are always welcome.

This document has the potential to grow very large, and so should be broken out into more documents when deemed necessary.

## Platform Goals
Medulla is a Kubernetes native low-code platform, It's a very ambitious platform with many goals and moving parts. As such whenever considering design choices, we should always be coming back to these goals and making sure that those choices conform to the design goals of the platform. These goals can always be revised in the future, If you feel these goals need a revision feel free to open an issue on the main [Medulla project](https://github.com/Recro/medulla/issues/new/choose).

* Use Kubernetes API's anywhere they make sense (Authentication, RBAC, etc)
* Extend the Kubernetes API with custom resources wherever possible (and where it makes sense)
* Take advantage of projects in the greater Kubernetes & CNCF ecosystems
* Be open to using multiple languages and technologies, polyglot development
* Design with scalability in mind, think about and document potential scaling issues for future reference

## High-Level Overview
Before diving fully into the architecture, its important to understand one of the most important design decisions around Medulla: Medulla should be fully deployable onto an empty Kubernetes cluster with no expectation of services already existing on the cluster. This means things like storage, databases, and a service mesh are not expected to be on the cluster. At the same time, we will allow people to use an existing cluster with these services already installed if they so choose through a configuration change at install time. We don't want to assume anyone has a solution to any specific need of the service, but we also don't want to force people to re-deploy those solutions through Medulla in the case they already have a solution through a cloud provider or on-prem.

Given this decision, the architecture diagrams follow an assumption that an end user is installing all Medulla dependencies through us; meaning they all run in the Kubernetes cluster.

Less text, more pictures:

![High-Level Architecture](../../images/concepts/high-level-architecture.png)

Yes there's a lot going on here, and were gonna break it down from the bottom up to explain in detail each and every component and reasoning behind them. We will also be linking to other pages documenting what actual open source project was chosen to fill each block as well as alternatives while also potentially going into the actual implementation while using said technology.

## Virtual / Physical Machine's
Were not going to get too much into the weeds here, there will be a document on deployment in time covering the various deployment scenarios someone may run into. In general this block is the general underlying infrastructure for the Kubernetes nodes. This could potentially be as a part of a managed Kubernetes offering such as AKS, EKS, GKE, etc.

## Kubernetes Nodes
This generally refers to Kubernetes itself, mostly workers but we may at some point have workloads running on the master nodes as well. These nodes may be a part of a managed Kubernetes offering and may provide access to underlying cloud services through storage classes as well. Just as with virtual / physical machine's, there will be a deployment document going in depth covering deployment scenarios.

## Distributed Storage
A reliable persistent storage option is required for the deployment of Medulla, a document covering more details on this can be found [here](../dependencies/distributed-storage.md).

Many services need access to a reliable storage solution, one of our biggest being databases. The CNCF has been full of distributed storage solutions for a while now that could be deployed as a part of a clean Medulla install on a bare cluster. Options we looked at and our choice will be detailed in the page linked above. During deployment our preferred storage solution may optionally be excluded from deployment in favor of using a users already deployed option, be this something deployed on cluster, a cloud storage offering integrated into the cluster, or something external (so long as a storage class and provisioner are installed on the cluster).

## Secrets Store
Many applications on Medulla will need a way to store secrets for any number of reasons, so as such Medulla requires some form of secret store to contain them. A document covering more details on this can be found [here](../dependencies/secrets-store.md).

As with distributed storage, Medulla applications will need somewhere to store and access secrets. This falls into three categories.

### Kubernetes secrets
This is our regular Kubernetes API supported secrets, can be gotten through the Kubernetes API

### On cluster secret stores running as an additional service
This could be any 3rd party secret store or even something written specifically for the Medulla project if needed (unlikely). This would run as another service within the cluster and accessed through an API.

### External secret stores
These are secret stores that are external to the cluster, this is things like Azure Key Vault, AWS Secrets Manager, etc.

## Database Cluster
A reliable database solution is required for Medulla applications to store structured data as well as for Medulla to use for internal service needs. A document covering more details on this can be found [here](../dependencies/database-server.md).

We are looking at a fairly similar situation as with secrets and storage. We have two situations we really see.

### In cluster database solution
There are many options for running databases within Kubernetes, some better than others. We document this more in depth in the above linked document. stateful services are looking way better than they have in the past years on Kubernetes, and so we definitely want to provide some out of the box option during deployment for people to use as well as support already deployed services that someone may be using.

### External Database Service
These are your Azure SQL, Amazon RDS/Aurora, etc. offerings or any database servers that someone may already have around.

## Service Mesh
Service meshes provide a lot of value, especially with the way Medulla's architecture is. One is absolutly required with the way Medulla is designed, and a document covering options and other details can be found [here](../dependencies/service-mesh.md).

A service mesh can cover many of our needs with service to service communication and routing, but can also give us access controls, authentication, and many other useful features. There is a very broad set of things a service mesh can do, as we ramp up development of Medulla our core use case is going to be routing. Anyone who wishes to add more functionality is very free to do so and we highly recommend opening a PR to add more functionality.

## Medulla Controller
This is a Kubernetes resource controller, this is also the very first component of Medulla that isn't 3rd party and is an actual part of the project itself. More design details about the controller can be found [here](../system-services/kubernetes-controller.md).

The goal of the controller is to handle all of the Medulla CRDs, more details on what these are can be found on the above linked page. Some of the things the controller handles is as follows:

* Application namespace
* Application database
* Application service mesh routes

There will be many more things that the controller will handle, and they will be documented under the controller wiki page as they come up.

## Secret Service
Facilitates the creation, management, and consumption of Medulla app secrets. More design details about the secret service can be found [here](../system-services/secret-service.md).

The secrets service goal is to abstract away the consumption and creation of app secrets, the location of the secrets being within the secret store. Seeing as there will likely be a need and want to have these secrets stored in various different services potentially inside the cluster on externally.

## Database Service
Facilitates the creation, management, and consumption of Medulla app databases, tables, views, and records. More design details about the database service can be found [here](../system-services/database-service.md).

The database service manages all aspects of application databases, both abstracting away the database engine itself but also providing security features on top of them related to ACL's as well as initiating triggers (webhooks) on records being created, read, updated, or deleted. One thing to note is that this service will only be connecting to the database to query it, creation of the database, views, and tables are handled by the Medulla controller. This service will however apply the controller resources to initiate a the creation of said resources.

## App Service
Facilitates the creation and management of Medulla apps. More design details about the app service can be found [here](../system-services/app-service.md).

As with the database service, this service will be creating the Kubernetes resources for an app (namespace as well as required app custom resources).

## Designer Backend Service
The direct backend for the designer frontend, this will be one of two things:

* Hosting a SPA application (services will be accessed directly via the service mesh gateway)
* The backend for a live connection based application (frontend is connected to the backend via a websocket connection and the backend calls the services)

The design document will go into more details on these, pros and cons, as well as our options for web frameworks which you can find [here](../system-services/designer-service.md)

## App Backend Service
Very similar to the designer backend service, depending on how we design this we will have one of two things:

* Hosting a SPA application (services will be accessed directly via the service mesh gateway)
* The backend for a live connection based application (frontend is connected to the backend via a websocket connection and the backend calls the services)

One thing to have of note here is that the app backend service will look at app specific configuration yaml to configure how the app actually works, for the time being this will just be a service written specifically as a low code container. In the future there could be the potential to have a bring-your-own-container where we could provide client SDKs for people to use when writing their own code in whatever language they want.

A design page can be found [here](../system-services/app-service.md) going into more details.

## Service Mesh Gateway
This handles ingress into the cluster and routing to apps and other Medulla services. This is more of a continuation of the service mesh and was placed in the diagram mostly to depict how traffic is getting in. More details on the service mesh page [here](../dependencies/service-mesh.md).

## Web Access
At this point we have covered all the main components that have been decided up until now. The designer is exposed normally through the service mesh on its DNS name. As for apps, when an app is created and spun up the service mesh configurations are created that route traffic for an app to their designated pods in the correct namespace. A DNS wildcard record would be set up to support `*.apps.medulla.io` as an example.
