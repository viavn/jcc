FROM node:14-alpine as build
WORKDIR /app
COPY package.json /app
RUN npm install --silent
COPY . .
RUN npm run build

FROM nginx:alpine
VOLUME /var/cache/nginx
COPY --from=build app/dist/frontend /usr/share/nginx/html
COPY ./config/nginx.conf /etc/nginx/conf.d/default.conf
