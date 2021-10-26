---
title: Contributing
shortTitle: Contributing
intro: Find out what you need to contribute to Medulla.
---
# Windows
## Dependencies
In order to contribute to Medulla from windows you will need a few things. Dependencies recommended are as follows:

- [Docker Desktop](https://docs.docker.com/desktop/windows/install/)
- [k3d](https://github.com/rancher/k3d/releases)
- [Helm](https://github.com/helm/helm/releases)
- [Tilt](https://github.com/tilt-dev/tilt/releases)

Along with these general dependencies, you will also require a code editor. You can use [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/vs/preview/), [Visual Studio Code](https://code.visualstudio.com/), or [JetBrains Rider](https://www.jetbrains.com/rider/).

Be aware that if you choose to use Rider it does not have full support for .NET 6 yet, due to this you may encounter various errors when editing code and compiling.

You will also need k3d, helm, and tilt to be in a directory in your path environment variable.

## Starting Medulla
In development you will use k3d and tilt as a part of your inner development loop. This means that as you edit code and save the files, tilt will automatically pick up those changes, compile them, and run them as new pods inside of kubernetes.

In order to get started you will need Docker open, once you have this done you can run the following in powershell:
```powershell
k3d cluster create medulla -s 1 -a 3 -p 80:80@loadbalancer -p 443:443@loadbalancer --k3s-server-arg "--disable=traefik" --registry-create
```

This will create a k3s Kubernetes cluster inside of Docker with 1 master node, 3 worker nodes, port forwards for HTTP and HTTPS, as well as a docker registry for pushing images.

After k3d has spun up your cluster, you can run the following in the root of the medulla repo to start Medulla:
```powershell
tilt up
```

Leave tilt running in the background, if you want to check the status of Medulla pods spinning up you can press space on the console window to open a web browser showing tilt resources.

You will need to append an entry into your hosts file located at `C:\Windows\System32\drivers\etc\hosts` such as the following:
```
127.0.0.1 medulla.local
```

Once all of the resources have come up in tilt you can navigate to `https://medulla.local` to access Medulla.

# Linux
**Stub Section, Contributors Needed**
# Mac
**Stub Section, Contributors Needed**
