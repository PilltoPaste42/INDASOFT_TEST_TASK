USE EVENTFRAME;
GO

CREATE OR ALTER PROCEDURE AddLink
(
	@parentEventId NVARCHAR(50),
	@childEventId NVARCHAR(50),
	@linkId NVARCHAR(50) OUTPUT
) AS 
BEGIN
	DECLARE @TableVar TABLE
	(
		Id uniqueidentifier
	);

	INSERT INTO Links
	(
		ParentEventFrameId,
		ParentEventFrameTypeId,
		ChildEventFrameId,
		ChildEventFrameTypeId
	)
	OUTPUT INSERTED.Id INTO @TableVar
	VALUES
	(
		@parentEventId,
		(SELECT EventFrameTypeId FROM EventFrames WHERE Id = @parentEventId), 
		@childEventId,
		(SELECT EventFrameTypeId FROM EventFrames WHERE Id = @childEventId)
	);

	SET @linkId = CAST((SELECT Id FROM @TableVar)AS NVARCHAR(50)) ;
END