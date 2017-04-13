//=========================================================================================
// Manager domain model.
// Represents a manager role in the system.
//=========================================================================================
#include "Manager.h"

namespace vms
{
   // Determines whether a user with the given role can approve requests
   bool Manager::canApproveRequests() { return true; }

   // Determines whether a user with the given role can lock/unlock vehicles
   bool Manager::canLockVehicles() { return true; }
}
