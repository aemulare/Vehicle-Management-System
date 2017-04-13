//=================================================================================================
// Class VehicleViewModel
// Vehicle view model.
// Implements a presentation logic regarding vehicles.
//=================================================================================================
using System;
using System.Collections.Generic;
using vms;
using Vms.Views;

namespace Vms.ViewModels
{
   /// <summary>
   /// Vehicle view model.
   /// </summary>
   public class VehicleViewModel : ItemViewModel<OwnedVehicle>
   {
      #region Private members

      private string engineVolume;
      private string yearManufactured;
      private string tankCapacity;
      private string mileage;
      private string maxPassengers;
      private string grossMass;

      #endregion

      #region Constructors

      public VehicleViewModel() : this(new PassengerCar())
      {
         IsNew = true;
      }



      public VehicleViewModel(OwnedVehicle vehicle) : base(vehicle)
      {
         InitializeVehicle();
      }

      #endregion

      #region Public properties

      /// <summary>
      /// A vehicle category.
      /// </summary>
      public string VehicleCategory
      {
         get
         {
            if(IsPassengerCar)
               return "Passenger car";
            if(IsCargoCar)
               return "Cargo car";
            if(IsUtilityCar)
               return "Utility car";
            throw new NotSupportedException("Unknown vehicle type");
         }
      }



      /// <summary>
      /// A car type string representation.
      /// </summary>
      public string CarType
      {
         get
         {
            if(IsPassengerCar)
               return ((PassengerCar)Model).getPassengerCarType().GetDisplayName();
            if(IsCargoCar)
               return ((CargoCar)Model).getCargoType().GetDisplayName();
            if(IsUtilityCar)
               return ((UtilityCar)Model).getUtilityCarType().GetDisplayName();
            throw new NotSupportedException("Unknown vehicle type");
         }
         set
         {
            if(IsPassengerCar)
            {
               var carType = EnumHelper.GetEnumElement<PassengerCar.PassengerCarType>(value);
               if(carType != null)
                  ((PassengerCar)Model).setPassengerCarType(carType.Value);
            }
            if(IsCargoCar)
            {
               var carType = EnumHelper.GetEnumElement<CargoCar.CargoCarType>(value);
               if(carType != null)
                  ((CargoCar)Model).setCargoCarType(carType.Value);
            }
            if(IsUtilityCar)
            {
               var carType = EnumHelper.GetEnumElement<UtilityCar.UtilityCarType>(value);
               if(carType != null)
                  ((UtilityCar)Model).setUtilityCarType(carType.Value);
            }
            RaisePropertyChanged(() => CarType);
         }
      }



      public IEnumerable<string> CarTypeValues
      {
         get
         {
            if(IsPassengerCar)
               return EnumHelper.GetEnumDisplayNames<PassengerCar.PassengerCarType>();
            if(IsCargoCar)
               return EnumHelper.GetEnumDisplayNames<CargoCar.CargoCarType>();
            if(IsUtilityCar)
               return EnumHelper.GetEnumDisplayNames<UtilityCar.UtilityCarType>();

            throw new NotSupportedException("Unknown vehicle type");
         }
      }



      /// <summary>
      /// Vehicle VIN number
      /// </summary>
      public string VinNumber
      {
         get { return Model.getVIN(); }
         set
         {
            Model.setVIN(value);
            RaisePropertyChanged(() => VinNumber);
         }
      }



      /// <summary>
      /// Vehicle maker.
      /// </summary>
      public string Maker
      {
         get { return Model.getMaker(); }
         set
         {
            Model.setMaker(value);
            RaisePropertyChanged(() => Maker);
         }
      }



      /// <summary>
      /// Vehicle model.
      /// </summary>
      public string VehicleModel
      {
         get { return Model.getModel(); }
         set
         {
            Model.setModel(value);
            RaisePropertyChanged(() =>VehicleModel);
         }
      }



      /// <summary>
      /// Vehicle color.
      /// </summary>
      public string Color
      {
         get { return Model.getColor(); }
         set
         {
            Model.setColor(value);
            RaisePropertyChanged(() => Color);
         }
      }



      /// <summary>
      /// The collection of available fuel types.
      /// </summary>
      public IEnumerable<string> FuelTypes => EnumHelper.GetEnumDisplayNames<Vehicle.FuelType>();



      /// <summary>
      /// Vehicle fuel type.
      /// </summary>
      public string FuelType
      {
         get { return Model.getFuelType().GetDisplayName(); }
         set
         {
            var val = EnumHelper.GetEnumElement<Vehicle.FuelType>(value);
            if(val != null)
               Model.setFuelType(val.Value);
            RaisePropertyChanged(() => FuelType);
         }
      }



      /// <summary>
      /// The collection of available transmission types.
      /// </summary>
      public IEnumerable<string> TransmissionTypes => EnumHelper.GetEnumDisplayNames<Vehicle.Transmission>();



