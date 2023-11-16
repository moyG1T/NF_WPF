use Hellraid_Tyr
-------------------------Лабораторная работа 2--------------------------------------
--1.Вывести ФИО, специализацию и дату рождения всех академиков.
Select First_name, Second_name, Patronymic,s.Name_specialization, a.Birthday_date from Academics a join Specialization s  on s.ID = a.ID_specialization
--2.Создать вычисляемое поле «О присвоении звания», которое содержит информацию об академиках в виде: «Петров Петр Петрович получил звание в 1974».
Select (a.Second_name + ' ' + a.First_name + ' ' + a.Patronymic + ' получил звание в ' + cast(a.Year_of_title as nvarchar)) as Кто_когда from Academics a
--3.Вывести ФИО академиков и вычисляемое поле «Через 5 лет после присвоения звания».
Select First_name, Second_name, Patronymic, ('Через '+ cast(a.Year_of_title+5 as nvarchar)+ ' лет после присвоения звания') AS ВЫЧ_ПОЛЕ from Academics A 
--4.Вывести список годов присвоения званий, убрав дубликаты.
Select DISTINCT a.Year_of_title from Academics a
--5.Вывести список академиков, отсортированный по убыванию даты рождения.
Select First_name, Second_name, Patronymic, a.Birthday_date from Academics A 
order by a.Birthday_date Desc
--6.Вывести список академиков, отсортированный в обратном алфавитном порядке специализаций, по убыванию года присвоения звания, и в алфавитном порядке ФИО.
Select First_name, Second_name, Patronymic, s.Name_specialization from Academics a join Specialization s on s.ID = a.ID_specialization
order by s.Name_specialization desc
--7.Вывести первую строку из списка академиков, отсортированного в обратном ал-фавитном порядке ФИО.
Select top 1 First_name, Second_name, Patronymic from Academics a 
order by a.Second_name desc
--8.Вывести фамилию академика, который раньше всех получил звание.
Select top 1 First_name, Second_name, Patronymic, Year_of_title from Academics a 
order by  a.Year_of_title
--9.Вывести первые 10% строк из списка академиков, отсортированного в алфавитном порядке ФИО.
Select top 10 PERCENT First_name, Second_name, Patronymic from Academics a 
order by a.Second_name 
--10.Вывести из таблицы «Академики», отсортированной по возрастанию года присво-ения звания, список академиков, у которых год присвоения звания – один из первых пяти в отсортированной таблице.
Select top 5 First_name, Second_name, Patronymic, Year_of_title from Academics a
order by  a.Year_of_title
--11.Вывести, начиная с десятого, список академиков, отсортированный по возраста-нию даты рождения.
Select First_name, Second_name, Patronymic, a.Birthday_date from Academics A 
order by a.Birthday_date 
Offset 9 rows
--12.Вывести девятую и десятую строку из списка академиков, отсортированного в ал-фавитном порядке ФИО.
Select  First_name, Second_name, Patronymic from Academics a 
order by a.Second_name 
 SELECT First_name, Second_name, Patronymic FROM (
    SELECT *, ROW_NUMBER() OVER (ORDER BY a.Second_name) AS row_num
    FROM Academics a) AS numbered_rows
WHERE row_num IN (10, 12);

-------------------------Лабораторная работа 3--------------------------------------
--1.	Вывести названия и столицы пяти наибольших стран по площади.
Select top 5 c.Name, c.Capital, c.Area from Countries c
order by c.Area
--2.	Вывести список африканских стран, население которых не превышает 1 млн. чел.
select * from Countries c
where c.Сontinent = 'Африка' and c.Population >= 1000000 
--3.	Вывести список стран, население которых больше 5 млн. чел., а площадь меньше 100	тыс. кв. км, и они расположены не в Европе.
select * from Countries c
where c.Сontinent <> 'Европа' and c.Population >= 5000000 and c.Area < 100000
--4.	Вывести список стран Северной и Южной Америки, население которых больше 20	млн. чел., или стран Африки, у которых население больше 30 млн. чел.
select * from Countries c
Where (c.Сontinent <> 'Южная Америка' and c.Сontinent <> 'Северная Америка' and c.Population >= 20000000) or (c.Сontinent = 'Африка' and c.Population >= 30000000)
--5.	Вывести список стран, население которых составляет от 10 до 100 млн. чел., а пло-щадь не больше 500 тыс. кв. км.
select * from Countries c
where (c.Population > 10000000 and c.Population < 100000000) and c.Area <= 500000
--6.	Вывести список стран, названия которых не начинаются с буквы «К>
Select * from Countries c
WHERE (LEFT(c.name , 1)) <> 'K'
--7.	Вывести список стран, в названии которых третья буква – «а», а предпоследняя –«и».
Select * from Countries c
WHERE LOWER(SUBSTRING(name, 3, 1)) = 'а'      
AND LOWER(SUBSTRING(name, len(name) - 1, 1)) = 'и'
--8.	Вывести список стран, в названии которых вторая буква – гласная.
Select * from Countries c
WHERE LOWER(SUBSTRING(name, 2, 1)) = 'а'   or   
 LOWER(SUBSTRING(name, 2, 1)) = 'я'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'у'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'ю'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'о'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'е'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'ё'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'э'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'и'   or    
 LOWER(SUBSTRING(name, 2, 1)) = 'ы'  
--9.	Вывести список стран, названия которых начинаются с букв от «К» до «П».
Select * from Countries c
WHERE (LEFT(c.name , 1)) = 'К' or
(LEFT(c.name , 1)) = 'Л' or
(LEFT(c.name , 1)) = 'М' or
(LEFT(c.name , 1)) = 'Н' or
(LEFT(c.name , 1)) = 'О' or
(LEFT(c.name , 1)) = 'П' 
--10.	Вывести список стран, названия которых начинаются с букв от «А» до «Г», но не с буквы «Б».
Select * from Countries c
WHERE (LEFT(c.name , 1)) = 'А' or
(LEFT(c.name , 1)) = 'В' or
(LEFT(c.name , 1)) = 'Г' 
--11.	Вывести список стран, столицы которых есть в базе.
Select * from Countries c
Where c.Capital is not null
--12.	Вывести список стран Африки, Северной и Южной Америки.
select c.Name, c.Сontinent from Countries c
where c.Сontinent = 'Африка' or c.Сontinent = 'Северная Америка' or c.Сontinent = 'Южная Америка'

