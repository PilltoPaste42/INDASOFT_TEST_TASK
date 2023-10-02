USE EVENTFRAME;
GO

CREATE OR ALTER PROCEDURE SelectEventsWithValuesById
(
	@EventFrameId NVARCHAR(50)
) AS
BEGIN
	SELECT 
		F.Id AS EventId,
		T.ID AS EventTypeId,
		F.Name AS EventName,
		TV.Type AS ValueType,
		TV.Name AS ValueName,
		V.ValueText,
		V.ValueFloat,
		V.ValueInt,
		V.ValueDatetime
	FROM 
		EventFrames AS F
	JOIN EventFrameTypes AS T
		ON T.Id = F.EventFrameTypeId
	JOIN EventFrameTypeValues AS TV
		ON TV.EventFrameTypeId = T.Id
	JOIN EventFrameValues AS V
		ON V.EventFrameId = F.Id
		AND V.UserfieldId = TV.Id
	WHERE
		F.Id = @EventFrameId
END