
# CRD Action

This resource defines a container image that will be available to an application for implementation by the frontend. It sets a protocol for interacting with it, gRPC, REST, or SOAP.


# CRD Action Group

This resources groups a number of actions to be executed at once from a single api call to this action group.
actions are bundled into a single resource, and can be ordered, prioritized, and parametized with a single data parameter


# CRD Application

This resource defines an application that will run on the platform, and sets name of the application, whether its published and publically available, sets company name, description, problem solved

# CRD Application Group

This resources bundles a number of applications together under the same domain name, so that the same domain and security provider can access all the applications. Should be the same as a SSO Single Sign On. Applications can define an application group from self developed applications, and including publicly published applications they want to self host.

# CRD Application Namespace

This resource defines a namespace that an application can be published under to contain resources. Apps within a namespace don't have access to apps in another namespace.

# CRD Blazor Code

This resource is code storage, and is the fundamental building block for components, pages.

# CRD Company

This resource defines a company that owns an application. name, description, location, problems solve.

# CRD Component

This resource defines a component that uses a blazor code file, and is used within a page.


# CRD Compound Component

This resource defines a component that uses a number of other components.


# CRD Data Source

This resource defines a data souce provides data to the frontend. Sets a protocol REST, gRPC, or SOAP.

# CRD Data Source Filter

This resource defines a filter for a datasource, the filter can be attached to any data source and will filter the results of the datasource before returning to the client.

# CRD Page

This resource defines a page which is composed of components, and compound components, a security policy, a path

# CRD Security Policy

This resource defines a security policy, which is composed of a access level, and auth provider

# CRD Share

This resource can reference any resource object in kubernetes and tells the system its share rights. 
It can say that a page can be shared publically and used in any other projects, or a component can be shared publically,
or it can say that a data source can be shared publically, or an application, application group, action, action group, or security policy. It will be the main way to publish anything on medulla.