-------------------------Лабораторная работа 4--------------------------------------
--1.	Вывести список академиков, отсортированный по количеству символов в ФИО.
SELECT CONCAT(a.Second_name, ' ', a.First_name,' ', a.Patronymic) as FIO FROM Academics a
ORDER BY LEN(CONCAT(a.Second_name, ' ', a.First_name,' ', a.Patronymic))
--2.	Вывести список академиков, убрать лишние пробелы в ФИО.
SELECT TRIM(CONCAT(a.Second_name, a.First_name, a.Patronymic)) FROM Academics a
--3.	Найти позиции «ов» в ФИО каждого академика. Вывести ФИО и номер позиции.
SELECT CHARINDEX('о',CONCAT(a.Second_name, ' ', a.First_name,' ', Patronymic)), 
CONCAT(a.Second_name, ' ', a.First_name,' ', Patronymic) 
FROM Academics a
--4.	Вывести ФИО и последние две буквы специализации для каждого академика.
SELECT a.Second_name, s.Name_specialization, LEFT(s.Name_specialization, 3) FROM Academics a
join Specialization s on s.ID= a.ID_specialization
--5.	Вывести список академиков, ФИО в формате Фамилия и Инициалы.
SELECT (a.Second_name + ' ' +TRIM(LEFT(a.First_name, 1)) + '. ' + TRIM(LEFT(a.Patronymic, 1 ))+ '. ') FROM Academics a
--6.	Вывести список специализаций в правильном и обратном виде. Убрать дубликаты.
SELECT DISTINCT s.Name_specialization, REVERSE(s.Name_specialization) FROM Academics a
join Specialization s on s.ID= a.ID_specialization
--7.	Вывести свою фамилию в одной строке столько раз, сколько вам лет.
SELECT Replicate(a.Second_name, YEAR(GETDATE()) - YEAR(a.Birthday_date)) FROM Academics a
SELECT YEAR(GETDATE()) - YEAR(a.Birthday_date) FROM Academics a
--8X.	Вывести абсолютное значение функций2 (2) −   (3  2) с точностью два знак после десятичной запятой.
--9.	Вывести количество дней до конца семестра.
PRINT 'Critical Error!!'
--10.	Вывести количество месяцев от вашего рождения.
PRINT CAST(MONTH(GETDATE()) - 8 as NVARCHAR(max)) + ' месяцев'
--11.	Вывести ФИО и високосность года рождения каждого академика.
SELECT a.Second_name, YEAR(a.Birthday_date) ДР, 
CASE YEAR(a.Birthday_date) % 4
     WHEN 0 THEN 'Високосный'
     ELSE 'Не високосный'
END Год
FROM Academics a
--12.	Вывести список специализаций без повторений. Для каждой специализации выве-сти «длинный» или «короткий», в зависимости от количества символов.
SELECT s.Name_specialization, 
CASE  
	WHEN LEN(s.Name_specialization) <= 5 THEN 'короткий'
	else 'длинный'
END Величина
FROM Specialization s
-------------------------Лабораторная работа 5--------------------------------------
--1.	Вывести минимальную площадь стран.
Select Min(c.Area) from Countries c
--2.	Вывести наибольшую по населению страну в Северной и Южной Америке.
Select Max(c.Population) from Countries c
where c.Сontinent = 'Северная Америка' or c.Сontinent = 'Южная Америка'
--3.	Вывести среднее население стран. Результат округлить до одного знака.
Select Round(AVG(c.Population),1) from Countries c
--4.	Вывести количество стран, у которых название заканчивается на «ан», кроме стран, у которых название заканчивается на «стан».
Select Count(c.Name) from Countries c
WHERE LOWER(SUBSTRING(name, len(name) - 2, 2)) = 'ан' 
and LOWER(SUBSTRING(name, len(name) - 4, 4)) <> 'стан' 
--5.	Вывести количество континентов, где есть страны, название которых начинается с буквы «Р».
Select Count(c.Name) from Countries c
WHERE (LEFT(c.name , 1)) = 'Р'
--6.	Сколько раз страна с наибольшей площадью больше, чем страна с наименьшей площадью?
Select Max(c.Area)/Min(c.Area) from Countries c
--7.	Вывести количество стран с населением больше, чем 100 млн. чел. на каждом кон-тиненте. Результат отсортировать по количеству стран по возрастанию.
Select COunt(c.Name) as c_counter, c.Сontinent from Countries c
where c.Population > 100000000
Group by c.Сontinent
order by c_counter
--8.	Вывести количество стран по количеству букв в названии. Результат отсортировать по убыванию.
Select c.Name, len(c.Name) as c_len from Countries c
order by c_len
--9.	Ожидается, что через 20 лет население мира вырастет на 10%. Вывести список континентов с прогнозируемым населением:
Select c.Сontinent, sum(c.Population*1.1) as Прогноз from Countries c
group by c.Сontinent
--10.	Вывести список континентов, где разница по площади между наибольшими и наименьшими странами не более в 10000 раз:
Select c.Сontinent,(max(c.Area)/Min(c.Area)) as Разница from Countries c
Group by c.Сontinent
Having max(c.Area)/Min(c.Area) <10000
--11.	Вывести среднюю длину названий Африканских стран.
Select Avg(len(c.Name)) from Countries c
where c.Сontinent = 'Африка' 
--12.	Вывести список континентов, у которых средняя плотность среди стран с населе-нием более 1 млн. чел. больше, чем 30 чел. на кв. км.
Select c.Сontinent, avg(C.Population/c.Area) as плотность from Countries c
Where c.Population < 1000000
Group by c.Сontinent
having avg(C.Population/c.Area) > 30

