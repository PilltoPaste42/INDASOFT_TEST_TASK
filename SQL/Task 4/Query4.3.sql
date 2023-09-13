USE EVENTFRAME;
GO

SELECT 
	FBatch.Id AS BatchId,
	FBatch.Name AS BatchName,
	AVG(VSample.ValueFloat) AS [AVG]
FROM Links AS L
JOIN EventFrameTypes AS TBatch
	ON TBatch.Name = 'Партия'
JOIN EventFrameTypes AS TSample
	ON TSample.Name = 'Образец'
JOIN EventFrames AS FBatch
	ON FBatch.EventFrameTypeId = TBatch.Id
JOIN EventFrames AS FSample
	ON FSample.EventFrameTypeId = TSample.Id
JOIN EventFrameTypeValues AS TVSample
	ON TVSample.EventFrameTypeId = TSample.Id
	AND TVSample.Name LIKE '%, [%]%'
JOIN EventFrameValues AS VSample
	ON VSample.EventFrameId = FSample.Id
	AND VSample.UserfieldId = TVSample.Id
WHERE
	L.ParentEventFrameId = FBatch.Id
	AND L.ChildEventFrameId = FSample.Id
GROUP BY FBatch.Id, FBatch.Name
HAVING AVG(VSample.ValueFloat) > 50
ORDER BY FBatch.Name
	
-- Старая версия
--SELECT F.Id, F.Name
--FROM
--	EventFrames AS F
--JOIN EventFrameTypes AS T
--	ON T.Name = 'Партия'
--JOIN Links AS L
--	ON L.ParentEventFrameId = F.Id
--WHERE
--	F.EventFrameTypeId = T.Id 
--	AND EXISTS 
--	(
--		SELECT *
--		FROM EventFrames AS F2, EventFrameTypes AS T2
--		WHERE
--			F2.Id = L.ChildEventFrameId AND
--			F2.EventFrameTypeId = T2.Id AND
--			T2.Name = 'Образец' AND
--			(
--				SELECT AVG(V2.ValueFloat)
--				FROM EventFrameValues AS V2
--				WHERE V2.EventFrameId = F2.Id
--				AND EXISTS 
--				(
--					SELECT *
--					FROM EventFrameTypeValues AS TV3
--					WHERE
--					TV3.Id = V2.UserfieldId AND
--					TV3.EventFrameTypeId = T2.Id AND
--					TV3.Name LIKE '%, [%]%'
--				)
--			) > 50
--	)
--ORDER BY F.Name