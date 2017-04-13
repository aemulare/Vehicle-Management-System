//=========================================================================================
// UtilityCar domain model.
// Represents an utility car.
// Inherited from OwnedVehicle class.
//=========================================================================================
#include "UtilityCar.h"

namespace vms
{
   // Default constructor
   UtilityCar::UtilityCar() : _utilityCarType(UtilityCarType::GARBAGE_TRUCK)
   {
   }


   // Copy constructor
   UtilityCar::UtilityCar(OwnedVehicle^ vehicle) : OwnedVehicle(vehicle),
      _utilityCarType(UtilityCarType::GARBAGE_TRUCK)
   {
   }



   // Returns an utility car type
   UtilityCar::UtilityCarType UtilityCar::getUtilityCarType()
   {
      return _utilityCarType;
   }


   // Sets an utility car type
   void UtilityCar::setUtilityCarType(UtilityCarType utilityType)
   {
      _utilityCarType = utilityType;
   }


   // Determines whether a vehicle is allowed for personal use
   bool UtilityCar::allowForPersonalUse() { return false; }
}
