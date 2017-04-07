
/* DATABASE FOR STORING GAMMA SPECTRUMS AND RESULTS */

--create database nrpa_lorakon
--go

use nrpa_lorakon
go

/* TABLES */

delete from SpectrumInfo
go

IF OBJECT_ID('dbo.SpectrumBackground', 'U') IS NOT NULL DROP TABLE dbo.SpectrumBackground;
IF OBJECT_ID('dbo.SpectrumResult', 'U') IS NOT NULL DROP TABLE dbo.SpectrumResult;
IF OBJECT_ID('dbo.SpectrumFile', 'U') IS NOT NULL DROP TABLE dbo.SpectrumFile;
IF OBJECT_ID('dbo.SpectrumChecksum', 'U') IS NOT NULL DROP TABLE dbo.SpectrumChecksum;
IF OBJECT_ID('dbo.SpectrumInfo', 'U') IS NOT NULL DROP TABLE dbo.SpectrumInfo;
IF OBJECT_ID('dbo.SpectrumValidationRules', 'U') IS NOT NULL DROP TABLE dbo.SpectrumValidationRules;
IF OBJECT_ID('dbo.SpectrumGeometryRules', 'U') IS NOT NULL DROP TABLE dbo.SpectrumGeometryRules;

IF EXISTS(SELECT * FROM sys.views WHERE name = 'SpectrumInfoLatest' AND schema_id = SCHEMA_ID('dbo')) DROP VIEW dbo.SpectrumInfoLatest;

if object_id('dbo.func_make_extended_acquisitiondate') is not NULL DROP FUNCTION dbo.func_make_extended_acquisitiondate;

