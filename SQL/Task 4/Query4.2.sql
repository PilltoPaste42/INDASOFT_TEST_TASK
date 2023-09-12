USE EVENTFRAME;
GO

SELECT 
	F.Id,
	F.Name
FROM 
	EventFrames AS F,
	EventFrameTypes AS T
WHERE 
	T.Name = 'Блок'
	AND F.EventFrameTypeId = T.Id
	AND NOT EXISTS
	(
		SELECT * 
		FROM Links AS L 
		WHERE 
			L.ChildEventFrameId = F.Id
			AND L.ParentEventFrameTypeId =
			(
				SELECT Id
				FROM EventFrameTypes
				WHERE Name = 'Партия'
			)
	)
