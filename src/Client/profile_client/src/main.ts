import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config'; // Импортируйте вашу конфигурацию приложения
import { AppComponent } from './app/app.component'; // Импортируйте корневой компонент

// Инициализация приложения с использованием корневого компонента и конфигурации
bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err)); // Обработка ошибок

