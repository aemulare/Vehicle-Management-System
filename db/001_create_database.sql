use [master]
go

/****** Object:  Database [vms]    Script Date: 11/5/2016 1:04:42 PM ******/
create database [vms] containment = none on primary 
( NAME = N'vms', FILENAME = N'C:\Dbs\vms.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'vms_log', FILENAME = N'C:\Dbs\vms.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [vms] SET COMPATIBILITY_LEVEL = 120
GO

IF(1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [vms].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [vms] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [vms] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [vms] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [vms] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [vms] SET ARITHABORT OFF 
GO
ALTER DATABASE [vms] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [vms] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [vms] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [vms] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [vms] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [vms] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [vms] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [vms] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [vms] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [vms] SET  DISABLE_BROKER 
GO
ALTER DATABASE [vms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [vms] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [vms] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [vms] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [vms] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [vms] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [vms] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [vms] SET RECOVERY FULL 
GO
ALTER DATABASE [vms] SET  MULTI_USER 
GO
ALTER DATABASE [vms] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [vms] SET DB_CHAINING OFF 
GO
ALTER DATABASE [vms] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [vms] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [vms] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [vms] SET  READ_WRITE 
GO
