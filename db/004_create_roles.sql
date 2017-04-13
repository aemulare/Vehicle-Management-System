use vms;
go

create table dbo.roles
(
  id                  int           not null  primary key identity,
  role_type           tinyint       not null,
  role_name           nvarchar(32)  not null,
  
  check (role_type between 0 and 3)
);
go

insert into dbo.roles (role_type, role_name)
  values
  (0, 'employee'),
  (1, 'admin'),
  (2, 'manager'),
  (3, 'garage attendant');
go
