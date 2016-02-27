уроки http://www.site-do.ru/db/sql1.php
инструкции по созданию http://msdn.microsoft.com/ru-ru/library/bb522469.aspx
книга http://books.google.com.ua/books?id=7DiOrj68d3cC&pg=PA137&lpg=PA137&dq=%D0%B4%D0%BE%D0%B1%D0%B0%D0%B2%D0%B8%D1%82%D1%8C+%D1%84%D0%B0%D0%B9%D0%BB%D0%BE%D0%B2%D1%83%D1%8E+%D0%B3%D1%80%D1%83%D0%BF%D0%BF%D1%83+%D0%B2+%D0%B1%D0%B0%D0%B7%D1%83+%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85&source=bl&ots=rvKLoRLTCA&sig=VmUbWQd41fkDNaj0k3OTZCFaViE&hl=ru&sa=X&ei=1hZGVLOGHIO4OLr6gOgJ&ved=0CDUQ6AEwBQ#v=onepage&q=%D0%B4%D0%BE%D0%B1%D0%B0%D0%B2%D0%B8%D1%82%D1%8C%20%D1%84%D0%B0%D0%B9%D0%BB%D0%BE%D0%B2%D1%83%D1%8E%20%D0%B3%D1%80%D1%83%D0%BF%D0%BF%D1%83%20%D0%B2%20%D0%B1%D0%B0%D0%B7%D1%83%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85&f=false

при связи многие ко многим (имеется два поля и они оба foreign key (...) references post(...) ) то чтобы нельзя было создать 
еще такого студента на том же курсе 
primary key (имя первого поля,имя второго поля)

-- переключиться между базами данными 
use mydb1

-- хранимые процедуры 
exec sp_helpdb 
-- и конкретно для базы данных €Ќ”ЋђЊЂ–€џ
exec sp_helpdb mydb1

-- изменение опций базы данных 
exec sp_dboption mydb1, 'ANSI NULL Default', 'True'

-- посмотреть значение опции 
exec sp_dboption mydb1, 'ANSI NULL Default'

-- удаление базы данных 
DROP DATABASE MyDB1

-- отключить автоинкремент
set IDENTITY_INSERT T2 off 

-- сколько не совпадающих строк
count(distinct(любое_поле))

-- задать авто инкремент
alter table T2 add f5 int identity

-- создать базу данных
CREATE DATABASE DB
ON PRIMARY 
(name='DB',size=5mb,
maxsize=100mb,FILEGROWTH=5mb,filename='c:\1\DB.mdf'),
FILEGROUP DBG1
(name='DBG1',size=5mb,
maxsize=100mb,FILEGROWTH=5mb,filename='c:\1\DBG1.ndf')
LOG ON 
(name='DB_LOG',size=5mb,
maxsize=100mb,FILEGROWTH=5mb,filename='c:\1\DB_LOG.LOG')

-- добавить файл в указанную файловую группу
ALTER DATABASE DB ADD FILE
(name = 'dbf4',size=10mb,filename='c:\1\DBF4.ndf')
to filegroup DBG1

-- добавить файловую группу 
alter database DB
ADD FILEGROUP DBG2 

-- добавить файл в файловую группу
ALTER DATABASE DB ADD FILE 
(name='n2file',size=10mb,FILEGROWTH=50%,filename='c:\1\n2file.ndf') 
TO FILEGROUP DBG1

-- установить файловую группу по умолчанию, чтобы все файлы (без указания ф.г) создавались в ней
ALTER DATABASE DB 
modify filegroup DBG1 default

-- создать таблицу
use DB
create table T1
(f1 float,f2 real) 

-- удалить таблицу
drop table T1

-- переименование столбца в таблице
exec sp_rename 'T1.f1', 'f12', 'COLUMN'

-- добавление столбца в таблицу
alter table T1 add f3 int not null 

-- добавить запись
use DB
INSERT INTO dbo.T2 (f1,f2,f4)
VALUES (102,455.55,GETDATE());
-- или
use DB
INSERT INTO dbo.T2 (f1,f2,f4)
VALUES (102,455.55,'2014-10-21 12:08:43');

insert into employee (lastname,firstname,name,_login,_password,d_birth,sex,teleph,city_id)
values ('Потребенко','Виктор','Владимирович','vitek','799','1990.01.01',0,'vite@mail.ru',
0950567499,61) 
--------------------------
real до (7)
float до (18)
{numeric (18)
decimal (18)}
bit (лог знач)
tinyint -127 128
smallint 2b
int 4 b
bigint 8b

для char(мах) максимальное значение подставляет автоматически
char (сколько нужно от 0-...)
varchar (занимает символов необходимых для слова)
nchar (2-х байтовые ячейки для хранения UNICOD)
nvarchar (сколько угодно)
money (тоже что и float)
smallmoney (тоже что и float)