IF (OBJECT_ID('dbo.proc_spectrum_info_insert') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_insert;
IF (OBJECT_ID('dbo.proc_spectrum_info_select') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_select;
IF (OBJECT_ID('dbo.proc_spectrum_info_select_where_account') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_select_where_account;
IF (OBJECT_ID('dbo.proc_spectrum_info_select_where_acquisitiondate_livetime') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_select_where_acquisitiondate_livetime;
IF (OBJECT_ID('dbo.proc_spectrum_info_select_latest') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_select_latest;
IF (OBJECT_ID('dbo.proc_spectrum_info_select_latest_where_account') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_select_latest_where_account;
IF (OBJECT_ID('dbo.proc_spectrum_info_select_latest_where_account_year') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_select_latest_where_account_year;
IF (OBJECT_ID('dbo.proc_spectrum_info_select_latest_where_acquisitiondate_livetime') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_select_latest_where_acquisitiondate_livetime;
IF (OBJECT_ID('dbo.proc_spectrum_info_count_id_where_acquisitiondate_livetime') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_count_id_where_acquisitiondate_livetime;
IF (OBJECT_ID('dbo.proc_spectrum_info_delete_where_id') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_info_delete_where_id;
IF (OBJECT_ID('dbo.proc_spectrum_background_insert') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_background_insert;
IF (OBJECT_ID('dbo.proc_spectrum_result_insert') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_result_insert;
IF (OBJECT_ID('dbo.proc_spectrum_file_insert') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_file_insert;
IF (OBJECT_ID('dbo.proc_spectrum_checksum_insert') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_checksum_insert;
IF (OBJECT_ID('dbo.proc_spectrum_checksum_count') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_checksum_count;
IF (OBJECT_ID('dbo.proc_spectrum_validation_rules_select') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_validation_rules_select;
IF (OBJECT_ID('dbo.proc_spectrum_validation_rules_insert') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_validation_rules_insert;
IF (OBJECT_ID('dbo.proc_spectrum_validation_rules_update') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_validation_rules_update;
IF (OBJECT_ID('dbo.proc_spectrum_geometry_rules_select') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_geometry_rules_select;
IF (OBJECT_ID('dbo.proc_spectrum_geometry_rules_insert') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_geometry_rules_insert;
IF (OBJECT_ID('dbo.proc_spectrum_geometry_rules_update') IS NOT NULL) DROP PROCEDURE dbo.proc_spectrum_geometry_rules_update;
go

create table SpectrumInfo
(
	ID uniqueidentifier not null primary key,
	AccountID uniqueidentifier not null,
	CreateDate datetime2 not null,
	UpdateDate datetime2 not null,
	AcquisitionDate datetime2 not null,
	ReferenceDate datetime2 default null,	
	Filename nvarchar(256) default null,
	BackgroundFile nvarchar(256) default null,
	LibraryFile nvarchar(256) default null,
	Sigma float default null,
	SampleType nvarchar(256) not null,
	Livetime int not null,	
	Laberatory nvarchar(128) default null,
	Operator nvarchar(128) default null,
	SampleComponent nvarchar(256) default null,
	Latitude float default null,
	Longitude float default null,
	Altitude float default null,
	LocationType nvarchar(128) default null,
	Location nvarchar(256) default null,	
	Community nvarchar(256) default null,
	SampleWeight float default null,
	SampleWeightUnit nvarchar(24) default null,
	SampleGeometry nvarchar(128) default null,
	ExternalID nvarchar(128) default null,
	AutoApproved bit default 0,
	Approved bit default 0,
	Rejected bit default 0,
	Comment nvarchar(256) default null
) 
go

create table SpectrumBackground
(
	ID uniqueidentifier not null primary key,
	SpectrumInfoID uniqueidentifier not null,
	CreateDate datetime2 not null,
	UpdateDate datetime2 not null,
	Energy float not null,
	OrigArea float default null,
	OrigAreaUncertainty float default null,
	SubtractedArea float default null,
	SubtractedAreaUncertainty float default null,	
	constraint FK_SpectrumBackground_SpectrumInfo 
		foreign key (SpectrumInfoID) 
		references SpectrumInfo(ID) 
		on delete cascade
) 
go

create table SpectrumResult
(
	ID uniqueidentifier not null primary key,
	SpectrumInfoID uniqueidentifier not null,
	CreateDate datetime2 not null,
	UpdateDate datetime2 not null,
	NuclideName nvarchar(24) not null,
	Confidence float default null,
	Activity float default null,
	ActivityUncertainty float default null,
	MDA float default null,	
	Evaluated bit default 0,
	AutoApproved bit default 0,
	Approved bit default 0,
	Rejected bit default 0,
	Comment nvarchar(256) default null,
	constraint FK_SpectrumResult_SpectrumInfo 
		foreign key (SpectrumInfoID) 
		references SpectrumInfo(ID) 
		on delete cascade
) 
go

create table SpectrumFile
(
	ID uniqueidentifier not null primary key,
	SpectrumInfoID uniqueidentifier not null,
	CreateDate datetime2 not null,
	UpdateDate datetime2 not null,
	SpectrumFileExtension nvarchar(16) not null,
	SpectrumFileContent varbinary(max) not null,
	ReportFileContent nvarchar(max) default null,
	constraint AK_SpectrumInfoID 
		unique(SpectrumInfoID),
	constraint FK_SpectrumFile_SpectrumInfo 
		foreign key (SpectrumInfoID) 
		references SpectrumInfo(ID) 
		on delete cascade
)
go

create table SpectrumChecksum
(		
	Sha256Sum char(64) not null primary key,
	SpectrumInfoID uniqueidentifier not null,
	constraint AK_Checksum 
		unique(Sha256Sum),
	constraint FK_SpectrumChecksum_SpectrumInfo 
		foreign key (SpectrumInfoID) 
		references SpectrumInfo(ID) 
		on delete cascade
)
go

create table SpectrumValidationRules
(		
	ID uniqueidentifier not null primary key,
	NuclideName nvarchar(24) not null,
	ActivityMin float default null,
	ActivityMax float default null,
	ConfidenceMin float default null,
	CanBeAutoApproved bit default 0
)
go

create table SpectrumGeometryRules
(		
	ID uniqueidentifier not null primary key,
	Geometry nvarchar(24) not null,
	Unit nvarchar(24) default null,
	Minimum float default null,
	Maximum float default null	
)
go

/* VIEWS */

create view SpectrumInfoLatest
as
	select *
	from 
	(
		select *, rank() over
		(
			partition by AcquisitionDate
			order by CreateDate desc
		) n
		from SpectrumInfo
	) m 
	where n = 1
go

/* FUNCTIONS */

create function func_make_extended_acquisitiondate(@AcquisitionDate datetime2, @Livetime int)
returns datetime2
as
begin    
	return dateadd(microsecond, @Livetime % 999999, cast(@AcquisitionDate as datetime2(0)))
end;  
go

/* PROCEDURES */

create proc proc_spectrum_info_insert
	@ID uniqueidentifier,
	@AccountID uniqueidentifier,
	@CreateDate datetime2,
	@UpdateDate datetime2,
	@AcquisitionDate datetime2(7),
	@ReferenceDate datetime2,
	@Filename nvarchar(256),
	@BackgroundFile nvarchar(256),
	@LibraryFile nvarchar(256),
	@Sigma float,
	@SampleType nvarchar(256),
	@Livetime int,	
	@Laberatory nvarchar(128),
	@Operator nvarchar(128),
	@SampleComponent nvarchar(256),
	@Latitude float,
	@Longitude float,
	@Altitude float,
	@LocationType nvarchar(128),
	@Location nvarchar(256),	
	@Community nvarchar(256),
	@SampleWeight float,
	@SampleWeightUnit nvarchar(24),
	@SampleGeometry nvarchar(128),
	@ExternalID nvarchar(128),	
	@AutoApproved bit,
	@Approved bit,
	@Rejected bit,
	@Comment nvarchar(256)
as	
	insert into SpectrumInfo(ID, AccountID, CreateDate, UpdateDate, AcquisitionDate, 
		ReferenceDate, Filename, BackgroundFile, LibraryFile, Sigma, SampleType, Livetime, Laberatory, Operator, 
		SampleComponent, Latitude, Longitude, Altitude, LocationType, 
		Location, Community, SampleWeight, SampleWeightUnit, 
		SampleGeometry, ExternalID, AutoApproved, Approved, Rejected, Comment)
	values(@ID, @AccountID, @CreateDate, @UpdateDate, 
		dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime), 
		@ReferenceDate, @Filename, @BackgroundFile, @LibraryFile, @Sigma, @SampleType, @Livetime, @Laberatory, @Operator, 
		@SampleComponent, @Latitude, @Longitude, @Altitude, @LocationType, 
		@Location, @Community, @SampleWeight, @SampleWeightUnit, 
		@SampleGeometry, @ExternalID, @AutoApproved, @Approved, @Rejected, @Comment)
go

create proc proc_spectrum_info_select
as 
	select * 
	from SpectrumInfo
	where Rejected = 0 
	order by CreateDate
go

create proc proc_spectrum_info_select_where_account
	@AccountID uniqueidentifier
as 
	select * 
	from SpectrumInfo 
	where Rejected = 0 and AccountID = @AccountID 
	order by CreateDate
go

create proc proc_spectrum_info_select_where_acquisitiondate_livetime
	@AcquisitionDate datetime2,
	@Livetime int
as 	
	select * 
	from SpectrumInfo 
	where Rejected = 0 and AcquisitionDate = dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime) 
	order by CreateDate
go

create proc proc_spectrum_info_select_latest
as 
	select * 
	from SpectrumInfoLatest
	where Rejected = 0
go

create proc proc_spectrum_info_select_latest_where_account
	@AccountID uniqueidentifier
as 
	select * 
	from SpectrumInfoLatest 
	where Rejected = 0 and AccountID = @AccountID 
	order by CreateDate
go

create proc proc_spectrum_info_select_latest_where_account_year
	@AccountID uniqueidentifier,
	@year int
as 
	select * 
	from SpectrumInfoLatest 
	where Rejected = 0 and AccountID = @AccountID and datepart(year, CreateDate) = @year 
	order by CreateDate
go

create proc proc_spectrum_info_select_latest_where_acquisitiondate_livetime
	@AcquisitionDate datetime2,
	@Livetime int
as 
	select top(1) * 
	from SpectrumInfoLatest 
	where Rejected = 0 and AcquisitionDate = dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime) 
	order by CreateDate
go

create proc proc_spectrum_info_count_id_where_acquisitiondate_livetime
	@AcquisitionDate datetime2,
	@Livetime int
as 
	select count(ID) 
	from SpectrumInfo 
	where Rejected = 0 and AcquisitionDate = dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime)	
go

create proc proc_spectrum_info_delete_where_id
	@ID uniqueidentifier
as
	delete from SpectrumInfo 
	where ID = @ID
go

create proc proc_spectrum_background_insert
	@ID uniqueidentifier,
	@SpectrumInfoID uniqueidentifier,
	@CreateDate datetime2,
	@UpdateDate datetime2,
	@Energy float,
	@OrigArea float,
	@OrigAreaUncertainty float,
	@SubtractedArea float,
	@SubtractedAreaUncertainty float
as 	
	insert into SpectrumBackground (ID, SpectrumInfoID, CreateDate, UpdateDate, Energy, 
		OrigArea, OrigAreaUncertainty, SubtractedArea, SubtractedAreaUncertainty)
	values(@ID, @SpectrumInfoID, @CreateDate, @UpdateDate, @Energy, @OrigArea, 
		@OrigAreaUncertainty, @SubtractedArea, @SubtractedAreaUncertainty)
go

create proc proc_spectrum_result_insert
	@ID uniqueidentifier,
	@SpectrumInfoID uniqueidentifier,
	@CreateDate datetime2,
	@UpdateDate datetime2,
	@NuclideName nvarchar(24),
	@Confidence float,
	@Activity float,
	@ActivityUncertainty float,
	@MDA float,
	@Evaluated bit,
	@AutoApproved bit,	
	@Approved bit,
	@Rejected bit,
	@Comment nvarchar(256)
as 	
	insert into SpectrumResult (ID, SpectrumInfoID, CreateDate, UpdateDate, NuclideName, 
		Confidence, Activity, ActivityUncertainty, MDA, Evaluated, AutoApproved, Approved, Rejected, Comment)
	values(@ID, @SpectrumInfoID, @CreateDate, @UpdateDate, @NuclideName, 
		@Confidence, @Activity, @ActivityUncertainty, @MDA, @Evaluated, @AutoApproved, @Approved, @Rejected, @Comment)
go

create proc proc_spectrum_file_insert
	@ID uniqueidentifier,
	@SpectrumInfoID uniqueidentifier,
	@CreateDate datetime2,
	@UpdateDate datetime2,
	@SpectrumFileExtension nvarchar(16),
	@SpectrumFileContent varbinary(max),
	@ReportFileContent nvarchar(max)
as 	
	insert into SpectrumFile (ID, SpectrumInfoID, CreateDate, UpdateDate, SpectrumFileExtension, SpectrumFileContent, ReportFileContent)
	values(@ID, @SpectrumInfoID, @CreateDate, @UpdateDate, @SpectrumFileExtension, @SpectrumFileContent, @ReportFileContent)
go

create proc proc_spectrum_checksum_insert
	@Sha256Sum char(64),
	@SpectrumInfoID uniqueidentifier
as 	
	insert into SpectrumChecksum (Sha256Sum, SpectrumInfoID) values(@Sha256Sum, @SpectrumInfoID)
go

create proc proc_spectrum_checksum_count
	@Sha256Sum char(64)	
as 	
	select count(*) from SpectrumChecksum where Sha256Sum = @Sha256Sum
go

create proc proc_spectrum_validation_rules_select
as 
	select * 
	from SpectrumValidationRules	
go

create proc proc_spectrum_validation_rules_insert
	@ID uniqueidentifier,
	@NuclideName nvarchar(24),
	@ActivityMin float,
	@ActivityMax float,
	@ConfidenceMin float,
	@CanBeAutoApproved bit
as 
	insert into SpectrumValidationRules (
		ID, 
		NuclideName, 
		ActivityMin, 
		ActivityMax, 
		ConfidenceMin, 
		CanBeAutoApproved
	) values (
		@ID, 
		@NuclideName, 
		@ActivityMin, 
		@ActivityMax, 
		@ConfidenceMin, 
		@CanBeAutoApproved
	)
go

create proc proc_spectrum_validation_rules_update
	@ID uniqueidentifier,
	@NuclideName nvarchar(24),
	@ActivityMin float,
	@ActivityMax float,
	@ConfidenceMin float,
	@CanBeAutoApproved bit
as 
	update SpectrumValidationRules set 
		NuclideName = @NuclideName, 
		ActivityMin = @ActivityMin, 
		ActivityMax = @ActivityMax, 
		ConfidenceMin = @ConfidenceMin, 
		CanBeAutoApproved = @CanBeAutoApproved
	where ID = @ID
go

create proc proc_spectrum_geometry_rules_select
as 
	select * 
	from SpectrumGeometryRules
go

create proc proc_spectrum_geometry_rules_insert
	@ID uniqueidentifier,
	@Geometry nvarchar(24),
	@Unit nvarchar(24),
	@Minimum float,
	@Maximum float	
as 
	insert into SpectrumGeometryRules (
		ID, 
		Geometry, 
		Unit, 
		Minimum, 
		Maximum		
	) values (
		@ID, 
		@Geometry, 
		@Unit, 
		@Minimum, 
		@Maximum		
	)
go

create proc proc_spectrum_geometry_rules_update
	@ID uniqueidentifier,
	@Geometry nvarchar(24),
	@Unit nvarchar(24),
	@Minimum float,
	@Maximum float	
as 
	update SpectrumGeometryRules set 
		Geometry = @Geometry, 
		Unit = @Unit, 
		Minimum = @Minimum, 
		Maximum = @Maximum		
	where ID = @ID
go
