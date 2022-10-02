
# Chart Dependencies

In order to deploy medulla through it's helm chart you need to have these dependencies installed on your cluster.

### Istio

[https://istio.io/latest/docs/setup/getting-started/#download](Install Istio through istioctl)

    istioctl install -y

### Cert Manager

    ##### install repo
    helm repo add jetstack https://charts.jetstack.io


    ##### update repo
    helm repo update


    ##### install helm chart
    helm install \
    cert-manager jetstack/cert-manager \
    --namespace cert-manager \
    --create-namespace \
    --version v1.9.1 \
    --set installCRDs=true

