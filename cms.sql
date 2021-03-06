USE [master]
GO
/****** Object:  Database [SMS]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE DATABASE [SMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SMS.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SMS_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SMS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [SMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SMS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SMS] SET RECOVERY FULL 
GO
ALTER DATABASE [SMS] SET  MULTI_USER 
GO
ALTER DATABASE [SMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SMS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SMS', N'ON'
GO
USE [SMS]
GO
/****** Object:  UserDefinedTableType [dbo].[fee_categories]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[fee_categories] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](45) NOT NULL,
	[recur] [varchar](10) NOT NULL,
	[is_transport] [varchar](10) NULL,
	[order] [int] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[FeeDueModel]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[FeeDueModel] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[student_id] [uniqueidentifier] NOT NULL,
	[student_balance] [float] NULL,
	[apply_from] [date] NOT NULL,
	[apply_to] [date] NOT NULL,
	[fine] [float] NULL,
	[concession_amount] [float] NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[fees]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[fees] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[fee_category_id] [uniqueidentifier] NOT NULL,
	[session_id] [uniqueidentifier] NOT NULL,
	[amount] [float] NOT NULL,
	[fee_cources] [varchar](max) NULL,
	[last_date] [date] NULL,
	[last_day] [int] NULL,
	[fine_per_day] [float] NULL,
	[is_allocated] [varchar](10) NOT NULL,
	[remarks] [varchar](max) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[files]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[files] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[name] [varchar](255) NULL,
	[extension] [varchar](45) NULL,
	[size] [int] NULL,
	[school_id] [int] NULL,
	[owner_user_id] [int] NULL,
	[created] [datetime2](0) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[grade_fees]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[grade_fees] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[fees_id] [uniqueidentifier] NOT NULL,
	[grade_id] [uniqueidentifier] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[grades]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[grades] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[block] [varchar](50) NULL,
	[name] [varchar](45) NOT NULL,
	[order] [int] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[parents]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[parents] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[f_first_name] [varchar](45) NULL,
	[f_middle_name] [varchar](45) NULL,
	[f_last_name] [varchar](45) NULL,
	[f_full_name] [varchar](100) NULL,
	[f_mobile] [varchar](15) NULL,
	[f_phone] [varchar](15) NULL,
	[f_office] [varchar](15) NULL,
	[f_email] [varchar](45) NULL,
	[m_first_name] [varchar](45) NULL,
	[m_middle_name] [varchar](45) NULL,
	[m_last_name] [varchar](45) NULL,
	[m_full_name] [varchar](100) NULL,
	[m_mobile] [varchar](15) NULL,
	[m_phone] [varchar](15) NULL,
	[m_office] [varchar](15) NULL,
	[m_email] [varchar](45) NULL,
	[g_fullname] [varchar](45) NULL,
	[g_mobile] [varchar](15) NULL,
	[g_email] [varchar](45) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[PaymentModel]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[PaymentModel] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[student_fees_id] [uniqueidentifier] NOT NULL,
	[amount] [float] NOT NULL,
	[fine] [float] NULL,
	[comment] [varchar](max) NULL,
	[recept_no] [varchar](45) NULL,
	[payment_mode] [varchar](50) NOT NULL,
	[payment_date] [datetime2](0) NOT NULL,
	[ip] [varchar](45) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[route_stops]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[route_stops] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[route_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](max) NOT NULL,
	[latitude] [varchar](max) NULL,
	[longitude] [varchar](max) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[route_vehicle_stops]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[route_vehicle_stops] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[route_vehicle_id] [uniqueidentifier] NOT NULL,
	[route_stop_id] [uniqueidentifier] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[route_vehicle_stops_fee_logs]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[route_vehicle_stops_fee_logs] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[route_vehicle_stop_id] [uniqueidentifier] NOT NULL,
	[fees_id] [uniqueidentifier] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[route_vehicles]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[route_vehicles] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[vehicle_id] [uniqueidentifier] NOT NULL,
	[route_id] [uniqueidentifier] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[routes]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[routes] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[start_location] [varchar](max) NULL,
	[end_location] [varchar](max) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[schools]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[schools] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[database_id] [uniqueidentifier] NULL,
	[subdomain] [varchar](45) NULL,
	[domain] [varchar](max) NULL,
	[name] [varchar](100) NOT NULL,
	[address] [varchar](max) NOT NULL,
	[website] [varchar](max) NULL,
	[phone] [varchar](max) NULL,
	[email] [varchar](max) NULL,
	[theme] [varchar](max) NULL,
	[created_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[sections]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[sections] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](10) NOT NULL,
	[capacity] [int] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[sessions]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[sessions] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](45) NOT NULL,
	[order] [int] NOT NULL,
	[from_date] [datetime2](0) NULL,
	[to_date] [datetime2](0) NULL,
	[is_active] [varchar](10) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[student_fees]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[student_fees] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[grade_fees_id] [uniqueidentifier] NULL,
	[student_id] [uniqueidentifier] NOT NULL,
	[route_vehicle_stops_fee_log_id] [uniqueidentifier] NULL,
	[apply_from] [date] NOT NULL,
	[apply_to] [date] NOT NULL,
	[fine] [float] NULL,
	[concession_amount] [float] NULL,
	[no_fine] [smallint] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[student_grade_session_log]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[student_grade_session_log] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[student_id] [uniqueidentifier] NOT NULL,
	[registration_id] [varchar](45) NOT NULL,
	[grade_id] [uniqueidentifier] NOT NULL,
	[section_id] [uniqueidentifier] NOT NULL,
	[roll_number] [varchar](100) NOT NULL,
	[exam_roll_number] [varchar](45) NULL,
	[session_id] [uniqueidentifier] NOT NULL,
	[sgsl_status] [varchar](50) NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[student_payments]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[student_payments] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[student_fees_id] [uniqueidentifier] NOT NULL,
	[amount] [float] NOT NULL,
	[fine] [float] NULL,
	[comment] [varchar](max) NULL,
	[recept_no] [varchar](45) NULL,
	[payment_mode] [varchar](50) NOT NULL,
	[payment_date] [datetime2](0) NOT NULL,
	[ip] [varchar](45) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[students]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[students] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[parent_id] [uniqueidentifier] NULL,
	[trip_stop_id] [uniqueidentifier] NULL,
	[enrollment_date] [date] NULL,
	[status] [varchar](50) NOT NULL,
	[dc_number] [varchar](50) NULL,
	[dc_date_of_issue] [datetime2](0) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[trip_stops]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[trip_stops] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[route_vehicle_stop_id] [int] NOT NULL,
	[trip] [varchar](50) NOT NULL,
	[pick] [time](7) NOT NULL,
	[drop] [time](7) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[user_avatar_files]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[user_avatar_files] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[school_id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[applicant_id] [uniqueidentifier] NULL,
	[file_id] [uniqueidentifier] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[users]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[users] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NULL,
	[student_id] [uniqueidentifier] NULL,
	[role_id] [uniqueidentifier] NULL,
	[user_type] [varchar](50) NOT NULL,
	[username] [varchar](45) NULL,
	[email] [varchar](100) NULL,
	[phone] [varchar](20) NULL,
	[address_line_one] [varchar](max) NULL,
	[address_line_two] [varchar](max) NULL,
	[area] [varchar](45) NULL,
	[first_name] [varchar](100) NOT NULL,
	[middle_name] [varchar](100) NULL,
	[last_name] [varchar](100) NULL,
	[full_name] [varchar](300) NOT NULL,
	[gender] [varchar](50) NULL,
	[blood_group] [varchar](50) NULL,
	[password] [varchar](200) NULL,
	[birth_date] [datetime2](0) NULL,
	[other_phones] [varchar](max) NULL,
	[default_phone_number_id] [uniqueidentifier] NULL,
	[adhaar_number] [varchar](40) NULL,
	[bank_name] [varchar](100) NULL,
	[bank_branch] [varchar](100) NULL,
	[bank_account_number] [varchar](100) NULL,
	[bank_ifsc_code] [varchar](100) NULL,
	[flags] [varchar](max) NULL,
	[last_login_time] [datetime2](0) NULL,
	[user_avatar_file_id] [uniqueidentifier] NULL,
	[is_active] [varchar](10) NULL,
	[created_on] [datetime2](0) NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[vehicles]    Script Date: 12/29/2017 1:01:24 PM ******/
CREATE TYPE [dbo].[vehicles] AS TABLE(
	[id_offline] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[tracker_id] [uniqueidentifier] NULL,
	[registration_number] [varchar](50) NOT NULL,
	[bus_number] [varchar](15) NULL,
	[insurance_renew_date] [date] NULL,
	[staff_id] [uniqueidentifier] NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
)
GO
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Authenticate User>
-- =============================================
CREATE PROCEDURE [dbo].[AuthenticateUser] @Username nvarchar(50), @Password nvarchar(200)
AS
IF(exists (SELECT 1 FROM Users WHERE username = @Username and password = @Password and is_active = 'true'))
	SELECT Users.*,u.full_name as CreatedBy FROM Users INNER JOIN users u ON Users.created_by = u.id_offline WHERE Users.username = @Username and Users.password = @Password
ELSE IF(exists (SELECT 1 FROM Users WHERE username = @Username and password = @Password and is_active = 'false'))
	SELECT 'Account is not active'
ELSE IF(exists (SELECT 1 FROM Users WHERE username = @Username))
	SELECT 'Password is incorrect'
ELSE IF(exists (SELECT 1 FROM Users WHERE password = @Password))
	SELECT 'Username is incorrect'
ELSE
	SELECT 'Account doesn''t exist'













GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyFeeAllocation]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create or Modify Fee Allocation>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyFeeAllocation]  @FeesModel fees READONLY,@GradeFeesModel grade_fees READONLY,@StudentFeesModel student_fees READONLY
AS
BEGIN TRAN
		IF(exists (select 1 from fees where id_offline=(SELECT obj.id_offline FROM @FeesModel As obj)))
			BEGIN
				UPDATE fees
					SET
						[id_online] =  obj.id_online,
						[school_id] =  obj.school_id,
						[fee_category_id] =  obj.fee_category_id,
						[session_id] =  obj.session_id,
						[amount] =  obj.amount,
						[fee_cources] =  obj.fee_cources,
						[last_date] =  obj.last_date,
						[last_day] =  obj.last_day,
						[fine_per_day] =  obj.fine_per_day,
						[is_allocated] =  obj.is_allocated,
						[remarks] =  obj.remarks,
						[updated_by] =  obj.updated_by,
						[updated_on] =  obj.updated_on
					FROM
						@FeesModel AS obj
					WHERE
						fees.id_offline = obj.id_offline

					-- Parent
					--EXEC [dbo].[CreateOrModifyParents] @Model = @ParentModel;				
			End
		 ELSE
			BEGIN
				INSERT INTO [dbo].[fees]
				   ([id_offline],
					[id_online],
					[school_id],
					[fee_category_id],
					[session_id],
					[amount],
					[fee_cources],
					[last_date],
					[last_day],
					[fine_per_day],
					[is_allocated],
					[remarks],
					[created_by],
					[created_on],
					[updated_by],
					[updated_on])
				 SELECT
						obj.id_offline,
						obj.id_online,
						obj.school_id,
						obj.fee_category_id,
						obj.session_id,
						obj.amount,
						obj.fee_cources,
						obj.last_date,
						obj.last_day,
						obj.fine_per_day,
						obj.is_allocated,
						obj.remarks,
						obj.created_by,
						obj.created_on,
						obj.updated_by,
						obj.updated_on
				  FROM
					 @FeesModel As obj
			END
			-- GradeFees
			Declare @GradeFeesTempTable grade_fees
			Declare @id_offline uniqueidentifier
			Select * into #GradeFeesTempTable from @GradeFeesModel
			While exists (Select * From #GradeFeesTempTable)
			Begin
				Delete from @GradeFeesTempTable

				Select Top 1 @id_offline = id_offline From #GradeFeesTempTable
				Insert into @GradeFeesTempTable Select Top 1 * from #GradeFeesTempTable

				EXEC [dbo].[CreateOrModifyGradeFees] @Model = @GradeFeesTempTable;

				Delete from #GradeFeesTempTable Where id_offline = @Id_offline
			End	
			Drop Table #GradeFeesTempTable

			-- StudentFees
			Declare @StudentFeesTempTable student_fees
			Select * into #StudentFeesTempTable from @StudentFeesModel
			While exists (Select * From #StudentFeesTempTable)
			Begin
				Delete from @StudentFeesTempTable

				Select Top 1 @id_offline = id_offline From #StudentFeesTempTable
				Insert into @StudentFeesTempTable Select Top 1 * from #StudentFeesTempTable

				EXEC [dbo].[CreateOrModifyStudentFees] @Model = @StudentFeesTempTable;

				Delete from #StudentFeesTempTable Where id_offline = @Id_offline

			End
			
COMMIT

















GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyFeeCategories]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify FeeCategories>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyFeeCategories]  @Model fee_categories READONLY
AS
DECLARE @maxOrder INT
BEGIN TRAN	
		SET @maxOrder = (SELECT ISNULL(MAX([order]),0) from fee_categories)
		SET @maxOrder += 1
		IF(exists (select 1 from fee_categories where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			BEGIN
				UPDATE fee_categories
					SET
						[id_online] =  obj.id_online,
						[school_id] =  obj.school_id,
						[name] =  obj.name,
						[recur] =  obj.recur,
						[is_transport] =  obj.is_transport,
						[order] =  obj.[order],
						[updated_by] =  obj.updated_by,
						[updated_on] =  obj.updated_on
					FROM
						@Model AS obj
					WHERE
						fee_categories.id_offline = obj.id_offline
			 End
		 ELSE
			 BEGIN
				INSERT INTO [dbo].[fee_categories]
					  (	[id_offline],
						[id_online],
						[school_id],
						[name],
						[recur],
						[is_transport],
						[order],
						[created_by],
						[created_on],
						[updated_by],
						[updated_on])
				 SELECT	
						obj.id_offline,
						obj.id_online,
						obj.school_id,
						obj.name,
						obj.recur,
						obj.is_transport,
						@maxOrder,
						obj.created_by,
						obj.created_on,
						obj.updated_by,
						obj.updated_on
				FROM
					 @Model As obj				
			END
COMMIT














GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyGradeFees]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify GradeFees>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyGradeFees]  @Model grade_fees READONLY
AS
BEGIN TRAN		
		IF(exists (select 1 from grade_fees where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			UPDATE grade_fees
				SET
					[id_offline] =  obj.id_offline,
					[id_online] =  obj.id_online,
					[school_id] =  obj.school_id,
					[fees_id] =  obj.fees_id,
					[grade_id] =  obj.grade_id,
					[updated_by] =  obj.updated_by,
					[updated_on] =  obj.updated_on
				FROM
					@Model AS obj
				WHERE
					grade_fees.id_offline = obj.id_offline
		 ELSE
			INSERT INTO [dbo].[grade_fees]
			   (
					[id_offline],
					[id_online],
					[school_id],
					[fees_id],
					[grade_id],
					[created_by],
					[created_on],
					[updated_by],
					[updated_on])
			 SELECT	
					obj.id_offline,
					obj.id_online,
					obj.school_id,
					obj.fees_id,
					obj.grade_id,
					obj.created_by,
					obj.created_on,
					obj.updated_by,
					obj.updated_on
			  FROM
				 @Model As obj
COMMIT








GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyGrades]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify Grades>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyGrades]  @Model grades READONLY
AS
DECLARE @maxOrder INT
BEGIN TRAN		
		SET @maxOrder = (SELECT ISNULL(MAX([order]),0) from grades)
		SET @maxOrder += 1
		IF(exists (select 1 from grades where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			UPDATE grades
				SET
					[school_id] = obj.school_id,
					[block] = obj.block,
					[name] = obj.name,
					[updated_by] = obj.updated_by,
					[updated_on] = obj.updated_on
				FROM
					@Model AS obj
				WHERE
					grades.id_offline = obj.id_offline
		 ELSE
			INSERT INTO [dbo].[grades]
			   (
				[id_offline]
			   ,[id_online]
			   ,[school_id]
			   ,[block]
			   ,[name]
			   ,[order]
			   ,[created_by]
			   ,[created_on]
			   ,[updated_by]
			   ,[updated_on])
			 SELECT	
					obj.id_offline,
					obj.id_online,				
					obj.school_id,
					obj.block,
					obj.name,
					@maxOrder,
					obj.created_by,
					obj.created_on,
					obj.updated_by,
					obj.updated_on
			FROM
				 @Model As obj
COMMIT








GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyParents]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify Parents>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyParents]  @Model parents READONLY
AS
DECLARE @maxOrder INT
BEGIN TRAN	
		IF(exists (select 1 from parents where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			BEGIN
				UPDATE parents
					SET
						[id_offline] = obj.id_offline,
						[id_online] = obj.id_online,
						[school_id] = obj.school_id,
						[f_first_name] = obj.f_first_name,
						[f_middle_name] = obj.f_middle_name,
						[f_last_name] = obj.f_last_name,
						[f_full_name] = obj.f_full_name,
						[f_mobile] = obj.f_mobile,
						[f_phone] = obj.f_phone,
						[f_office] = obj.f_office,
						[f_email] = obj.f_email,
						[m_first_name] = obj.m_first_name,
						[m_middle_name] = obj.m_middle_name,
						[m_last_name] = obj.m_last_name,
						[m_full_name] = obj.m_full_name,
						[m_mobile] = obj.m_mobile,
						[m_phone] = obj.m_phone,
						[m_office] = obj.m_office,
						[m_email] = obj.m_email,
						[g_fullname] = obj.g_fullname,
						[g_mobile] = obj.g_mobile,
						[g_email] = obj.g_email,
						[created_by] = obj.created_by,
						[created_on] = obj.created_on,
						[updated_by] = obj.updated_by,
						[updated_on] = obj.updated_on
					FROM
						@Model AS obj
					WHERE
						parents.id_offline = obj.id_offline
			 End
		 ELSE
			 BEGIN
				INSERT INTO [dbo].[parents]
					  (	[id_offline],
						[id_online],
						[school_id],
						[f_first_name],
						[f_middle_name],
						[f_last_name],
						[f_full_name],
						[f_mobile],
						[f_phone],
						[f_office],
						[f_email],
						[m_first_name],
						[m_middle_name],
						[m_last_name],
						[m_full_name],
						[m_mobile],
						[m_phone],
						[m_office],
						[m_email],
						[g_fullname],
						[g_mobile],
						[g_email],
						[created_by],
						[created_on],
						[updated_by],
						[updated_on])
				 SELECT	
						obj.id_offline,
						obj.id_online,
						obj.school_id,
						obj.f_first_name,
						obj.f_middle_name,
						obj.f_last_name,
						obj.f_full_name,
						obj.f_mobile,
						obj.f_phone,
						obj.f_office,
						obj.f_email,
						obj.m_first_name,
						obj.m_middle_name,
						obj.m_last_name,
						obj.m_full_name,
						obj.m_mobile,
						obj.m_phone,
						obj.m_office,
						obj.m_email,
						obj.g_fullname,
						obj.g_mobile,
						obj.g_email,
						obj.created_by,
						obj.created_on,
						obj.updated_by,
						obj.updated_on
				FROM
					 @Model As obj				
			END
COMMIT














GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifySections]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify Sections>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifySections]  @Model sections READONLY
AS
BEGIN TRAN		
		IF(exists (select 1 from sections where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			UPDATE sections
				SET
					[school_id] = obj.school_id,
					[name] = obj.name,
					[capacity] = obj.capacity,
					[updated_by] = obj.updated_by,
					[updated_on] = obj.updated_on
				FROM
					@Model AS obj
				WHERE
					sections.id_offline = obj.id_offline
		 ELSE
			INSERT INTO [dbo].[sections]
				   ([id_offline]
				   ,[id_online]
				   ,[school_id]
				   ,[name]
				   ,[capacity]
				   ,[created_by]
				   ,[created_on]
				   ,[updated_by]
				   ,[updated_on])
			 SELECT	
					obj.id_offline,
					obj.id_online,				
					obj.school_id,
					obj.name,
					obj.capacity,
					obj.created_by,
					obj.created_on,
					obj.updated_by,
					obj.updated_on
			FROM
				 @Model As obj
COMMIT









GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifySessions]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify Sessions>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifySessions]  @Model sessions READONLY
AS
DECLARE @maxOrder INT
BEGIN TRAN	
		SET @maxOrder = (SELECT ISNULL(MAX([order]),0) from sessions)
		SET @maxOrder += 1	
		IF(exists (select 1 from sessions where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			BEGIN
				UPDATE Sessions
					SET
						[school_id] = obj.school_id,
						[name] = obj.name,
						[from_date] = obj.from_date,
						[to_date] = obj.to_date,
						[is_active] = obj.is_active,
						[updated_by] = obj.updated_by,
						[updated_on] = obj.updated_on
					FROM
						@Model AS obj
					WHERE
						Sessions.id_offline = obj.id_offline
				IF((SELECT obj.is_active FROM @Model As obj) = 'true')
				BEGIN
					UPDATE sessions set is_active = 'false' where id_offline != (SELECT obj.id_offline FROM @Model As obj)		
				END
			 End
		 ELSE
			 BEGIN
				INSERT INTO [dbo].[Sessions]
					  ([id_offline]
					  ,[id_online]
					  ,[school_id]
					  ,[name]
					  ,[order]
					  ,[from_date]
					  ,[to_date]
					  ,[is_active]
					  ,[created_by]
					  ,[created_on]
					  ,[updated_by]
					  ,[updated_on])
				 SELECT	
						obj.id_offline,
						obj.id_online,				
						obj.school_id,
						obj.name,
						@maxOrder,
						obj.from_date,
						obj.to_date,
						obj.is_active,
						obj.created_by,
						obj.created_on,
						obj.updated_by,
						obj.updated_on
				FROM
					 @Model As obj
				IF((SELECT obj.is_active FROM @Model As obj) = 'true')
				BEGIN
					UPDATE sessions set is_active = 'false' where id_offline != (SELECT obj.id_offline FROM @Model As obj)		
				END
			END
COMMIT











GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyStudent_grade_session_log]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify Student_grade_session_log>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyStudent_grade_session_log]  @Model Student_grade_session_log READONLY
AS
BEGIN TRAN	
		IF(exists (select 1 from Student_grade_session_log where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			BEGIN
				UPDATE student_grade_session_log
					SET
						[id_offline] = obj.id_offline,
						[id_online] = obj.id_online,
						[school_id] = obj.school_id,
						[student_id] = obj.student_id,
						[registration_id] = obj.registration_id,
						[grade_id] = obj.grade_id,
						[section_id] = obj.section_id,
						[roll_number] = obj.roll_number,
						[exam_roll_number] = obj.exam_roll_number,
						[session_id] = obj.session_id,
						[sgsl_status] = obj.sgsl_status,
						[updated_by] = obj.updated_by,
						[updated_on] = obj.updated_on
					FROM
						@Model AS obj
					WHERE
						student_grade_session_log.id_offline = obj.id_offline
			 End
		 ELSE
			 BEGIN
				INSERT INTO [dbo].[student_grade_session_log]
					  (	[id_offline],
						[id_online],
						[school_id],
						[student_id],
						[registration_id],
						[grade_id],
						[section_id],
						[roll_number],
						[exam_roll_number],
						[session_id],
						[sgsl_status],
						[created_by],
						[created_on],
						[updated_by],
						[updated_on])
				 SELECT	
						obj.id_offline,
						obj.id_online,
						obj.school_id,
						obj.student_id,
						obj.registration_id,
						obj.grade_id,
						obj.section_id,
						obj.roll_number,
						obj.exam_roll_number,
						obj.session_id,
						obj.sgsl_status,
						obj.created_by,
						obj.created_on,
						obj.updated_by,
						obj.updated_on
				FROM
					 @Model As obj				
			END
COMMIT















GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyStudentFees]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify StudentFees>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyStudentFees]  @Model Student_fees READONLY
AS
BEGIN TRAN		
		IF(exists (select 1 from Student_fees where id_offline=(SELECT obj.id_offline FROM @Model As obj))
		   OR exists(Select 1 from student_fees where grade_fees_id = (SELECT obj.grade_fees_id FROM @Model As obj)
						AND student_id = (SELECT obj.student_id FROM @Model As obj)
						AND apply_from =  (SELECT obj.apply_from FROM @Model As obj)
			  )
		)
			UPDATE Student_fees
				SET
					[id_online] =  obj.id_online,
					[school_id] =  obj.school_id,
					[grade_fees_id] =  obj.grade_fees_id,
					[student_id] =  obj.student_id,
					[route_vehicle_stops_fee_log_id] =  obj.route_vehicle_stops_fee_log_id,
					[apply_from] =  obj.apply_from,
					[apply_to] =  obj.apply_to,
					[fine] =  obj.fine,
					[concession_amount] =  obj.concession_amount,
					[no_fine] =  obj.no_fine,
					[updated_by] =  obj.updated_by,
					[updated_on] =  obj.updated_on
				FROM
					@Model AS obj
				WHERE
					Student_fees.id_offline = obj.id_offline
					OR (Student_fees.grade_fees_id = (SELECT obj.grade_fees_id FROM @Model As obj)
						AND Student_fees.student_id = (SELECT obj.student_id FROM @Model As obj)
						AND Student_fees.apply_from =  (SELECT obj.apply_from FROM @Model As obj))
		 ELSE
			INSERT INTO [dbo].[Student_fees]
			   (
					[id_offline],
					[id_online],
					[school_id],
					[grade_fees_id],
					[student_id],
					[route_vehicle_stops_fee_log_id],
					[apply_from],
					[apply_to],
					[fine],
					[concession_amount],
					[no_fine],
					[created_by],
					[created_on],
					[updated_by],
					[updated_on])
			 SELECT	
					obj.id_offline,
					obj.id_online,
					obj.school_id,
					obj.grade_fees_id,
					obj.student_id,
					obj.route_vehicle_stops_fee_log_id,
					obj.apply_from,
					obj.apply_to,
					obj.fine,
					obj.concession_amount,
					obj.no_fine,
					obj.created_by,
					obj.created_on,
					obj.updated_by,
					obj.updated_on
			  FROM
				 @Model As obj
