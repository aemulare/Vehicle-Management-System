use vms;
go

create table dbo.user_accounts
(
  id                  int           not null  primary key identity,
  user_account_id     nvarchar(32)  not null,
  user_password       nvarchar(32)  not null,
  role_id             int           not null,
  person_id           int           not null,
  is_locked           tinyint       not null  default 0,

  foreign key (person_id) references dbo.persons (id),
  foreign key (role_id) references dbo.roles (id),
);
go

insert into dbo.user_accounts (user_account_id, user_password, role_id, person_id)
  values
  ('Blonde', 'admin', 2, 1),
  ('Atticus', 'Mockingbird16', 3, 2),
  ('Aud', 'Moonriver29', 1, 3),
  ('Bobo', 'Misery48', 1, 4),
  ('Marcia', 'Mist59', 1, 5),
  ('King-of-Horror', 'writer47', 4, 6),
  ('Mors', 'kot', 1, 8)
go