-------------------------Лабораторная работа 6--------------------------------------
--1.Вывести из таблиц «Кафедра», «Специальность» и «Студент» данные о студентах, которые обучаются на данном факультете (например, «ит»).
SELECT stud.FIO, s.Name_spec, d.Dep_Name FROM dbo.Department d	
JOIN dbo.Speciality s ON d.ID = s.ID_dep
JOIN dbo.Student stud ON s.ID = stud.ID_spec
--2.Вывести из таблиц «Кафедра», «Специальность» и «Сотрудник» данные о выпускающих кафедрах (факультет, шифр, название, фамилию заведующего).
--Выпускающей счита-ется та кафедра, на которую есть ссылки в таблице «Специальность».
SELECT d.Dep_Name ,d.Abbreviation, s.Name_spec, e.FIO FROM dbo.Department d	
JOIN dbo.Speciality s ON d.ID = s.ID_dep
JOIN dbo.Employe e ON d.ID = e.ID_dep	
JOIN dbo.Deparment_alfa da ON e.ID = da.ID	
--3.Вывести в запросе для каждого сотрудника номер и фамилию его непосредственного руководителя.
--Для заведующих кафедрами поле руководителя оставить пустым.
SELECT e.ID,e.FIO, iif(e.ID = e1.Chef, NULL, e1.FIO) AS 'Руководитель'
FROM Employe AS e
LEFT JOIN Employe AS e1 ON e.Chef = e1.ID
--4.Вывести список студентов, сдавших минимум два экзамена.
SELECT s.FIO, count(e.ID_stud) Количество_экзаменов FROM dbo.Exasm e 
join dbo.Student s ON e.ID_stud = s.ID	
GROUP BY s.FIO
HAVING count(e.ID_stud) >= 2
--5.Вывести список инженеров с зарплатой, меньшей 20000 руб.
SELECT e.FIO,e.Job_title,e.Salary FROM dbo.Employe e
JOIN dbo.Engineer en ON e.ID = en.ID
WHERE e.Salary <20000
--6.Вывести список студентов, сдавших экзамены в заданной аудитории 'т505'.
SELECT DISTINCT s.FIO, e.Audit FROM dbo.Exasm e 
join dbo.Student s ON e.ID_stud = s.ID	
where e.Audit = 'т505'
--7.Вывести из таблиц «Студент» и «Экзамен» учетные номера и фамилии студентов,
--а также количество сданных экзаменов и средний балл для каждого студента только для тех студентов,
--у которых средний балл не меньше заданного (например, 4).
SELECT s.ID,s.FIO, count(e.ID_stud) Количество_сданных_экзаменов, avg(e.Mark) 'Средняя оценка' FROM dbo.Exasm e 
join dbo.Student s ON e.ID_stud = s.ID	
GROUP BY s.ID,s.FIO
HAVING avg(e.Mark) > 3
--8.Вывести список заведующих кафедрами и их зарплаты, и степень.
SELECT e.FIO, e.Salary, t.Rank FROM dbo.Employe e	
JOIN dbo.Deparment_alfa da ON e.ID = da.ID
JOIN dbo.Teacher t ON e.ID = t.ID
--9.Вывести список профессоров.
SELECT e.FIO, e.Salary, t.teach_Title FROM dbo.Employe e	
JOIN dbo.Teacher t ON e.ID = t.ID
WHERE t.teach_Title	= 'профессор'
--10.Вывести название дисциплины, фамилию, должность и степень преподавателя,
--дату и место проведения экзаменов в хронологическом порядке в заданном интервале даты.
SELECT dp.Dep_Name, e.FIO,e.Job_title, t.teach_Title, ex.Date_ex, ex.Audit FROM Employe e 
join dbo.Teacher t ON e.ID = t.ID
JOIN dbo.Exasm ex ON e.ID = ex.ID_empl
JOIN dbo.Discipline d ON ex.ID_dis = d.ID
JOIN dbo.Department dp ON d.ID_dep = dp.ID
JOIN dbo.Deparment_alfa da ON e.ID = da.ID
WHERE ex.Date_ex BETWEEN '2015-06-05' AND '2015-06-10'
ORDER BY ex.Date_ex
--11.Вывести фамилию преподавателей, принявших более трех экзаменов.
SELECT e2.FIO, count(e.ID_stud) Количество_принятых_экзаменов FROM dbo.Exasm e 
join dbo.Employe e2 ON e.ID_empl = e2.ID	
JOIN dbo.Teacher t ON e2.ID = t.ID	
GROUP BY e2.FIO
HAVING count(e2.FIO) >= 3
--12.Вывести список студентов, не сдавших ни одного экзамена в указанной дате.
SELECT  s.FIO, count(e.ID_stud) Количество_принятых_экзаменов FROM dbo.Student s
JOIN dbo.Exasm e ON s.ID = e.ID_stud
WHERE e.Date_ex	= '2015-06-05'
GROUP BY s.FIO
HAVING count(e.ID_stud) = 0
-------------------------Лабораторная работа 7--------------------------------------
--1.Вывести объединенный результат выполнения запросов, которые выбирают страны
--с площадью меньше 500 кв. км и с площадью больше 5 млн. кв. км:
SELECT c.Name, c.Area from Countries c
WHERE c.Area < 500
UNION ALL 
SELECT c.Name, c.Area FROM Countries c
WHERE c.Area > 5000000
--2.Вывести список стран с площадью больше 1 млн. кв. км, исключить страны с
--насе-лением меньше 100 млн. чел.
SELECT c.Name, c.Area, c.Population FROM Countries c
WHERE c.Area > 1000000
EXCEPT
SELECT c.Name, c.Area, Population FROM Countries c
WHERE Population < 100000000
--3.Вывести список стран с площадью меньше 500 кв. км и с населением меньше 100	тыс. чел.
SELECT c.Name, c.Area, c.Population FROM Countries c
WHERE c.Area < 500
INTERSECT
SELECT c.Name, c.Area, Population FROM Countries c
WHERE c.Population < 100000 

-------------------------Лабораторная работа 8--------------------------------------
--1.Вывести список стран и процентное соотношение площади каждой из них к общей площади всех стран мира.
SELECT c.Name ,
ROUND(CAST(c.Population AS FLOAT) * 100 /
(SELECT SUM(c.Population) FROM Countries c), 3) AS Процент
FROM Countries c
ORDER BY Процент DESC
--2.Вывести список стран мира, плотность населения которых больше, чем средняя плотность населения всех стран мира.
SELECT c.Name, c.Population  FROM Countries c
WHERE c.Population > (SELECT AVG(c.Population) FROM Countries c) 
--3.С помощью подзапроса вывести список европейских стран, население которых меньше 5 млн. чел.
SELECT * FROM (SELECT * FROM Countries c WHERE c.Сontinent = 'Европа') Country
WHERE Population < 5000000
--4.Вывести список стран и процентное соотношение их площади к суммарной пло-щади той части мира, где они находятся.
SELECT cc.Сontinent, cc.Сontinent,
ROUND(CAST(Population AS FLOAT) * 100 /
(SELECT SUM(Population) FROM Countries c WHERE c.Сontinent= cc.Сontinent), 3) AS Процент
FROM Countries cc
ORDER BY Процент DESC
--5.Вывести список стран мира, площадь которых больше, чем средняя площадь стран той части света, где они находятся.
SELECT * FROM Countries c
WHERE c.Сontinent > (SELECT AVG(cn.Сontinent) FROM Countries cn where cn.Сontinent = c.Сontinent)
--6.Вывести список стран мира, которые находятся в тех частях света, средняя плот-ность населения которых превышает общемировую.
SELECT * FROM Countries c
WHERE c.Population > (SELECT AVG(cc.Population) FROM Countries cc)
--7.Вывести список южноамериканских стран, в которых живет больше людей, чем в любой африканской стране.
SELECT * FROM Countries c
WHERE Population > (SELECT AVG(Population) FROM Countries cc WHERE cc.Name = 'Ангола')
AND c.Сontinent = 'Южная Америка'
--8.Вывести список африканских стран, в которых живет больше людей, чем хотя бы в	одной южноамериканской стране.
SELECT * FROM Countries c
WHERE c.Population > (SELECT AVG(cc.Population) FROM  Countries cc WHERE cc.Name = 'Боливия')
AND c.Сontinent = 'Африка'
--9.Если в Африке есть хотя бы одна страна, площадь которой больше 2 млн. кв. км, вывести список всех африканских стран.
SELECT * FROM Countries c 
WHERE c.Сontinent = 'Африка'
AND EXISTS (SELECT * FROM Countries cc WHERE cc.Сontinent = 'Африка' AND cc.Area > 2000000)
--10Вывести список стран той части света, где находится страна «Фиджи».
SELECT * FROM Countries c 
where c.Сontinent = (select cc.Сontinent from Countries cc where cc.Name = 'Фиджи')
--11Вывести список стран, население которых не превышает население страны «Фиджи».
SELECT * FROM Countries c
where c.Population <= (select cc.Population from Countries cc where cc.CName = 'Фиджи')
--12Вывести название страны с наибольшим населением среди стран с наименьшей площадью на каждом континенте.
SELECT * FROM Countries c
WHERE c.Population = 
(SELECT MAX(MinPopulation) FROM (SELECT MIN(Population) as MinPopulation
FROM Countries cc
GROUP BY cc.Сontinent) A)

