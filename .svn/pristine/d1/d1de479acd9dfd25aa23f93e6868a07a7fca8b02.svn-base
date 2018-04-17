
ALTER TABLE [CustomerContact]
ADD ContactImageOriginalFileName varchar(255) NULL

GO

ALTER TABLE [CustomerContact]
ADD ContactImageFileName AS CONVERT(varchar,CustomerContactId)+ '.'+ reverse(left(reverse(CONVERT(varchar,ContactImageOriginalFileName)),charindex('.',reverse(CONVERT(varchar,ContactImageOriginalFileName)))-1)) PERSISTED

