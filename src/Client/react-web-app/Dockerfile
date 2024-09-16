FROM node:alpine as build

EXPOSE 3000
EXPOSE 3001

COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

FROM nginx:stable-alpine
COPY --from=build /dist /usr/share/nging/html
COPY --from=build nginx.conf /etc/nginx/conf.d/default.conf

CMD [ "nginx", "-g", "daemon.off;" ]