USE EVENTFRAME;
GO

SELECT 
	F.Id,
	F.Name
FROM EventFrames AS F
JOIN EventFrameTypes AS T
	ON T.Name = 'Блок'
WHERE 
	F.EventFrameTypeId = T.Id
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