image (как гиперссылка в аксессе)
text (как гиперссылка в аксессе)

datetime  хранит мил-ы
smalldatetime не хранит мил-ы(меньший диапазон даты)

select e.LASTNAME
from ProductsDone pd,Employee e
where e.ID=pd.idEmp and pd.idProd = all (select id
from Products) 


______________________________
если поле nchar то преобразовать - N"любое слово"

размер колонки мв
select COL_LENGTH('city','city_id')

-- получить свойства столбца по таблице 
select @@SERVERNAME
select COLUMNPROPERTY(
object_id ('city'),'city_id',
'precision') или 'isnullallowed' разрешены ли нули для столбца

-- имя сервера
select @@SERVERNAME

-- по номеру базы 
select DB_NAME(10)

-- добавить таблицу
create table t1 (slr real,
p1 int, fio char(20),id int)

-- изменить значение столбца
alter table t1 alter column p1 float

-- удалить столбец в таблице
alter table t1 drop column p1 

-- добавить столбец
alter table t1 add p11 int  

-- вставить несколько записей
insert into t1
values (100.,'Sakharov',3,17),
(200.,'Taylor',4,123)

-- не хранимый
create table t2 (slr real,
p1 int, N# int,fio char(20),id int,total as slr+p1 persisted)

-- хранимый
create table t2 (slr real,
p1 int, N# int,fio char(20),id int,total as slr+p1 stored)
_________________________________________________
-- отсоеденить базу данных
sp_detach_db imp

-- присоеденить базу
sp_attach_db 'imp', 'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\imp_Data.mdf'

для переноса базы на другой сервер, на базе правой кнопкой, task->generate scripts->advance выбрать schema and data

-- равно ли поле null
select columnproperty(object_id('t1'),'slr','allows null')
select columnproperty(object_id('t1'),'slr','IsIdentity')

-- возвращает имя столбца 
select col_name(object_id('t1'),1)

-- все файлы 
select file_name(1),file_name(2),file_name(3333)

-- возвращает название группы
select filegroup_name(1)

-- поумолчанию ли назначена файловая группа
select filegroupproperty('primary','IsDefault')
-- или
select filegroupproperty(filegroup_name(1),'IsDefault')

-- возвращает в страницах памти
select FILEPROPERTY('dbf4','SpaceUsed') IsLogFile,IsPrimaryFile,IsReadOnly
select filegroupproperty('primary','IsDefault')

-- применяет ко всем столбцам таблице то что NULL по умолчанию включен
set ansi_null_deft_on on

-- устанавливает как реагировать на null, если встречается null то ничего не выведет
use imp
drop table tt
create table tt(num int,dt smalldatetime default GetDate())
set ansi_nulls on -------------------------------------------------
insert tt (num) values (1)
insert tt (dt) values (GetDate())
select *
from tt 
where num<>null
-- а здесь выведет одну строку
select *
from tt 
where num is not null с нулем сравниваем так!
drop table tt


-- задает как выводить название столбца если присутствует null
use imp
create table tt(num int,dt smalldatetime default GetDate())
set ansi_nulls off 
insert tt (num) values (1)
insert tt (dt) values (GetDate())
set concat_null_yields_null on ------------------------------------
select null+'b'
from tt 
where num<>null 

select *
from tt 
where num is not null
drop table tt

if else логическое ветвление, т.е. 0 при сравнении с NULL возврает NULL, а когда off
возвращает true, т.к. действительно не равен 
use imp
create table tt(num int,dt smalldatetime default GetDate())
set ansi_nulls on 
insert tt (num) values (1)
insert tt (dt) values (GetDate())
if 0<>null begin select '!='end--------------------
else begin select '=' select 'null' end
____________________________________________________________________________________________________
---------------------------------------------
http://www.sql-tutorial.ru/ru/book_string_functions_in_sql_server.html - все фнкции-------------------------------------------------------------!!!!!!!!!!!!!
-------------------------------------------------------------------
exp(x)
sin(pi)
cos(radians(90))
degrees (1)
power (1.2,2)
rand()
rand(95)
select GETDATE() - получить дату
select datepart(m,GETDATE()) - получаем месяц из дату
select datediff(YYYY,'10-31-2010',GETDATE()) - разница между датами
select dateadd(YYYY,1,GETDATE()) - возвращат новую дату
select isdate('1.13.2001') - вернет 1 если дата
set datefirst 2 - установить дату по умолчанию


update sp
set qty=convert(int,'333') - конвертирует 
set qty=cast('555' as int) - 2 вариант преобразования
where pid='p3'
_____________________________________________________________
строковые функции
http://www.sql-tutorial.ru/ru/book_string_functions_in_sql_server.html
select STR(12.345)+'2' - число в сроку 
select SUBSTRING('sdfsdf',2,3) - новую строку образует dfs
select reverse('12345') - в обратном порядке
select replicate('12345',2) -добавляет такую же строку
select ascii('a') - возвращает код символа
select char(98) - в символ
select right('12.3',2) - урезает справа .3
select left('12.3',2) - урезает слева 
select ltrim(rtrim('   12.3 ')) - убирает пробелы слева и права
select stuff('12345',4,0,'RT') - вставляет, 123RT45
select stuff('12345',4,1,'RT') - вставляет заменяет 123RT5

-- создать вьювер
create view w as
select sp.pid,p.pname,sp.qty
from p left outer join sp on p.pid=sp.pid

к вьюверу нельзя применить - Order by

-- добавить в вьювер
insert into w1 
values ('p14',1222)

drop view w - удалить вьювер
-- обращаться к вьюверам как и к любой таблице в frow указывать_имя_вьювера
__________________________________________
-- уникальность полей, включение и добавление уникальности при добавлении нового элемента
--create table T2(f1 int identity,f2 varchar(10))
set identity_insert dbo.T2  on
insert dbo.T2 (f1,f2) values (10,'lll')
---------------------------------------------------------------
-- уникальность полей, включение и добавление уникальности при добавлении нового элемента (увеличение ключа на 10)
select @@IDENTITY 
set identity_insert T2 on
insert T2(f1,f2) values
((10+ident_current('T2')),'Zw')
set identity_insert T2 off
insert T2 values ('Tx');

CREATE PROCEDURE ProcedureAddUser
AS
begin
declare @i int = 0;
while @i<1000000
begin
	set identity_insert Users off;
	insert into Users  
	values (concat('logi',@i),222,GETDATE(),5,'ivan','petrenko',GETDATE());
	set @i=@i+1;
	end;	
end;
--------------------------------------------------------------
use mydb1
select sp.pid,p.pname,sp.qty
from p left join sp on p.pid=sp.pid

left outer join - выведет все строки, те которые не подпадают заполняются NULL

http://potapov.com.ua/library/21/
Table A       Table B
id name       id  name
-- ----       --  ----
1  Pirate     1   Rutabaga
2  Monkey     2   Pirate
3  Ninja      3   Darth Vader
4  Spaghetti  4   Ninja

http://dimonchik.com/sql-inner-join.html статья по join ___________________________!!!!!!!!!!!!

пример left outer join
SELECT  p.Pid, p.PNAME, sp.QTY
FROM  dbo.P p,dbo.SP sp
where p.Pid=sp.Pid 
union all
SELECT p.Pid,p.PNAME, null 
FROM  dbo.P p
where p.Pid <> all (select SP.Pid 
from dbo.SP
where SP.Pid is not null)  
или-------------
where p.Pid not in (select SP.Pid
from dbo.SP) 
-------------------------------------------------------------------------------------

-- Constraint - ограничения на столбец (имя ограничения pk)
create table mt (f1 char(20),
constraint pk primary key (f2),
f2 int identity)

-- удалить назначеное ограничение по имени ограничения
alter table mt 
drop constraint pk

-- добавить ограничения к таблице
alter table mt 
add constraint pk primary key(f1)

-- создается таблица с первичным ключом двух столбцов, автоматически добавляет null в поля 
create table mt (f1 char(20),
constraint pk primary key (f1,f2),
f2 int)

alter table mt 
drop constraint pk

alter table mt 
add constraint pk primary key(f2,f1)

-- создает таблицу в файловой группе, с доболнительными характеристиками ОГРАНИЧЕНИЯ
-- ignore_dup_key- позволяет добавлять в индексный файл не уникальные индексы
create table mt (f1 char(20),f2 int,
constraint pk primary key (f2)
with (fillfactor=90,ignore_dup_key=on) on [primary])
on DBG1

-- добавить кластаризованный индексный
create index mti 
on mt (f1)

drop_existing=on - если существует индекс то он перезаписывается
----------------------------------------------------------------------------------
-- кого перекрывает Kokonin (по деталям), кого он может заменить (т.е. делает все то что и другой сотрудник)
select LASTNAME
from Employee e,
(select e.ID
from ProductsDone as pd
inner join Employee as e on e.ID = pd.idEmp
where e.LASTNAME not like 'kokonin' 
group by e.ID) as t1

where t1.ID=e.ID and not exists (select *
				from 
				(select distinct pd.idProd 
				from ProductsDone as pd
				where t1.ID=pd.idEmp) as tmp
				where tmp.idProd not in (select pd.idProd
										from ProductsDone as pd
										inner join Employee as e on e.ID = pd.idEmp
										where e.LASTNAME like 'kokonin'
										group by pd.idProd))
--------------------------------
select distinct LASTNAME
from Employee e
inner join ProductsDone as pd on e.ID=pd.idEmp 
where LASTNAME not like 'kokonin'
except
select distinct LASTNAME
from Employee e
inner join ProductsDone as pd on e.ID=pd.idEmp 
where LASTNAME not like 'kokonin' and pd.idProd not in (select distinct pd.idProd
										from ProductsDone as pd
										inner join Employee as e on e.ID = pd.idEmp
										where e.LASTNAME like 'kokonin')
---------------------------------------------------------------
-- кластраризованная, т.е. упорядоченная , если б не применили constraint mtpk primary key (f2), то таблица была б простая
create table mt1 (f1 int not null,
constraint mtpk primary key (f2),
f2 int, f3 int, f4 int)		
-- удалили уникальность поля
alter table mt1
drop constraint mtpk
-- главное чтобы комбинация полей была уникальна (f1 asc,f2), создать более двух кластаризованных индексов создать не получитьс
create unique clustered index 
mtpk on mt1(f1 asc,f2)	
include (BIRTHDAY)			
-- ключ интелектуальный 
alter table mt1 add
constraint mtpk primary key (f2,f1)	
------------------------------------------
-- скобки фигурные дают иной результат
select LASTNAME
from Employee
except
(select LASTNAME
from Employee
except 
select LASTNAME
from Employee
where LASTNAME like 'riehl' or LASTNAME like 'kokonin')
_------------------------------------------------------------
-- при создании таблицы можно указать о допустимости ограничения
alter table mt1 with nocheck 
add constraint c1c2 check
(f1>0 and f2<1000)
-- ограничения на поля, СОЗДАЛ
alter table mt
add constraint c1c2 check
(f1>0 and f2<1000)
-- УДАЛИЛ
alter table mt
drop constraint c1c2
--------------------------------------------------------------------
declare @i int,@c char(5),@r real
set @i=2 
set @c='xhr1'
set @r=12.3
if @i=0
set @r=3
else
begin 
select @r=-3
print @r
end

select @i=7,@r=1.7
select @i,@c
----------------------------------------------------
-- подсчитывает сумму чисел от 1 до 5
declare @i int,@sum int
select @sum=0,@i=0
lb:
set @i=@i+1 set @sum=@sum+@i
if(@i=5)
goto lk
goto lb
lk:
print @sum
-----------------------------
-- с while
declare @i int,@sum int
select @sum=0,@i=0
while @i<5
begin
set @i=@i+1 
set @sum=@sum+@i
end
print @sum
-------------------------------------
declare @i int,@sum int
select @sum=0,@i=0
while @i<10
begin
set @i+=1 
set @sum=@sum+@i

end
print @sum
select @i,'summa ' = 'summa = ' + Convert(char,@sum) 
---------------------------------
-- создать процедуры (with encryption - не отоброжать для всех,recompile - пересмотреть план запроса, перекомпановка (если изменились столбцы))
alter proc pr with encryption,recompile
as 
select e.LASTNAME,p.NAME_
from Products p,ProductsDone pd,Employee e
where p.ID=pd.idProd and pd.idEmp=e.ID

exec pr with recompile (with recompile - или сюда можно вставлять)
---------------------------------
функция которая принимает значения и изменяет их возвращая новые (ref C#)
alter proc #prp (@p1 real = 2,@p2 real output, @p3 real=1 output)
as begin 
set @p3=@p1*@p2/@p3
set @p2=@p2/@p1
end

declare @x real
select @x=7
exec #prp 5,@x output
print @x
___________________________
-- именнованные переменные (имена должны совпадать с именами параметров функции)
declare @x real
select @x=7
exec #prp @p2=@x output, @p1=5
print @x
-------------------------------------------------------
alter proc #prp (@fac int=0 output)
as begin
declare @i int=0,@tmp int=@fac
set @fac=1
while(@i<@tmp)
begin 
set @i=@i+1
set @fac=@i*@fac
end
end

declare @x real = 4
exec #prp @x output
print @x	
------------------------------------
-- процедура КТО ПЕРЕКРЫВАЕТ КОКОНИНА-----------------------------------!!!!!!!!!ПРОЦЕДУРЫ!!!!!!!!!!!!!!!!11
create proc tmp (@name varchar(10)) 
as begin
select LASTNAME
from Employee e,
(select e.ID
from ProductsDone as pd
inner join Employee as e on e.ID = pd.idEmp
where e.LASTNAME not like @name 
group by e.ID) as t1
where t1.ID=e.ID and not exists (select *
				from (select distinct pd.idProd
				from ProductsDone as pd
				inner join Employee as e on e.ID = pd.idEmp
				where e.LASTNAME like @name) as tmp
				where tmp.idProd not in (select distinct pd.idProd 
										from ProductsDone as pd
										where t1.ID=pd.idEmp))
end

exec tmp 'kokonin'
-------------------------------------------------------------	
-- процедура КОГО ПЕРЕКРЫВАЕТ КОКОНИН
create proc WhoKokonin (@name varchar(10))
as begin
select distinct LASTNAME
from Employee e
inner join ProductsDone as pd on e.ID=pd.idEmp 
where LASTNAME not like @name
except
select distinct LASTNAME
from Employee e
inner join ProductsDone as pd on e.ID=pd.idEmp 
where LASTNAME not like @name and pd.idProd not in (select distinct pd.idProd
										from ProductsDone as pd
										inner join Employee as e on e.ID = pd.idEmp
										where e.LASTNAME like @name)
end

exec WhoKokonin 'kokonin'
----------------------------------------------------------
-- процедура КТО ДЕЛАЕТ ТАКИЕ ЖЕ ТОЧНО ДЕТАЛИ КАК КОКОНИН
alter proc WhoIsHasKokonin (@name varchar(10)) 
as begin
(select distinct LASTNAME
from Employee e
inner join ProductsDone as pd on e.ID=pd.idEmp 
where LASTNAME not like @name 
except
select distinct LASTNAME
from Employee e
inner join ProductsDone as pd on e.ID=pd.idEmp 
where LASTNAME not like @name  and pd.idProd not in (select distinct pd.idProd
										from ProductsDone as pd
										inner join Employee as e on e.ID = pd.idEmp
										where e.LASTNAME like @name ))
intersect
select LASTNAME
from Employee e,
(select e.ID
from ProductsDone as pd
inner join Employee as e on e.ID = pd.idEmp
where e.LASTNAME not like @name 
group by e.ID) as t1
where t1.ID=e.ID and not exists (select *
				from (select distinct pd.idProd
				from ProductsDone as pd
				inner join Employee as e on e.ID = pd.idEmp
				where e.LASTNAME like @name) as tmp
				where tmp.idProd not in (select distinct pd.idProd 
										from ProductsDone as pd
										where t1.ID=pd.idEmp))
end

exec WhoIsHasKokonin 'kokonin'
-----------------------------------------------
-- процедура КТО ДЕЛАЕТ ВСЕ ДЕТАЛИ НА ЗАВОДЕ
create proc #tn as
begin
select LASTNAME
from Employee e,
(select distinct e.ID
from ProductsDone as pd
inner join Employee as e on e.ID = pd.idEmp) as t1
where t1.ID=e.ID and not exists (select *
				from (select distinct pd.idProd
				from ProductsDone pd) as tmp
				where tmp.idProd not in (select distinct pd.idProd 
										from ProductsDone as pd
										where t1.ID=pd.idEmp))
end

exec #tn
----------------------------------------------------------------
----------------------------------------------------------------
-- функции
create function MyFunction(@par int,@par2 real)
Returns int
begin
Return @par/@par2
end

declare @e int
set @e=dbo.MyFunction(5,5)
print @e
-- или
declare @e int
select @e=dbo.MyFunction(5,5)
print @e 
-----------------------------------
-- функци возвращающая подсчет (возвращает таблицу)
alter function MyFunction(@par int)
Returns int
begin
Return 
(select SUM(p.COST*pd.QUANT)
from ProductsDone pd,Products p
where pd.idProd=p.ID and pd.ID=@par)
end

declare @e int=1
select lastname,dbo.MyFunction(@e) 
from Employee 
where id=@e
print @e 
--------------------------------------
-- функция возвращающая таблицу
alter function Fil()
Returns table as
Return 
(select pd.idProd,SUM(p.COST*pd.QUANT) as summa
from ProductsDone pd,Products p
group by pd.idProd)


select *
from Fil() as t
---------------------------------------
-- функция возвращающая кто сделал деталей больше лимита (инлайн функция begin as - не нужно)
alter function Fil(@lim int)
Returns table as
Return 
(select e.LASTNAME,p.COST*SUM(pd.QUANT) as summa
from ProductsDone pd,Products p,Employee e
where pd.idProd=p.ID and e.ID=pd.idEmp
group by e.LASTNAME
having SUM(p.COST*pd.QUANT)>@lim)

select *
from Fil(1000) as t
-- или
alter function Fil(@lim int)
Returns table as
Return 
(select e.LASTNAME,SUM(t1.summa) as summa
from Employee e,
(select e.ID,p.COST*SUM(pd.QUANT) as summa
from ProductsDone pd,Products p,Employee e
where pd.idProd=p.ID and e.ID=pd.idEmp
group by e.ID,p.COST) as t1
where e.ID=t1.ID 
group by e.LASTNAME,e.ID
having SUM(t1.summa)>@lim)

select *
from Fil(1000) as t
--------------------------------------------------------
-- мультифункция
alter function Multistate(@del char(100))
Returns @tx table (item char (25))
as begin
insert @tx values ('xx'),('zz');
update @tx set item = 'bb' where item = 'zz'
delete from @tx where item = @del  
return 
end 

select *
from Multistate('bb') as t
--------------------------------------------
-- мультифункция (вставить таблицу во временную таблицу)
alter function FnSln(@cust_id int) 
Returns @tx table (item char(25))
as begin
insert @tx 
select Item 
from Sales
where @cust_id=Cust_id
return 
end

select *
from FnSln(3)
---------------------------------------------
-- добавяет в таблицу временную таблицу (cross apply - обеспечивает передачу значений из одной таблице в другую)
alter function FnSln(@cust_id int) 
Returns @tx table (item char(25))
as begin
insert @tx 
select Item 
from Sales
where @cust_id=Cust_id
return 
end

select *
from Customers cross apply FnSln(Cust_id)
-- или
select *
from Customers outer apply FnSln(Cust_id)
--------------------------------------------------
set nocount on 
select Cust_name,t.*
from Customers cross apply (select * from Sales where Sales.Cust_id=Cust_id) as t
where Customers.Cust_id = t.Cust_id
select @@ROWCOUNT
--------------------------------------------------------
-- type
-- создание
create type ii from int
create type vc from varchar(22)
-- запуск
declare @ii ii,@vc vc
select @ii=7,@vc='7'
print @ii
print @vc
drop type ii
-------------------
-- case
declare @i int =5,@w int =17
select case
when @w>10 and @i between 0 and 10
then 'is'
else 'natin'
end
as 'is in'
-------------------------------
-- case из таблицы Products
select NAME_, case cost/10
when 30 then '3hundred'
when 20 then '20hundred'
when 10 then '10hundred'
else 'less'
end
as balance 
from Products
where YEARLOG>0 
-----------------------------------
select NAME_, case 
when YEARLOG<100 then 'no nameter'
when cost/10=30 then '20hundred'
else 'another'
end
as balance 
from Products
where YEARLOG>0 
-------------------------------------ДОМАШНЕЕ
select NAME_
from (select p.NAME_, t =
(case 
when p.YEARLOG<100 then 'no nameter'
when p.cost/10=30 then '20hundred'
else 'another'
end)
from Products p) as balance 
where balance.t not like 'no nameter'
-------------------------------------------
create table X1(f1 int ,f2 int)
create table X2(g1 int ,g2 int)

declare @xx table (p1 int,p2 int,p3 int)
insert @xx select g1,f1,g2
from X1,X2
where f1=g1
select *
from @xx
------------------------------------
alter proc #tmp (@x1 real,@x2 real output)
as begin
if @x2=0 
return 100
select @x2=@x1/@x2
return 0
end 

declare @z1 real = 2,@z2 real = 0,@rc int
exec @rc=#tmp @z1,@z2 output
select 'частное'=@z2, 'код'=@rc
--------------------------------------------ТРАНЗАКЦИЯ_________________________!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
-- завершить транзакцию commit transaction с подтверждением
-- select @@TRANCOUNT глубина транзакции
set implicit_transactions on
insert Sales values (17,'xxx')
select @@TRANCOUNT
commit transaction
-------------------------------------------
set implicit_transactions on
begin transaction 
insert Sales values (33,'yy333')
save transaction lb1
insert Sales values (44,'yy444')
if 1=1
begin 
select 'err',@@trancount
rollback transaction lb1
select @@TRANCOUNT
commit transaction
select @@TRANCOUNT
end
else
commit transaction

select @@TRANCOUNT

while @@TRANCOUNT>0
commit transaction
------------------------------------------------
alter table dbo.Customers add
constraint uc unique clustered (Cust_id asc,Cust_name desc)

alter table dbo.Customers 
drop constraint uc
----------------------------------------------------TRIGGER______________!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
-- создание тригера, после удаления выведет на экран удаленные поля
create trigger trt
on Customers
after 
delete
--insert,update
as begin
select * 
from deleted
end
-- после удаляем поле
delete from Customers
where Cust_id=1
------------------------------------------------------------------
-- добавление удаленной записи во временную таблицу
alter trigger trt
on Customers
after 
delete
--insert,update
as begin
insert TMP 
select *
from deleted
end

--create table TMP(id int,name varchar(20))
delete from Customers
where Cust_id=1
------------------------------------------------
alter trigger trt
on Customers
after 
update,insert
as 
if @@ROWCOUNT>0 ----------------------------------------если хоть что то добавится
begin
--insert TMP 
select *
from inserted,deleted
end


--create table TMP(id int,name varchar(20))
insert Customers
values (122,'error')
update Customers
set Cust_id=757
where Cust_id=657
-----------------------------------------------
-- срабатывает только при ЗАМЕНЕ ОПРЕДЕЛЕННОГО ПОЛЯ
alter trigger trt
on Customers
after update

as 
if @@ROWCOUNT>0  and update (Cust_id)
begin
--insert TMP 
select *
from inserted,deleted
end


--create table TMP(id int,name varchar(20))
insert Customers
values (122,'error')
update Customers
set Cust_name='Sanek'
where Cust_name='error'
-----------------------------------------------------
-- ОТКЛЮЧИТЬ тригера
alter table Customers
disable trigger trt
-- или все тригеры
alter table Customers
disable trigger ALL
-----------------------------------------------------
-- делает вместо (instead of update)!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
create trigger tri
on Customers
instead of update
as 
begin
select *
from inserted
end

set Cust_name='Vetal'
where Cust_id=12
-----------------------------------------------------------------
-- включить коссвенную рекурсию 1, выключить косвенную рекурсию 2-х
exec sp_configure 'nested triggers',1 
--включить отключить прямую рекурсию
EXEC sp_dboption 'bd3_2SQL', 'recursive triggers', 'FALSE'
--------------------------------------------------------------------
-- ДОМАШНЕЕ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
select distinct e.ID,e.LASTNAME,p.NAME_
from Employee e left join ProductsDone pd on e.ID=pd.idEmp
left join Products p on pd.idProd = p.ID
where pd.idEmp is null or not exists 
							(select *
							from ProductsDone pd1,Employee e1
							where e.ID=e1.ID and e1.ID=pd1.idEmp and pd.idProd>pd1.idProd)
-- или еще проще
select LASTNAME,(select top(1)p.nm
from p,pd
where p.id=pd.idProd and pd.idEmp=e.ID)
from Employee e
--------------
alter trigger addTable
on Employee
after insert
as 
begin 
declare @id int
select dbo.AddItemToTable(inserted.id)
from inserted 

if(dbo.AddItemToTable(@id)=0) 
update Employee
set id_prod=(select top(1)id from product)
where id_prod=@id
end



--------------------------------------------
create function AddItemToTable(@id int)
returns int
as begin
return
(select count(*)
from Customers c
where @id=c.Cust_id) 
end

alter trigger addTable
on Customers
for insert
as 
begin
select *
from inserted
end

--create table TMP(id int,name varchar(20))
insert Customers
values (122,'error')
update Customers
set Cust_name='Vetal'
where Cust_id=12

EXEC sp_dboption 'bd3_2SQL', 'recursive triggers', 'FALSE'

exec sp_configure 'nested triggers',1 
alter table Customers
disable trigger ALL
------------------------------------------ДОМАШКА------------------------------
alter function AddItemToTable(@id int)
returns int
as begin
if((select count(*)
from product p
where @id=p.id)=0)
begin
execute dbo.pr @id
return 1
end
return 0
end

alter proc pr(@id int)
as begin 
if((select count(*)
from product p
where @id=p.id)=0)
	update Employee
	set id_prod=(select top(1)id from product)
	where id_prod=@id
end

alter trigger addTable
on Employee
after insert
as begin 
declare @i int = 0
select @i=inserted.id_prod
from inserted
execute dbo.pr @i
end

--create table TMP(id int,name varchar(20))
insert Employee
values ('Vetal',5)

insert Employee
select *
from tmp

update Employee
set name='Vetal'
where id_prod=12
-------------------------------------------------заменяет все несуществующие id 
 
-----------------------СОЗДАЕМ ТАБЛИЦУ В txt и записываем ее в таблицуcreate table TMP (f1 int,f2 varchar(20))

bulk insert TMP
from 'c:\1\1.txt'
with (firstrow=1,--начальная строка в файле
lastrow=3,--конечная строка в файле
datafiletype='char',--
rowterminator='\n',--каким символом разделяются строки в файле
fieldterminator=' ',--каким символом разделяются столбцы в файле
keepnulls, --как реагировать на ноль
keepidentity, --чтобы идентификация была
fire_triggers --чтобы тригеры срабатывали
)

select sum(Salary)
from Employee
group by case
when Salary<500 then 11
when Salary>=500 then 3
else Salary
end
------------------------------ИНФОРМАЦИЯ О ТАБЛИЦЕ
exec sp_helpconstraint 'persone'
exec sp_helpindex 'persone'-- - узнать свойства полей (уникальность, кластеризованность ...)
--но лучше
exec sp_help'Employee'
 
-- ограничения таблицы
select OBJECT_NAME(OBJECT_ID)as castName,OBJECT_NAME(parent_object_id)
as tableName, type_desc
from sys.objects
where type_desc like '%AULT_CON%'
and OBJECT_NAME(OBJECT_ID)='DF__persone__bdate__07020F21'
---------------------------------------------------------------------------------------------------------------ПОРЯДОК ВЫПОЛНЕНИЯ
identity (insert)
NULL
типы данных 
insted off (trigger)
Primary key
check
foreign key
ВЫПОЛНЯТСЯ DML ОПЕРАТОР
ОБНОВЛЕНИЕ LOG 
after trigger
commit или rollback запись LOG
запись в файл
--------------------------------------------------------------
select OBJECT_NAME(parent_object_id) as 'table',OBJECT_NAME(object_id) as 'trigger'
from sys.objects
where type like 'TR'
или-------------
select zz.name, ss.name from
(select name,parent_object_id  from sys.objects where type = 'tr') zz,
( select name,object_id  from sys.objects where type = 'u')ss where parent_object_id = object_id

select *
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME='Employee'
------------------------------------ПРОЦЕДУРА ВЫВОДЯЩАЯ ПОЛЯ КОТОРЫЕ ДОПУСКАЮТ ЗНАЧЕНИЕ NULL
alter proc ShowFieldNull(@tb varchar(150))
as begin
declare @item varchar(500) = '';
select @item=@item+','+name
from sys.columns
where object_id=object_id(@tb) and is_nullable = 1;
set @item=substring(@item,2,LEN(@item));
exec ('select '+@item+' from '+@tb);
end

exec ShowFieldNull 'Employee'
_______________________________________________КУРСОР
declare cur cursor scroll
declare @nm char(20),@cst int,@qvnt real,@sum int = 0
for select NAME_,COST,quant
from Products p,ProductsDone pd
where p.ID=pd.idProd

fetch 
open cur
select @@CURSOR_ROWS
close cur
DEALLOCATE cur 

--подсчитывает сумму и обновляем данные на поле где нахоится курсор
declare cur cursor scroll 
for select NAME_,COST,quant
from Products p,ProductsDone pd 
where p.ID=pd.idProd
for update of quant --для обновления открыли и указали поле

declare @nm char(20),@cst int,@qvnt real,@sum int = 0
open cur
print(@@CURSOR_ROWS)
fetch last from cur into @nm,@cst,@qvnt

while @@FETCH_STATUS=0
begin
set @sum=@sum+@cst*@qvnt
print(@sum)

update ProductsDone
set QUANT=@qvnt+1
where CURRENT of cur
fetch prior from cur into @nm,@cst,@qvnt

end
close cur
DEALLOCATE cur  
-------------------------------------------------ПРОВЕРКА БД
DBCC checkdb 
-----------------------------------ПРОВЕРКА ТАБЛИЦЫ
DBCC checktable ('Products')
________________________________ОТАТАЧИЛ БАЗУ ПРИОТАЧИЛ
sp_detach_db 'DB'
sp_attach_db @dbname='DB',@filename1='c:\1\DB.mdf'
------------------------------------------backup БАЗЫ ДАННЫХ
backup database DB
to disk='c:\backup\DB.BAK'

exec sp_addumpdevice 'DISK','DB2','c:\backup\db2.bak'
exec sp_addumpdevice  'DISK','D2L','c:\backup\D2L.bak'

alter database DB
set recovery full--для того чтобы списать лог

backup LOG [DB]
to D2L
-------------------------------------------restore БАЗЫ ДАННЫХ
restore database DBR
from DISK='c:\backup\DB.bak' 
---________________________________УДАЛЕНИЕ ВСЕ ОГРАНИЧЕНИЙ  CONSTRAINT
alter function GetIdTable(@name varchar(100))
returns int
as begin 
return
(select object_id
from sys.objects
where type like 'U' and name like @name)
end

alter proc RemoveConstraint(@name varchar(20))
as begin
declare @str varchar(500)='' 
select  @str+='alter table '+@name+' drop constraint '+object_name(convert(char,object_id))+'; '
from sys.objects
where type_desc like '%CONSTRAINT' and parent_object_id=dbo.GetIdTable(@name)
exec(@str)
end

exec RemoveConstraint 'pd'
--или КОРОТКО
alter proc RemoveConstraint(@name varchar(20))
as begin
declare @str varchar(500)='' 
select  @str+='alter table '+@name+' drop constraint '+object_name(convert(char,object_id))+'; '
from sys.objects
where type_desc like '%CONSTRAINT' and object_name(parent_object_id) like (@name)
exec(@str)
end

 

