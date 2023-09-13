USE EVENTFRAME; 
GO

DECLARE @batchId NVARCHAR(50);
DECLARE @blockId NVARCHAR(50);

EXECUTE AddEventFrame '������ 54321-10', '������', @batchId OUTPUT;
EXECUTE AddEventFrame '���� ���� 99-98', '����', @blockId OUTPUT;

DECLARE @BatchParams AS ParametersTableType;
DECLARE @BlockParams AS ParametersTableType;

INSERT INTO @BatchParams (Name, Value) VALUES
	('����� ������', '54321-10'),
	('����� (������. ���.)', '�'),
	('��� �����������', '�������� �. �.'),
	('��������', '�708-256'),
	('��� (����� ������)', '�������� �.�.'),
	('��� �����������', '����������'),
	('��� ����', '�����'),
	('���� ������������', '02.03.2023'),
	('��� �����������', '����� �. �.'),
	('��� ��', '������ �.�.'),
	('������������', '�����������'),
	('��� ����������� 1', '��������� �. �.'),
	('����� (������), ��', '4513.2'),
	('���������', '����� � �������������'),
	('���-�� ������ ����, ��', '1'),
	('����� ��������', '��. �����������'),
	('���� (������. ���.)', '04.03.2023'),
	('������', '��'),
	('��� �����. ���, ���������� ������', '������ �. �.'),
	('������������ (������. ���.)', '���������� �. �.'),
	('����� (�����), ��', '4120.0'),
	('������������', '���� 519'),
	('��� ��', '�������'),
	('����������', 'ATI'),
	('�����', '��-90'),
	('���� ��������', '02.09.2023'),
	('��� ������������, �������� ������', '��� �. �. �.'),
	('��� ����������� 2', '������� �. �.'),
	('������� (LIMS)', '12+80')

INSERT INTO @BlockParams (Name, Value) VALUES
	('����� �����', '99-98'),
	('������ (������. ���.)', '������� �. �.'),
	('����� �������', '1110'),
	('���������', '�����������'),
	('����� (������. ���.)', '2'),
	('��������', '������ �. �.'),
	('���������', 'B'),
	('���� (������. ���.)', '01.03.2023'),
	('�����, ��', '4700')

EXEC AddEventFrameValues @batchId, @BatchParams;
EXEC AddEventFrameValues @blockId, @BlockParams;

--EXEC SelectEventsWithValuesById @batchId;
--EXEC SelectEventsWithValuesById @blockId;

DECLARE @batchNumber NVARCHAR(50) = '54321-10';
DECLARE @parameterName NVARCHAR(50) = '������� (LIMS)';
DECLARE @newParameterValue NVARCHAR(100) = '��������!'

EXEC UpdateBatchParameter @batchNumber, @parameterName, @newParameterValue
EXEC SelectEventsWithValuesById @batchId



--DECLARE @linkId NVARCHAR(50);

--EXEC AddLink @batchId, @blockId, @linkId OUTPUT;

--SELECT * FROM Links WHERE ParentEventFrameId=@batchId

--DELETE FROM Links WHERE Id=@linkId;
DELETE FROM dbo.EventFrames WHERE Id=@batchId OR Id=@blockId;
