По ссылке находится само задание с XML файлом: https://t.ly/OcA8i

Исходный код проекта доступен по ссылке: https://github.com/Kimgim007/ProjecyTest

Внутри решения два проекта: ProjecyTest и DataProcessorService.

В проектах ProjecyTest и DataProcessorService содержатся файлы конфигурации "appsettings", где можно изменить настройки подключения к RabbitMQ.

1.Для развёртывания приложения необходимо перейти в репозиторий и скопировать проект.

2.После установки проекта, посетите официальный сайт RabbitMQ и выполните установку по ссылке: https://www.rabbitmq.com/download.html.

3.После установки RabbitMQ, зайдите в каждый проект и найдите папку RabbitMQ. В классе RabbitMQSendMessage укажите явный путь к файлу конфигурации в переменной jsonFileConfig.

4.В каждом из проектов есть файлы конфигурации "appsettings", в которых можно изменить настройки подключения (если необходимо): логин, пароль, порт и т.д.

5.После этого перейдите в проект DataProcessorService, затем в папку MyDbContext. В классе SQLiteDataBase, в методе OnConfiguring, измените путь (YourPath) к базе данных SQLite, которая уже находится в проекте.

6.Теперь необходимо открыть свойства решения (Solution Properties) => Общие свойства (Common Properties) => Запуск (Startup Project) => выбрать "Несколько проектов для запуска" (Multiple startup projects) и установить параметр "Запуск" (Action) для обоих проектов на "Запуск" (Start).

7.Запустите проект.