COMMIT








GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyStudents]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create or Modify Students>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyStudents]  @StudentModel students READONLY, @UserModel users READONLY, @ParentModel parents READONLY, @Student_grade_session_logModel Student_grade_session_log READONLY
AS
BEGIN TRAN
		IF(exists (select 1 from students where id_offline=(SELECT obj.id_offline FROM @StudentModel As obj)))
			BEGIN
				UPDATE students
					SET
						[school_id] = obj.school_id,
						[user_id] = obj.user_id,
						[parent_id] = obj.parent_id,
						[trip_stop_id] = obj.trip_stop_id,
						[enrollment_date] = obj.enrollment_date,
						[status] = obj.status,
						[dc_number] = obj.dc_number,
						[dc_date_of_issue] = obj.dc_date_of_issue,
						[created_by] = obj.created_by,
						[created_on] = obj.created_on,
						[updated_by] = obj.updated_by,
						[updated_on] = obj.updated_on
					FROM
						@StudentModel AS obj
					WHERE
						students.id_offline = obj.id_offline
					
					-- User
					EXEC [dbo].[CreateOrModifyUsers] @Model = @UserModel;

					-- Parent
					EXEC [dbo].[CreateOrModifyParents] @Model = @ParentModel;
					
					-- Student_grade_session_log	
					EXEC [dbo].[CreateOrModifyStudent_grade_session_log] @Model = @Student_grade_session_logModel;			
			End
		 ELSE
			BEGIN
				INSERT INTO [dbo].[students]
				   ([id_offline]
				   ,[id_online]
				   ,[school_id]
				   ,[user_id]
				   ,[parent_id]
				   ,[trip_stop_id]
				   ,[enrollment_date]
				   ,[status]
				   ,[dc_number]
				   ,[dc_date_of_issue]
				   ,[created_by]
				   ,[created_on]
				   ,[updated_by]
				   ,[updated_on])
				 SELECT
						obj.id_offline,
						obj.id_online,
						obj.school_id,
						obj.user_id,
						obj.parent_id,
						obj.trip_stop_id,
						obj.enrollment_date,
						obj.status,
						obj.dc_number,
						obj.dc_date_of_issue,
						obj.created_by,
						obj.created_on,
						obj.updated_by,
						obj.updated_on
				  FROM
					 @StudentModel As obj

				-- User
				EXEC [dbo].[CreateOrModifyUsers] @Model = @UserModel;

				UPDATE students 
					set user_id = (SELECT obj.id_offline FROM @UserModel As obj), 
					parent_id = (SELECT obj.id_offline FROM @ParentModel As obj) 
				WHERE id_offline = (SELECT obj.id_offline FROM @StudentModel As obj);

				-- Parent
				EXEC [dbo].[CreateOrModifyParents] @Model = @ParentModel;

				-- Student_grade_session_log	
				EXEC [dbo].[CreateOrModifyStudent_grade_session_log] @Model = @Student_grade_session_logModel;
			END
			
COMMIT

















