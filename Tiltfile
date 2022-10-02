k8s_yaml(helm('chart'))

docker_build('ghcr.io/recro/medulla/documentation:0.1.0', 'src', dockerfile='src/Documentation/Server/Dockerfile')

k8s_resource('docs-deployment', port_forwards=8080)
