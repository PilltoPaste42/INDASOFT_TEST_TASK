USE EVENTFRAME;
GO

CREATE OR ALTER PROCEDURE SelectEventsWithValuesById
(
	@EventFrameId NVARCHAR(50)
) AS
BEGIN
	SELECT 
		F.Name AS Event,
		TV.Type AS ValueType,
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
		F.Id = @EventFrameId
		AND T.Id = F.EventFrameTypeId
		AND TV.EventFrameTypeId = T.Id
		AND V.EventFrameId = F.Id
		AND V.UserfieldId = TV.Id
END