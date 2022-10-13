# Medulla Helm Chart
In order to deploy this chart you must first install an ingress, we recommend Traefik:

```bash
helm repo add traefik https://helm.traefik.io/traefik
helm repo update
helm install traefik traefik/traefik --namespace traefik-system --create-namespace --wait
```

From there you can install Medulla:

```bash
helm install medulla .
```

If using the default values, you should be able to access the application on [https://localhost](https://localhost).
