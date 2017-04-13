use vms;
go

create table dbo.requests
(
  id                    int             not null  primary key identity,
  code                  nvarchar(32)    not null,
  vehicle_id            int             not null,
  request_status        tinyint         not null  default 0,
  purpose               nvarchar(32)    not null,
  destination           nvarchar(64)    not null,
  planned_trip_start    datetime        not null,
  planned_trip_end      datetime        not null,
  for_personal_use      tinyint         not null,
  driver_id             int             not null,
  inventory_content     nvarchar(64)    null,
  submitted_at          datetime        not null,
  requestor_id          int             not null,
  approved_at           datetime        null,
  approver_id           int             null,

  foreign key (requestor_id) references dbo.user_accounts (id),
  foreign key (vehicle_id) references dbo.vehicles (id),
  foreign key (driver_id) references dbo.user_accounts (id),
  foreign key (approver_id) references dbo.user_accounts (id),

  check (for_personal_use between 0 and 1),
  check (request_status between 0 and 4),
  check (planned_trip_start < planned_trip_end),
  check (submitted_at < approved_at)
);
go


insert into dbo.requests (code, request_status, planned_trip_start, planned_trip_end, destination, purpose, for_personal_use,
  requestor_id, submitted_at, approver_id, approved_at, vehicle_id, driver_id, inventory_content)   
  values
  ('REQ2016.10.11.0001', 1, '2016-10-15T08:00:00', '2016-10-18T17:00:00', 'Washington DC', 'meeting with client', 0,
    6, '2016-10-10T14:25:10', 5, '2016-10-10T16:00:00', 3, 6, null),
  ('REQ2016.11.01.0005', 3, '2016-11-02T09:30:00', '2016-11-08T09:30:00', 'New York NY', 'business trip', 0,
  4, '2016-11-01T10:00:00', 6, '2016-11-01T11:00:00', 4, 4, null);
go
