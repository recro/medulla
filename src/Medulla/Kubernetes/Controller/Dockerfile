FROM node:latest

WORKDIR /app

COPY . .

RUN npm i

WORKDIR /app/src

RUN npx tsc

WORKDIR /app

CMD node src


