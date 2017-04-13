//=========================================================================================
// PassengerCar domain model.
// Represents a passenger car.
// Inherited from OwnedVehicle class.
//=========================================================================================
#include "PassengerCar.h"

namespace vms
{
   // Default constructor
   PassengerCar::PassengerCar() : _passCarType(PassengerCarType::SEDAN), _maxPassengers(0)
   {
   }


   // Copy constructor
   PassengerCar::PassengerCar(OwnedVehicle^ vehicle) : OwnedVehicle(vehicle),
      _passCarType(PassengerCarType::SEDAN), _maxPassengers(0)
   {
   }



   // Returns a max number of passengers
   int PassengerCar::getMaxPassengers() { return _maxPassengers; }


   // Returns a passenger car type
   PassengerCar::PassengerCarType PassengerCar::getPassengerCarType()
   {
      return _passCarType;
   }


   // Sets a max number of passengers
   void PassengerCar::setMaxPassengers(int passengersNumber)
   {
      _maxPassengers = passengersNumber;
   }


   // Sets a passenger car type
   void PassengerCar::setPassengerCarType(PassengerCarType passCarType)
   {
      _passCarType = passCarType;
   }


   // Determines whether a vehicle is allowed for personal use
   bool PassengerCar::allowForPersonalUse() { return true; }
}
