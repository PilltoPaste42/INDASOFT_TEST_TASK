USE EVENTFRAME;
GO

SELECT  
	MONTH([���� ������������]) AS [�����],
	COUNT([BatchName]) AS [���-�� ������],
	SUM([����� (�����), ��]) AS [����� ���� (�����), ��], 
	SUM([����� (������), ��]) AS [����� ���� (������), ��]
FROM
(
	SELECT
		F.Name AS BatchName,
		FloatParams.Name AS FloatParams,
		DateParams.Name AS DateParams,
		FloaTVUpdateals.ValueFloat AS FloaTVUpdateals,
		DateVals.ValueDatetime AS DateVals
	FROM EventFrameTypes AS T
	JOIN EventFrames AS F
		ON F.EventFrameTypeId = T.Id
	JOIN EventFrameTypeValues AS FloatParams
		ON FloatParams.EventFrameTypeId = T.Id
		AND FloatParams.Type = '������������ �����'
	JOIN EventFrameTypeValues AS DateParams
		ON DateParams.EventFrameTypeId = T.Id
		AND DateParams.Type = '����'
	JOIN EventFrameValues AS FloaTVUpdateals
		ON FloaTVUpdateals.EventFrameId = F.Id
		AND FloaTVUpdateals.UserfieldId = FloatParams.Id
	JOIN EventFrameValues AS DateVals
		ON DateVals.EventFrameId = F.Id
		AND DateVals.UserfieldId = DateParams.Id
	WHERE T.Name = '������'
) AS R
PIVOT
(
	SUM(FloaTVUpdateals) FOR FloatParams IN ([����� (�����), ��], [����� (������), ��])
) AS pvt1
PIVOT
(
	MAX(DateVals) FOR DateParams IN ([���� ������������])
) AS pvt2 
GROUP BY MONTH([���� ������������])