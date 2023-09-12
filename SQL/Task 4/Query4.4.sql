USE EVENTFRAME;
GO

SELECT 
	T.Name AS EventType,
	(
		SELECT COUNT(TV.Type) 
		FROM EventFrameTypeValues AS TV
		WHERE TV.Type = '�����' AND TV.EventFrameTypeId = T.Id
	) AS TextValues,
	(
		SELECT COUNT(TV.Type) 
		FROM EventFrameTypeValues AS TV
		WHERE TV.Type = '����� �����' AND TV.EventFrameTypeId = T.Id
	) AS IntValues,
	(
		SELECT COUNT(TV.Type) 
		FROM EventFrameTypeValues AS TV
		WHERE TV.Type = '������������ �����' AND TV.EventFrameTypeId = T.Id
	) AS FloatValues,
	(
		SELECT COUNT(TV.Type) 
		FROM EventFrameTypeValues AS TV
		WHERE TV.Type = '����' AND TV.EventFrameTypeId = T.Id
	) AS DataValues,
	(
		SELECT COUNT(TV.Type) 
		FROM EventFrameTypeValues AS TV
		WHERE 
TV.EventFrameTypeId = T.Id
	) AS AllValues
	
FROM EventFrameTypes AS T