-------------------------Лабораторная работа 9--------------------------------------
--1.Создать таблицу «Управление_ВашаФамилия». Определить основной ключ, иден-тификатор, значение по умолчанию
CREATE TABLE Management_Safarov (
ID_management int CONSTRAINT PK_Manage primary key identity(1,1),
Manage_Name nvarchar(50) CONSTRAINT D_Manage_Name DEFAULT ('Noname'))
--2.Создать таблицу «Страны_ВашаФамилия». Определить основной ключ, разреше-ние / запрет на NULL, условие на вводимое значение.
CREATE TABLE Country_Safarov (
ID_coun int CONSTRAINT PK_Con primary key identity(1,1),
Country_Name nvarchar(50) CONSTRAINT D_Country_Name DEFAULT ('Without name') NOT NULL)
--3.Создать таблицу «Цветы_ВашаФамилия». Определить основной ключ, значения столбца «ID» сделать уникальными, для столбца «Класс» установить значение по умолчанию «Двудольные».
CREATE TABLE Flower_Safarov (
ID_flower int CONSTRAINT UQ_ID_flower UNIQUE,
Flower_Classification nvarchar(50) CONSTRAINT D_Flower_Classification DEFAULT ('Without classification'))
--4.Создать таблицу «Животные_ВашаФамилия». Определить основной ключ, значе-ния столбца «ID» сделать уникальными, для столбца «Отряд» установить значение по умол-чанию «Хищные».
CREATE TABLE Animal_Safarov(
ID_animal int CONSTRAINT UQ_ID_animal UNIQUE,
Animal_Group nvarchar(50) CONSTRAINT D_Animal_Group DEFAULT ('Without group'))
-------------------------Лабораторная работа 10--------------------------------------
CREATE TABLE LR5_Students		
(					
	ID_student	INT PRIMARY KEY IDENTITY(1,1),	
	Second_name	VARCHAR(50) NOT NULL,		
	Stud_Subject	VARCHAR(50) NOT NULL,		
	School	VARCHAR(50) NOT NULL,		
	Mark FLOAT CHECK ((Mark >= 0) AND (Mark <= 100)) NULL
)
--1.В таблицу «Ученики» внести новую запись для ученика школы № 18 Трошкова, оценка которого по химии неизвестна.
INSERT INTO LR5_Students VALUES 
('Трошкова', 'Химия', 'Школа №18', NULL)
--2.В таблицу «Ученики» внести три строки.
INSERT INTO LR5_Students VALUES 
('Анонимусов', 'Химия', 'Школа №18', NULL),
('Симгмачев', 'Физ-ра', 'Школа №118', 40),
('Халоконтов', 'Математический анализ', 'Школа №181', 99)
--3.В таблице «Ученики» изменить данные Трошкова, школу исправить на № 21, пред-мет на математику, а оценку на 56.
UPDATE LR5_Students SET School = 'Школа №21',Stud_Subject = 'Математика', Mark = 56
Where Second_name = 'Трошкова'
SELECT * from LR5_Students
--4.В таблице «Ученики» изменить данные всех учеников по химии, оценку увеличить на 10%, если она ниже 60 баллов.
UPDATE LR5_Students SET Mark *= 1.1
Where Mark <= 60 and Stud_Subject = 'Химия'
SELECT * from LR5_Students
--5.В таблице «Ученики» удалить данные всех учеников из школы №21.
DELETE FROM LR5_Students
Where School = 'Школа №21'
select * from LR5_Students
--6.Создать таблицу «Гимназисты» и скопировать туда данные всех гимназистов, кроме тех, которые набрали меньше 60 баллов.
SELECT * INTO Gimnazist FROM LR5_Students
WHERE Mark > 60
SELECT * FROM Gimnazist
--7.Очистить таблицу «Гимназисты».
DROP TABLE Gimnazist
-------------------------Лабораторная работа 11--------------------------------------
--1.Даны числа A и B. Найти и вывести их произведение.
DECLARE	@A int, @B int
SET @A = 10
SET @B = 10
PRINT trim(str(@A*@B))	
--2.В таблице «Ученики» найти разницу между средними баллами лицеистов и гимназистов.
DECLARE @licey float, @gimn float, @diff float
SET @licey = (SELECT Avg(dbo.LR5_Students.Mark) FROM LR5_Students WHERE dbo.LR5_Students.School ='Школа №181')
SET @gimn = (SELECT Avg(dbo.LR5_Students.Mark) FROM LR5_Students WHERE dbo.LR5_Students.School ='Школа №21')
SET @diff =abs(@licey-@gimn)
PRINT @diff
--3.В таблице «Ученики» проверить на четность количество строк.
DECLARE @n int, @otvet nvarchar(50)
SELECT @n = COUNT(*) FROM LR5_Students
IF @n % 2 = 0
	SET @otvet = 'четно'
ELSE 
	SET @otvet = 'нечетно'
PRINT @otvet
--4.Дано четырехзначное число. Вывести сумму его цифр.
DECLARE @chislo nvarchar(4), @one_quarter nvarchar(1), @two_quarter nvarchar(1),@three_quarter nvarchar(1),@four_quarter nvarchar(1), @omlet int
set @chislo = '1050'
SET @one_quarter = SUBSTRING(@chislo,1,1)
SET @two_quarter = SUBSTRING(@chislo,2,1)
SET @three_quarter = SUBSTRING(@chislo,3,1)
SET @four_quarter = SUBSTRING(@chislo,4,1)
SET @omlet = cast(@one_quarter AS int) +cast(@two_quarter AS int) +cast(@three_quarter AS int) +cast(@four_quarter AS int) 
PRINT trim(str(@omlet))
--5.Даны случайные целые числа a, b и c. Найти наименьшее из них.
DECLARE @A int, @B int, @C int, @current int
SET @A = 4
SET @B = 6
SET @C = 2

IF @A < @B
	SET @current = @A
ELSE 
	SET @current = @B

IF @current < @c
	PRINT trim(str(@current))
ELSE 
	PRINT trim(str(@C))
--6.Дано случайное целое число a. Проверить, делится ли данное число на 11.
DECLARE @XXX int
SET @XXX = 10
IF @XXX %11=0
	PRINT 'Делится на 11'
ELSE 
	PRINT 'Не делится на 11'
--7.Дано случайное целое число N (N < 1000). Если оно является степенью числа 3, то вывести «Да», если не является – вывести «Нет».
DECLARE @N INT = 29

WITH PowerOfThree (PowerValue) AS
(
    SELECT 1
    UNION ALL
    SELECT PowerValue * 3
    FROM PowerOfThree
    WHERE PowerValue < 1000
)

SELECT 
    CASE 
        WHEN EXISTS (SELECT 1 FROM PowerOfThree WHERE PowerValue = @N) THEN 'Да'
        ELSE 'Нет'
    END AS Result;
--8.Даны случайные целые числа a и b. Найти наименьший общий кратный (НОК).
CREATE FUNCTION dbo.GCD (@a INT, @b INT)  
RETURNS INT  
AS  
BEGIN  
    RETURN  
        CASE   
            WHEN @b = 0 THEN @a  
            ELSE dbo.GCD(@b, @a % @b)  
        END  
END;  

DECLARE @a INT = 12;  
DECLARE @b INT = 15;  

DECLARE @gcd INT = dbo.GCD(@a, @b);

SELECT 
    CASE 
        WHEN @gcd = 0 THEN 0  
        ELSE ABS(@a * @b) / @gcd  
    END AS LCM;
--9.Даны два целых числа A и B (A<B). Найти сумму квадратов всех целых чисел от A до B включительно.
DECLARE @A INT = 3;
DECLARE @B INT = 7;
SELECT (POWER(@B, 3) + @B + POWER(@B, 2) - POWER(@A, 3) - @A - POWER(@A, 2)) / 6 AS SumOfSquares;
--10.Найти первое натуральное число, которое при делении на 2, 3, 4, 5, и 6 дает остаток 1, но делится на 7.
DECLARE @number INT
SET @number = 1

