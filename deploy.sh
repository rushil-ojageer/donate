docker build -t rushilojageer/donate-gateway:latest -t rushilojageer/donate-gateway:$SHA -f ./Backend/Donate/Donate/Gateway/Donate.Gateway/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-charityapi:latest -t rushilojageer/donate-charityapi:$SHA -f ./Backend/Donate/Donate/CharityService/Donate.CharityService.API/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-donorapi:latest -t rushilojageer/donate-donorapi:$SHA -f ./Backend/Donate/Donate/DonorService/Donate.DonorService.API/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-donorworker:latest -t rushilojageer/donate-donorworker:$SHA -f ./Backend/Donate/Donate/DonorService/Donate.DonorService.IntegrationWorker/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-fundapi:latest -t rushilojageer/donate-fundapi:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.API/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-fundworker:latest -t rushilojageer/donate-fundworker:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.IntegrationWorker/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-transactionfeed:latest -t rushilojageer/donate-transactionfeed:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.IntegrationWorker/Dockerfile ./Backend/Donate/Donate/
docker build -t rushilojageer/donate-transactionprocessor:latest -t rushilojageer/donate-transactionprocessor:$SHA -f ./Backend/Donate/Donate/FundService/Donate.FundService.TransactionProcessor/Dockerfile ./Backend/Donate/Donate/

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