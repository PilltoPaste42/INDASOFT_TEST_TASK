USE EVENTFRAME;
GO

CREATE TYPE ParametersTableType AS TABLE
	(
		Name NVARCHAR(50),
		Value NVARCHAR(100)
	);