WHILE 1=1
BEGIN
    IF (@number % 2 = 1) AND (@number % 3 = 1) AND (@number % 4 = 1)
       AND (@number % 5 = 1) AND (@number % 6 = 1) AND (@number % 7 = 0)
    BEGIN
        PRINT 'Найдено число: ' + CONVERT(NVARCHAR, @number)
        BREAK
    END

    SET @number = @number + 1
END
--11.Вывести свою фамилию на экран столько раз, сколько в нем букв.
DECLARE @Second_name NVARCHAR(50)
SET @Second_name = 'Сафаров'

DECLARE @lettersCount INT
SET @lettersCount = LEN(@Second_name)

DECLARE @i INT
SET @i = 1

WHILE @i <= @lettersCount
begin
	print @Second_name
	set @i +=1
end
--12Напишите код для вывода на экран с помощью цикла:
DECLARE @Word NVARCHAR(50)
SET @Word = 'Нижний Вартовск'

DECLARE @Word_len INT
SET @Word_len = LEN(@Word)

DECLARE @j INT
SET @j= 1

WHILE @j <= @Word_len
BEGIN
    DECLARE @substr NVARCHAR(50)
    SET @substr = SUBSTRING(@Word, 1, @j)
    PRINT @substr
    SET @j += 1
END

-------------------------Лабораторная работа 12--------------------------------------
--1.Напишите функцию для вывода названия страны с заданной столицей, и вызовите ее.
DROP FUNCTION Hanuwu_MHe_CTpaHy

CREATE FUNCTION Hanuwu_MHe_CTpaHy (@capital nvarchar(max))
RETURNS nvarchar(max) AS
BEGIN
	DECLARE @name nvarchar(max)	
	SELECT @name = Name FROM Countries
	where Capital = @capital
	RETURN @name
END
SELECT dbo.Hanuwu_MHe_CTpaHy('Вена') Страна;

--2.Напишите функцию для перевода населения в млн. чел. и вызовите ее.
DROP FUNCTION nepeBedu_MHe_B_MuJlJluoHbl

CREATE FUNCTION nepeBedu_MHe_B_MuJlJluoHbl (@population float)
RETURNS nvarchar(max) AS
BEGIN
	DECLARE @p float	
	set @p = (@population / 1000000)
	RETURN CAST(@p as nvarchar(max)) + ' млн.'
END

SELECT dbo.nepeBedu_MHe_B_MuJlJluoHbl (1000000) чЕЛ

--3.Напишите функцию для вычисления плотности населения заданной части света и вызовите ее.
DROP FUNCTION Haudu_nJlOTHOCTb

CREATE FUNCTION Haudu_nJlOTHOCTb (@continent nvarchar(max))
RETURNS float AS
BEGIN
	DECLARE @Plotnost float

	SELECT @Plotnost = ROUND(cast(Population as float)/Area, 3) FROM Countries c
	where Сontinent = @continent
	
	RETURN @Plotnost
END
SELECT dbo.Haudu_nJlOTHOCTb ('Южная Америка') Плотность

--4.Напишите функцию для поиска страны, третьей по населению и вызовите ее.
DROP FUNCTION Haudu_CTPAHY

CREATE FUNCTION Haudu_CTPAHY ()
RETURNS nvarchar(max) AS
BEGIN
	DECLARE @p nvarchar(max), @m1 float, @m2 float, @m3 float

	SELECT @m1 = MAX(Population)  FROM Countries
	SELECT @m2 = MAX(Population)  FROM Countries
	where Population < @m1	
	SELECT @m3 = MAX(Population)  FROM Countries
	where Population < @m2

	SELECT @P = Name FROM Countries
	where Population = @m3

	RETURN @P
END

SELECT dbo.Haudu_CTPAHY () Страна

--5.Напишите функцию для поиска страны с максимальным населением в
-- заданной ча-сти света и вызовите ее. Если часть света не указана, выбрать Азию.
DROP FUNCTION Haudu_MAKC_HACEJlEHUE

CREATE FUNCTION Haudu_MAKC_HACEJlEHUE (@continent nvarchar(max))
RETURNS nvarchar(max) AS
BEGIN
	DECLARE @country nvarchar(max), @maxpop float

	IF @continent IS NULL
		SET @continent = 'Азия'

	SELECT @maxpop = MAX(Population) FROM Countries c
	WHERE  c.Сontinent = @continent 
	
	SELECT @country = Name FROM Countries
	WHERE Population = @maxpop

	RETURN @country
END

SELECT * FROM Countries c
ORDER BY c.[Сontinent]
SELECT dbo.Haudu_MAKC_HACEJlEHUE ('Южная Америка') Страна
SELECT dbo.Haudu_MAKC_HACEJlEHUE (NULL) Страна

--6.Напишите функцию для замены букв в заданном слове от третьей до предпослед-ней на “тест” и примените ее для столицы страны.
DROP FUNCTION Zamena

CREATE FUNCTION Zamena (@text nvarchar(max))
RETURNS nvarchar(max) AS
BEGIN
	RETURN LEFT(@text, 2) + REPLICATE('тест',LEN(@text) - 2) + RIGHT(@text, 1)
END

SELECT dbo.Zamena(Capital) FROM dbo.Countries c
--7.Напишите функцию, которая возвращает количество стран, не содержащих в назва-нии заданную букву.
DROP FUNCTION KOJlBO_CTPAH

CREATE FUNCTION KOJlBO_CTPAH (@t char(1))
RETURNS INT AS
BEGIN
	DECLARE @K int

	SELECT @K = COUNT(*) FROM Countries
	WHERE CHARINDEX(@t, Name) = 0

	RETURN @K
END

SELECT dbo.KOJlBO_CTPAH('В') 
--8.Напишите функцию для возврата списка стран с площадью меньше заданного числа и вызовите ее.
DROP FUNCTION nJlowadb_CTRAHbl

CREATE FUNCTION nJlowadb_CTRAHbl (@n int)
RETURNS TABLE AS RETURN
(
	SELECT * FROM dbo.Countries c
	WHERE Area < @n
)

SELECT * FROM nJlowadb_CTRAHbl(1000000)
--9.Напишите функцию для возврата списка стран с населением в интервале заданных значений и вызовите ее.
DROP FUNCTION HACJleHUE_CTRAHbl

CREATE FUNCTION HACJleHUE_CTRAHbl (@n1 int, @n2 int)
RETURNS TABLE AS RETURN
(
	SELECT * FROM dbo.Countries c
	WHERE Population >= @n1 and Population <= @n2
)

SELECT * FROM HACJleHUE_CTRAHbl(500000, 5000000)
--10.Напишите функцию для возврата таблицы с названием континента и суммарным населением и вызовите ее.
DROP FUNCTION HACJleHUE_KOHTUHEHTA

CREATE FUNCTION HACJleHUE_KOHTUHEHTA ()
RETURNS TABLE AS RETURN
(
	SELECT c.Сontinent, SUM(Population) Pop FROM dbo.Countries c
	GROUP BY c.Сontinent
)

