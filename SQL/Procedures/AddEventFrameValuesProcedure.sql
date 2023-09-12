USE EVENTFRAME;
GO

CREATE OR ALTER PROCEDURE AddEventFrameValues
(
	@eventId NVARCHAR(50),
	@Params ParametersTableType READONLY
) AS 
BEGIN
	DECLARE @eventTypeId NVARCHAR(50);
	SET @eventTypeId=(SELECT EventFrameTypeId FROM dbo.EventFrames WHERE Id = @eventId);
	
	INSERT INTO dbo.EventFrameValues (EventFrameId, UserfieldId, ValueText, ValueInt, ValueFloat, ValueDatetime)
	SELECT 
		@eventId, 
		T.Id, 
		IIF(T.Type = 'Текст', P.Value, NULL), 
		IIF(T.Type = 'Целое число', CAST(P.Value AS INT), NULL), 
		IIF(T.Type = 'Вещественное число', CAST(P.Value AS FLOAT), NULL), 
		IIF(T.Type = 'Дата', CAST(P.Value AS DATETIME), NULL)
	FROM 
		@Params AS P,
		dbo.EventFrameTypeValues AS T
	WHERE
		T.EventFrameTypeId = @eventTypeId
		AND P.Name = T.Name

	RETURN 0;
END