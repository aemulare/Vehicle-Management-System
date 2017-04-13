//=========================================================================================
// Vehicle domain model.
// A base abstract class for all kind of vehicles.
// Contains vehicle manufacturer properties.
//=========================================================================================
#include "Vehicle.h"

namespace vms
{
   // Default constructor
   // All string members of the class automatically initialized by their default constructors
   Vehicle::Vehicle() : _fuelType(FuelType::GASOLINE), _transmission(Transmission::AUTOMATIC),
      _yearManufactured(0), _sweptVolume(0), _fuelTankCapacity(0)
   {
   }


   // Copy constructor
   Vehicle::Vehicle(Vehicle^ vehicle) : _vin(vehicle->_vin), _maker(vehicle->_maker),
      _model(vehicle->_model), _fuelType(vehicle->_fuelType), _transmission(vehicle->_transmission),
      _yearManufactured(vehicle->_yearManufactured), _sweptVolume(vehicle->_sweptVolume),
      _fuelTankCapacity(vehicle->_fuelTankCapacity), _color(vehicle->_color)
   {
   }



   // Getter methods:

   // Returns VIN number
   string Vehicle::getVIN() { return _vin; }

   // Returns a vehicle maker
   string Vehicle::getMaker() { return _maker; }

   // Returns a vehicle model
   string Vehicle::getModel() { return _model; }

   // Returns a vehicle fuel type
   Vehicle::FuelType Vehicle::getFuelType() { return _fuelType; }

   // Returns a vehicle transmission type
   Vehicle::Transmission Vehicle::getTransmission() { return _transmission; }

   // Returns a year when a vehicle was manufactured
   int Vehicle::getYearManufactured() { return _yearManufactured; }

   // Returns a vehicle engine volume
   double Vehicle::getSweptVolume() { return _sweptVolume; }

   // Returns a vehicle fuel tank capacity
   double Vehicle::getFuelTankCapacity() { return _fuelTankCapacity; }

   // Returns a vehicle color
   string Vehicle::getColor() { return _color; }



   // Setter methods:

   // Sets a vehicle VIN number
   void Vehicle::setVIN(const string vin) { _vin = vin; }

   // Sets a vehicle maker
   void Vehicle::setMaker(const string maker) { _maker = maker; }

   // Sets a vehicle model
   void Vehicle::setModel(const string model) { _model = model; }

   // Sets a vehicle fuel type
   void Vehicle::setFuelType(FuelType fuel) { _fuelType = fuel; }

   // Sets a vehicle transmission type
   void Vehicle::setTransmission(Transmission transmission) { _transmission = transmission; }

   // Sets a year when a vehicle was manufactured
   void Vehicle::setYearManufactured(int year) { _yearManufactured = year; }

   // Sets a vehicle engine volume
   void Vehicle::setSweptVolume(double sweptVolume) { _sweptVolume = sweptVolume; }

   // Sets a vehicle fuel tank capacity
   void Vehicle::setFuelTankCapacity(double tankCapacity) { _fuelTankCapacity = tankCapacity; }

   // Sets a vehicle color
   void Vehicle::setColor(const string color) { _color = color; }


   // Vehicle string representation
   string Vehicle::toString()
   {
      return _maker + " " + _model;
   }
}
