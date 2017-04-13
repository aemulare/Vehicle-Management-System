//=========================================================================================
// GarageAttendant domain model.
// Represents a garage attendant person.
//=========================================================================================
#include "GarageAttendant.h"

namespace vms
{
   // Determines whether a user with the given role can check in/check out vehicles
   bool GarageAttendant::canCheckInVehicles() { return true; }

   // Determines whether a user with the given role can lock/unlock vehicles
   bool GarageAttendant::canLockVehicles() { return true; }
}
