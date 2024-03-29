# Учебный проект: Трехуровневая архитектура Маркетплейса на C#

## Описание проекта
Этот проект демонстрирует простую трехуровневую архитектуру для Маркетплейса, реализованную на C#. Проект включает в себя бэкенд и простой интерфейс для взаимодействия с ним.

## Структура проекта
- **Backend**: содержит бизнес-логику и слои доступа к данным (DAL).
- **Frontend**: упрощённый пользовательский интерфейс для взаимодействия с бэкендом.

## Трехуровневая архитектура
Проект использует трехуровневую архитектуру, включающую:
1. **Presentation Layer**: отвечает за представление данных и взаимодействие с пользователем.
2. **Business Logic Layer (BLL)**: содержит бизнес-логику приложения.
3. **Data Access Layer (DAL)**: обеспечивает доступ к данным и взаимодействие с базой данных.

## Используемые технологии
- **C#**: основной язык программирования.
- **ASP.NET Core**: для создания веб-приложения и веб-API.
- **Entity Framework Core**: для работы с базой данных.
- **CSVHelper**: для работы с CSV файлами.

## Структура проекта
- **/Backend**
- **/Presentation**: содержит контроллеры и модели для взаимодействия с пользователем.
- **/BLL**: содержит классы с бизнес-логикой приложения.
- **/DAL**
- **/Repositories**: содержит интерфейсы и реализации репозиториев для доступа к данным.
- **/Models**: модели данных.
- **/CSV**: реализация репозитория для работы с CSV файлами.
- **/Database**: реализация репозитория для работы с базой данных.

- **/Frontend**: содержит пользовательский интерфейс.

## Инструкции по развертыванию
1. Клонируйте репозиторий: `git clone https://github.com/artm44/Marketplace`
2. Установите необходимые зависимости с помощью менеджера пакетов NuGet.
3. Запустите проект в вашей среде разработки или соберите и запустите исполняемый файл.
4. Для работы с базой данных убедитесь, что у вас установлен и сконфигурирован соответствующий сервер баз данных.

## Автор
artm44
