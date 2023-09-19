USE EVENTFRAME;
GO

DECLARE @batchTypeId NVARCHAR(MAX)
SELECT @batchTypeId = T.Id
FROM EventFrameTypes AS T
WHERE T.Name = 'Партия'

DECLARE @batchTypeIdStr NVARCHAR(MAX) = QUOTENAME(@batchTypeId,'''');

DECLARE @cols NVARCHAR(MAX)
	
SELECT @cols = ISNULL(@cols + ', ', '') + QUOTENAME(TV.Name)
FROM EventFrameTypeValues AS TV
WHERE TV.EventFrameTypeId = @batchTypeId


DECLARE @subquery NVARCHAR(MAX) =
'
SELECT 
	F.Id AS [Id партии],
	TV.Name AS [Param],
	COALESCE
	(
		CAST(V.ValueInt AS NVARCHAR(100)),
		CAST(V.ValueFloat AS NVARCHAR(100)),
		CAST(V.ValueText AS NVARCHAR(100)),
		CONVERT(NVARCHAR(100), V.ValueDatetime, 104)
	) AS [Val]
FROM EventFrames AS F
JOIN EventFrameTypeValues AS TV
	ON TV.EventFrameTypeId = F.EventFrameTypeId
JOIN EventFrameValues AS V
	ON V.EventFrameId = F.Id
	AND V.UserfieldId = TV.Id
WHERE F.EventFrameTypeId = ' + @batchTypeIdStr + '
'

DECLARE @query NVARCHAR(MAX) =
'
SELECT * FROM
(
	' + @subquery + '
) AS Result
PIVOT
(
	MAX(Val) FOR Param IN (' + @cols + ')
) AS pvt1
ORDER BY [Номер партии]

'
EXEC (@query)
--EXEC (@subquery)