SELECT * FROM HACJleHUE_KOHTUHEHTA()
--11.Напишите функцию IsPalindrom(P) целого типа, возвращающую 1, если целый па-раметр P (P > 0) является палиндромом, и 0 в противном случае.
DROP FUNCTION IsPalindrom

CREATE FUNCTION IsPalindrom(@FIRST INT)
RETURNS INT AS
BEGIN
    DECLARE @ReversedStr NVARCHAR(100) , @SECOND int, @otvet int
	SET @SECOND = @FIRST
    SET @ReversedStr = REVERSE(CAST(@FIRST AS NVARCHAR(100)));

    IF @ReversedStr = CAST(@SECOND AS NVARCHAR(100))
        SET @otvet = 1
    ELSE
        SET @otvet = 0
	RETURN @otvet
END

SELECT dbo.IsPalindrom(1221) 
--12.Напишите функцию Quarter(x, y) целого типа, определяющую номер координатной четверти, содержащей точку с ненулевыми вещественными координатами (x, y).
CREATE FUNCTION MyQuarter(@x FLOAT, @y FLOAT)
RETURNS INT AS
BEGIN
    DECLARE @Quadrant INT;

    IF @x > 0 AND @y > 0
        SET @Quadrant = 1
    ELSE IF @x < 0 AND @y > 0
        SET @Quadrant = 2
    ELSE IF @x < 0 AND @y < 0
        SET @Quadrant = 3
    ELSE IF @x > 0 AND @y < 0
        SET @Quadrant = 4
    ELSE
        SET @Quadrant = 0

    RETURN @Quadrant
END

SELECT DBO.MyQuarter(10, -6) Корды_дома_в_майне
--13.Напишите функцию IsPrime(N) целого типа, возвращающую 1, если целый пара-метр N (N > 1) является простым числом, и 0 в противном случае.
CREATE FUNCTION IsPrime(@N INT)
RETURNS INT AS
BEGIN
    DECLARE @IsPrime INT = 1

    IF @N <= 1
        SET @IsPrime = 0
    ELSE
		BEGIN
		    DECLARE @i INT = 2;

		    WHILE @i * @i <= @N
				BEGIN
				    IF @N % @i = 0
						BEGIN
						    SET @IsPrime = 0
						    BREAK;
						END
				    SET @i = @i + 1
				END
		END

    RETURN @IsPrime
END

SELECT DBO.isPrime(-5)
--14.Напишите код для удаления созданных вами функций 
Drop FUNCTION IsPrime
-------------------------Лабораторная работа 13-1-------------------------------------
--1.Создайте представление, содержащее список африканских стран, население которых больше 10 млн. чел., а площадь больше 500 тыс. кв. км, и используйте его.
Create view Number1_12
as
select * from Countries c
where c.Сontinent = 'Африка' and c.Population > 10000000 and c.Area > 500000
select * from Number1_12
--2.Создайте представление, содержащее список континентов, среднюю площадь стран, которые находятся на нем, среднюю плотность населения, и используйте его.
Create view Number2_12
as
select c.Сontinent, avg(c.Area) 'Средняя площадь стран', avg(c.Population/c.Area) 'Средная плотность населения' from Countries c
group by c.Сontinent
Select * from Number2_12
--3.Создайте представление, содержащее фамилии преподавателей, их должность, зва-ние, степень, место работы, количество их экзаменов, и используйте его.
create view Number3_12
as 
select e.FIO, e.Job_title, t.Rank, t.teach_Title, count(ex.ID_empl) 'Кол-во эказменов' from Employe e
join Teacher t on t.ID = e.ID
join Exasm ex on ex.ID_empl = t.ID
group by e.FIO, e.Job_title, t.Rank, t.teach_Title
select * from Number3_12
--4.Создайте табличную переменную, содержащую три столбца («Номер месяца», «Название месяца», «Количество дней»), заполните ее для текущего года, и используйте ее.
DECLARE @Number4_12 TABLE (
    Num_Month INT,
    Month_name NVARCHAR(50),
    Month_days_count INT
)

DECLARE @YearNow INT
SET @YearNow = YEAR(GETDATE())

DECLARE @Num_Month INT
SET @Num_Month = 1

WHILE @Num_Month <= 12
BEGIN
    DECLARE @Date4 DATE
    SET @Date4 = DATEFROMPARTS(@YearNow, @Num_Month, 1)

    DECLARE @Month_name NVARCHAR(50)
    SET @Month_name = DATENAME(MONTH, @Date4)

    DECLARE @Month_days_count INT
    SET @Month_days_count = DAY(EOMONTH(@Date4))

    INSERT INTO @Number4_12 (Num_Month, Month_name, Month_days_count)
    VALUES (@Num_Month, @Month_name, @Month_days_count)

    SET @Num_Month += 1
END

SELECT *
FROM @Number4_12
--5.Создайте табличную переменную, содержащую список стран, площадь которых в 100	раз меньше, чем средняя площадь стран на континенте, где они находятся, и используйте ее.
Declare @NameConti nvarchar(50) = 'Азия'
Declare @Number5_12 TABLE
(
	CountyName nvarchar(50),
	CapitalName nvarchar(50),
	Area int,
	Continent nvarchar(50)
)
insert into @Number5_12
select c.Name,c.Capital,c.Area,c.Сontinent from Countries c
where c.Area *100 < (Select avg(c.Area) from Countries c) and c.Сontinent = @NameConti

Select * from @Number5_12
--6.Создайте локальную временную таблицу, имеющую три столбца («Номер недели», «Количество экзаменов», «Количество студентов»), заполните и используйте ее.
Select 
	DATENAME(WEEK,e.Date_ex) as Number_week,
	COUNT(DISTINCT e.ID) AS [Count_exasm],
	COUNT(DISTINCT e.ID_stud) AS [Count_studs]
	into #Number6_12
	from Exasm e
	Group by DATENAME(WEEK,e.Date_ex)
	Select * from #Number6_12
	Drop table #Number6_12
--7.Создайте глобальную временную таблицу, содержащую название континентов, наибольшую и наименьшую площадь стран на них, заполните и используйте ее.
Create table #Number7_12
(
	NameConti nvarchar(50),
	MaxArea int,
	MinArea int
)
insert into #Number7_12
(NameConti,MaxArea,MinArea)
Select c.Сontinent, Max(c.Area), min(c.Area)  from Countries c
group by c.Сontinent
Select * from #Number7_12
Drop table #Number7_12
--8.С помощью обобщенных табличных выражений напишите запрос для вывода списка сотрудников, чьи зарплаты меньше, чем средняя зарплата по факультету, их зарплаты и название факультета.
With Number8_12 as
(
Select e.ID,e.FIO, e.Salary, fac.Name_Fac from Employe e
join Teacher t on t.ID = e.ID 
join Department d on d.ID = e.ID_dep
join Faculty fac on fac.ID = d.ID_fac
)
SELECT  em.ID,em.FIO, em.Salary, fac.Name_Fac FROM Employe em
JOIN Number8_12 nnn on nnn.ID = em.ID 
join Teacher t on t.ID = em.ID 
join Department d on d.ID = em.ID_dep
join Faculty fac on fac.ID = d.ID_fac
where em.Salary <= (SELECT avg(Salary) FROM Employe )

--9.Напишите команды для удаления всех созданных вами представлений.
--Drop view  

