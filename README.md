# Приложение "Дерево Подразделений"

## Описание
Приложение "Дерево Подразделений" отображает древовидную структуру подразделений организации, каждое из которых имеет уникальное наименование, статус (Активно/Заблокировано) и может входить в состав другого подразделения.

## Основные функции:
* Получение и отображение структуры подразделений в виде дерева.
* Фильтрация подразделений по наименованию.
* Автоматическое обновление данных каждые 3 секунды.
* Синхронизация данных с внешним источником по запросу.
* API для взаимодействия с другими сервисами.
## Технологии

### Backend:
* ASP.NET Core — для создания API сервисов А и B.
* Entity Framework Core — для взаимодействия с базой данных PostgreSQL.
* IdentityServer4 — для авторизации и аутентификации между сервисами.
* PostgreSQL — для хранения структуры подразделений.
### Frontend:
* React — для создания клиентского интерфейса.
* HTML/CSS/JavaScript — для работы с интерфейсом и реализации фильтрации данных.
## Структура проекта
ServiceA — предоставляет API для получения статусов подразделений.<br/>
ServiceB — отвечает за получение и отображение структуры подразделений, взаимодействует с ServiceA.<br/>
Frontend — веб-интерфейс для отображения подразделений в виде дерева, с возможностью фильтрации и синхронизации.<br/>
IdentityServer — используется для реализации защиты API и получения токенов авторизации для взаимодействия между сервисами.

# Установка
### Копировать код
`git clone https://github.com/your-username/department-tree.git`

### Настройка базы данных:
Убедитесь, что установлен PostgreSQL.<br/>
Настройте строку подключения в файле appsettings.json проекта ServiceB.<br/>
### Запуск API 
`Systems\Api\DepartmentTree.ApiA`<br/>
`dotnet run`

`cd Systems\Api\DepartmentTree.ApiB`<br/>
`dotnet run`

`cd Systems\Identity\DepartmentTree.Identity` <br/>
`dotnet run`

## Запуск Frontend
В папке с проектом Frontend выполните команды:<br/>
`cd Web/test_web`<br/>
`npm i react-router-dom`<br/>
`npm i react`<br/>
`npm i axios`<br/>

Запустите фронтенд:<br/>
`npm start`<br/>
Приложение будет доступно по адресу http://localhost:3000.<br/>
# Структура API:
## ServiceA
`GET api/ControllerA/{id}`<br/>
Возвращает статус для подразделения по его ID.<br/>

## ServiceB
`GET /api/ControllerB`<br/>
Возвращает список всех департаментов.<br/>
`POST /api/ControllerB/sync`<br/>
Выполняет синхронизацию статусов подразделений с ServiceA.<br/>

## Функционал фронтенда:
Иерархическая структура: отображает подразделения в виде дерева.<br/>
Синхронизация статусов: кнопка "Синхронизировать данные" обновляет вносит изменения в БД в соответствии с файлом.<br/>
Фильтрация подразделений: строка поиска и фильтрации подразделений по именам.<br/>

## Структура входного файла:
Файл должен состоять из строк, составляющих id, parentId, имя и статус подразделения.<br/>
### Пример:
1  Finance_subdivision Активно<br/>
2 1 Marketing_subdivision Заблокировано<br/>
То есть если подразделение находится на вешине дерева, то в parentId ничего не указывается.
