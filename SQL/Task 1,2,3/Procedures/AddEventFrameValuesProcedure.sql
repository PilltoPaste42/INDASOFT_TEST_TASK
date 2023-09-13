USE EVENTFRAME;
GO

CREATE OR ALTER PROCEDURE AddEventFrameValues
(
	@eventId NVARCHAR(50),
	@Params ParametersTableType READONLY
) AS 
BEGIN
	DECLARE @eventTypeId NVARCHAR(50);
	SET @eventTypeId=(SELECT EventFrameTypeId FROM EventFrames WHERE Id = @eventId);
	
	INSERT INTO EventFrameValues (EventFrameId, UserfieldId, ValueText, ValueInt, ValueFloat, ValueDatetime)
	SELECT 
		@eventId, 
		TV.Id, 
		IIF(TV.Type = 'Текст', P.Value, NULL), 
		IIF(TV.Type = 'Целое число', CAST(P.Value AS INT), NULL), 
		IIF(TV.Type = 'Вещественное число', CAST(P.Value AS FLOAT), NULL), 
		IIF(TV.Type = 'Дата', CAST(P.Value AS DATETIME), NULL)
	FROM 
		@Params AS P,
		EventFrameTypeValues AS TV
	WHERE
		TV.EventFrameTypeId = @eventTypeId
		AND P.Name = TV.Name

	RETURN 0;
END