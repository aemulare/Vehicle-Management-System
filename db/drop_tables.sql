use vms
go

if object_id('dbo.passengers', 'U') is not null
  drop table dbo.passengers;

if object_id('dbo.trips', 'U') is not null
  drop table dbo.trips;

if object_id('dbo.requests', 'U') is not null
  drop table dbo.requests;

if object_id('dbo.user_accounts', 'U') is not null
  drop table dbo.user_accounts;

if object_id('dbo.roles', 'U') is not null
  drop table dbo.roles;

if object_id('dbo.persons', 'U') is not null
  drop table dbo.persons;

if object_id('dbo.vehicles', 'U') is not null
  drop table dbo.vehicles;
go

