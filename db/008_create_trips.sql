use vms;
go

create table dbo.trips
(
  id                  int             not null primary key identity,
  request_id          int             not null,
  started_at          datetime        not null,
  finished_at         datetime        null,
  mileage             numeric(8,1)    null,
  fuel_cost           numeric(8,1)    null,

  foreign key (request_id) references dbo.requests(id),
);
go

insert into dbo.trips (request_id, started_at, finished_at, mileage, fuel_cost)
 values
  (1, '2016-10-15T08:00:00', '2016-10-18T16:00:00', 100, 200);
go

