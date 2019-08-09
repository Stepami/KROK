# KROK
SmartDressroom project on clever mirror gazelle.

Use VPN if can't connect

https://46.101.81.176/ - HTTP/2

http://46.101.81.176/ - HTTP/1.1

# WebSocket API(для мобильного приложения):

- Строка подключения: http://46.101.81.176:80/mirrorhub

- [Документация к SignalR Core](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-2.2)

# Методы

- onConsultantLoggedIn(void) : void (send)

Добавляет консультанта в группу хаба для получения списка запросов. Далее вызывает onQueriesReceived.

- onQueriesReceived(void) : string (receive)

Возвращает JSON-строку со списком запросов на метод.

Пример ответа от хаба:
```javascript
[
  {
    "id" : "90f436be-522f-465d-a3aa-c20d1c1110f2",
    "createdAt" : "2019-08-09T17:05:58.874Z",
    "status" : 1,
    "servedBy" : null,
    "room" :
    {
      "number" : 1,
      "responsible" : null,
      "hubID" : "azIO6oem9xa26iT_n3bFfA"
    },
    "product" : null,
    "needsConsultant" : false
  },
  {
    "id" : "48427a30-ed8a-486f-b523-7bb261d25b73",
    "createdAt" : "2019-08-09T17:06:02.927Z",
    "status" : 1,
    "servedBy" : null,
    "room" :
    {
      "number" : 1,
      "responsible" : null,
      "hubID" : "azIO6oem9xa26iT_n3bFfA"
    },
    "product" :
    {
      "vendorCode" : "R240580",
      "selectedSize" : "S",
      "imgUrl" : "~/images/clothes/KANZLER/R240580/{0}.jpg",
      "imgCount" : 5
    },
    "needsConsultant" : true
  }
]
```
- onQuerySent(string queryID, string servedBy) : void (send)

Функция принятия запроса консультантом. Принимает на вход ID принимаемого запроса и имя консультанта, который захотел принять этот запрос. Вызывает onQueryConfirmed.

- onQueryConfirmed(void) : string, bool (receive)

Функция которая принимает решение о том, может ли консультант принять запрос. Отправляет консультанту ID запроса и булевский флаг отражающий решение системы. Далее вызывает onQueriesReceived.

- onQueryClose(string queryID, string servedBy) : void (send)

Функция для уведомления системы о том, что консультант завершил обслуживание данного запроса. ринимает на вход ID принимаемого запроса и имя консультанта, который захотел закрыть этот запрос. Далее вызывает onQueriesReceived.
