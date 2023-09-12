USE EVENTFRAME;
GO

SELECT F.Id, F.Name
FROM
	EventFrames AS F,
	EventFrameTypes AS T,
	Links AS L
WHERE
	F.EventFrameTypeId = T.Id AND T.Name = 'Партия' AND
	L.ParentEventFrameId = F.Id AND
	EXISTS 
	(
		SELECT *
		FROM EventFrames AS F2, EventFrameTypes AS T2
		WHERE
			F2.Id = L.ChildEventFrameId AND
			F2.EventFrameTypeId = T2.Id AND
			T2.Name = 'Образец' AND
			(
				SELECT AVG(V2.ValueFloat)
				FROM EventFrameValues AS V2
				WHERE V2.EventFrameId = F2.Id
				AND EXISTS 
				(
					SELECT *
					FROM EventFrameTypeValues AS TV3
					WHERE
					TV3.Id = V2.UserfieldId AND
					TV3.EventFrameTypeId = T2.Id AND
					TV3.Name LIKE '%, [%]%'
				)
			) > 50
	)
ORDER BY F.Name