# Deploy: tell Tilt what YAML to deploy
#k8s_yaml('chart/templates/docs.yaml')
k8s_yaml(helm('chart'))

# Build: tell Tilt what images to build from which directories
docker_build('ghcr.io/recro/medulla/documentation:0.1.0', 'src/Documentation/Server')
#docker_build('companyname/backend', 'backend')
# ...

# Watch: tell Tilt how to connect locally (optional)
k8s_resource('docs-deployment', port_forwards=8080)
