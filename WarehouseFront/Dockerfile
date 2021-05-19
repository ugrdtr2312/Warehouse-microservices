FROM node:14.16.1 as build
WORKDIR /usr/local/app
COPY package.json /usr/local/app/
RUN npm install
COPY ./ /usr/local/app/
RUN npm run build -- --output-path=./dist/out --configuration production --build-optimizer

FROM nginx:latest
COPY --from=build usr/local/app/dist/out/ /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80