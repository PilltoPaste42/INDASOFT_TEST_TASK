USE EVENTFRAME;
GO

CREATE OR ALTER PROCEDURE AddEventFrame
(
	@eventName NVARCHAR(50),
	@eventType NVARCHAR(50),
	@batchId NVARCHAR(50) OUTPUT
) AS 
BEGIN
	DECLARE @TableVar TABLE
	(
		Id uniqueidentifier
	);

	INSERT INTO EventFrames (Name, EventFrameTypeId)
	OUTPUT INSERTED.Id INTO @TableVar
	SELECT
		@eventName,
		T.Id
	FROM dbo.EventFrameTypes AS T
	WHERE T.Name = @eventType
	
	SET @batchId = CAST((SELECT Id FROM @TableVar)AS NVARCHAR(50)) ; 
END