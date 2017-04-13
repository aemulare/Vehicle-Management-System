use vms;
go

create table dbo.passengers
(
  id                  int         not null primary key identity,
  person_id           int         not null,
  request_id          int         not null,

  foreign key (request_id) references dbo.requests (id),
  foreign key (person_id) references dbo.persons (id)
);
go
