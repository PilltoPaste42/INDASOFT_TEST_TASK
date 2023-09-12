USE EVENTFRAME;
GO

SELECT  
	MONTH([Дата изготовления]) AS [Месяц],
	COUNT([BatchName]) AS [Кол-во партий],
	SUM([Масса (нетто), кг]) AS [Сумма масс (нетто), кг], 
	SUM([Масса (брутто), кг]) AS [Сумма масс (брутто), кг]
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
		AND FloatParams.Type = 'Вещественное число'
	JOIN EventFrameTypeValues AS DateParams
		ON DateParams.EventFrameTypeId = T.Id
		AND DateParams.Type = 'Дата'
	JOIN EventFrameValues AS FloaTVUpdateals
		ON FloaTVUpdateals.EventFrameId = F.Id
		AND FloaTVUpdateals.UserfieldId = FloatParams.Id
	JOIN EventFrameValues AS DateVals
		ON DateVals.EventFrameId = F.Id
		AND DateVals.UserfieldId = DateParams.Id
	WHERE T.Name = 'Партия'
) AS R
PIVOT
(
	SUM(FloaTVUpdateals) FOR FloatParams IN ([Масса (нетто), кг], [Масса (брутто), кг])
) AS pvt1
PIVOT
(
	MAX(DateVals) FOR DateParams IN ([Дата изготовления])
) AS pvt2 
GROUP BY MONTH([Дата изготовления])