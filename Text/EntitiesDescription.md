## Документация

#### На будущее:
* Выделить метод считывания исходных данных (при изменении формата исходных данных нужно будет менять только один метод);


// ! — элемент нужно задать при создании объекта

#### Основные сущности:
* Id ВС; +
* ВС; +
* Взлетающий ВС; +
* Садящийся ВС; +
* Спец площадка; +
* ВПП; +
* Контроллер реализуемости планового момента выхода; +
* Генератор ВС; +
* Момент ВС; +
* Интервал ВС; +
* Перечисление типов моментов; +
* Перечисление типов интервалов; +
* Момент; +
* Интервал; +
* Таблица (пока что таблица, дальше видно будет); +
* Строка таблицы; +
* Модель (организующий класс); +
* Форма; +
* Стадия выполнения; +
* Перечисление {Стадий выполнения}; +
* Генератор Id для ВС; +

---

#### Содержание сущностей:

##### Id ВС: +

##### _Данные:_
* Значение Id;


##### _Методы:_
* Вернуть Id; (ИНТЕРФЕЙС) И.1
* Задать Id; (ИНТЕРФЕЙС) И.2


=================================================================================

##### Генератор Id для ВС: (Синглтон) +

##### _Данные:_
* Id;
* Данные для обеспечения паттерна Синглтон;


##### _Методы:_
##### * Получить уникальный Id:
	1) Возвращаем хранящийся Id + 1;

##### * Методы для обеспечения паттерна Синглтон;


----------------------

##### ВС: (ИНТЕРФЕЙС) +

##### _Данные:_
* Id;
* Моменты;
* Интервалы;

##### _Методы:_
* Возврат интервала занимания {ВПП} (И.1);


----------------------------------

#### Взлетающий ВС: (наследование от ВС) +

##### _Данные:_
* {ВПП};
* {Спец площадка}
* Флаг необходимости обработки; ! (ИНТЕРФЕЙС)
* максимум ожидания обработки; !
* константный интервал для безопасного слияния; !
* максимум ожидания на предварительном старте; !

##### _Методы:_
##### * Расчет момента выхода на ПРСТ (уже с учетом задержек): (2.1, 3.1) +
	1) Если обработка не нужна => вызываем метод (1.1), прибавляем время движения от стоянки до ПРСТ. Возвращаем результат;
	2) Если обработка нужна => вызываем метод (1.2), прибавляем время движения от стоянки до спец площадки, прибавляем время обработки и время движения от спец площадки до ПРСТ. Возвращаем сумму;

##### * Расчет интервала ожидания на ПРСТ: (1.1) +
	1) Вызываем метод (1.1.1), получаем момент выхода на исполнительный старт (без учета задержек);
	2) Вызываем метод (1.1.2), получаем момент взлета (без учета задержек);
	3) Вызываем метод {ВПП} И.1 и передаем в него полученные моменты; 
	4) Возвращаем результат;

##### * Расчет суммы интервала ожидания обработки и интервала для безопасного слияния: (1.2) +
	1) Вызываем метод (1.2.1), получаем момент прибытия на {Спец площадку} (без учета задержек);
	2) Вызываем метод (1.2.2), получаем момент покидания {Спец площадки} (без учета задержек);
	3) Вызываем метод {Спец площадки} И.1 и передаем в него полученные моменты;
	4) Возвращаем результат;

##### * Расчет момента выхода на исполнительный старт (без учета задержек): (1.1.1) +
	1) Возвращаем сумму = время движения от стоянки до ПРСТ + время руления на ИСПСТ;

##### * Расчет момента взлета (без учета задержек): (1.1.2) +
	1) Возвращаем сумму = возврат метода (1.1.1) + время взлета;

##### * Расчет момента прибытия на {Спец площадку} (без учета задержек): (1.2.1) +
	1) Возвращаем время движения от стоянки до Спец площадки;

##### * Расчет момента покидания {Спец площадки} (без учета задержек): (1.2.2) +
	1) Возвращаем сумму = возврат метода (1.2.1) + время обработки;

##### * Расчет момента выхода на исполнительный старт (уже с учетом задержек): (2) +
	1) Вызываем метод (2.1);
	2) Прибавляем к полученному/известному моменту выхода на ПРСТ интервал времени руления на исп. старт и возвращаем сумму;

##### * Расчет момента взлета (покидания ВПП): (3) +
	1) Вызываем метод (2);
	2) Прибавляем к полученному/известному моменту выхода на ИСПСТ интервал времени взлета. Возвращаем сумму;

