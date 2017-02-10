
/* DATABASE FOR STORING GAMMA SPECTRUMS AND RESULTS */

create database nrpa_lorakon
go

use nrpa_lorakon
go

/* TABLES */

create table SpectrumInfo
(
	ID uniqueidentifier not null primary key,
	CreateDate datetime2 not null,
	UpdateDate datetime2 not null,
	AcquisitionDate datetime2 not null,
	ReferenceDate datetime2 default null,	
	SampleType nvarchar(256) default null,
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
	Comment nvarchar(256) default null
) 
go

create table SpectrumResult
(
	ID uniqueidentifier not null primary key,
	SpectrumInfoID uniqueidentifier not null,
	CreateDate datetime2 not null,
	UpdateDate datetime2 not null,
	NuclideName nvarchar(24) not null,
	Activity float default null,
	ActivityUncertainty float default null,
	MDA float default null,
	Evaluated bit default 0,
	Approved bit default 0,
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
	FileExtension nvarchar(16) not null,
	FileContent varbinary(max) not null,
	constraint AK_SpectrumInfoID 
		unique(SpectrumInfoID),
	constraint FK_SpectrumFile_SpectrumInfo 
		foreign key (SpectrumInfoID) 
		references SpectrumInfo(ID) 
		on delete cascade
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
	@CreateDate datetime2,
	@UpdateDate datetime2,
	@AcquisitionDate datetime2(7),
	@ReferenceDate datetime2,
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
	@Comment nvarchar(256)
as	
	insert into SpectrumInfo(ID, CreateDate, UpdateDate, AcquisitionDate, 
		ReferenceDate, SampleType, Livetime, Laberatory, Operator, 
		SampleComponent, Latitude, Longitude, Altitude, LocationType, 
		Location, Community, SampleWeight, SampleWeightUnit, 
		SampleGeometry, ExternalID, Comment)
	values(@ID, @CreateDate, @UpdateDate, 
		dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime), 
		@ReferenceDate, @SampleType, @Livetime, @Laberatory, @Operator, 
		@SampleComponent, @Latitude, @Longitude, @Altitude, @LocationType, 
		@Location, @Community, @SampleWeight, @SampleWeightUnit, 
		@SampleGeometry, @ExternalID, @Comment)
go

create proc proc_spectrum_info_select
as 
	select * from SpectrumInfo
go

create proc proc_spectrum_info_select_where_acquisitiondate_livetime
	@AcquisitionDate datetime2,
	@Livetime int
as 	
	select * from SpectrumInfo 
	where AcquisitionDate = dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime)
go

create proc proc_spectrum_info_select_latest
as 
	select * from SpectrumInfoLatest
go

create proc proc_spectrum_info_select_latest_where_acquisitiondate_livetime
	@AcquisitionDate datetime2,
	@Livetime int
as 
	select top(1) * from SpectrumInfoLatest 
	where AcquisitionDate = dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime)
go

create proc proc_spectrum_info_count_id_where_acquisitiondate_livetime
	@AcquisitionDate datetime2,
	@Livetime int
as 
	select count(ID) from SpectrumInfo 
	where AcquisitionDate = dbo.func_make_extended_acquisitiondate(@AcquisitionDate, @Livetime)
go

create proc proc_spectrum_info_delete_where_id
	@ID uniqueidentifier
as
	delete from SpectrumInfo 
	where ID = @ID
go

create proc proc_spectrum_result_insert
	@ID uniqueidentifier,
	@SpectrumInfoID uniqueidentifier,
	@CreateDate datetime2,
	@UpdateDate datetime2,
	@NuclideName nvarchar(24),
	@Activity float,
	@ActivityUncertainty float,
	@MDA float,
	@Evaluated bit,
	@Approved bit,
	@Comment nvarchar(256)
as 	
	insert into SpectrumResult (ID, SpectrumInfoID, CreateDate, UpdateDate, NuclideName, Activity, 
		ActivityUncertainty, MDA, Evaluated, Approved, Comment)
	values(@ID, @SpectrumInfoID, @CreateDate, @UpdateDate, @NuclideName, @Activity, 
		@ActivityUncertainty, @MDA, @Evaluated, @Approved, @Comment)
go

create proc proc_spectrum_file_insert
	@ID uniqueidentifier,
	@SpectrumInfoID uniqueidentifier,
	@CreateDate datetime2,
	@UpdateDate datetime2,
	@FileExtension nvarchar(16),
	@FileContent varbinary(max)
as 	
	insert into SpectrumFile (ID, SpectrumInfoID, CreateDate, UpdateDate, FileExtension, FileContent)
	values(@ID, @SpectrumInfoID, @CreateDate, @UpdateDate, @FileExtension, @FileContent)
go

