# BoxInfo64
Мониторинг железа ПК
<p align = "center">
  <img src = "https://user-images.githubusercontent.com/51737588/190921645-40004822-dc70-4a1f-966b-dc4b328c7870.png">
</p>

<p align = "center">
<img src = "https://img.shields.io/badge/%D0%9E%D0%A1%3A-Windows%207--10-blue">
</p>



<b>Написана на языке C#</b>

Программа BoxInfo64 предназначена для просмотра комплектующих в ПК. 

При создании программы я вдохновлялся другими похожими программами. Например в названии в конце я написал 64 как отсылка к программе "aida64" , а сам дизайн и интерфейс я взял как у программы "speccy"😉🎨.

Цель: понять как вовыдить инфу по железу и температуру

Как это работает?🧐

Если начать гуглить как вывести то или иное железо и температуру будет много сайтов где есть код при этом он будет для консоли то есть <b>нигде</b> не будет хорошего примера с WinForm. При этом если начать в тупую копи пастить то скорее всего будет ошибка примерно вот такая:


![Безымянный_result](https://user-images.githubusercontent.com/51737588/187036901-92fbeb2b-eba4-44f5-978b-f85b2fb3a83a.jpg)

Так вот она будет возникать если создать проект будь то формы или консоль это неважно , а то что <>bесли неправильно выбрать платформу при создании проекта</b>.

Впринципе можно сказать , что есть 2 основные платформы это : 1) .Net Core 2) . Net framework .

Различия примерно следующие 👀: 

- Net Core более новый и кроссплатформенный то есть linux , mac , windows и т.д можно кодить под разные ОС но нет поддержки старых библеотек .

- Net framework более старый но стабильный.Есть поддержка библеотек.Работает только на винде.

При создании проекта как на скрине нужно выбрать WinForm (.Net Framework)

![Безымянный](https://user-images.githubusercontent.com/51737588/187037592-17a0d34f-3d82-4bd3-b82e-a7a70ffd4337.jpg)

Опять же можно и не делать этого но например температуру вы врядли получите🌡️. Кстати чтобы выводит имена всяких комплектующих я использовал <b>"WMI"</b>.Это технология которая позволяет получить информаию о железе и системе 🖥️. А чтобы получить температуру я использовал библеотеку OpenHardwareMonitor от одноименной программы https://openhardwaremonitor.org/ . Благо это программа тоже написана c# и она с открытым исходным кодом. Нужно было скачать программу и вытащить из нее dll с таким же названием и подключить через using(смотри в коде).

Я приложил исходный код(с моими коменнтариями) и установщик (запускать в папке debug -> BoxInfo64 setup.msi ).

<hr>

<h3>Возможные баги</h3>

- На ноутбуках может появится при запуске ошибка и не вывести модель материнки



Скриншот🤠

![1](https://user-images.githubusercontent.com/51737588/191033676-2a20785e-36ae-4284-86f1-e80608e7fe9f.jpg)
![2](https://user-images.githubusercontent.com/51737588/191033680-d16d4e56-389b-40e8-bf31-40b5464e8bbb.jpg)

Полезные ссылки🔗:

1) https://www.youtube.com/watch?v=t35ji7wrDJQ (ролик с обяснением как работать с WMI )
2) https://docs.microsoft.com/ru-ru/windows/win32/wmisdk/using-wmi (сайт майков с документацией по WMI , также инфа по классам)
3) https://cpab.ru/v-chem-raznica-mezhdu-net-framework-i-net-core-cloudsavvy-it/ (В чем разница между .NET Framework и .NET Core )
4) https://www.youtube.com/watch?v=w5og9lCaqE8 (Что такое .Net Core и .Net Standard)
5) https://performancepsu.com/open-hardware-monitor-source-code-dll-with-c/ (статья на англ. о том как юзать библеотеку OpenHardware)
6) https://gist.github.com/grandsilence/cd7ce9d8bf87a5414b685e3e32542dd3 (удобная библеотека для получения инфы о мониторе)
7) https://www.youtube.com/watch?v=_tfDHzQ_7_U и  https://drive.google.com/drive/folders/1--GMIwuge6KdexB5CxqJG6LNSibgBjGL (тоже неплохая библеотека для получения ины о железе)

<b>На всякий пожарный оставлю здесь ссылки на неплохие статьи по WMI</b>

- Получение температуры процессора https://ourcodeworld.com/articles/read/335/how-to-retrieve-the-cpu-s-temperature-with-c-in-winforms

- Обьемный массив информации о оборудавнии (принтеры , ОС , жесткие диски крч много всего) https://ourcodeworld.com/articles/read/294/how-to-retrieve-basic-and-advanced-hardware-and-software-information-gpu-hard-drive-processor-os-printers-in-winforms-with-c-sharp

- Как получить объем оперативной памяти https://ourcodeworld.com/articles/read/879/how-to-retrieve-the-ram-amount-available-on-the-system-in-winforms-with-c-sharp

- Как получить информацию о материнской плате https://ourcodeworld.com/articles/read/314/how-to-retrieve-the-motherboard-information-with-c-sharp-in-winforms