##### * Расчет момента запуска двигателей: (ИНТЕРФЕЙС) (И.1) +
	1) Если обработка не нужна => вызываем метод (1.1), прибавляем момент появления и возвращаем новый момент;
	2) Если обработка нужна => вызываем метод (1.2), прибавляем прибавляем момент появления и возвращаем новый момент;

##### * Возврат интервала занимания {ВПП} (уже с учетом задержек, чтобы записать в {ВПП}): (ИНТЕРФЕЙС) (И.2) (Уже есть от родителя) +
	1) Вызываем метод (2), получаем момент прибытия на ИСПСТ;
	2) Вызываем метод (3), получаем момент взлета;;
	3) Формируем интервал и возвращаем его;

##### * Возврат интервала занимания {Спец площадки} (уже с учетом задержек, чтобы записать в {Спец площадку}): (ИНТЕРФЕЙС) (И.3) +
	0) Если обработка не нужна — выбрасываем исключение с соответствующим сообщением;
	1) Вызываем метод (1.2) и прибавляем время движения от стоянки до спец. площадки. Получили момент прибытия;
	2) К полученному моменту прибавляем время обработки. Получили момент освобождения;
	3) Формируем интервал и возвращаем его;

##### * Проверка выполнимости планового момента: (ИНТЕРФЕЙС) (И.4) +
	1) Получаем класс-контроллер;
	2) В зависимости от необходимости обработки вызываем у него нужный метод;
	2) Возвращаем значение;


-------------

#### Садящийся ВС: (наследование от ВС) +

##### _Данные:_
* Моменты; (от родителя)
* Интервалы; (от родителя)

##### _Методы:_
##### * Возврат интервала занимания {ВПП}: (ИНТЕРФЕЙС) (И.1) (от родителя)
	1) Момент посадки нам задан;
	2) Получаем момент освобождения ВПП, прибавив к моменту посадки интервал пробега по ВПП;
	3) Создаем интервал занимания ВПП;
	3) Возвращаем полученный интервал;


=================================================================================

#### Перечисление типов моментов: +

##### Типы:
* появление; !
* плановое прибытие на предварительный старт; !
* отправление (запуск двигателей);
* прибытие на предварительный старт;
* прибытие на исполнительный старт;
* покидание ВПП (взлет);


=================================================================================

#### Перечисление типов интервалов: +

##### Типы:
* движение от стоянки на предварительный старт; ! (мб не задано)
* движение от стоянки до спец площадки; !
* ожидание обработки; 
* ожидание безопасного слияния;
* обработка; !
* движение от площадки к предварительному старту; !
* ожидание на предварительном старте; 
* руление на исполнительный старт; !
* взлет; !


=================================================================================

#### Момент: +

##### _Данные:_
* тип; (ИНТЕРФЕЙС)
* временное значение, мс;

##### _Методы:_
* Получить временное значение; (ИНТЕРФЕЙС) И.2
* Задать временное значение; (ИНТЕРФЕЙС) И.3
* Оператор сложения момента и интервала и наоборот; 


=================================================================================

#### Интервал: +

##### _Данные:_
* тип; (ИНТЕРФЕЙС)
* начальный момент; (ИНТЕРФЕЙС)
* конечный момент; (ИНТЕРФЕЙС)

##### _Методы:_
* Получить значение интервала в мс между своими моментами; (ИНТЕРФЕЙС) И.1
* Оператор сложения интервалов; 
* Оператор сложения интервала и момента и наоборот;


=================================================================================

#### Спец площадка: (наследование от {Зоны с последовательным доступом}) +

##### _Данные:_
* Id; (ИНТЕРФЕЙС)
* интервалы занимания площадки: (есть от ЗПД); (ИНТЕРФЕЙС)


##### _Методы:_

##### * Метод расчета интервала для безопасного слияния: (1)
	1) Принимаем два интервала. 
	2) Вычисляем модуль разности между начальными моментами этих интервалов;
	3) Если полученная разность >= интервалу для безопасного слияния => возвращаем ноль;
	4) Если нет => возвращаем интервал для безопасного слияния = константное значение
		интервала для безопасного слияния - рассчитанная разность;

##### * Метод, возвращающий задержки при добавлении нового судна в конец очереди: (3)
	1) Принимаем интервал обратившегося судна;
	2) Сохраняем интервал ожидания обработки = момент покидания площадки последним записанным судном 
	минус момент прибытия (без задержки) обратившегося судна;
	3) Вызываем метод (1) и передаем ему последний записанный интервал и текущий интервал, сдвинутый на сохраненное ожидание обработки;
	4) Сохраняем полученное значение;
	4) Возвращаем в кортеже интервал ожидания обработки и интервал для безопасного слияния;

