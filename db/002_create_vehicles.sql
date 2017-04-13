use vms;
go

create table dbo.vehicles
(
  id                  int           not null primary key identity,
  car_type            tinyint       not null,
  car_subtype         tinyint       not null,
  vin                 nchar(17)     not null unique,
  maker               nvarchar(32)  not null,
  model               nvarchar(64)  not null,
  color               nvarchar(64)  not null,
  fuel_type           tinyint       not null,
  transmission        tinyint       not null,
  year_manufactured   int           not null,
  swept_volume        numeric(3,1)  not null,
  fuel_tank_capacity  numeric(4,1)  not null,
  plate               nvarchar(8)   not null unique,
  mileage             numeric(8,1)  not null,
  is_available        tinyint       not null  default 1,
  max_passengers      tinyint       null,
  gross_mass          int           null,


  check (car_type between 0 and 2), -- PassengerCar, CargoCar, UtilityCar
  check (car_subtype between 0 and 8),
  check (fuel_type between 0 and 6),
  check (is_available between 0 and 1),
  check (transmission between 0 and 4),
  check (year_manufactured between 1900 and 2017)
);
go

-- passengers cars
insert into dbo.vehicles (car_type, car_subtype, vin, maker, model, color, fuel_type, transmission,
  year_manufactured, swept_volume, fuel_tank_capacity, plate, mileage, max_passengers)
values
(0, 0, '1J4FA29145P370908', 'Chevrolet', 'Corvette Stingray', 'Black', 2, 2, 2017, 6.2, 18.5, 'ZBX-3277', 50, 1),
(0, 1, '1G6DJ5EV8A0122772', 'Lincoln', 'Town Car', 'Silver', 2, 0, 2011, 4.6, 19, 'TPU-4421', 82576, 6),
(0, 2, '2D8GV58215H622257', 'Honda', 'Pilot EXL', 'Cherry', 0, 0, 2014, 3.5, 20, 'ERT-1842', 10000, 7),
(0, 3, '1G4GD5E33CF265088', 'Buick', 'Skyhawk Sport Turbo', 'White', 2, 2, 1987, 2, 12, 'QBS-0944', 150000, 4),
(0, 4, '1G1AK12F557663240', 'Mitsubishi', 'Outlander ES', 'Dark Grey', 2, 4, 2015, 2.4, 16.6, 'MSH-6919', 60000, 7),
(0, 5, '5N1AR1NBXBC608972', 'Honda', 'Odyssey LX', 'Dark Blue', 2, 0, 2014, 3.5, 21, 'DXT-7997', 105000, 7),
(0, 6, '2C4RDGCG1CR104195', 'Ford', 'Transit-350 XLT', 'Grey Metallic', 0, 0, 2015, 3.2, 26, 'HFU-0833', 45000, 12),
(0, 7, 'WDZPE8CC2C5616343', 'Mercedes-Benz', 'Sprinter', 'Silver', 0, 0, 2012, 3, 25, 'VMV-4188', 26643, 14),
(0, 8, 'JALC4B14417008124', 'Setra', 'TopClass S 417', 'Gold', 0, 0, 2011, 18.2, 180, 'OWL-6744', 43276, 56);

-- cargo cars
insert into dbo.vehicles (car_type, car_subtype, vin, maker, model, color, fuel_type, transmission,
  year_manufactured, swept_volume, fuel_tank_capacity, plate, mileage, gross_mass)
values
(1, 0, '1HGEJ8253YL143630', 'Mitsubishi', 'Triton GLX 2WD', 'White', 6, 1, 2012, 2.4, 75, 'RWB-7062', 94371, 2720),
(1, 1, '2D8GV57298H229869', 'Toyota', 'Tundra 1794 Edition', 'Red', 2, 0, 2016, 5.7, 38, 'RPX-1836', 35000, 7000);
go

-- utility car
insert into dbo.vehicles (car_type, car_subtype, vin, maker, model, color, fuel_type, transmission,
  year_manufactured, swept_volume, fuel_tank_capacity, plate, mileage)
values
(2, 2, 'WBAWV53547PW24706', 'ELGIN', 'Crosswind Sweeper', 'White', 1, 0, 2007, 5.9, 30, 'OOU-5798', 68366);
go
