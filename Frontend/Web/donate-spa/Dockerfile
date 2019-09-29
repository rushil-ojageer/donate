FROM node:10.16.3-alpine AS builder
COPY . ./app
WORKDIR /app
RUN npm i
RUN $(npm bin)/ng build
FROM nginx:1.15.8-alpine
COPY --from=builder /app/dist/donate-spa/ /usr/share/nginx/html
