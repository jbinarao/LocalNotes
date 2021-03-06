USE [master]
GO

/****** Object:  Database [LocalNotes] ******/
CREATE DATABASE [LocalNotes]
GO

USE [LocalNotes]
GO

/****** Object:  Table [dbo].[NoteData] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteData](
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[NoteName] [nvarchar](50) NULL,
	[NoteText] [nvarchar](max) NULL,
	[NoteTime] [datetime2](7) NULL,
	[IsRemove] [bit] NULL
)
GO

/****** Object:  StoredProcedure [dbo].[spInsertNoteData] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	INSERT details about the note.
-- =============================================
CREATE PROCEDURE [dbo].[spInsertNoteData]
	-- Add the parameters for the stored procedure here
	(
		@noteName AS NVARCHAR(50),
		@noteText AS NVARCHAR(MAX)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	INSERT INTO [LocalNotes].[dbo].[NoteData]
	( [NoteName], [NoteText], [NoteTime], [IsRemove] )
	VALUES
	( @NoteName, @NoteText, GETDATE(), 'false' )
	SELECT CAST(SCOPE_IDENTITY() AS INT)
END
GO

/****** Object:  StoredProcedure [dbo].[spSelectActiveNote] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	SELECT note record by Note ID.
-- =============================================
CREATE PROCEDURE [dbo].[spSelectActiveNote]
	-- Add the parameters for the stored procedure here
	(
		@noteID AS INT
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	SELECT [NoteName]
		, [NoteText]
		, [NoteTime]
	FROM [LocalNotes].[dbo].[NoteData]
	WHERE
		[NoteID] = @noteID
		AND [IsRemove] = 'false'
END
GO

/****** Object:  StoredProcedure [dbo].[spSelectNoteData] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Obtain details about the note.
-- =============================================
CREATE PROCEDURE [dbo].[spSelectNoteData]
	-- Add the parameters for the stored procedure here
	(
		@isRemove AS BIT,
		@lenAbbrText AS INT,
		@isSortDesc AS BIT
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [NoteID]
		, [NoteName]
		, CASE
			WHEN @lenAbbrText < LEN([NoteText]) THEN
				--LEFT([NoteText], @lenAbbrText)
				CONCAT(LEFT([NoteText], @lenAbbrText), '...')
			ELSE
				[NoteText]
		END
		AS NoteAbbr
		, [NoteTime]
		, CASE
			WHEN DATEDIFF(SS, [NoteTime], GETDATE()) BETWEEN 0 AND 59 THEN
				'just now'
			WHEN DATEDIFF(SS, [NoteTime], GETDATE()) BETWEEN 60 AND 119 THEN
				'a minute ago'
			WHEN DATEDIFF(MI, [NoteTime], GETDATE()) BETWEEN 2 AND 59 THEN
				CONCAT(DATEDIFF(MI, [NoteTime], GETDATE()), ' minutes ago')
			WHEN DATEDIFF(MI, [NoteTime], GETDATE()) BETWEEN 60 AND 119 THEN
				'an hour ago'
			WHEN DATEDIFF(HH, [NoteTime], GETDATE()) BETWEEN 2 AND 23 THEN
				CONCAT(DATEDIFF(HH, [NoteTime], GETDATE()), ' hours ago')
			WHEN DATEDIFF(HH, [NoteTime], GETDATE()) BETWEEN 24 AND 47 THEN
				'a day ago'
			WHEN DATEDIFF(DD, [NoteTime], GETDATE()) BETWEEN 2 AND 30 THEN
				CONCAT(DATEDIFF(DD, [NoteTime], GETDATE()), ' days ago')
			WHEN DATEDIFF(DD, [NoteTime], GETDATE()) BETWEEN 31 AND 59 THEN
				'a month ago'
			WHEN DATEDIFF(MM, [NoteTime], GETDATE()) BETWEEN 2 AND 11 THEN
				CONCAT(DATEDIFF(MM, [NoteTime], GETDATE()), ' months ago')
			WHEN DATEDIFF(MM, [NoteTime], GETDATE()) BETWEEN 12 AND 23 THEN
				'a year ago'
			WHEN DATEDIFF(YY, [NoteTime], GETDATE()) BETWEEN 2 AND 11 THEN
				CONCAT(DATEDIFF(YY, [NoteTime], GETDATE()), ' years ago')
			ELSE
				CONCAT(DATEDIFF(YY, [NoteTime], GETDATE()), ' years ago')
		END AS Modified
	FROM [LocalNotes].[dbo].[NoteData]
	WHERE [IsRemove] = @isRemove
	ORDER BY
		CASE
			WHEN @isSortDesc = 'false' THEN [NoteTime]
		END ASC
		, CASE
			WHEN @isSortDesc = 'true' THEN [NoteTime]
		END DESC
END
GO

/****** Object:  StoredProcedure [dbo].[spUpdateNoteDetail] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Update details about the note.
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateNoteDetail]
	-- Add the parameters for the stored procedure here
	(
		@noteID AS INT,
		@noteName AS NVARCHAR(50),
		@noteText AS NVARCHAR(MAX)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	UPDATE [LocalNotes].[dbo].[NoteData]
		SET [NoteName] = @noteName
			, [NoteText] = @noteText
			, [NoteTime] = GETDATE()
		WHERE (NoteID = @noteID)
END
GO

/****** Object:  StoredProcedure [dbo].[spUpdateNoteRemove] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	UPDATE IsRemove value to true.
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateNoteRemove]
	-- Add the parameters for the stored procedure here
	(
		@noteID AS INT
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	UPDATE [LocalNotes].[dbo].[NoteData]
		SET [IsRemove] = 'true'
		WHERE (NoteID = @noteID)
END
GO

/****** Object:  StoredProcedure [dbo].[spUpdateNoteRestore] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	UPDATE IsRemove value to false.
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateNoteRestore]
	-- Add the parameters for the stored procedure here
	(
		@noteID AS INT
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	UPDATE [LocalNotes].[dbo].[NoteData]
		SET [IsRemove] = 'false'
		WHERE (NoteID = @noteID)
END
GO

USE [master]
GO

ALTER DATABASE [LocalNotes] SET READ_WRITE 
GO
