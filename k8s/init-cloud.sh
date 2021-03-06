#!/usr/bin/env bash
echo "#################### Waiting for Azure to provision external IP ####################"

ip_regex='([0-9]{1,3}\.){3}[0-9]{1,3}'
while true; do
    printf "."
    frontendUrl=$(kubectl get svc frontend-nginx-ingress-controller -o=jsonpath="{.status.loadBalancer.ingress[0].ip}")
    if [[ $frontendUrl =~ $ip_regex ]]; then
        break
    fi
    sleep 5s
done

printf "\n"
externalDns=$frontendUrl
echo "Using $externalDns as the external DNS/IP of the K8s cluster"

echo "#################### Creating application configuration ####################"

kubectl delete configmap urls || true

# urls configmap
kubectl create configmap urls \
    "--from-literal=BasketApiClient=http://$externalDns/basket-api" \
    "--from-literal=OrderingApiClient=http://$externalDns/orders-api" \
    "--from-literal=MapUserMvcClient=http://$externalDns/mapuser-mvc" \
    "--from-literal=MvcClient=http://$externalDns/web-mvc" \
    "--from-literal=WebhooksWebClient=http://$externalDns/webhooks-client" \
    "--from-literal=WebhooksApiClient=http://$externalDns/webhooks-api" \
    "--from-literal=WebShoppingAggClient=http://$externalDns/webshoppingagg" \
    "--from-literal=IdentityUrlExternal=http://$externalDns/identity" \
    "--from-literal=IdentityUrl=http://$externalDns/identity" \
    "--from-literal=webshoppingapigw_e=http://$externalDns/webshoppingapigw"
kubectl label configmap urls app=eshop