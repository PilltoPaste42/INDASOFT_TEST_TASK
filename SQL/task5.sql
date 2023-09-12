USE EVENTFRAME;
GO

CREATE OR ALTER PROCEDURE UpdateBatchParameter
(
	@batchNumber NVARCHAR(100),
	@parameterName NVARCHAR(50),
	@newParameterValue NVARCHAR(100)
) AS
BEGIN
	DECLARE @BatchUpdateResult TABLE 
	(
		batchNumber NVARCHAR(100),
		parameterName NVARCHAR(50),
		oldValue NVARCHAR(100),
		newValue NVARCHAR(100)
	);
	
	UPDATE EventFrameValues
	SET
		ValueInt = IIF(R.ValueType = 'Целое число', CAST(@newParameterValue AS INT), NULL),
		ValueFloat = IIF(R.ValueType = 'Вещественное число', CAST(@newParameterValue AS FLOAT), NULL),
		ValueText = IIF(R.ValueType = 'Текст', @newParameterValue, NULL),
		ValueDatetime = IIF(R.ValueType = 'Дата', CAST(@newParameterValue AS DATETIME), NULL)
	OUTPUT 
		R.BatchNumber,
		R.ParameterName,
		COALESCE
		(
			CAST(deleted.ValueInt AS NVARCHAR(100)),
			CAST(deleted.ValueFloat AS NVARCHAR(100)),
			CAST(deleted.ValueText AS NVARCHAR(100)),
			CONVERT(NVARCHAR(100), deleted.ValueDatetime, 104)
		),
		COALESCE
		(
			CAST(inserted.ValueInt AS NVARCHAR(100)),
			CAST(inserted.ValueFloat AS NVARCHAR(100)),
			CAST(inserted.ValueText AS NVARCHAR(100)),
			CONVERT(NVARCHAR(100), inserted.ValueDatetime, 104)
		)
	INTO @BatchUpdateResult (batchNumber, parameterName, oldValue, newValue)
	FROM 
	(
		SELECT 
			VUpdate.Id AS ValueId,
			VNumber.ValueText AS BatchNumber,
			TVUpdate.Type AS ValueType,
			TVUpdate.Name AS ParameterName
		FROM EventFrameValues AS VUpdate
			JOIN EventFrameTypes AS T
				ON T.Name = 'Партия'
			JOIN EventFrameTypeValues AS TVNumber
				ON TVNumber.EventFrameTypeId = T.Id
				AND TVNumber.Name = 'Номер партии'
			JOIN EventFrameValues AS VNumber
				ON VNumber.UserfieldId = TVNumber.Id
				AND VNumber.ValueText = @batchNumber
			JOIN EventFrames AS F
				ON F.Id = VNumber.EventFrameId
				AND F.EventFrameTypeId = T.Id
			JOIN EventFrameTypeValues AS TVUpdate
				ON TVUpdate.Name = @parameterName
				AND TVUpdate.EventFrameTypeId = T.Id
		WHERE 
			VUpdate.EventFrameId = F.Id
			AND VUpdate.UserfieldId = TVUpdate.Id
	) AS R
	WHERE
		EventFrameValues.Id = R.ValueId

	SELECT * FROM @BatchUpdateResult
	
END