      /// <summary>
      /// Vehicle transmission.
      /// </summary>
      public string Transmission
      {
         get { return Model.getTransmission().GetDisplayName(); }
         set
         {
            var val = EnumHelper.GetEnumElement<Vehicle.Transmission>(value);
            if(val != null)
               Model.setTransmission(val.Value);
            RaisePropertyChanged(() => Transmission);
         }
      }



      /// <summary>
      /// A year when the car was manufactured.
      /// </summary>
      public string YearManufactured
      {
         get { return this.yearManufactured; }
         set
         {
            this.yearManufactured = value;
            int year;
            if(int.TryParse(this.yearManufactured, out year) && year > 0)
            {
               Model.setYearManufactured(year);
               RaisePropertyChanged(() => YearManufactured);
            }
         }
      }



      /// <summary>
      /// The engine volume.
      /// </summary>
      public string EngineVolume
      {
         get { return this.engineVolume; }
         set
         {
            this.engineVolume = value;
            double volume;
            if(double.TryParse(this.engineVolume, out volume))
            {
               Model.setSweptVolume(volume);
               RaisePropertyChanged(() => EngineVolume);
            }
         }
      }



      /// <summary>
      /// Fuel tank capacity.
      /// </summary>
      public string FuelTankCapacity
      {
         get { return this.tankCapacity; }
         set
         {
            this.tankCapacity = value;
            double capacity;
            if(double.TryParse(tankCapacity, out capacity) && capacity > 3)
            {
               Model.setFuelTankCapacity(capacity);
               RaisePropertyChanged(() => FuelTankCapacity);
            }
         }
      }



      /// <summary>
      /// Vehicle plate.
      /// </summary>
      public string Plate
      {
         get { return Model.getPlate(); }
         set
         {
            Model.setPlate(value);
            RaisePropertyChanged(() => Plate);
         }
      }



      /// <summary>
      /// Vehicle current mileage value.
      /// </summary>
      public string Mileage
      {
         get { return this.mileage; }
         set
         {
            this.mileage = value;
            double val;
            if(double.TryParse(value, out val))
            {
               Model.setMileage(val);
               RaisePropertyChanged(() => Mileage);
            }
         }
      }



      /// <summary>
      /// Determines whether a vehicle is available for requests.
      /// </summary>
      public bool IsAvailable
      {
         get { return Model.isAvailable(); }
         set
         {
            if(value)
               Model.@unlock();
            else
               Model.@lock();
            RaisePropertyChanged(() => IsAvailable);
         }
      }



      /// <summary>
      /// A max number of passangers.
      /// </summary>
      public string MaxPassengers
      {
         get { return this.maxPassengers; }
         set
         {
            this.maxPassengers = value;
            int num;
            if(IsPassengerCar && int.TryParse(value, out num) && num > 0)
            {
               ((PassengerCar)Model).setMaxPassengers(num);
               RaisePropertyChanged(() => MaxPassengers);
            }
         }
      }



      /// <summary>
      /// Cargo car gross mass.
      /// </summary>
      public string GrossMass
      {
         get { return this.grossMass; }
         set
         {
            this.grossMass = value;
            int gross;
            if(IsCargoCar && int.TryParse(value, out gross) && gross > 0)
            {
               ((CargoCar)Model).setGrossVehicleMass(gross);
               RaisePropertyChanged(() => GrossMass);
            }
         }
      }



      /// <summary>
      /// Determines whether the vehicle is a passenger car.
      /// </summary>
      public bool IsPassengerCar
      {
         get { return VehicleType == typeof(PassengerCar); }
         set
         {
            Model = new PassengerCar(Model);
            InitializeVehicle();
            UpdateVehicleType(typeof(PassengerCar));
            ClearValidationErrors(() => MaxPassengers);
            ClearValidationErrors(() => GrossMass);
         }
      }



      /// <summary>
      /// Determines whether the vehicle is a cargo car.
      /// </summary>
      public bool IsCargoCar
      {
         get { return VehicleType == typeof(CargoCar); }
         set
         {
            Model = new CargoCar(Model);
            InitializeVehicle();
            UpdateVehicleType(typeof(CargoCar));
            ClearValidationErrors(() => MaxPassengers);
            ClearValidationErrors(() => GrossMass);
         }
      }



      /// <summary>
      /// Determines whether the vehicle is an utility car.
      /// </summary>
      public bool IsUtilityCar
      {
         get { return VehicleType == typeof(UtilityCar); }
         set
         {
            Model = new UtilityCar(Model);
            InitializeVehicle();
            UpdateVehicleType(typeof(UtilityCar));
            ClearValidationErrors(() => MaxPassengers);
            ClearValidationErrors(() => GrossMass);
         }
      }

      #endregion

      #region Public methods

