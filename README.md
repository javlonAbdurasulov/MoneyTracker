Проект был выполнен на MVC. 
я создал свои микросервисы, инфраструктуру, домен и аппликейшн. 
В инфраструктуре работают все сервисы, репозитории, которые работают с DbContext, 
а в аппликейшн сервисы, которые не работают с базой данных и обабатывают поступившие данные. 
Главное запускать проект через главный start project MoneyTracker. При отсутствии кнопки выбора стартового проекта попробуйте перейти на branch-> asp и снова вернутся на master (лично у меня была такая проблема). 
Использовал Postgres. прикрпеплю файл или можно дать мигрейщин на части MoneyTracker.Infrastructure.
apppsettings я поставил все данные, которые я использовал для связи локальный Postgres. 
изменить этот Upsettings под свой Postgres. 
