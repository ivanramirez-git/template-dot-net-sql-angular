FROM node:latest as build
# set the working directory in the container
WORKDIR /app
# copy the package.json file to the container
COPY package.json /app
# install all the dependencies
RUN npm install
# copy the source code to the container
COPY . /app
# build the Angular app
RUN npm run build
# use the official nginx image as the base image

# use the latest version of the official nginx image as the base image
FROM nginx:latest
# copy the custom nginx configuration file to the container in the
# default location
COPY nginx.conf /etc/nginx/nginx.conf
# copy the built Angular app files to the default nginx html directory
COPY --from=build /app/dist/app/browser /usr/share/nginx/html
# expose port 80
EXPOSE 80
# run the nginx server
CMD ["nginx", "-g", "daemon off;"]

# docker rm -f web-app && docker build -t web-app . && docker run --name web-app -d -p 8080:80 web-app