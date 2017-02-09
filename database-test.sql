
use SpectrumData
go

delete from SpectrumInfo

declare @specid uniqueidentifier

set @specid = NEWID()
exec dbo.proc_spectrum_info_insert @specid,'02.01.2016','01.01.2016','01.01.2001 12:00:00','01.01.2000','biota/hest',1000,'Østerås','drb','nakke',60.16,11.32,210.3,'Bruksnummer','12765456','Oslo, Oslo',12,'g','L1','321','Test kommentar...'

set @specid = NEWID()
exec dbo.proc_spectrum_info_insert @specid,'01.01.2016','01.01.2016','01.01.2001 12:00:00','01.01.2000','biota/hest',1000,'Østerås','drb','nakke',60.16,11.32,210.3,'Bruksnummer','12765456','Oslo, Oslo',12,'g','L1','321','Test kommentar...'

set @specid = NEWID()
exec dbo.proc_spectrum_info_insert @specid,'02.01.2016','02.01.2016','01.01.2001 13:10:03','01.02.2000','biota/ku',3600,'Hvitting','ola','nakke',61.16,12.1,209.3,'Bruksnummer','0938456','Oslo, Oslo',13,'g','L1','321','Test kommentar...'

set @specid = NEWID()
exec dbo.proc_spectrum_info_insert @specid,'02.01.2016','02.01.2016','01.01.2001 12:00:01','01.02.2000','biota/ku',500.45,'Hamar','drb','nakke',61.16,12.1,209.3,'Bruksnummer','0938456','Oslo, Oslo',13,'g','L1','321','Test kommentar...'


declare @resid uniqueidentifier
declare @createdate datetime2

select 
	@resid = NEWID(),
	@createdate = SYSDATETIME()

exec dbo.proc_spectrum_result_insert @resid, @specid, @createdate, @createdate, 'CS-137', 0.123, 0.002, null, 0, 0, ''


exec dbo.proc_spectrum_info_delete_where_id @specid