GO
/****** Object:  StoredProcedure [dbo].[CreateOrModifyUsers]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Create Or Modify users>
-- =============================================
CREATE PROCEDURE [dbo].[CreateOrModifyUsers]  @Model users READONLY
AS
DECLARE @maxOrder INT
BEGIN TRAN	
		IF(exists (select 1 from users where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			BEGIN
				UPDATE users
					SET
						[id_online] = obj.id_online,
						[school_id] = obj.school_id,
						[student_id] = obj.student_id,
						[role_id] = obj.role_id,
						[user_type] = obj.user_type,
						[username] = obj.username,
						[email] = obj.email,
						[phone] = obj.phone,
						[address_line_one] = obj.address_line_one,
						[address_line_two] = obj.address_line_two,
						[area] = obj.area,
						[first_name] = obj.first_name,
						[middle_name] = obj.middle_name,
						[last_name] = obj.last_name,
						[full_name] = obj.full_name,
						[password] = obj.password,
						[birth_date] = obj.birth_date,
						[other_phones] = obj.other_phones,
						[default_phone_number_id] = obj.default_phone_number_id,
						[adhaar_number] = obj.adhaar_number,
						[bank_name] = obj.bank_name,
						[gender] = obj.gender,
						[blood_group] = obj.blood_group,
						[bank_branch] = obj.bank_branch,
						[bank_account_number] = obj.bank_account_number,
						[bank_ifsc_code] = obj.bank_ifsc_code,
						[flags] = obj.flags,
						[last_login_time] = obj.last_login_time,
						[user_avatar_file_id] = obj.user_avatar_file_id,
						[is_active] = obj.is_active,
						[updated_on] = obj.updated_on,
						[updated_by] = obj.updated_by
					FROM
						@Model AS obj
					WHERE
						users.id_offline = obj.id_offline
			 End
		 ELSE
			 BEGIN
				INSERT INTO [dbo].[users]
					  ([id_offline],
						[id_online],
						[school_id],
						[student_id],
						[role_id],
						[user_type],
						[username],
						[email],
						[phone],
						[address_line_one],
						[address_line_two],
						[area],
						[first_name],
						[middle_name],
						[last_name],
						[full_name],
						[gender],
						[blood_group],
						[password],
						[birth_date],
						[other_phones],
						[default_phone_number_id],
						[adhaar_number],
						[bank_name],
						[bank_branch],
						[bank_account_number],
						[bank_ifsc_code],
						[flags],
						[last_login_time],
						[user_avatar_file_id],
						[is_active],
						[created_on],
						[created_by],
						[updated_on],
						[updated_by])
				 SELECT	
						obj.id_offline,
						obj.id_online,
						obj.school_id,
						obj.student_id,
						obj.role_id,
						obj.user_type,
						obj.username,
						obj.email,
						obj.phone,
						obj.address_line_one,
						obj.address_line_two,
						obj.area,
						obj.first_name,
						obj.middle_name,
						obj.last_name,
						obj.full_name,
						obj.gender,
						obj.blood_group,
						obj.password,
						obj.birth_date,
						obj.other_phones,
						obj.default_phone_number_id,
						obj.adhaar_number,
						obj.bank_name,
						obj.bank_branch,
						obj.bank_account_number,
						obj.bank_ifsc_code,
						obj.flags,
						obj.last_login_time,
						obj.user_avatar_file_id,
						obj.is_active,
						obj.created_on,
						obj.created_by,
						obj.updated_on,
						obj.updated_by
				FROM
					 @Model As obj				
			END
COMMIT













GO
/****** Object:  StoredProcedure [dbo].[DeleteRecord]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Delete Record>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRecord] @key nvarchar(200),@value nvarchar(200), @tableName nvarchar(150)
AS
DECLARE @sqlCommand varchar(200)
BEGIN TRAN
	SET @sqlCommand = 'DELETE from ' + @tableName +' WHERE  '+ @key +'='+  @value 
	EXEC (@sqlCommand)
COMMIT













GO
/****** Object:  StoredProcedure [dbo].[GetCurrentSession]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Get Current Session>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetCurrentSession]
AS
	SELECT	
	   s.[id_offline]
      ,s.[id_online]
      ,s.[school_id]
      ,s.[name]
      ,s.[order]
      ,s.[from_date]
      ,s.[to_date]
      ,s.[is_active]
      ,s.[created_by]
      ,s.[created_on]
      ,s.[updated_by]
      ,s.[updated_on]
	  ,u.[full_name] as CreatedBy
	FROM sessions s
		LEFT JOIN users AS u ON u.id_offline = s.created_by
	WHERE s.is_active = 'true'








GO
/****** Object:  StoredProcedure [dbo].[GetDashboardGeneralInfoWidgetDetails]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetGradesList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetDashboardGeneralInfoWidgetDetails]
AS
	SELECT  (
			SELECT COUNT(id_offline) AS StudentCount FROM Students WHERE status = 'active' 
			) AS StudentCount,
			(
			SELECT SUM(amount) from student_payments WHERE CONVERT (date, payment_date)  = CONVERT (date, GETDATE()) 
			) AS TodaysRevenue


















GO
/****** Object:  StoredProcedure [dbo].[GetFeeAllocationList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetFeeAllocationList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetFeeAllocationList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200)
AS
WITH FeeAllocationListTable AS 
        ( 
			SELECT DISTINCT
				ROW_NUMBER() OVER(order by MAX([name])) AS 'RowNumber',  
				--fees
				MAX(fees.id_offline) as [fees.id_offline],
				MAX(fees.id_online) as [fees.id_online],
				MAX(fees.school_id) as [fees.school_id],
				MAX(fees.fee_category_id) as [fees.fee_category_id],
				MAX(fees.session_id) as [fees.session_id],
				MAX(fees.amount) as [fees.amount],
				MAX(fees.fee_cources) as [fees.fee_cources],
				MAX(fees.last_date) as [fees.last_date],
				MAX(fees.last_day) as [fees.last_day],
				MAX(fees.fine_per_day) as [fees.fine_per_day],
				MAX(fees.is_allocated) as [fees.is_allocated],
				MAX(fees.remarks) as [fees.remarks],
				MAX(fees.created_by) as [fees.created_by],
				MAX(fees.created_on) as [fees.created_on],
				MAX(fees.updated_by) as [fees.updated_by],
				MAX(fees.updated_on) as [fees.updated_on],
				--fee_categories
				MAX(fee_categories.id_offline) as [fee_categories.id_offline],
				MAX(fee_categories.id_online) as [fee_categories.id_online],
				MAX(fee_categories.school_id) as [fee_categories.school_id],
				MAX(fee_categories.name) as [fee_categories.name],
				MAX(fee_categories.recur) as [fee_categories.recur],
				MAX(fee_categories.is_transport) as [fee_categories.is_transport],
				MAX(fee_categories.[order]) as [fee_categories.order],
				MAX(fee_categories.created_by) as [fee_categories.created_by],
				MAX(fee_categories.created_on) as [fee_categories.created_on],
				MAX(fee_categories.updated_by) as [fee_categories.updated_by],
				MAX(fee_categories.updated_on) as [fee_categories.updated_on],
				STUFF((SELECT ', ' + CAST(grades.name AS varchar(MAX))
						 FROM grade_fees grade_fees
						 INNER JOIN grades on grade_fees.grade_id = grades.id_offline
						 where MAX(fees.id_offline) = grade_fees.fees_id order by grades.[order]
							FOR XML PATH(''), TYPE
							).value('.', 'varchar(MAX)') 
						,1,2,'') GradesAppliedTo,
				STUFF((SELECT ', ' + CAST(grades.id_offline AS varchar(MAX))
						 FROM grade_fees grade_fees
						 INNER JOIN grades on grade_fees.grade_id = grades.id_offline
						 where MAX(fees.id_offline) = grade_fees.fees_id
							FOR XML PATH(''), TYPE
							).value('.', 'varchar(MAX)') 
						,1,2,'') AppliedToGradeIDs,
				(SELECT 
						COUNT(Distinct grade_fees_id) 
				 FROM grade_fees
						 INNER JOIN student_fees on student_fees.grade_fees_id = grade_fees.id_offline
				 WHERE MAX(fees.id_offline) = grade_fees.fees_id
				 ) AS StudentCount,
				 MAX(users.full_name) AS CreatedBy
				FROM fees
				INNER JOIN fee_categories on fees.fee_category_id = fee_categories.id_offline
				INNER JOIN users on fees.created_by = users.id_offline
				GROUP BY
					fees.id_offline
		) 
		SELECT 
			*
			FROM FeeAllocationListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo












GO
/****** Object:  StoredProcedure [dbo].[GetFeeCategoriesList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetFeeCategoriesList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetFeeCategoriesList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200)
AS
WITH FeeCategoriesListTable AS 
        ( 
           SELECT
			    ROW_NUMBER() OVER(order by MAX([order])) AS 'RowNumber',  
				MAX(fee_categories.id_offline) as id_offline,
				MAX(fee_categories.id_online) as id_online,
				MAX(fee_categories.school_id) as school_id,
				MAX(fee_categories.name) as name,
				MAX(fee_categories.recur) as recur,
				MAX(fee_categories.is_transport) as is_transport,
				MAX(fee_categories.[order]) as [order],
				MAX(fee_categories.created_by) as created_by,
				MAX(fee_categories.created_on) as created_on,
				MAX(fee_categories.updated_by) as updated_by,
				MAX(fee_categories.updated_on) as updated_on,
				MAX(users.full_name) as CreatedBy
			FROM 
				fee_categories
			LEFT JOIN Users ON users.id_offline = fee_categories.created_by
			GROUP BY
			fee_categories.id_offline
		) 
		SELECT 
			*
			FROM FeeCategoriesListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo












GO
/****** Object:  StoredProcedure [dbo].[GetGradeFeesList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetGradeFeesList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetGradeFeesList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200)
AS
WITH GradeFeesListTable AS 
        ( 
           SELECT
			    ROW_NUMBER() OVER(order by MAX([grade_id])) AS 'RowNumber',  
			   	MAX(grade_fees.id_offline) as [grade_fees.id_offline],
				MAX(grade_fees.id_online) as [grade_fees.id_online],
				MAX(grade_fees.school_id) as [grade_fees.school_id],
				MAX(grade_fees.fees_id) as [grade_fees.fees_id],
				MAX(grade_fees.grade_id) as [grade_fees.grade_id],
				MAX(grade_fees.created_by) as [grade_fees.created_by],
				MAX(grade_fees.created_on) as [grade_fees.created_on],
				MAX(grade_fees.updated_by) as [grade_fees.updated_by],
				MAX(grade_fees.updated_on) as [grade_fees.updated_on]
			FROM 
				grade_fees
			LEFT JOIN users AS u ON u.id_offline = grade_fees.created_by
			GROUP BY
			grade_fees.id_offline
		)
		SELECT 
			*
			FROM GradeFeesListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo





















GO
/****** Object:  StoredProcedure [dbo].[GetGradesList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetGradesList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetGradesList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200)
AS
WITH GradesListTable AS 
        ( 
           SELECT
			   ROW_NUMBER() OVER(order by MAX([order])) AS 'RowNumber',  
			   MAX(g.id_offline) as id_offline,
			   MAX(g.id_online) as id_online,
			   MAX(g.school_id) as school_id,
			   MAX(g.block) as block,
			   MAX(g.name) as name,
			   MAX(g.[order]) as [order],
			   MAX(g.created_by) as created_by,
			   Max(g.created_on) as created_on,
			   Max(g.updated_by) as updated_by,
			   Max(g.updated_on) as updated_on,
			   Max(u.full_name) as createdBy
			FROM 
				grades AS g
			LEFT JOIN users AS u ON u.id_offline = g.created_by
			GROUP BY
			g.id_offline
		) 
		SELECT 
			*
			FROM GradesListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo





















GO
/****** Object:  StoredProcedure [dbo].[GetList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Get List>
-- =============================================
CREATE PROCEDURE [dbo].[GetList] @tableName nvarchar(100), @keyColumn nvarchar(100), @valueColumn nvarchar(100),@orderBy nvarchar(100)
AS
EXEC('select '+ @keyColumn +','+ @valueColumn +' from ' + @tableName +' order by ' + @orderBy)














GO
/****** Object:  StoredProcedure [dbo].[GetSchoolInfo]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Get School Info>
-- =============================================

Create PROCEDURE [dbo].[GetSchoolInfo]
AS
BEGIN
	Select top 1 * from Schools
END













GO
/****** Object:  StoredProcedure [dbo].[GetSectionsList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetSectionsList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetSectionsList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200)
AS
WITH SectionsListTable AS 
        ( 
           SELECT
			   ROW_NUMBER() OVER(order by MAX([name])) AS 'RowNumber',  
			   MAX(s.id_offline) as id_offline,
			   MAX(s.id_online) as id_online,
			   MAX(s.school_id) as school_id,
			   MAX(s.capacity) as capacity,
			   MAX(s.name) as name,		
			   MAX(s.created_by) as created_by,
			   Max(s.created_on) as created_on,
			   Max(s.updated_by) as updated_by,
			   Max(s.updated_on) as updated_on,
			   Max(u.full_name) as createdBy
			FROM 
				sections AS s
			LEFT JOIN users AS u ON u.id_offline = s.created_by
			GROUP BY
			s.id_offline
		) 
		SELECT 
			*
			FROM SectionsListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo








GO
/****** Object:  StoredProcedure [dbo].[GetSessionsList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetSessionsList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetSessionsList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200)
AS
WITH SessionsListTable AS 
        ( 
           SELECT
			   ROW_NUMBER() OVER(order by MAX([order])) AS 'RowNumber',  
			   MAX(s.id_offline) as id_offline,
			   MAX(s.id_online) as id_online,
			   MAX(s.school_id) as school_id,
			   MAX(s.name) as name,	
			   MAX(s.[order]) as [order],	
			   MAX(s.from_date) as from_date,
			   MAX(s.to_date) as to_date,	
			   MAX(s.is_active) as is_active,
			   MAX(s.created_by) as created_by,
			   Max(s.created_on) as created_on,
			   Max(s.updated_by) as updated_by,
			   Max(s.updated_on) as updated_on,
			   Max(u.full_name) as createdBy
			FROM 
				sessions AS s
			LEFT JOIN users AS u ON u.id_offline = s.created_by
			GROUP BY
			s.id_offline
		) 
		SELECT 
			*
			FROM SessionsListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo









GO
/****** Object:  StoredProcedure [dbo].[GetSettings]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Get Settings>
-- =============================================
CREATE PROCEDURE [dbo].[GetSettings]  @key nvarchar(100)
AS 
	SELECT * FROM Settings where [key] = @key













GO
/****** Object:  StoredProcedure [dbo].[GetStudentBalances]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetStudentBalances>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentBalances]  @StudentID nvarchar(200) = '%%', @receptNo nvarchar(200) = '%%',@studentFeeIds nvarchar(200) = '%%',@feesCategorieIds nvarchar(200) = '%%',@applyFrom nvarchar(200) = '01/01/2000',@applyTo nvarchar(200) = '01/01/3000'
AS
SELECT 
		MAX(sf.id_offline) as id_offline,
		MAX(sf.apply_from) as apply_from,
		MAX(f.last_day) as last_day,
		MAX(f.fine_per_day) as fine_per_day,
		MAX(fc.name) AS fees_category,
		MAX(sf.student_id) as student_id,
		MAX(f.id_offline) AS fees_id,
		MAX(f.amount) AS fee_amount,
		MAX(sp.payment_mode) as payment_mode,
		MAX(sp.payment_date) as payment_date,
		MAX(sp.recept_no) as recept_no,
		MAX(sp.comment) as comment,
		MAX(sp.amount) AS payment_amount,
		MAX(sp.fine) AS payment_fine,
		MAX(sf.fine) as fine,
		--MAX(sf.concession_amount) as concession_amount,
		CASE WHEN MAX(sf.concession_amount) IS NULL THEN 0 ELSE MAX(sf.concession_amount) END AS concession_amount,
		CASE WHEN MAX(f.amount) IS NULL THEN 0 ELSE MAX(f.amount) END - CASE WHEN SUM(sp.amount) IS NULL THEN 0 ELSE MAX(sp.amount) END AS amount_to_pay,
		CASE WHEN MAX(sf.fine) IS NULL THEN 0 ELSE MAX(sf.fine) END AS fine_to_pay,
		SUM(sp.amount) AS paid_amount,
		CASE WHEN MAX(sp.fine) IS NULL THEN 0 ELSE MAX(sp.fine) END AS fine_paid,
		(CASE WHEN MAX(f.amount) IS NULL THEN 0 ELSE MAX(f.amount) END + CASE WHEN MAX(sf.fine) IS NULL THEN 0 ELSE MAX(sf.fine) END) - (CASE WHEN SUM(sp.amount) IS NULL THEN 0 ELSE MAX(sp.amount) END + CASE WHEN SUM(sp.fine) IS NULL THEN 0 ELSE MAX(sp.fine) END) - CASE WHEN MAX(sf.concession_amount) IS NULL THEN 0 ELSE MAX(sf.concession_amount) END AS balance_amount,				
		CASE WHEN FORMAT(MAX(sf.apply_from),'MMM yyyy') <> FORMAT(MAX(sf.apply_to),'MMM yyyy') THEN  CONCAT(FORMAT(MAX(sf.apply_from),'MMM yyyy'), ' To ' , FORMAT(MAX(sf.apply_to),'MMM yyyy')) ELSE  FORMAT(MAX(sf.apply_from),'MMM yyyy') END AS period
	FROM 
		student_fees AS sf
	LEFT JOIN grade_fees AS gf ON gf.id_offline = sf.grade_fees_id
	LEFT JOIN route_vehicle_stops_fee_logs AS rvsfl ON rvsfl.id_offline = sf.route_vehicle_stops_fee_log_id
	LEFT JOIN fees AS f ON f.id_offline = gf.fees_id OR f.id_offline = rvsfl.fees_id
	LEFT JOIN student_payments AS sp ON sf.id_offline = sp.student_fees_id
	LEFT JOIN fee_categories AS fc ON fc.id_offline = f.fee_category_id
	WHERE
		ISNULL(sf.student_id,'00000000-0000-0000-0000-000000000000') like @studentId
	AND
		ISNULL(sp.recept_no,'') like @receptNo
	AND
		ISNULL(sf.id_offline,'00000000-0000-0000-0000-000000000000') like @studentFeeIds
	AND
		ISNULL(fc.id_offline,'00000000-0000-0000-0000-000000000000') like @feesCategorieIds
	AND
		sf.apply_from Between @applyFrom AND @applyTo
	GROUP BY 
		sf.id_offline
	HAVING
		(CASE WHEN MAX(f.amount) IS NULL THEN 0 ELSE MAX(f.amount) END + CASE WHEN MAX(sf.fine) IS NULL THEN 0 ELSE MAX(sf.fine) END - CASE WHEN MAX(sf.concession_amount) IS NULL THEN 0 ELSE MAX(sf.concession_amount) END) - (CASE WHEN SUM(sp.amount) IS NULL THEN 1 ELSE SUM(sp.amount) END + CASE WHEN SUM(sp.fine) IS NULL THEN 0 ELSE SUM(sp.fine) END) > 0
	ORDER BY  
		--MAX(fc.[order]) ASC, 
		MAX(sf.apply_from) ASC;
		













GO
/****** Object:  StoredProcedure [dbo].[GetStudentFeeAllocatedList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetStudentFeeAllocatedList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentFeeAllocatedList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200), @SectionID  nvarchar(200) = '%%', @GradeID  nvarchar(200) = '%%',@RollNumber nvarchar(50) = '%%',@RegistrationID  nvarchar(200) = '%%',@ConcessionAmount  nvarchar(200) = '%%'
AS
WITH StudentFeeAllocationTable AS 
        ( 
           SELECT 
            ROW_NUMBER() OVER(order by MAX(full_name)) AS 'RowNumber', 
			MAX(s.id_offline) as id_offline,
			MAX(s.school_id) as school_id,
			MAX(sgsl.registration_id) as registration_id,
			MAX(sgsl.sgsl_status) AS status,
			MAX(uaf.file_id) AS file_id,
			MAX(sgsl.session_id) as session_id,
			MAX(f.id_offline) AS fees_id,
			MAX(sf.grade_fees_id) as grade_fees_id,
			MAX(sf.apply_from) as apply_from,
			MAX(sf.apply_to) as apply_to,
			MAX(sf.concession_amount) as concession_amount,
			MAX(sf.fine) as fine,
			MAX(sgsl.grade_id) as grade_id,
			MAX(sgsl.section_id) as section_id,
			MAX(u.full_name) as full_name,
			MAX(sgsl.roll_number) as roll_number,
			MAX(g.name) AS grade_name,
			MAX(sc.name) AS section_name,
			MAX(g.[order]) as [order],
			CONCAT(MAX(g.name), ' - ', MAX(sc.name)) AS grade_section,
			COUNT(sf.id_offline) AS allocated_fee_cource_count,
			--CONCAT('<abbr title="Fathers Name">F:</abbr> ',MAX(p.f_first_name), ' ', MAX(p.f_middle_name), ' ', MAX(p.f_last_name),'<br> <abbr title="Mothers Name">M:</abbr> ',MAX(p.m_first_name), ' ', MAX(p.m_middle_name), ' ', MAX(p.m_last_name)) AS parentage
			CONCAT('F: ',MAX(p.f_first_name), ' ', MAX(p.f_middle_name), ' ', MAX(p.f_last_name),' - M: ',MAX(p.m_first_name), ' ', MAX(p.m_middle_name), ' ', MAX(p.m_last_name)) AS parentage
		FROM 
			students AS s
		LEFT JOIN users AS u ON u.id_offline = s.user_id
		LEFT JOIN student_fees AS sf ON s.id_offline = sf.student_id
		LEFT JOIN grade_fees AS gf ON gf.id_offline = sf.grade_fees_id
		LEFT JOIN fees AS f ON f.id_offline = gf.fees_id
		LEFT JOIN student_grade_session_log AS sgsl ON s.id_offline = sgsl.student_id
		LEFT JOIN grades AS g ON sgsl.grade_id = g.id_offline
		LEFT JOIN sections AS sc ON sc.id_offline = sgsl.section_id
		LEFT JOIN user_avatar_files uaf ON uaf.id_offline = u.user_avatar_file_id
		LEFT JOIN parents AS p ON p.id_offline = s.parent_id
		GROUP BY
			s.id_offline,sgsl.session_id
			having MAX(sc.id_offline) like @SectionID 
			AND MAX(g.id_offline) like @GradeID 
			AND MAX(sgsl.roll_number) like @RollNumber
			AND MAX(sgsl.registration_id) like @RegistrationID
			AND MAX(sf.concession_amount) like @ConcessionAmount
				) 
				SELECT 
					*
				FROM StudentFeeAllocationTable 
				WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo


















GO
/****** Object:  StoredProcedure [dbo].[GetStudentFeeDue]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetStudentFeeDue>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentFeeDue] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200), @StudentID nvarchar(200) = '%%', @FeeCategoryID nvarchar(200) = '%%'
AS
WITH StudentFeeDue AS 
        ( 
           SELECT
				ROW_NUMBER() OVER(order by sp.updated_on DESC) AS 'RowNumber', 
				sf.id_offline,
				s.school_id,
				uaf.file_id AS file_id,
				sgsl.session_id,
				CASE WHEN f.id_offline IS NULL THEN 0 ELSE NULL END AS fees_id,
				s.id_offline AS student_id,
				sf.grade_fees_id,
				f.last_day,
				f.amount,	
				rvsfl.route_vehicle_stop_id,
				sf.apply_from,
				sf.apply_to,
				sf.concession_amount,
				ROUND(100/f.amount*sf.concession_amount,2) AS concession_percentage,
				sf.fine,
				sgsl.grade_id,
				sgsl.section_id,
				sgsl.registration_id,
				u.full_name,
				u.first_name,
				u.middle_name,
				u.last_name,
				u.address_line_one,
				CONCAT(p.f_first_name, ' ', p.f_middle_name, ' ', p.f_last_name) AS parentage,
				p.f_mobile,
				sgsl.roll_number,
				g.name AS grade_name,
				g.[order],
				fc.id_offline AS fee_category_id,
				fc.name AS category_name,
				f.amount + CASE WHEN sf.fine IS NULL THEN  0 ELSE sf.fine END - CASE WHEN sf.concession_amount IS NULL THEN  0 ELSE sf.concession_amount END - CASE WHEN sp.amount IS NULL THEN  0 ELSE sp.amount END + CASE WHEN sp.fine IS NULL THEN  0 ELSE sp.fine END AS student_balance,
				sf.created_by AS created_by,
				sf.created_on AS created_on,
				sf.updated_by AS updated_by,
				sf.updated_on AS updated_on
			FROM 
				students AS s
			LEFT JOIN users AS u ON u.id_offline = s.user_id
			LEFT JOIN parents AS p ON p.id_offline = s.parent_id
			LEFT JOIN student_fees AS sf ON s.id_offline = sf.student_id
			LEFT JOIN grade_fees AS gf ON gf.id_offline = sf.grade_fees_id
			LEFT JOIN route_vehicle_stops_fee_logs AS rvsfl ON rvsfl.id_offline = sf.route_vehicle_stops_fee_log_id
			LEFT JOIN fees AS f ON f.id_offline = gf.fees_id OR f.id_offline = rvsfl.fees_id
			LEFT JOIN fee_categories AS fc ON fc.id_offline = f.fee_category_id
			LEFT JOIN student_grade_session_log AS sgsl ON s.id_offline = sgsl.student_id
			LEFT JOIN grades AS g ON sgsl.grade_id = g.id_offline
			LEFT JOIN user_avatar_files uaf ON uaf.id_offline = u.user_avatar_file_id
			LEFT JOIN student_payments AS sp ON sf.id_offline = sp.student_fees_id	
			WHERE
				s.id_offline like @StudentID AND
				fc.id_offline like @FeeCategoryID
		) 
		SELECT 
			*
		FROM StudentFeeDue 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo




















GO
/****** Object:  StoredProcedure [dbo].[GetStudentGenderRatio]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetStudentGenderRatio>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentGenderRatio]
AS
	select COUNT(students.id_offline) as Count,gender from students 
	inner join users on students.user_id = users.id_offline 
	where status = 'active' group by users.gender





















GO
/****** Object:  StoredProcedure [dbo].[GetStudentPaymentHistory]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetStudentPaymentHistory>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentPaymentHistory] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200), @StudentID nvarchar(200) = '%%'
AS
WITH StudentPaymentHistory AS 
        ( 
           SELECT
				ROW_NUMBER() OVER(order by sp.updated_on DESC) AS 'RowNumber', 
				sp.*,
				u.full_name AS [student_name],
				f.id_offline AS [fees_id],
				sf.student_id,
				sf.grade_fees_id,
				sf.concession_amount,
				rvsfl.route_vehicle_stop_id, 
				Convert(char(3), sf.apply_from, 0) as [month],
				sf.apply_from,
				sf.apply_to,
				f.amount AS [fee_amount],
				f.fee_category_id,
				fc.name AS [category_name]
			FROM
				student_payments AS sp
			LEFT JOIN student_fees AS sf ON sf.id_offline = sp.student_fees_id
			LEFT JOIN students AS s ON s.id_offline = sf.student_id
			LEFT JOIN users AS u ON u.id_offline = s.user_id
			LEFT JOIN grade_fees AS gf ON gf.id_offline = sf.grade_fees_id
			LEFT JOIN route_vehicle_stops_fee_logs AS rvsfl ON rvsfl.id_offline = sf.route_vehicle_stops_fee_log_id
			LEFT JOIN route_vehicle_stops AS rvs ON rvs.id_offline = rvsfl.route_vehicle_stop_id
			LEFT JOIN fees AS f ON f.id_offline = gf.fees_id OR f.id_offline = rvsfl.fees_id
			LEFT JOIN fee_categories AS fc ON fc.id_offline = f.fee_category_id		
			WHERE s.id_offline like @StudentID		
				) 
				SELECT 
					*
				FROM StudentPaymentHistory 
				WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo














GO
/****** Object:  StoredProcedure [dbo].[GetStudentPaymentsSumAsPerMonth]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetStudentPaymentsAsPerMonth>
-- =============================================
CREATE PROCEDURE [dbo].[GetStudentPaymentsSumAsPerMonth]
AS
	SELECT SUM(student_payments.amount) AS Amount, student_fees.apply_from AS Month
		FROM student_payments
	INNER JOIN student_fees ON student_payments.student_fees_id = student_fees.id_offline
	GROUP BY student_fees.apply_from























GO
/****** Object:  StoredProcedure [dbo].[GetStudentsList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetStudentsList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- ===================[dbo].[GetStudentsList]==========================
CREATE PROCEDURE [dbo].[GetStudentsList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200),@GradesModel grades Readonly,@SectionsModel sections Readonly,@RollNumber nvarchar(50) = '%%',@RegistrationID  nvarchar(200) = '%%'
AS
WITH StudentsListTable AS 
        ( 
           SELECT
			    ROW_NUMBER() OVER(order by MAX('users.full_name')) AS 'RowNumber',
				--Student 
				MAX(students.id_offline) as [students.id_offline],
				MAX(students.id_online) as [students.id_online],
				MAX(students.school_id) as [students.school_id],
				MAX(students.user_id) as [students.user_id],
				MAX(students.parent_id) as [students.parent_id],
				MAX(students.trip_stop_id) as [students.trip_stop_id],
				MAX(students.enrollment_date) as [students.enrollment_date],
				MAX(students.status) as [students.status],
				MAX(students.dc_number) as [students.dc_number],
				MAX(students.dc_date_of_issue) as [students.dc_date_of_issue],
				MAX(students.created_by) as [students.created_by],
				MAX(students.created_on) as [students.created_on],
				MAX(students.updated_by) as [students.updated_by],
				MAX(students.updated_on) as [students.updated_on],
				MAX(u.full_name) as [CreatedBy],
				--User
				MAX(users.id_offline) as [users.id_offline],
				MAX(users.id_online) as [users.id_online],
				MAX(users.school_id) as [users.school_id],
				MAX(users.student_id) as [users.student_id],
				MAX(users.role_id) as [users.role_id],
				MAX(users.user_type) as [users.user_type],
				MAX(users.username) as [users.username],
				MAX(users.email) as [users.email],
				MAX(users.phone) as [users.phone],
				MAX(users.address_line_one) as [users.address_line_one],
				MAX(users.address_line_two) as [users.address_line_two],
				MAX(users.area) as [users.area],
				MAX(users.first_name) as [users.first_name],
				MAX(users.middle_name) as [users.middle_name],
				MAX(users.last_name) as [users.last_name],
				MAX(users.full_name) as [users.full_name],
				MAX(users.gender) as [users.gender],
				MAX(users.blood_group) as [users.blood_group],
				MAX(users.password) as [users.password],
				MAX(users.birth_date) as [users.birth_date],
				MAX(users.other_phones) as [users.other_phones],
				MAX(users.default_phone_number_id) as [users.default_phone_number_id],
				MAX(users.adhaar_number) as [users.adhaar_number],
				MAX(users.bank_name) as [users.bank_name],
				MAX(users.bank_branch) as [users.bank_branch],
				MAX(users.bank_account_number) as [users.bank_account_number],
				MAX(users.bank_ifsc_code) as [users.bank_ifsc_code],
				MAX(users.flags) as [users.flags],
				MAX(users.last_login_time) as [users.last_login_time],
				MAX(users.user_avatar_file_id) as [users.user_avatar_file_id],
				MAX(users.is_active) as [users.is_active],
				MAX(users.created_on) as [users.created_on],
				MAX(users.created_by) as [users.created_by],
				MAX(users.updated_on) as [users.updated_on],
				MAX(users.updated_by) as [users.updated_by],
				--Section
				MAX(sections.id_offline) as [sections.id_offline],
				MAX(sections.id_online) as [sections.id_online],
				MAX(sections.school_id) as [sections.school_id],
				MAX(sections.name) as [sections.name],
				MAX(sections.capacity) as [sections.capacity],
				MAX(sections.created_by) as [sections.created_by],
				MAX(sections.created_on) as [sections.created_on],
				MAX(sections.updated_by) as [sections.updated_by],
				MAX(sections.updated_on) as [sections.updated_on],
				--grade
				MAX(grades.id_offline) as [grades.id_offline],
				MAX(grades.id_online) as [grades.id_online],
				MAX(grades.school_id) as [grades.school_id],
				MAX(grades.block) as [grades.block],
				MAX(grades.name) as [grades.name],
				MAX(grades.[order]) as [grades.order],
				MAX(grades.created_by) as [grades.created_by],
				MAX(grades.created_on) as [grades.created_on],
				MAX(grades.updated_by) as [grades.updated_by],
				MAX(grades.updated_on) as [grades.updated_on],
				--parents
				MAX(parents.id_offline) as [parents.id_offline],
				MAX(parents.id_online) as [parents.id_online],
				MAX(parents.school_id) as [parents.school_id],
				MAX(parents.f_first_name) as [parents.f_first_name],
				MAX(parents.f_middle_name) as [parents.f_middle_name],
				MAX(parents.f_last_name) as [parents.f_last_name],
				MAX(parents.f_full_name) as [parents.f_full_name],
				MAX(parents.f_mobile) as [parents.f_mobile],
				MAX(parents.f_phone) as [parents.f_phone],
				MAX(parents.f_office) as [parents.f_office],
				MAX(parents.f_email) as [parents.f_email],
				MAX(parents.m_first_name) as [parents.m_first_name],
				MAX(parents.m_middle_name) as [parents.m_middle_name],
				MAX(parents.m_last_name) as [parents.m_last_name],
				MAX(parents.m_full_name) as [parents.m_full_name],
				MAX(parents.m_mobile) as [parents.m_mobile],
				MAX(parents.m_phone) as [parents.m_phone],
				MAX(parents.m_office) as [parents.m_office],
				MAX(parents.m_email) as [parents.m_email],
				MAX(parents.g_fullname) as [parents.g_fullname],
				MAX(parents.g_mobile) as [parents.g_mobile],
				MAX(parents.g_email) as [parents.g_email],
				MAX(parents.created_by) as [parents.created_by],
				MAX(parents.created_on) as [parents.created_on],
				MAX(parents.updated_by) as [parents.updated_by],
				MAX(parents.updated_on) as [parents.updated_on],
				--sessions
				MAX(sessions.id_offline) as [sessions.id_offline],
				MAX(sessions.id_online) as [sessions.id_online],
				MAX(sessions.school_id) as [sessions.school_id],
				MAX(sessions.name) as [sessions.name],
				MAX(sessions.[order]) as [sessions.order],
				MAX(sessions.from_date) as [sessions.from_date],
				MAX(sessions.to_date) as [sessions.to_date],
				MAX(sessions.is_active) as [sessions.is_active],
				MAX(sessions.created_by) as [sessions.created_by],
				MAX(sessions.created_on) as [sessions.created_on],
				MAX(sessions.updated_by) as [sessions.updated_by],
				MAX(sessions.updated_on) as [sessions.updated_on],
				--student_grade_session_log
				MAX(student_grade_session_log.id_offline) as [student_grade_session_log.id_offline],
				MAX(student_grade_session_log.id_online) as [student_grade_session_log.id_online],
				MAX(student_grade_session_log.school_id) as [student_grade_session_log.school_id],
				MAX(student_grade_session_log.student_id) as [student_grade_session_log.student_id],
				MAX(student_grade_session_log.registration_id) as [student_grade_session_log.registration_id],
				MAX(student_grade_session_log.grade_id) as [student_grade_session_log.grade_id],
				MAX(student_grade_session_log.section_id) as [student_grade_session_log.section_id],
				MAX(student_grade_session_log.roll_number) as [student_grade_session_log.roll_number],
				MAX(student_grade_session_log.exam_roll_number) as [student_grade_session_log.exam_roll_number],
				MAX(student_grade_session_log.session_id) as [student_grade_session_log.session_id],
				MAX(student_grade_session_log.sgsl_status) as [student_grade_session_log.sgsl_status],
				MAX(student_grade_session_log.created_by) as [student_grade_session_log.created_by],
				MAX(student_grade_session_log.created_on) as [student_grade_session_log.created_on],
				MAX(student_grade_session_log.updated_by) as [student_grade_session_log.updated_by],
				MAX(student_grade_session_log.updated_on) as [student_grade_session_log.updated_on]
			FROM 
				Students
			LEFT JOIN student_grade_session_log  ON student_grade_session_log.student_id = students.id_offline
			LEFT JOIN users  ON users.id_offline = students.user_id
			LEFT JOIN users u  ON u.id_offline = students.created_by
			LEFT JOIN grades  ON grades.id_offline = student_grade_session_log.grade_id
			LEFT JOIN sections  ON sections.id_offline = student_grade_session_log.section_id
			LEFT JOIN parents  ON parents.id_offline = students.parent_id
			LEFT JOIN sessions  ON sessions.id_offline = student_grade_session_log.session_id		
			WHERE users.user_type = 'student'
			AND student_grade_session_log.sgsl_status = 'active'
			GROUP BY
			students.id_offline
			HAVING MAX(grades.id_offline) IN (SELECT id_offline FROM @GradesModel)
			AND MAX(sections.id_offline) IN (SELECT id_offline FROM @SectionsModel)
			AND MAX(student_grade_session_log.roll_number) like @RollNumber
			AND MAX(student_grade_session_log.registration_id) like @RegistrationID
		) 
		SELECT 
			*
			FROM StudentsListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo





GO
/****** Object:  StoredProcedure [dbo].[GetSyncTableInfo]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Get SyncTableInfo>
-- =============================================
CREATE PROCEDURE [dbo].[GetSyncTableInfo]
AS 
	SELECT * FROM SyncTableInfo














GO
/****** Object:  StoredProcedure [dbo].[GetUsersList]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<GetUsersList>
--				Get Specific rows based on FromRowNo and ToRowNo
-- =============================================
CREATE PROCEDURE [dbo].[GetUsersList] @FromRowNo nvarchar(200), @ToRowNo nvarchar(200)
AS
WITH usersListTable AS 
        ( 
           SELECT
			    ROW_NUMBER() OVER(order by MAX(u.full_name)) AS 'RowNumber',  
			   	MAX(u.id_offline) as id_offline,
				MAX(u.id_online) as id_online,
				MAX(u.school_id) as school_id,
				MAX(u.student_id) as student_id,
				MAX(u.role_id) as role_id,
				MAX(u.user_type) as user_type,
				MAX(u.username) as username,
				MAX(u.email) as email,
				MAX(u.phone) as phone,
				MAX(u.address_line_one) as address_line_one,
				MAX(u.address_line_two) as address_line_two,
				MAX(u.area) as area,
				MAX(u.first_name) as first_name,
				MAX(u.middle_name) as middle_name,
				MAX(u.last_name) as last_name,
				MAX(u.full_name) as full_name,
				MAX(u.gender) as gender,
				MAX(u.blood_group) as blood_group,
				MAX(u.password) as password,
				MAX(u.birth_date) as birth_date,
				MAX(u.other_phones) as other_phones,
				MAX(u.default_phone_number_id) as default_phone_number_id,
				MAX(u.adhaar_number) as adhaar_number,
				MAX(u.bank_name) as bank_name,
				MAX(u.bank_branch) as bank_branch,
				MAX(u.bank_account_number) as bank_account_number,
				MAX(u.bank_ifsc_code) as bank_ifsc_code,
				MAX(u.flags) as flags,
				MAX(u.last_login_time) as last_login_time,
				MAX(u.user_avatar_file_id) as user_avatar_file_id,
				MAX(u.is_active) as is_active,
				MAX(u.created_on) as created_on,
				MAX(u.created_by) as created_by,
				MAX(u.updated_on) as updated_on,
				MAX(u.updated_by) as updated_by,
				MAX(users.full_name) as CreatedBy
			FROM 
				users AS u
			LEFT JOIN users ON u.created_by = users.id_offline
			WHERE u.user_type = 'staff'
			GROUP BY
			u.id_offline
		) 
		SELECT 
			*
			FROM usersListTable 
		WHERE RowNumber BETWEEN @FromRowNo AND @ToRowNo











GO
/****** Object:  StoredProcedure [dbo].[IsExistingUser]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Check User Exits>
-- =============================================
CREATE PROCEDURE [dbo].[IsExistingUser] @Username nvarchar(50)
AS
SELECT * FROM Users WHERE username = @Username













GO
/****** Object:  StoredProcedure [dbo].[IsSchoolSetup]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Check School is Setup>
-- =============================================
CREATE PROCEDURE [dbo].[IsSchoolSetup]
AS
SELECT * 
FROM schools













GO
/****** Object:  StoredProcedure [dbo].[MakePayment]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Make Payment>
-- =============================================
CREATE PROCEDURE [dbo].[MakePayment] @PaymentTable PaymentModel READONLY
AS
BEGIN TRAN
		INSERT INTO [dbo].[student_payments]
           ([id_offline],
			[id_online],
			[school_id],
			[student_fees_id],
			[amount],
			[fine],
			[comment],
			[recept_no],
			[payment_mode],
			[payment_date],
			[ip],
			[created_by],
			[created_on],
			[updated_by],
			[updated_on])
     SELECT
            objPayment.id_offline,
			objPayment.id_online,
		    objPayment.school_id,
			objPayment.student_fees_id,
			objPayment.amount,
			objPayment.fine,
			objPayment.comment,
			objPayment.recept_no,
			objPayment.payment_mode,
			objPayment.payment_date,
			objPayment.ip,
			objPayment.created_by,
			objPayment.created_on,
			objPayment.updated_by,
			objPayment.updated_on
	FROM
		 @PaymentTable As objPayment

COMMIT

















GO
/****** Object:  StoredProcedure [dbo].[SaveSettings]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Save Settings>
-- =============================================
CREATE PROCEDURE [dbo].[SaveSettings]  @key nvarchar(100), @value nvarchar(100)
AS
BEGIN TRAN
		if(exists (select 1 from Settings where [key]=@key))
			UPDATE Settings Set value = @value WHERE [key] = @key
		ELSE
			INSERT INTO Settings
			   (
					[key]
					,[value]
			   )
			VALUES
			(
				@key,
				@value
			)    
COMMIT












GO
/****** Object:  StoredProcedure [dbo].[SetSchoolInfo]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<SetSchoolInfo>
-- =============================================
CREATE PROCEDURE [dbo].[SetSchoolInfo]  @Model schools READONLY
AS
BEGIN TRAN
		IF(exists (select 1 from schools where id_offline=(SELECT obj.id_offline FROM @Model As obj)))
			UPDATE schools
				SET
					[database_id] = obj.database_id,
					[subdomain] = obj.subdomain,
					[domain] = obj.domain,
					[name] = obj.name,
					[address] = obj.address,
					[website] = obj.address,
					[phone] = obj.address,
					[email] = obj.address,
					[theme] = obj.theme,
					[created_on] = obj.created_on
				FROM
					@Model AS obj
				WHERE
					schools.id_offline = obj.id_offline
		 ELSE
			INSERT INTO [dbo].[schools]
			   ([id_offline]
			   ,[id_online]
			   ,[database_id]
			   ,[subdomain]
			   ,[domain]
			   ,[name]
			   ,[address]
			   ,[website]
			   ,[phone]
			   ,[email]
			   ,[theme]
			   ,[created_on])
			 SELECT
					obj.id_offline,
					obj.id_online,
					obj.database_id,
					obj.subdomain,
					obj.domain,
					obj.name,
					obj.address,
					obj.website,
					obj.phone,
					obj.email,
					obj.theme,
					obj.created_on
			  FROM
				 @Model As obj
COMMIT













GO
/****** Object:  StoredProcedure [dbo].[UpdatePayment]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Update Payment>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePayment] @PaymentTable PaymentModel READONLY
AS
BEGIN TRAN
	UPDATE student_payments
		SET
			recept_no = objPayment.recept_no,
			fine = objPayment.fine,
			amount = objPayment.amount,
			comment = objPayment.comment,
			updated_by = objPayment.updated_by,
			updated_on = objPayment.updated_on
		FROM
			@PaymentTable AS objPayment
		WHERE
			student_payments.id_offline = objPayment.id_offline

COMMIT















GO
/****** Object:  StoredProcedure [dbo].[UpdateSyncTableInfo]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Arif Nazir>
-- Create date: <Create Date,,>
-- Description:	<Update SyncTableInfo>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSyncTableInfo] @ID uniqueidentifier , @LastSyncedOn datetime 
AS
BEGIN
	UPDATE SyncTableInfo
		SET
			LastSyncedOn =  @LastSyncedOn
		WHERE
			id_Offline = @ID

END

















GO
/****** Object:  Table [dbo].[fee_categories]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fee_categories](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](45) NOT NULL,
	[recur] [varchar](10) NOT NULL,
	[is_transport] [varchar](10) NULL,
	[order] [int] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fees]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fees](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[fee_category_id] [uniqueidentifier] NOT NULL,
	[session_id] [uniqueidentifier] NOT NULL,
	[amount] [float] NOT NULL,
	[fee_cources] [varchar](max) NULL,
	[last_date] [date] NULL,
	[last_day] [int] NULL,
	[fine_per_day] [float] NULL,
	[is_allocated] [varchar](10) NOT NULL,
	[remarks] [varchar](max) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[files]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[files](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[name] [varchar](255) NULL,
	[extension] [varchar](45) NULL,
	[size] [int] NULL,
	[school_id] [int] NULL,
	[owner_user_id] [int] NULL,
	[created] [datetime2](0) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[grade_fees]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grade_fees](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[fees_id] [uniqueidentifier] NOT NULL,
	[grade_id] [uniqueidentifier] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[grades]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[grades](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[block] [varchar](50) NULL,
	[name] [varchar](45) NOT NULL,
	[order] [int] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[parents]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[parents](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[f_first_name] [varchar](45) NULL,
	[f_middle_name] [varchar](45) NULL,
	[f_last_name] [varchar](45) NULL,
	[f_full_name] [varchar](100) NULL,
	[f_mobile] [varchar](15) NULL,
	[f_phone] [varchar](15) NULL,
	[f_office] [varchar](15) NULL,
	[f_email] [varchar](45) NULL,
	[m_first_name] [varchar](45) NULL,
	[m_middle_name] [varchar](45) NULL,
	[m_last_name] [varchar](45) NULL,
	[m_full_name] [varchar](100) NULL,
	[m_mobile] [varchar](15) NULL,
	[m_phone] [varchar](15) NULL,
	[m_office] [varchar](15) NULL,
	[m_email] [varchar](45) NULL,
	[g_fullname] [varchar](45) NULL,
	[g_mobile] [varchar](15) NULL,
	[g_email] [varchar](45) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[route_stops]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[route_stops](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[route_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](max) NOT NULL,
	[latitude] [varchar](max) NULL,
	[longitude] [varchar](max) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[route_vehicle_stops]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[route_vehicle_stops](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[route_vehicle_id] [uniqueidentifier] NOT NULL,
	[route_stop_id] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[route_vehicle_stops_fee_logs]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[route_vehicle_stops_fee_logs](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[route_vehicle_stop_id] [uniqueidentifier] NOT NULL,
	[fees_id] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[route_vehicles]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[route_vehicles](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[vehicle_id] [uniqueidentifier] NOT NULL,
	[route_id] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[routes]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[routes](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[start_location] [varchar](max) NULL,
	[end_location] [varchar](max) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[schools]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[schools](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[database_id] [uniqueidentifier] NULL,
	[subdomain] [varchar](45) NULL,
	[domain] [varchar](max) NULL,
	[name] [varchar](100) NOT NULL,
	[address] [varchar](max) NOT NULL,
	[website] [varchar](max) NULL,
	[phone] [varchar](max) NULL,
	[email] [varchar](max) NULL,
	[theme] [varchar](max) NULL,
	[created_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sections]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[sections](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](10) NOT NULL,
	[capacity] [int] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sessions]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[sessions](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[name] [varchar](45) NOT NULL,
	[order] [int] NOT NULL,
	[from_date] [datetime2](0) NULL,
	[to_date] [datetime2](0) NULL,
	[is_active] [varchar](10) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_sessions] PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[key] [nvarchar](100) NOT NULL,
	[value] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[student_fees]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[student_fees](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[grade_fees_id] [uniqueidentifier] NULL,
	[student_id] [uniqueidentifier] NOT NULL,
	[route_vehicle_stops_fee_log_id] [uniqueidentifier] NULL,
	[apply_from] [date] NOT NULL,
	[apply_to] [date] NOT NULL,
	[fine] [float] NULL,
	[concession_amount] [float] NULL,
	[no_fine] [smallint] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[student_grade_session_log]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[student_grade_session_log](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[student_id] [uniqueidentifier] NOT NULL,
	[registration_id] [varchar](45) NOT NULL,
	[grade_id] [uniqueidentifier] NOT NULL,
	[section_id] [uniqueidentifier] NOT NULL,
	[roll_number] [varchar](100) NOT NULL,
	[exam_roll_number] [varchar](45) NULL,
	[session_id] [uniqueidentifier] NOT NULL,
	[sgsl_status] [varchar](50) NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[student_payments]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[student_payments](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[student_fees_id] [uniqueidentifier] NOT NULL,
	[amount] [float] NOT NULL,
	[fine] [float] NULL,
	[comment] [varchar](max) NULL,
	[recept_no] [varchar](45) NULL,
	[payment_mode] [varchar](50) NOT NULL,
	[payment_date] [datetime2](0) NOT NULL,
	[ip] [varchar](45) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[students]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[students](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[parent_id] [uniqueidentifier] NULL,
	[trip_stop_id] [uniqueidentifier] NULL,
	[enrollment_date] [date] NULL,
	[status] [varchar](50) NOT NULL,
	[dc_number] [varchar](50) NULL,
	[dc_date_of_issue] [datetime2](0) NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SyncTableInfo]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyncTableInfo](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[TableName] [nvarchar](100) NOT NULL,
	[LastSyncedOn] [datetime] NULL,
 CONSTRAINT [PK_Sync] PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[trip_stops]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[trip_stops](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[route_vehicle_stop_id] [int] NOT NULL,
	[trip] [varchar](50) NOT NULL,
	[pick] [time](7) NOT NULL,
	[drop] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_avatar_files]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_avatar_files](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NULL,
	[applicant_id] [uniqueidentifier] NULL,
	[file_id] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NULL,
	[student_id] [uniqueidentifier] NULL,
	[role_id] [uniqueidentifier] NULL,
	[user_type] [varchar](50) NOT NULL,
	[username] [varchar](45) NULL,
	[email] [varchar](100) NULL,
	[phone] [varchar](20) NULL,
	[address_line_one] [varchar](max) NULL,
	[address_line_two] [varchar](max) NULL,
	[area] [varchar](45) NULL,
	[first_name] [varchar](100) NOT NULL,
	[middle_name] [varchar](100) NULL,
	[last_name] [varchar](100) NULL,
	[full_name] [varchar](300) NOT NULL,
	[gender] [varchar](50) NULL,
	[blood_group] [varchar](50) NULL,
	[password] [varchar](200) NULL,
	[birth_date] [datetime2](0) NULL,
	[other_phones] [varchar](max) NULL,
	[default_phone_number_id] [uniqueidentifier] NULL,
	[adhaar_number] [varchar](40) NULL,
	[bank_name] [varchar](100) NULL,
	[bank_branch] [varchar](100) NULL,
	[bank_account_number] [varchar](100) NULL,
	[bank_ifsc_code] [varchar](100) NULL,
	[flags] [varchar](max) NULL,
	[last_login_time] [datetime2](0) NULL,
	[user_avatar_file_id] [uniqueidentifier] NULL,
	[is_active] [varchar](10) NULL,
	[created_on] [datetime2](0) NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vehicles]    Script Date: 12/29/2017 1:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vehicles](
	[id_offline] [uniqueidentifier] NOT NULL,
	[id_online] [uniqueidentifier] NULL,
	[school_id] [uniqueidentifier] NOT NULL,
	[tracker_id] [uniqueidentifier] NULL,
	[registration_number] [varchar](50) NOT NULL,
	[bus_number] [varchar](15) NULL,
	[insurance_renew_date] [date] NULL,
	[staff_id] [uniqueidentifier] NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[created_on] [datetime2](0) NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[updated_on] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_offline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[fee_categories] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[fee_categories] ADD  DEFAULT ('0') FOR [recur]
GO
ALTER TABLE [dbo].[fee_categories] ADD  DEFAULT (NULL) FOR [is_transport]
GO
ALTER TABLE [dbo].[fees] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[fees] ADD  DEFAULT (NULL) FOR [last_date]
GO
ALTER TABLE [dbo].[fees] ADD  DEFAULT (NULL) FOR [last_day]
GO
ALTER TABLE [dbo].[fees] ADD  DEFAULT (NULL) FOR [fine_per_day]
GO
ALTER TABLE [dbo].[fees] ADD  DEFAULT ('0') FOR [is_allocated]
GO
ALTER TABLE [dbo].[files] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[files] ADD  DEFAULT (NULL) FOR [name]
GO
ALTER TABLE [dbo].[files] ADD  DEFAULT (NULL) FOR [extension]
GO
ALTER TABLE [dbo].[files] ADD  DEFAULT (NULL) FOR [size]
GO
ALTER TABLE [dbo].[files] ADD  DEFAULT (NULL) FOR [school_id]
GO
ALTER TABLE [dbo].[files] ADD  DEFAULT (NULL) FOR [owner_user_id]
GO
ALTER TABLE [dbo].[files] ADD  DEFAULT (NULL) FOR [created]
GO
ALTER TABLE [dbo].[grade_fees] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[grades] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[grades] ADD  DEFAULT (NULL) FOR [block]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [f_middle_name]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [f_mobile]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [f_phone]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [f_office]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [f_email]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [m_first_name]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [m_middle_name]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [m_last_name]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [m_mobile]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [m_phone]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [m_office]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [m_email]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [g_fullname]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [g_mobile]
GO
ALTER TABLE [dbo].[parents] ADD  DEFAULT (NULL) FOR [g_email]
GO
ALTER TABLE [dbo].[route_stops] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[route_vehicle_stops] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[route_vehicle_stops_fee_logs] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[route_vehicles] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[routes] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[schools] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[sections] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[sessions] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[sessions] ADD  DEFAULT (NULL) FOR [from_date]
GO
ALTER TABLE [dbo].[sessions] ADD  DEFAULT (NULL) FOR [to_date]
GO
ALTER TABLE [dbo].[Settings] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[student_fees] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[student_fees] ADD  DEFAULT (NULL) FOR [grade_fees_id]
GO
ALTER TABLE [dbo].[student_fees] ADD  DEFAULT (NULL) FOR [route_vehicle_stops_fee_log_id]
GO
ALTER TABLE [dbo].[student_fees] ADD  DEFAULT ('0') FOR [fine]
GO
ALTER TABLE [dbo].[student_fees] ADD  DEFAULT ('0') FOR [concession_amount]
GO
ALTER TABLE [dbo].[student_fees] ADD  DEFAULT ('0') FOR [no_fine]
GO
ALTER TABLE [dbo].[student_grade_session_log] ADD  DEFAULT (NULL) FOR [id_offline]
GO
ALTER TABLE [dbo].[student_grade_session_log] ADD  DEFAULT (NULL) FOR [exam_roll_number]
GO
ALTER TABLE [dbo].[student_grade_session_log] ADD  DEFAULT ('active') FOR [sgsl_status]
GO
ALTER TABLE [dbo].[student_payments] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[student_payments] ADD  DEFAULT (newid()) FOR [id_online]
GO
ALTER TABLE [dbo].[student_payments] ADD  DEFAULT (NULL) FOR [fine]
GO
ALTER TABLE [dbo].[student_payments] ADD  DEFAULT (NULL) FOR [recept_no]
GO
ALTER TABLE [dbo].[student_payments] ADD  DEFAULT (NULL) FOR [ip]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT (NULL) FOR [parent_id]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT (NULL) FOR [trip_stop_id]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT ('active') FOR [status]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT (NULL) FOR [dc_number]
GO
ALTER TABLE [dbo].[students] ADD  DEFAULT (NULL) FOR [dc_date_of_issue]
GO
ALTER TABLE [dbo].[SyncTableInfo] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[trip_stops] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[user_avatar_files] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[user_avatar_files] ADD  DEFAULT (NULL) FOR [user_id]
GO
ALTER TABLE [dbo].[user_avatar_files] ADD  DEFAULT (NULL) FOR [applicant_id]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [student_id]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [role_id]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [username]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [email]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [phone]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [area]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [middle_name]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [last_name]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [full_name]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [blood_group]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [default_phone_number_id]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [adhaar_number]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [bank_name]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [bank_branch]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [bank_account_number]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [bank_ifsc_code]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [last_login_time]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [user_avatar_file_id]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [created_by]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [updated_on]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (NULL) FOR [updated_by]
GO
ALTER TABLE [dbo].[vehicles] ADD  DEFAULT (newid()) FOR [id_offline]
GO
ALTER TABLE [dbo].[vehicles] ADD  DEFAULT (NULL) FOR [tracker_id]
GO
ALTER TABLE [dbo].[vehicles] ADD  DEFAULT (NULL) FOR [bus_number]
GO
ALTER TABLE [dbo].[vehicles] ADD  DEFAULT (NULL) FOR [insurance_renew_date]
GO
ALTER TABLE [dbo].[vehicles] ADD  DEFAULT (NULL) FOR [staff_id]
GO
ALTER TABLE [dbo].[fee_categories]  WITH CHECK ADD CHECK  (([recur]='12' OR [recur]='11' OR [recur]='10' OR [recur]='9' OR [recur]='8' OR [recur]='7' OR [recur]='6' OR [recur]='5' OR [recur]='4' OR [recur]='3' OR [recur]='2' OR [recur]='1' OR [recur]='0'))
GO
ALTER TABLE [dbo].[fee_categories]  WITH CHECK ADD CHECK  (([recur]='12' OR [recur]='11' OR [recur]='10' OR [recur]='9' OR [recur]='8' OR [recur]='7' OR [recur]='6' OR [recur]='5' OR [recur]='4' OR [recur]='3' OR [recur]='2' OR [recur]='1' OR [recur]='0'))
GO
ALTER TABLE [dbo].[fee_categories]  WITH CHECK ADD CHECK  (([recur]='12' OR [recur]='11' OR [recur]='10' OR [recur]='9' OR [recur]='8' OR [recur]='7' OR [recur]='6' OR [recur]='5' OR [recur]='4' OR [recur]='3' OR [recur]='2' OR [recur]='1' OR [recur]='0'))
GO
ALTER TABLE [dbo].[student_grade_session_log]  WITH CHECK ADD CHECK  (([sgsl_status]='new_admission' OR [sgsl_status]='in_active' OR [sgsl_status]='pass_out' OR [sgsl_status]='active'))
GO
ALTER TABLE [dbo].[student_grade_session_log]  WITH CHECK ADD CHECK  (([sgsl_status]='new_admission' OR [sgsl_status]='in_active' OR [sgsl_status]='pass_out' OR [sgsl_status]='active'))
GO
ALTER TABLE [dbo].[student_grade_session_log]  WITH CHECK ADD CHECK  (([sgsl_status]='new_admission' OR [sgsl_status]='in_active' OR [sgsl_status]='pass_out' OR [sgsl_status]='active'))
GO
ALTER TABLE [dbo].[student_payments]  WITH CHECK ADD CHECK  (([payment_mode]='online' OR [payment_mode]='challan' OR [payment_mode]='cheque' OR [payment_mode]='cash'))
GO
ALTER TABLE [dbo].[student_payments]  WITH CHECK ADD CHECK  (([payment_mode]='online' OR [payment_mode]='challan' OR [payment_mode]='cheque' OR [payment_mode]='cash'))
GO
ALTER TABLE [dbo].[student_payments]  WITH CHECK ADD CHECK  (([payment_mode]='online' OR [payment_mode]='challan' OR [payment_mode]='cheque' OR [payment_mode]='cash'))
GO
ALTER TABLE [dbo].[students]  WITH CHECK ADD CHECK  (([status]='In Active' OR [status]='Pass Out' OR [status]='Active'))
GO
ALTER TABLE [dbo].[trip_stops]  WITH CHECK ADD CHECK  (([trip]='3' OR [trip]='2' OR [trip]='1'))
GO
ALTER TABLE [dbo].[trip_stops]  WITH CHECK ADD CHECK  (([trip]='3' OR [trip]='2' OR [trip]='1'))
GO
ALTER TABLE [dbo].[trip_stops]  WITH CHECK ADD CHECK  (([trip]='3' OR [trip]='2' OR [trip]='1'))
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD CHECK  (([blood_group]='AB-' OR [blood_group]='AB+' OR [blood_group]='B-' OR [blood_group]='B+' OR [blood_group]='A-' OR [blood_group]='A+' OR [blood_group]='O-' OR [blood_group]='O+' OR [blood_group]=''))
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD CHECK  (([gender]='' OR [gender]='female' OR [gender]='male'))
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD CHECK  (([user_type]='parent' OR [user_type]='student' OR [user_type]='staff'))
GO
USE [master]
GO
ALTER DATABASE [SMS] SET  READ_WRITE 
GO