##### * Метод, возвращающий задержку для ожидания обработки и для безопасного слияния (в кортеже): (ИНТЕРФЕЙС) (И.1)
	1) Создаем интервал занимания обратившегося судна из переданных им данных;
	2) Получаем ключи левого и правого интервала для словаря интервалов (метод (1) ЗПД) относительно созданного интервала;
	3) Проверяем пересечение созданного интервала с левым и правым (метод (2) ЗПД):
		3.1) Если пересечений нет => 
			3.1.1) Вызываем метод (1) для текущего и левого и текущего и правого интервалов;
			3.1.2) Если метод (1) в обоих случаях вернул 0 => возвращаем два нуля;
			3.1.3) Если нет => сохраняем полученное число и определяем, с каким интервалом, левым или правым, 
			идет конфликт (уже сделано в пункте 3.1.1):
				3.1.3.1) Если с правым => возвращаем значение метода (3);				
				3.1.3.2) Если с левым => сдвигаем интервал ВС вправо на сохраненное в пункте 3.1.3 значение ожидания (метод (2)). 
			3.1.4) Снова проверяем наличие безопасных интервалов между моментами прибытия (метод (1)). 
			Вызываем метод (1) для текущего и левого и текущего и правого интервалов:
				3.1.4.1) Если метод (1) в обоих случаях вернул 0 => проверяем пересечение интервалов (метод (2) ЗПД):
					3.1.4.1.1) Если есть пересечение => возвращаем значение метода (3);
					3.1.4.1.2) Если нет => возвращаем интервал ожидания обработки = 0 и интервал для безопасного слияния = 
					сохраненному в пункте 3.1.3 значению ожидания;
				3.1.4.2) Если нет => возвращаем значение метода (3);
		3.2) Если пересечения есть => возвращаем значение метода (3);

##### * Метод, возвращающий задержку для ожидания обработки и для безопасного слияния (в кортеже):
	1) Принимаем интервал занимания Спец площадки и задержку для безопасного слияния от обратившегося судна;
	2) Получаем новый расширенный влево и вправо на задержку для безопасного слияния интервал:
		2.1) Вычитаем (не уходя в отрицательные числа) из начального момента переданную задержку;
		2.2) Прибавляем к конечному моменту переданную задержку;
	3) Проверяем пересечение расширенного интервала с ближайшими левым и правым интервалом;
	4) Если пересечения нет => возвращаем два нуля;
	5) Если есть => проверяем пересечение для изначального интервала:
	6) Если есть пересечение => рассчитываем задержку ожидания обработки и задержку для безопасного слияния;
	7) Если нет => рассчитываем задержку для безопасного слияния;

##### * Метод сдвига интервала: (2)
	1) Получаем интервал и значение сдвига;
	2) Увеличиваем значение начального и конечного момента на переданное значение;
	3) Возвращаем новый интервал;
	

---------

#### ВПП: (наследование от {Зоны с последовательным доступом}): +

##### _Данные:_
* Id; (ИНТЕРФЕЙС)
* интервалы занимания ВПП: (есть от ЗПД); (ИНТЕРФЕЙС)

##### _Методы:_
##### * Возвращающий минимальное время ожидания на ПРСТ: (ИНТЕРФЕЙС) (И.1)
	1) Создаем интервал занимания обратившегося судна из переданных им данных;
	2) Проверяем пересечение полученного интервала с записанными в ВПП интервалами (метод (2) ЗПД):
		2.1) Если пересечений нет => возвращаем ноль;
		2.2) Если есть => 
			2.2.1) Получаем начальный момент (ключ для словаря) последнего обратившегося судна;
			2.2.2) Получаем момент покидания ВПП последнего обратившегося судна;
			2.2.3) Рассчитываем интервал ожидания на ПРСТ = момент покидания ВПП последним записанным судном 
            минус момент прибытия (без задержки) обратившегося судна;
            2.2.4) Возвращаем полученный интервал;


===============================================================================

#### Зона с последовательным доступом (ЗПД): (ИНТЕРФЕЙС) +

##### _Данные:__
1) интервалы занимания зоны:
	Словарь, где ключ — начальный момент, значение — конечный момент;

