USE EVENTFRAME;
GO

SELECT 
	F.Name AS Event,
	TV.Name AS ValueName,
	V.ValueText,
	V.ValueFloat,
	V.ValueInt,
	V.ValueDatetime
FROM EventFrames AS F
JOIN EventFrameTypes AS T
	ON T.Id = F.EventFrameTypeId
JOIN EventFrameTypeValues AS TV
	ON TV.EventFrameTypeId = T.Id
JOIN EventFrameValues AS V
	ON V.EventFrameId = F.Id
	AND V.UserfieldId = TV.Id

ORDER BY Event