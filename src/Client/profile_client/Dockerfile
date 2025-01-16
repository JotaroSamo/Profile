
FROM node:22.12-alpine AS build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH

# Копируем package.json и yarn.lock
COPY package.json yarn.lock ./
# Устанавливаем Angular CLI
RUN npm install -g @angular/cli@19.0.0
# Устанавливаем зависимости
RUN yarn install
# Копируем остальные файлы приложения
COPY . ./
# Собираем приложение
RUN ng build --configuration production

# Этап развертывания
FROM nginx:1.19.6-alpine
# Копируем nginx конфигурацию
COPY nginx.config /etc/nginx/conf.d/default.conf
# Копируем собранные файлы из предыдущего этапа
COPY --from=build /app/dist/profile /usr/share/nginx/html

# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]

