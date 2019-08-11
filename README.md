# KROK
SmartDressroom project on clever mirror gazelle.

Use VPN if can't connect

https://46.101.81.176/ - HTTP/2

http://46.101.81.176/ - HTTP/1.1

# WebSocket API:

- Строка подключения: http://46.101.81.176:80/mirrorhub

- [Документация к SignalR Core](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-2.2)
# Методы(для веб-интерфейса)

- onRoomInitialized(void) : void (send)

Запускает процесс инициализации комнаты на сервере. По окончании вызывает onRoomAdded.

- onRoomAdded(void) : int (receive)

Возвращает целочисленный номер комнаты от 1 до int.MaxValue, добавленной на сервер после вызова onRoomInitialized.

- onQueryMade(bool needsConsultant, object product) : void (send)

Запускает процесс создания запроса на сервере. На вход принимает булевый флаг, показывающий нужен ли консультант, и объект вещи.

Пример создания объекта вещи:
```javascript
var product = {
            "VendorCode": 'R241160',
            "SelectedSize": 'L,
            "ImgUrl": '~/images/clothes/KANZLER/R241160/{0}.jpg',
            "ImgCount": 5
        }
```
Вызов метода осуществляется либо для вызова консультанта, либо для запроса вещи, соответственно, возможны только два варианта комбинации аргументов:
```javascript
hub.send('onQueryMade', true, null); // вызов консультанта
hub.send('onQueryMade', false, product); // запрос вещи
```
По окончании выполнения вызывает onQueriesReceived.
# Методы(для мобильного приложения)

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
    "needsConsultant" : true
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
    "needsConsultant" : false
  }
]
```
- onQuerySent(string queryID, string servedBy) : void (send)

Функция принятия запроса консультантом. Принимает на вход ID принимаемого запроса и имя консультанта, который захотел принять этот запрос. Вызывает onQueryConfirmed.

- onQueryConfirmed(void) : string, bool (receive)

Функция которая принимает решение о том, может ли консультант принять запрос. Отправляет консультанту ID запроса и булевский флаг отражающий решение системы. Далее вызывает onQueriesReceived.

- onQueryClose(string queryID, string servedBy) : void (send)

Функция для уведомления системы о том, что консультант завершил обслуживание данного запроса. ринимает на вход ID принимаемого запроса и имя консультанта, который захотел закрыть этот запрос. Далее вызывает onQueriesReceived.
