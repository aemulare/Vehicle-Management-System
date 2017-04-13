//=========================================================================================
// CargoCar domain model.
// Represents a cargo car.
// Inherited from OwnedVehicle class.
//=========================================================================================
#include "CargoCar.h"

namespace vms
{
   // Default constructor
   CargoCar::CargoCar() : _grossVehicleMass(0), _cargoType(CargoCarType::DELIVERY_VAN)
   {
   }


   // Copy constructor
   CargoCar::CargoCar(OwnedVehicle^ vehicle) : OwnedVehicle(vehicle),
      _grossVehicleMass(0), _cargoType(CargoCarType::DELIVERY_VAN)
   {
   }



   // Returns a gross vehicle mass
   int CargoCar::getGrossVehicleMass() { return _grossVehicleMass; }

   // Returns a cargo car type 
   CargoCar::CargoCarType CargoCar::getCargoType() { return _cargoType; }

   // Sets a gross vehicle mass
   void CargoCar::setGrossVehicleMass(int gvm)
   {
      _grossVehicleMass = gvm;
   }


   // Sets a cargo car type
   void CargoCar::setCargoCarType(CargoCarType cargoType)
   {
      _cargoType = cargoType;
   }


   // Determines whether a vehicle is allowed for personal use
   bool CargoCar::allowForPersonalUse() { return false; }
}
