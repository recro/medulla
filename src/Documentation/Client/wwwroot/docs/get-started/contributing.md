---
title: Contributing
shortTitle: Contributing
intro: Find out what you need to contribute to Medulla.
---
# Windows
## Dependencies
In order to contribute to Medulla from windows you will need a few things. Dependencies recommended are as follows:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Rancher Desktop](https://rancherdesktop.io/)

Along with these general dependencies, you will also require a code editor. You can use [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/vs/preview/), [Visual Studio Code](https://code.visualstudio.com/), or [JetBrains Rider](https://www.jetbrains.com/rider/).

On another note, the reason we use Rancher Desktop instead of Docker Desktop is because of both licensing concerns as well as for the Kubernetes k3s distribution thats included.

You can use Docker Desktop if you wish, but you must have a Kubernetes cluster accessable for Medulla to operate correctly.

## Starting Medulla
In development you will use tye as a part of your inner development loop. This means that as you edit code and save the files, tye will automatically pick up those changes, compile them, and run them.

In order to get started you will need to install the Tye .NET global tool, you can install it by running the following in powershell:
```powershell
dotnet tool install -g Microsoft.Tye --version "0.10.0-alpha.21420.1"
```

After tye has been installed, you can run the following in the `/src` directory to start Medulla.
```powershell
tye run --watch
```

# Linux
**Stub Section, Contributors Needed**
# Mac
**Stub Section, Contributors Needed**
