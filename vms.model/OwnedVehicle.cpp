//=========================================================================================
// OwnedVehicle domain model.
// An abstract class representing owned vehicle.
// Contains vehicle assigned properties belong to owner.
//=========================================================================================
#include "OwnedVehicle.h"

namespace vms
{
   // Default constructor
   // All string members of the class automatically initialized by their default constructors
   OwnedVehicle::OwnedVehicle() : _mileage(0), _isAvailable(true)
   {
   }


   // Copy constructor
   OwnedVehicle::OwnedVehicle(OwnedVehicle^ vehicle) : Vehicle(vehicle),
      _plate(vehicle->_plate), _mileage(vehicle->_mileage), _isAvailable(vehicle->_isAvailable)
   {
   }



   // Getter methods:

   // Returns a plate number
   string OwnedVehicle::getPlate() { return _plate; }

   // Returns a current mileage value
   double OwnedVehicle::getMileage() { return _mileage; }

   // Determines whether a vehicle is available for requests
   bool OwnedVehicle::isAvailable() { return _isAvailable; }


   // Setter methods:

   // Sets a vehicle plate number
   void OwnedVehicle::setPlate(const string plate) { _plate = plate; }

   // Sets a vehicle mileage
   void OwnedVehicle::setMileage(double mileage) { _mileage = mileage; }

   // Locks a vehicle
   void OwnedVehicle::lock() { _isAvailable = false; }

   // Unlocks a vehicle
   void OwnedVehicle::unlock() { _isAvailable = true; }
}
