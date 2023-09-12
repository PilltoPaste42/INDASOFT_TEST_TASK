SELECT 
	F.Name AS Event,
	TV.Name AS ValueName,
	V.ValueText,
	V.ValueFloat,
	V.ValueInt,
	V.ValueDatetime
FROM 
	EventFrames AS F,
	EventFrameTypes AS T,
	EventFrameTypeValues AS TV,
	EventFrameValues AS V
WHERE
	T.Id = F.EventFrameTypeId
	AND TV.EventFrameTypeId = T.Id
	AND V.EventFrameId = F.Id
	AND V.UserfieldId = TV.Id
ORDER BY Event