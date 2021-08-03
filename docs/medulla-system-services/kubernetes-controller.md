---
title: Kubernetes Controller
shortTitle: Kubernetes Controller
intro: The project is to have a kubernetes controller that helps with many things related to Medulla applications, this page is here to help define an initial set of CRDs that would be needed for the platform.
---
## Application
**Description:** Defines a Medulla application

**What It Does:**
* Create application namespace
* Create application SQL database

**Example Resource:**
```yaml
apiVersion: medulla.io/v1alpha1
kind: Application
metadata:
  name: hello-world
spec:
  friendlyName: "Hello World"
  description: "My hello world app"
  gitRepository: "https://github.com/Recro/medulla.git"
```

***

## Table
**Description:** Defines an application table

**What It Does:**
* Creates a table within the application database

**Example Resource:**
```yaml
apiVersion: medulla.io/v1alpha1
kind: Table
metadata:
  name: product
spec:
  friendlyName: "Product"
  fields:
    - name: id
      type: integer
      label: ID
    - name: name
      type: string
      label: Name
    - name: ip
      type: ip-address
      label: IP Address
```

***

## FieldType
**Description:** Defines an custom table field type

**What It Does:**
* Defines an custom table field type

**Example Resource:**
```yaml
apiVersion: medulla.io/v1alpha1
kind: FieldType
metadata:
  name: ip-address
spec:
  friendlyName: "IP Address"
  type: string
  pattern: "^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$"
  mask: "XXX.XXX.XXX.XXX"
```