-------------------------Лабораторная работа 14--------------------------------------
--1.Создайте курсор, содержащий отсортированные по баллам фамилии и баллы учеников, откройте его, выведите первую строку, закройте и освободите курсор.
Declare Number1_14 Cursor 
for
	Select ls.Second_name, ls.Mark from LR5_Students ls
	order by ls.Mark
Open Number1_14
FETCH Number1_14
Close Number1_14
DEALLOCATE Number1_14
--2.Создайте курсор с прокруткой, содержащий список учеников, откройте его, выведите пятую, предыдущую, с конца четвертую, следующую, первую строку, закройте и освобо-дите курсор.
Declare Number2_14 Cursor Scroll
for
	Select * from LR5_Students ls
Open Number2_14
FETCH ABSOLUTE 5 FROM Number2_14
FETCH PRIOR FROM Number2_14
FETCH ABSOLUTE -4 FROM Number2_14
FETCH NEXT FROM Number2_14
Fetch Number2_14
Close Number2_14
DEALLOCATE Number2_14
--3.Создайте курсор с прокруткой, содержащий список учеников, откройте его, выведите последнюю, шесть позиций назад находящуюся, четыре позиций вперед находящуюся строку, закройте и освободите курсор.
Declare Number3_14 Cursor Scroll
for 
	Select * from LR5_Students ls
Open Number3_14
FETCH LAST FROM Number3_14
FETCH RELATIVE -6 FROM Number3_14
FETCH RELATIVE 4 FROM Number3_14
Close Number3_14
DEALLOCATE Number3_14
--4.С помощью курсора, вычислите сумму баллов у учеников с наибольшим и наименьшим баллом.
Declare Number4_14 Cursor Scroll
for 
	Select ls.Mark from LR5_Students ls
	order by ls.Mark
Declare @A1 float =0, @B1 float
Open Number4_14
	fetch first from Number4_14 into @A1
	print @a1
	fetch last from Number4_14 into @B1
	print @B1 

	set @A1 +=@B1
	print @A1
Close Number4_14
DEALLOCATE Number4_14

--5.С помощью курсора, сгенерируйте строку вида «Ученики <список фамилий и названий предметов, разделенных запятыми> участвовали в олимпиаде».
Declare Number5_14 Cursor scroll
for
	select ls.Second_name, ls.Stud_Subject from LR5_Students ls
Declare @output_14 nvarchar(300), @FIO_with_SS nvarchar(100), @SS nvarchar(100)
Open Number5_14 
Fetch next from Number5_14 into @FIO_with_SS, @SS
while @@FETCH_STATUS =0
begin
	SET @FIO_with_SS='Ученик,'+ @FIO_with_SS + ' по предмету '+@SS+','
	FETCH NEXT FROM Number5_14 INTO @FIO_with_SS, @SS
end
Set @FIO_with_SS = @FIO_with_SS + ' участвовали на олимпиаде.'
PRINT @FIO_with_SS

CLOSE Number5_14
DEALLOCATE Number5_14
--6.Создайте курсор, содержащий список учеников, с его помощью выведите учеников с нечетной позицией.
Declare Number6_14 Cursor Scroll
for 
	Select * from LR5_Students
Open Number6_14
FETCH ABSOLUTE 1 FROM Number6_14
WHILE @@FETCH_STATUS = 0
	FETCH RELATIVE 2 FROM Number6_14
CLOSE Number6_14
DEALLOCATE Number6_14
--7.Создайте курсор, содержащий отсортированный по убыванию баллов список учеников, откройте его, для каждого ученика выведите фамилию, предмет, школу, баллы и процентное соотношение баллов с предыдущим учеником.
DECLARE @FIO NVARCHAR(50)
DECLARE @S_Subject NVARCHAR(50)
DECLARE @School NVARCHAR(50)
DECLARE @Mark INT
DECLARE @PercentRelation FLOAT
DECLARE @BeforeMark INT

DECLARE Number7_14 CURSOR FOR
SELECT Second_name, Stud_Subject, School, Mark
FROM LR5_Students
ORDER BY Mark DESC

OPEN Number7_14

FETCH NEXT FROM Number7_14 INTO @FIO, @S_Subject, @School, @Mark

SET @PercentRelation = 100
SET @BeforeMark = @Mark

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT CONCAT('Фамилия: ', @FIO)
    PRINT CONCAT('Предмет: ', @S_Subject)
    PRINT CONCAT('Школа: ', @School)
    PRINT CONCAT('Баллы: ', @Mark)
    PRINT CONCAT('Процентное соотношение баллов с предыдущим учеником: ', @PercentRelation, '%')

    SET @PercentRelation = (@Mark / @BeforeMark) * 100
    SET @BeforeMark = @Mark

    FETCH NEXT FROM Number7_14 INTO @FIO, @S_Subject, @School, @Mark
END

CLOSE Number7_14
DEALLOCATE Number7_14
-------------------------Лабораторная работа 15--------------------------------------
--1.Вывести список учеников и разницу между баллами ученика и максимальным баллом в каждой строке.
Select *,ls.Mark - MAX(ls.Mark) OVER() AS Макс_Балл from LR5_Students ls
--2.Вывести список учеников и процентное соотношение к среднему баллу в каждой строке.
Select *,ls.Mark*100 / avg(ls.Mark) OVER() AS  Процент from LR5_Students ls
--3.Вывести список учеников и минимальный балл в школе в каждой строке.
Select *,min(ls.Mark) OVER(PARTITION by ls.School) AS  Мин from LR5_Students ls
--4.Вывести список учеников и суммарный балл в школе в каждой строке, отсортиро-вать по школам в оконной функции.
Select *,Sum(ls.Mark) OVER(PARTITION by ls.School order by ls.School) AS  Мин from LR5_Students ls
--5.Вывести список учеников и номер строки при сортировке по фамилиям в обратном алфавитном порядке.
Select *,ROW_NUMBER() OVER(order by ls.School DESC) AS  номер_строки from LR5_Students ls
--6.Вывести список учеников, номер строки внутри школы и количество учеников в школе при сортировке по баллам по убыванию.
Select *,ROW_NUMBER() OVER(PARTITION by ls.School order by ls.School DESC) AS  номер_строки from LR5_Students ls
--7.Вывести список учеников и ранг по баллам.
Select *,RANK() OVER(PARTITION by ls.School order by ls.School DESC) AS  ранг from LR5_Students ls
--8.Вывести список учеников и сжатый ранг по баллам. Результат отсортировать по фамилии в алфавитном порядке.
Select *,DENSE_RANK() OVER(PARTITION by ls.School order by ls.School DESC) AS  ранг from LR5_Students ls
--9.Вывести список учеников, распределенных по пяти группам по фамилии.
Select *,NTILE(5) OVER(ORDER BY ls.Second_name) AS Гр_Фам from LR5_Students ls
--10.Вывести список учеников, распределенных по трем группам по баллам внутри школы.
Select *,NTILE(3) OVER(PARTITION BY ls.School ORDER BY ls.Mark DESC) As гр_оценка from LR5_Students ls
--11.Вывести список учеников и разницу с баллами ученика, находящегося выше на три позиции при сортировке по возрастанию баллов.
Select *,ls.Mark - LAG(ls.Mark) OVER(ORDER BY ls.Mark) AS Разница from LR5_Students ls
--12.Вывести список учеников и разницу с баллами следующего ученика при сортировке по убыванию баллов, значение по умолчанию использовать 0.
Select *,ls.Mark - LEAD(ls.Mark,1,0) OVER(ORDER BY ls.Mark) AS Разница from LR5_Students ls
-------------------------Лабораторная работа 16--------------------------------------
--1.Напишите запрос, который выводит максимальный балл учеников по школам, по каждому предмету по каждой школе и промежуточные итоги.
SELECT ISNULL(Surname, 'Все'), ISNULL(Subject, 'Пром итог'), MAX(Pts) FROM Pupils
GROUP BY Surname, Subject
WITH ROLLUP	