##### _Методы:_
##### * Определяющий пересечение интервалов: (2)
	1) Принимаем интервал обратившегося судна;
	2) Вызываем метод (1), передаем ему созданный интервал. Получаем левый и правый интервалы;
	3) Если начальный момент текущего интервала меньше конечного момента левого интервала => пересечение;
	4) Если конечный момент текущего интервала больше начального момента правого интервала => пересечение;
	5) Если ни то и ни другое => нет пересечения;

##### * Метод, находящий интервалы ближайших судов слева и справа относительно момента прибытия обратившегося судна: (1)
	1) Получаем интервал обратившегося судна; 
	2) Локально получаем список ключей словаря из полей класса;
	3) Добавляем в список начальный момент полученного интервала;
	4) Сортируем список по возрастанию;
	5) Получаем начальные моменты (по сути ключи словаря) левого и правого интервала;
	6) Находим интервалы;
	7) Возвращаем эти интервалы;

##### * Добавление нового интервала занимания: (3)
	1) Получаем интервал;
	2) Добавляем в словарь;

##### * Удаление интервала занимания: (4)
	1) Получаем интервал;
	2) Удаляем интервал по ключу (начальному моменту переданного интервала);


-------

#### Генератор ВС: +


##### _Данные:__
* Генератор Id для ВС;
* Рандомайзер;
* константный максимум ожидания обработки; 
* константный интервал для безопасного слияния; 
* константный максимум ожидания на предварительном старте; 


##### _Методы:__
##### * Метод создания {Взлетающего ВС}: (ИНТЕРФЕЙС) (И.1)
	1) Вызываем метод (1);
	2) Создаем {Взлетающее ВС};
	3) Возвращаем;

##### * Метод создания {Садящегося ВС}: (ИНТЕРФЕЙС) (И.2)
	1) Вызываем метод (2)
	2) Создаем {Садящееся ВС};
	3) Возвращаем;

##### * Метод, возвращающий исходные данные для создания {Взлетающего ВС}: (1) +
	1) Получаем (из файла, записью напрямую или еще как) данные для создания взлетающего ВС;

##### * Метод, возвращающий интервалы для создания {Взлетающего ВС}: (3) +
	1) 

##### * Метод, возвращающий исходные данные для создания {Садящегося ВС}; (2) +
	1) Получаем (из файла, записью напрямую или еще как) данные для создания садящегося ВС;

-----

#### Модель (организующий класс): +

##### _Данные:_
* Таймер для {Взлетающих ВС}; (2)
* Таймер для {Садящихся ВС}; (1)
* Секундомер;
* {Генератор ВС};
* {Генератор Id для ВС};
* ВП полосы;
* Спец площадки;
* {Таблица};
* {Класс-контроллер};
* {Стадия выполнения};
* Событие изменения {Стадии выполнения};


##### _Конструктор:_
	1) Принимаем {Таблицу};

##### _Методы:_
##### * Обработка таймера (1):
	1) Вызываем метод {Генератора} и передаем ему ссылку на ВПП (И.2);
	2) Вызываем метод (8) и передаем в него созданный ВС;

##### * Обработка таймера (2): 
	1) Вызываем метод {Генератора} и передаем ссылки на ВПП и Спец площадки (И.1);
	2) Вызываем метод (7) и передаем в него созданный ВС;

##### * Регистрация {ВС} на {ВПП}: (3) +
	1) Получаем интервал занимания ВПП текущим судном;
	2) Добавляем интервал занимания ВПП через метод (3) ЗПД;

##### * Регистрация {Взлетающего ВС} на {Спец площадке}: (4) +
	1) Получаем интервал занимания Спец площадки текущим судном;
	2) Добавляем интервал занимания Спец площадки через метод (3) ЗПД;

##### * Вывод информации в таблицу: (5) +
	1) Принимаем данные для вывода в таблицу;
	1) Вызываем метод {Таблицы} (И.1) и передаем в него данные;

##### * Изменить {Cтадию выполнения}: (ИНТЕРФЕЙС) (И.1) +
	1) Получаем тип стадии;
	2) Изменяем тип;
	3) Дергаем событие изменения {Стадии выполнения};

##### * Обработка события изменения {Стадии выполнения}: (6) +
	1) Если стадия = "Подготовка" =>
		1.1) Сбрасываем секундомер;
		1.2) Останавливаем таймеры;
		1.3) Очищаем {Спец площадки}, {ВПП}, {Таблицу};
	2) Если стадия = "Запущена" => 
		2.1) Запускаем таймеры;
		2.2) Запускаем секундомер;
	3) Если стадия = "Пауза" => 
		3.1) Останавливаем таймеры;
		3.1) Останавливаем секундомер;

