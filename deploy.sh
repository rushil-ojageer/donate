docker build -t rushilojageer/donate-gateway:latest -t rushilojageer/donate-gateway:$SHA -f ./Backend/Donate/Donate/Gateway/Donate.Gateway/Dockerfile ./donate-gateway
docker build -t rushilojageer/donate-charityapi:latest -t rushilojageer/donate-charityapi:$SHA -f ./Backend/Donate/Donate/CharityService/Donate.CharityService.API/Dockerfile ./donate-charityapi
docker build -t rushilojageer/donate-donorapi:latest -t rushilojageer/donate-donorapi:$SHA -f ./Backend/Donate/Donate/DonorService/Donate.DonorService.API/Dockerfile ./donate-donorapi
docker build -t rushilojageer/donate-donorworker:latest -t rushilojageer/donate-donorworker:$SHA -f ./Backend/Donate/Donate/DonorService/Donate.DonorService.IntegrationWorker/Dockerfile ./donate-donorworker
docker build -t rushilojageer/donate-fundapi:latest -t rushilojageer/donate-fundapi:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.API/Dockerfile ./donate-fundapi
docker build -t rushilojageer/donate-fundworker:latest -t rushilojageer/donate-fundworker:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.IntegrationWorker/Dockerfile ./donate-fundworker
docker build -t rushilojageer/donate-transactionfeed:latest -t rushilojageer/donate-transactionfeed:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.IntegrationWorker/Dockerfile ./donate-transactionfeed
docker build -t rushilojageer/donate-transactionprocessor:latest -t rushilojageer/donate-transactionprocessor:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.TransactionProcessor/Dockerfile ./donate-transactionprocessor

docker push rushilojageer/donate-gateway:latest
docker push rushilojageer/donate-charityapi:latest
docker push rushilojageer/donate-donorapi:latest
docker push rushilojageer/donate-donorworker:latest
docker push rushilojageer/donate-fundapi:latest
docker push rushilojageer/donate-fundworker:latest
docker push rushilojageer/donate-transactionfeed:latest
docker push rushilojageer/donate-transactionprocessor:latest

docker push rushilojageer/donate-gateway:$SHA
docker push rushilojageer/donate-charityapi:$SHA
docker push rushilojageer/donate-donorapi:$SHA
docker push rushilojageer/donate-donorworker:$SHA
docker push rushilojageer/donate-fundapi:$SHA
docker push rushilojageer/donate-fundworker:$SHA
docker push rushilojageer/donate-transactionfeed:$SHA
docker push rushilojageer/donate-transactionprocessor:$SHA

kubectl apply -f K8S
kubectl set image deployments/gateway-deployment client=rushilojageer/donate-gateway:$SHA
kubectl set image deployments/charityapi-deployment server=rushilojageer/donate-charityapi:$SHA
kubectl set image deployments/donorapi-deployment client=rushilojageer/donate-donorapi:$SHA
kubectl set image deployments/donorworker-deployment worker=rushilojageer/donate-donorworker:$SHA
kubectl set image deployments/fundapi-deployment server=rushilojageer/donate-fundapi:$SHA
kubectl set image deployments/fundworker-deployment client=rushilojageer/donate-fundworker:$SHA
kubectl set image deployments/transactionprocessor-deployment worker=rushilojageer/donate-transactionprocessor:$SHA
kubectl set image deployments/transactionfeed-deployment server=rushilojageer/donate-transactionfeed:$SHA