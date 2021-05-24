kubectl expose deployment kibana --type=LoadBalancer --name=kibana-service

minikube service kibana-service

kubectl expose deployment elasticsearch --type=LoadBalancer --name=elasticsearch-service

minikube service elasticsearch-service
