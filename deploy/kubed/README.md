# kubed
In order to get the upstream manifests run the following:
```
helm repo add appscode https://charts.appscode.com/stable/
helm template --namespace kube-system kubed appscode/kubed --version {version} > kubed.yaml
```