      public override void PerformValidate()
      {
         if(string.IsNullOrWhiteSpace(VinNumber))
            SetValidationError(() => VinNumber, "VIN number cannot be blank");
         else if(VinNumber.Length < 17)
            SetValidationError(() => VinNumber, "Invalid VIN number");

         if(string.IsNullOrWhiteSpace(Maker))
            SetValidationError(() => Maker, "Maker cannot be blank");
         if(string.IsNullOrWhiteSpace(VehicleModel))
            SetValidationError(() => VehicleModel, "Model cannot be blank");
         if(string.IsNullOrWhiteSpace(Color))
            SetValidationError(() => Color, "Color cannot be blank");
         if(string.IsNullOrWhiteSpace(Plate))
            SetValidationError(() => Plate, "Plate number cannot be blank");

         if(string.IsNullOrWhiteSpace(YearManufactured))
            SetValidationError(() => YearManufactured, "Year manufactored cannot be blank");
         else
         {
            int date;
            if(!int.TryParse(YearManufactured, out date))
               SetValidationError(() => YearManufactured, "Invalid year format");
            else if(date < 1900 || date > DateTime.Today.Year)
               SetValidationError(() => YearManufactured, "Invalid year manufactured range");
         }

         if(string.IsNullOrWhiteSpace(EngineVolume))
            SetValidationError(() => EngineVolume, "Engine size cannot be blank");
         else
         {
            double volume;
            if(!double.TryParse(EngineVolume, out volume) || volume < 0.5)
               SetValidationError(() => EngineVolume, "Invalid engine volume value");
         }

         if(string.IsNullOrWhiteSpace(FuelTankCapacity))
            SetValidationError(() => FuelTankCapacity, "Fuel tank capacity cannot be blank");
         else
         {
            double capacity;
            if(!double.TryParse(FuelTankCapacity, out capacity) || capacity < 3)
               SetValidationError(() => FuelTankCapacity, "Invalid fuel tank capacity value");
         }

         if(string.IsNullOrWhiteSpace(Mileage))
            SetValidationError(() => Mileage, "Mileage cannot be blank");
         else
         {
            double mil;
            if(!double.TryParse(Mileage, out mil) || mil < 0)
               SetValidationError(() => Mileage, "Invalid mileage value");
         }
         if(IsPassengerCar)
         {
            if(string.IsNullOrWhiteSpace(MaxPassengers))
               SetValidationError(() => MaxPassengers, "Max passengers number cannot be blank");
            int num;
            if(!int.TryParse(MaxPassengers, out num) || num < 1)
               SetValidationError(() => MaxPassengers, "Invalid max passengers value");
         }
         if(IsCargoCar)
         {
            if(string.IsNullOrWhiteSpace(GrossMass))
               SetValidationError(() => GrossMass, "Gross vehicle mass cannot be blank");
            int gross;
            if(!int.TryParse(GrossMass, out gross) || gross < 0)
               SetValidationError(() => GrossMass, "Invalid vehicle gross mass value");
         }
      }

      #endregion

      #region Internal properties

      /// <summary>
      /// Displayed name (long form).
      /// </summary>
      internal override string LongName => $"vehicle '{Maker} {VehicleModel}'";

      /// <summary>
      /// Displayed name (short form).
      /// </summary>
      internal override string ShortName => "vehicle";

      /// <summary>
      /// Returns the max number of passengers.
      /// </summary>
      internal int MaxPassengersValue => IsPassengerCar ? ((PassengerCar)Model).getMaxPassengers() : 0;

      #endregion

      #region Internal methods

      /// <summary>
      /// Checks out the vehicle.
      /// </summary>
      internal void Checkout()
      {
         IsAvailable = false;
         AppServices.Save(Model);
      }



      /// <summary>
      /// Checks in the vehicle.
      /// </summary>
      internal void Checkin()
      {
         IsAvailable = true;
//         Model.setMileage(Model.getMileage() + actualMileage);
         AppServices.Save(Model);
      }

      #endregion

      #region Private properties

      /// <summary>
      /// The actual vehicle type.
      /// </summary>
      private Type VehicleType { get; set; }

      #endregion

      #region Private methods

      private void UpdateVehicleType(Type vehicleType)
      {
         VehicleType = vehicleType;
         RaisePropertyChanged(() => IsPassengerCar);
         RaisePropertyChanged(() => IsCargoCar);
         RaisePropertyChanged(() => IsUtilityCar);
         RaisePropertyChanged(() => CarType);
         RaisePropertyChanged(() => CarTypeValues);
      }



      private void InitializeVehicle()
      {
         VehicleType = Model.GetType();
         var volume = Model.getSweptVolume();
         this.engineVolume = volume > 0 ? volume.ToString() : null;
         var year = Model.getYearManufactured();
         this.yearManufactured = year > 0 ? year.ToString() : null;
         var capacity = Model.getFuelTankCapacity();
         this.tankCapacity = capacity > 0 ? capacity.ToString() : null;
         var m = Model.getMileage();
         this.mileage = m > 0 ? m.ToString() : null;
         if(IsPassengerCar)
         {
            var num = ((PassengerCar)Model).getMaxPassengers();
            this.maxPassengers = num > 0 ? num.ToString() : null;
         }
         if(IsCargoCar)
         {
            var gross = ((CargoCar)Model).getGrossVehicleMass();
            this.grossMass = gross > 0 ? gross.ToString() : null;
         }
      }

      #endregion
   }
}