--2.Напишите запрос, который выводит минимальный балл учеников по школам и по предметам, и промежуточные итоги.
SELECT ISNULL(Surname, 'Все'), ISNULL(Subject, 'Пром итог'), MIN(Pts) FROM Pupils
GROUP BY Surname, Subject
WITH ROLLUP	

--3.Напишите запрос, который выводит средний балл учеников по школам и по предметам.
SELECT ISNULL(Surname, 'Все'), ISNULL(Subject, 'Пром итог'), AVG(Pts) FROM Pupils
GROUP BY Surname, Subject
WITH ROLLUP	

--4.Напишите запрос, который выводит количество учеников по каждой школе по пред-метам и промежуточные итоги. 
--NULL значения заменить на соответствующий текст.
SELECT ISNULL(Surname, 'Все'), ISNULL(Subject, 'Пром итог'), COUNT(Pts) FROM Pupils
GROUP BY Surname, Subject
WITH ROLLUP	

--5.Напишите запрос, который выводит суммарный балл учеников по школам и по пред-метам, и промежуточные итоги.
--В итоговых строках NULL значения заменить на соответству-ющий текст в зависимости от группировки.
SELECT ISNULL(Surname, 'Все'), ISNULL(Subject, 'Пром итог'), SUM(Pts) FROM Pupils
GROUP BY Surname, Subject
WITH ROLLUP	

--6.Напишите запрос, который выводит максимальный балл учеников по школам и по предметам.
--В итоговых строках NULL значения заменить на соответствующий текст в зави-симости от уровней группировки.
SELECT ISNULL(Surname, 'Все'), ISNULL(Subject, 'Пром итог'), MAX(Pts) FROM Pupils
GROUP BY Surname, Subject
WITH ROLLUP	

--7.Напишите запрос, который выводит средний балл учеников по школам в столбцы.
SELECT * FROM (SELECT Education, Pts FROM Pupils)
AS SourceTable PIVOT
(AVG(Pts) FOR Education IN ([Лицей], [Гимназия])) 
AS PivotTable;
--8.Напишите запрос, который выводит средний балл учеников по школам в столбцы и по предметам в строки.
SELECT Subject,
       [Лицей] AS Лицей,
       [Гимназия] AS Гимназия
FROM
(
    SELECT Subject, Education, AVG(Pts) AS AvgPts
    FROM Pupils
    GROUP BY Subject, Education
) AS SourceTable
PIVOT
(
    AVG(AvgPts)
    FOR Education IN ([Лицей], [Гимназия])
) AS PivotTable;
--9.Напишите запрос, который выводит названия предметов, фамилии учеников и школы в один столбец.
SELECT CONCAT(Subject, ', ', Surname, ', ', Education) AS CombinedColumn
FROM Pupils;
-------------------------Лабораторная работа 17--------------------------------------
--1.Создайте и запустите динамический SQL-запрос, выбирающий первые N строк из заданной таблицы.
DECLARE @tableName NVARCHAR(128) = 'Countries'
DECLARE @rowsnumber INT = 4

DECLARE @sql NVARCHAR(MAX);
SET @sql = 'SELECT TOP ' + CAST(@rowsnumber AS NVARCHAR) + ' * FROM ' + @tableName;

EXEC(@sql);

--2.Создайте и запустите динамический SQL-запрос, выбирающий все страны из таб-лицы «Страны»,
--последняя буква названия которых расположена в заданном диапазоне.
DECLARE @startLetter CHAR(1) = 'м';
DECLARE @endLetter CHAR(1) = 'н';

DECLARE @sql2 NVARCHAR(MAX);
SET @sql2 = 'SELECT * FROM Countries
WHERE SUBSTRING(Name, LEN(Name), 1) >= ''' + @startLetter + '''
AND SUBSTRING(Name, LEN(Name), 1) <= ''' + @endLetter + '''';

EXEC(@sql2);

SELECT * FROM Countries
--3.Создайте временную таблицу #Temp и добавьте к ней название столбцов таблицы «Страны». Создайте курсор, который, построчно читая таблицу #Temp, выбирает каждый раз соответствующий столбец из таблицы «Страны».
SELECT c.Name
    INTO #temp 
    FROM Countries c
	DECLARE TC CURSOR
FOR SELECT * FROM #temp

OPEN TC
DECLARE @TN VARCHAR(50)

FETCH FROM TC INTO @TN
WHILE @@FETCH_STATUS = 0
BEGIN
EXECUTE ('SELECT * FROM ' + @TN)
FETCH FROM TC INTO @TN
END

CLOSE TC
DEALLOCATE TC

DROP TABLE #temp

--4.Создайте процедуру, которая принимает как параметр список столбцов, название таблицы и выбирает заданные столбцы из заданной таблицы.
CREATE PROCEDURE SelectColumnsFromTable
    @columnList NVARCHAR(MAX),
    @tableName NVARCHAR(MAX)    
AS
BEGIN
    DECLARE @sql3 NVARCHAR(MAX);
    SET @sql3 = 'SELECT ' + @columnList + ' FROM ' + @tableName;

    EXEC(@sql3);
END

EXEC dbo.SelectColumnsFromTable 'Id, Name, Capital', 'Countries';
--5.Создайте процедуру, принимающую как параметр список столбцов, название таб-лицы,
--название проверяемого столбца, знак сравнения, значение проверки и выбирающую за-данные столбцы из заданной таблицы в заданных условиях.
CREATE PROCEDURE SelectColumnsFromTable2
    @columnList NVARCHAR(MAX),
    @tableName NVARCHAR(MAX),
	@columnName NVARCHAR(MAX),
	@compareSign NVARCHAR(2),
	@compareValue NVARCHAR(MAX)
AS
BEGIN
    DECLARE @sql4 NVARCHAR(MAX);
    SET @sql4 = 'SELECT ' + @columnList + ' FROM ' + @tableName +
                    ' WHERE ' + @columnName + ' ' + @compareSign + ' ''' + @compareValue + '''';


    EXEC(@sql4);
END

EXEC dbo.SelectColumnsFromTable2 'Id, Name, Capital', 'Countries', 'Capital', '=', 'Вена';
--6.Выберите список европейских стран из таблицы «Страны» и экспортируйте в RAW формате XML.
SELECT * FROM dbo.Countries c FOR XML RAW
--7.Выберите список стран с населением больше 100 млн. чел. из таблицы «Страны» и экспортируйте в PATH формате XML.
SELECT * FROM dbo.Countries c FOR XML PATH
--8.Выберите список учеников из школы «Лицей» из таблицы «Ученики» и экспорти-руйте в PATH формате JSON
SELECT * FROM dbo.Countries c FOR JSON PATH
 
