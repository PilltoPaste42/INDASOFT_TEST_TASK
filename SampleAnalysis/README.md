### Инструкция по запуску проекта SampleAnalysis
1. Настроить подключение к БД, для этого в файле .\src\SampleAnalysis.WebAPI\appsettings.json в параметре DefaultConnection необходимо указать строку подключения к своей БД 
2. Запустить серверный компонент в режиме отладки через Visual Studio, для этого нужно назначить запускаемый проект SampleAnalysis.WebAPI и запустить отладку. Сервер будет доступен по адресу http://localhost:5066
3. Запустить web-клиент, для этого необходимо в директории .\src\SampleAnalysis.WebUI через терминал выполнить команду npm install для загрузки библиотек, затем выполнить команду npm run dev для запуска dev-сервера клиента. После данных действий клиент будет доступен по адресу http://localhost:5173

После успешного запуска проекта, открыть в браузере http://localhost:5173 и начать использование