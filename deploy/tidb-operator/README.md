# tidb-operator
In order to get the upstream manifests run the following:
```
curl https://raw.githubusercontent.com/pingcap/tidb-operator/{version}/manifests/crd.yaml -o tidb-crd.yaml
helm repo add pingcap https://charts.pingcap.org/
helm template --namespace tidb-operator tidb-operator pingcap/tidb-operator --version {version} > tidb-operator.yaml
```
