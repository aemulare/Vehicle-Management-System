use vms;
go

create table dbo.persons
(
  id                  int           not null  primary key identity,
  first_name          nvarchar(32)  not null,
  last_name           nvarchar(32)  not null,
  dob                 date          null,
  driver_license      nvarchar(9)   null,
  insurance           nvarchar(64)  null,
  phone_number        nvarchar(32)  null,
  email               nvarchar(64)  null,
  street_address_1    nvarchar(64)  null,
  street_address_2    nvarchar(64)  null,
  city                nvarchar(64)  null,
  us_state            nchar(2)      null,
  zip                 nchar(5)      null,

  check (dob > '18700101'),
);
go

insert into dbo.persons (first_name, last_name, dob, driver_license, insurance, phone_number, email,
            street_address_1, street_address_2, city, us_state, zip)
values
  ('Marilyn', 'Monroe', '1926-06-01', 'V4882817', 'GEICO 123', '619-307-1279', 'marylin@hollywood.com', '6676 Santa Monica Blvd', 'apt. 8B', 'Los Angeles', 'CA', '90038'),
  ('Gregory', 'Peck', '1916-04-05', 'G6968088', 'Allstate 783', '951-349-8652', 'gregory.peck@hollywood.com', '6676 Santa Monica Blvd', 'apt. 8C', 'Los Angeles', 'CA', '90038'),
  ('Audrey', 'Hepburn', '1929-05-04','D6965681', 'Progressive 23874', '707-486-0606', 'tiffany@hollywood.com', '6676 Santa Monica Blvd', 'apt. 8A', 'Los Angeles', 'CA', '90038'),
  ('Kathy', 'Bates', '1948-06-28', '554641480', 'Amica 899', '865-254-0167', 'favorite.actress@StephenKing.com', '27 E 69th St', 'apt. 3B', 'New York', 'NY', '10021'),
  ('Marcia', 'Harden', '1959-08-14', '46562061', 'State Farm 235', '956-970-6288', 'actress@hollywood.com', '505 W 22nd St', 'apt. 1A', 'Austin', 'TX', '78705'),
  ('Stephen', 'King', '1947-09-21', '5028865', '21st Century 242', '207-714-4609', 'me@StephenKing.com', '47 West Broadway Street', null, 'Bangor', 'ME', '04401'),
  ('Gwen', 'Hvostataya', '2011-04-27', null, null, null, null, '21 Goodall street', null, 'Staten Island', 'NY', '10308'),
  ('Morfey', 'Dreamy', '1993-09-05', null, null, '+7 (924) 336-0146', 'mors@firstcat.org', '138 Pacific Ocean street', '312', 'Khabarovsk', 'KH', '68003');
go