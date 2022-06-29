FROM node:alpine3.14

WORKDIR /app

COPY package.json .
COPY package-lock.json .

RUN npm i

COPY . .

EXPOSE 3000

CMD node app.js
