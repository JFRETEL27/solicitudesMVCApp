USE [bd_upc]
GO
/****** Object:  Table [dbo].[Alumno]    Script Date: 1/12/2022 17:21:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alumno](
	[IdAlumno] [uniqueidentifier] NOT NULL,
	[Nombres] [varchar](1000) NOT NULL,
	[Apellidos] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_Alumno] PRIMARY KEY CLUSTERED 
(
	[IdAlumno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curso]    Script Date: 1/12/2022 17:21:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curso](
	[IdCurso] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](1000) NOT NULL,
	[Description] [varchar](4000) NOT NULL,
	[NroCreditos] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Cursos] PRIMARY KEY CLUSTERED 
(
	[IdCurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleSolicitud]    Script Date: 1/12/2022 17:21:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleSolicitud](
	[IdDetalleSol] [uniqueidentifier] NOT NULL,
	[IdSolicitud] [uniqueidentifier] NOT NULL,
	[IdCurso] [uniqueidentifier] NOT NULL,
	[Profesor] [varchar](1000) NOT NULL,
	[Aula] [varchar](1000) NOT NULL,
	[Sede] [varchar](1000) NOT NULL,
	[Observación] [varchar](4000) NOT NULL,
 CONSTRAINT [PK_DetalleSolicitud] PRIMARY KEY CLUSTERED 
(
	[IdDetalleSol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solicitud]    Script Date: 1/12/2022 17:21:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solicitud](
	[IdSolicitud] [uniqueidentifier] NOT NULL,
	[IdAlumno] [uniqueidentifier] NOT NULL,
	[FechaSolicitud] [datetime] NOT NULL,
	[CodRegistrante] [varchar](1000) NOT NULL,
	[Carrera] [varchar](1000) NOT NULL,
	[Periodo] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED 
(
	[IdSolicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetalleSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_DetalleSolicitud_Curso] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Curso] ([IdCurso])
GO
ALTER TABLE [dbo].[DetalleSolicitud] CHECK CONSTRAINT [FK_DetalleSolicitud_Curso]
GO
ALTER TABLE [dbo].[DetalleSolicitud]  WITH CHECK ADD  CONSTRAINT [FK_DetalleSolicitud_Solicitud] FOREIGN KEY([IdSolicitud])
REFERENCES [dbo].[Solicitud] ([IdSolicitud])
GO
ALTER TABLE [dbo].[DetalleSolicitud] CHECK CONSTRAINT [FK_DetalleSolicitud_Solicitud]
GO
ALTER TABLE [dbo].[Solicitud]  WITH CHECK ADD  CONSTRAINT [FK_Solicitud_Alumno] FOREIGN KEY([IdAlumno])
REFERENCES [dbo].[Alumno] ([IdAlumno])
GO
ALTER TABLE [dbo].[Solicitud] CHECK CONSTRAINT [FK_Solicitud_Alumno]
GO
