docker build -t rushilojageer/donate-charityapi:latest -t rushilojageer/donate-charityapi:$SHA -f ./Backend/Donate/Donate/CharityService/Donate.CharityService.API/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-donorapi:latest -t rushilojageer/donate-donorapi:$SHA -f ./Backend/Donate/Donate/DonorService/Donate.DonorService.API/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-donorworker:latest -t rushilojageer/donate-donorworker:$SHA -f ./Backend/Donate/Donate/DonorService/Donate.DonorService.IntegrationWorker/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-fundapi:latest -t rushilojageer/donate-fundapi:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.API/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-fundworker:latest -t rushilojageer/donate-fundworker:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.IntegrationWorker/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-transactionfeed:latest -t rushilojageer/donate-transactionfeed:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.IntegrationWorker/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-transactionprocessor:latest -t rushilojageer/donate-transactionprocessor:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.TransactionProcessor/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-client:latest -t rushilojageer/donate-client:$SHA -f ./Frontend/Web/donate-spa/Dockerfile ./Frontend/Web/donate-spa/

docker push rushilojageer/donate-charityapi:latest
docker push rushilojageer/donate-donorapi:latest
docker push rushilojageer/donate-donorworker:latest
docker push rushilojageer/donate-fundapi:latest
docker push rushilojageer/donate-fundworker:latest
docker push rushilojageer/donate-transactionfeed:latest
docker push rushilojageer/donate-transactionprocessor:latest
docker push rushilojageer/donate-client:latest

docker push rushilojageer/donate-charityapi:$SHA
docker push rushilojageer/donate-donorapi:$SHA
docker push rushilojageer/donate-donorworker:$SHA
docker push rushilojageer/donate-fundapi:$SHA
docker push rushilojageer/donate-fundworker:$SHA
docker push rushilojageer/donate-transactionfeed:$SHA
docker push rushilojageer/donate-transactionprocessor:$SHA
docker push rushilojageer/donate-client:$SHA

kubectl apply -f K8S
kubectl set image deployments/charityapi-deployment charityapi=rushilojageer/donate-charityapi:$SHA
kubectl set image deployments/donorapi-deployment donorapi=rushilojageer/donate-donorapi:$SHA
kubectl set image deployments/donorworker-deployment donorworker=rushilojageer/donate-donorworker:$SHA
kubectl set image deployments/fundapi-deployment fundapi=rushilojageer/donate-fundapi:$SHA
kubectl set image deployments/fundworker-deployment fundworker=rushilojageer/donate-fundworker:$SHA
kubectl set image deployments/transactionprocessor-deployment transactionprocessor=rushilojageer/donate-transactionprocessor:$SHA
kubectl set image deployments/transactionfeed-deployment transactionfeed=rushilojageer/donate-transactionfeed:$SHA
kubectl set image deployments/client-deployment client=rushilojageer/donate-client:$SHA