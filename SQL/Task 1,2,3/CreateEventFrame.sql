USE EVENTFRAME; 
GO

DECLARE @batchId NVARCHAR(50);
DECLARE @blockId NVARCHAR(50);

EXECUTE AddEventFrame 'Партия 54321-10', 'Партия', @batchId OUTPUT;
EXECUTE AddEventFrame 'Блок ТЕСТ 99-98', 'Блок', @blockId OUTPUT;

DECLARE @BatchParams AS ParametersTableType;
DECLARE @BlockParams AS ParametersTableType;

INSERT INTO @BatchParams (Name, Value) VALUES
	('Номер партии', '54321-10'),
	('Смена (послед. изм.)', 'В'),
	('ФИО раздельщика', 'Курочкин А. П.'),
	('Контракт', 'А708-256'),
	('ФИО (поток мелкий)', 'Арызбаев У.Я.'),
	('Тип контрагента', 'Внутренний'),
	('Тип тары', 'Бочка'),
	('Дата изготовления', '02.03.2023'),
	('ФИО раздельщика', 'Исаев А. Н.'),
	('ФИО УУ', 'Смехов А.П.'),
	('Согласование', 'Подтвержден'),
	('ФИО раздельщика 1', 'Сидоренко С. Я.'),
	('Масса (брутто), кг', '4513.2'),
	('Состояние', 'Готов к перетариванию'),
	('Кол-во тарных мест, шт', '1'),
	('Место хранения', 'Уч. взвешивания'),
	('Дата (послед. изм.)', '04.03.2023'),
	('Состав', 'Да'),
	('ФИО контр. ОТК, принявшего партию', 'Кречун Е. М.'),
	('Пользователь (послед. изм.)', 'Регидронов Р. А.'),
	('Масса (нетто), кг', '4120.0'),
	('Спецификация', 'ГОСТ 519'),
	('Тип ТГ', 'Сортная'),
	('Контрагент', 'ATI'),
	('Марка', 'ТГ-90'),
	('Срок годности', '02.09.2023'),
	('ФИО сортировщика, сдавшего партию', 'Али А. Ю. Ж.'),
	('ФИО раздельщика 2', 'Кучеров Е. М.'),
	('Фракция (LIMS)', '12+80')

INSERT INTO @BlockParams (Name, Value) VALUES
	('Номер блока', '99-98'),
	('Мастер (послед. изм.)', 'Лизунов А. А.'),
	('Номер реторты', '1110'),
	('Состояние', 'Переработан'),
	('Смена (послед. изм.)', '2'),
	('Выбивщик', 'Уколов А. С.'),
	('Категория', 'B'),
	('Дата (послед. изм.)', '01.03.2023'),
	('Масса, кг', '4700')

EXEC AddEventFrameValues @batchId, @BatchParams;
EXEC AddEventFrameValues @blockId, @BlockParams;

--EXEC SelectEventsWithValuesById @batchId;
--EXEC SelectEventsWithValuesById @blockId;

DECLARE @batchNumber NVARCHAR(50) = '54321-10';
DECLARE @parameterName NVARCHAR(50) = 'Фракция (LIMS)';
DECLARE @newParameterValue NVARCHAR(100) = 'Работает!'

EXEC UpdateBatchParameter @batchNumber, @parameterName, @newParameterValue
EXEC SelectEventsWithValuesById @batchId



--DECLARE @linkId NVARCHAR(50);

--EXEC AddLink @batchId, @blockId, @linkId OUTPUT;

--SELECT * FROM Links WHERE ParentEventFrameId=@batchId

--DELETE FROM Links WHERE Id=@linkId;
DELETE FROM dbo.EventFrames WHERE Id=@batchId OR Id=@blockId;
