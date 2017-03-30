SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Author](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED
(
	[AuthorId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED
(
	[CategoryId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Slug] [nvarchar](50) NOT NULL,
	[Summary] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED
(
	[PostId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
SET IDENTITY_INSERT [Author] ON

GO
INSERT [Author] ([AuthorId], [DisplayName], [FirstName], [LastName], [Slug], [IsDeleted]) VALUES (1, N'Sidney A', N'Sidney', N'Andrews', N'sidney-andrews', 0)
GO
SET IDENTITY_INSERT [Author] OFF
GO
SET IDENTITY_INSERT [Category] ON

GO
INSERT [Category] ([CategoryId], [Name], [Slug], [IsDeleted]) VALUES (1, N'General', N'general', 0)
GO
SET IDENTITY_INSERT [Category] OFF
GO
SET IDENTITY_INSERT [Post] ON

GO
INSERT [Post] ([PostId], [CategoryId], [AuthorId], [Title], [Slug], [Summary], [Content], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (2, 1, 1, N'Example Two', N'example-welcome', N'Hey! I’m your first Markdown document in StackEdit. Don’t delete me, I’m very helpful! I can be recovered anyway in the Utils tab of the Settings dialog.', N'Welcome to StackEdit!
===================

Hey! I''m your first Markdown document in **StackEdit**[^stackedit]. Don''t delete me, I''m very helpful! I can be recovered anyway in the **Utils** tab of the **Settings** dialog.

----------

Documents
-------------

StackEdit stores your documents in your browser, which means all your documents are automatically saved locally and are accessible **offline!**

> **Note:**

> - StackEdit is accessible offline after the application has been loaded for the first time.
> - Your local documents are not shared between different browsers or computers.
> - Clearing your browser''s data may **delete all your local documents!** Make sure your documents are synchronized with **Google Drive** or **Dropbox** (check out the [Synchronization](#synchronization) section).
', CAST(N'2017-03-26T19:16:35.247' AS DateTime), CAST(N'2017-03-26T21:28:02.760' AS DateTime), 0)
GO
INSERT [Post] ([PostId], [CategoryId], [AuthorId], [Title], [Slug], [Summary], [Content], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (3, 1, 1, N'Test Post', N'test-post', N'Test post', N'**Test** _post_', CAST(N'2017-03-26T21:28:43.543' AS DateTime), CAST(N'2017-03-26T22:23:35.733' AS DateTime), 0)
GO
SET IDENTITY_INSERT [Post] OFF
GO
ALTER TABLE [Author] ADD  CONSTRAINT [DF_Author_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [Category] ADD  CONSTRAINT [DF_Category_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [Post] ADD  CONSTRAINT [DF_Post_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Post] ADD  CONSTRAINT [DF_Post_UpdatedAt]  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [Post] ADD  CONSTRAINT [DF_Post_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Author] FOREIGN KEY([AuthorId])
REFERENCES [Author] ([AuthorId])
GO
ALTER TABLE [Post] CHECK CONSTRAINT [FK_Post_Author]
GO
ALTER TABLE [Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Category] FOREIGN KEY([CategoryId])
REFERENCES [Category] ([CategoryId])
GO
ALTER TABLE [Post] CHECK CONSTRAINT [FK_Post_Category]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [tgr_post_updatedat]
ON [Post]
AFTER UPDATE AS
	UPDATE Post
	SET UpdatedAt = GETDATE()
	WHERE PostId IN (SELECT DISTINCT PostId FROM inserted)

GO
ALTER TABLE [dbo].[Post] ENABLE TRIGGER [tgr_post_updatedat]
GO