##### * Полный рабочий цикл {Взлетающего ВС}: (7) +
	1) Принимаем созданное {Взлетающее ВС} ({ВВС});
	2) Проверяем выполнимость планового выхода на ПРСТ:
		2.1) Если обработка нужна => вызываем метод {Контроллера} (И.2) и передаем {ВВС};
		2.2) Если обработка не нужна => вызываем метод {Контроллера} (И.1) и передаем {ВВС};
		2.3) Если выполнимо => 
			2.3.1) Получаем интервал занимания ВПП через метод {ВВС} (И.2);
			2.3.2) Регистрируем {ВВС} на ВПП через метод (3);
			2.3.3) Если обработка нужна => 
				2.3.3.1) Получаем интервал занимания {Спец площадки} через метод {ВВС} (И.3);
				2.3.3.2) Регистрируем {ВВС} на {Спец площадке} через метод (4);
			2.3.4) Получаем момент запуска двигателей через метод {ВВС} (И.1);
			2.3.5) Добавляем информацию о ВС в {Таблицу} через метод (5);
		2.4) Если не выполнимо => 
			2.4.1) Добавляем информацию о ВС в {Таблицу} через метод (5);s

##### * Полный рабочий цикл {Садящегося ВС}: (8) +
	1) Принимаем созданное {Садящееся ВС} ({СВС});
	2) Получаем интервал занимания ВПП через метод {СВС} (И.1);
	3) Регистрируем {СВС} на ВПП через метод (3);


---------------------------------------

#### Таблица: +

##### _Конструктор:_
	1) Принимаем DataGridView;

#### _Данные:_
* DataGridView как графическая основа;
* Список {Строк таблицы};

#### _Методы:_
##### * Добавление строки: (ИНТЕРФЕЙС) (И.1)
	1) Принимаем данные, необходимые для создания строки;
	2) Создаем {Строку таблицы} через метод (1);
	3) Добавляем ее в список;

##### * Удаление строки: (ИНТЕРФЕЙС) (И.2)
	1) Принимаем Id {Строки таблицы};
	2) Получаем индекс {Строки таблицы} в списке через метод (2);
	3) Удаляем строку из списка;

##### * Изменение значения строки: (ИНТЕРФЕЙС) (И.4)
	1) Принимаем Id {Строки таблицы};
	2) Принимаем новую строку с новыми значениями;
	3) Получаем индекс {Строки таблицы} в списке через метод (2);
	4) Удаляем старую строку;
	5) Вставляем на место старой строки на новую;

##### * Создать {Строку таблицы}: (1)
	1) Получаем все данные, кроме Id строки;
	2) Получаем Id как: количество строк в таблице + 1;
	3) Создаем {Строку таблицы};
	4) Возвращаем;

##### * Получить индекс строки по Id строки: (2)
	1) Получаем Id {Строки таблицы};
	2) Возвращаем Id - 1;


----------------------------

#### Строка таблицы: +

##### _Данные:_
* Id строки;
* Id {Взлетающего ВС};
* Момент запуска двигателей;
* Признак выполнимости планового момента;

##### _Конструктор:_
	1) Принимаем все необходимые данные для создания строки;
	2) Заполняем поля переданными данными;


--------------------------------

#### Контроллер реализуемости планового момента выхода: +


##### _Методы:_
##### * Метод проверки реализуемости без ПОО: (ИНТЕРФЕЙС) (И.1)
	1) Принимаем данные, необходимые для проверки;
	2) Возвращаем результат проверки;

##### * Метод проверки реализуемости с ПОО : (ИНТЕРФЕЙС) (И.2)
	1) Принимаем данные, необходимые для проверки;
	2) Возвращаем результат проверки;


------------------

#### Форма:

##### _Данные:_
* {Модель}
* {Таблица}
* Кнопка "Старт";
* Кнопка "Пауза";
* Кнопка "Стоп";


##### _Методы:_
##### * Обработчик нажатия кнопки "Старт":
	1) Изменяем стадию выполнения на "Запущено" через метод {Модели} (И.1)

##### * Обработчик нажатия кнопки "Пауза":
	1) Изменяем стадию выполнения на "Пауза" через метод {Модели} (И.1)

##### * Обработчик нажатия кнопки "Стоп":
	1) Изменяем стадию выполнения на "Подготовка" через метод {Модели} (И.1)

##### * Создание {Таблицы}:
	1) Создаем DataGridView и передаем в конструктор {Таблицы};



-----------------------------

#### Перечисление {Стадий выполнения}: +

##### _Данные:_
* Подготовка;
* Запущена;
* Пауза;