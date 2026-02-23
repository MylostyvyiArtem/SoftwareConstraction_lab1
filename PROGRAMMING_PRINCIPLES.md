# Принципи програмування у проєкті Chat Server

## 1. KISS (Keep It Simple, Stupid)
Система спроектована максимально просто та зрозуміло. Методи, такі як `Connect`, `Disconnect` та `SendPrivateMsg`, виконують рівно те, що вказано в їхній назві, без зайвої складності або перевантаженої логіки. Використовуються прості формати рядків для повідомлень замість складних протоколів.
* **Посилання на код:** [ServiceChat.cs (Метод SendPrivateMsg)](https://github.com/MylostyvyiArtem/SoftwareConstraction_lab1/blob/3eb1af3721f99f56b1b3b0c9d067190d3cabde48/ChatServer/ServiceChat.cs#L60-L77)

## 2. DRY (Don't Repeat Yourself)
Для уникнення дублювання коду було створено допоміжний метод `UpdateAllUsersList()`. Замість того, щоб двічі писати цикл для оновлення списку користувачів в інтерфейсі при підключенні та відключенні клієнта, ця логіка винесена в один метод, який викликається як у `Connect()`, так і в `Disconnect()`.
* **Посилання на код:** [ServiceChat.cs (Метод UpdateAllUsersList)](https://github.com/MylostyvyiArtem/SoftwareConstraction_lab1/blob/3eb1af3721f99f56b1b3b0c9d067190d3cabde48/ChatServer/ServiceChat.cs#L79-L90)

## 3. Encapsulation (Інкапсуляція)
Внутрішній стан сервера, зокрема список активних підключень (`List<ServerUser> users`) та лічильник ідентифікаторів (`int nextId`), приховані всередині класу `ServiceChat` (мають модифікатор доступу `private` за замовчуванням). Це захищає список користувачів від випадкових змін ззовні, змушуючи інші класи (наприклад, клієнтські) використовувати лише безпечні публічні методи сервера.
* **Посилання на код:** [ServiceChat.cs (Оголошення змінних стану)](https://github.com/MylostyvyiArtem/SoftwareConstraction_lab1/blob/3eb1af3721f99f56b1b3b0c9d067190d3cabde48/ChatServer/ServiceChat.cs#L13-L14)
