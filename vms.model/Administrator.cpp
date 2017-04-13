//=========================================================================================
// Administrator domain model.
// Represents admin role in the system.
//=========================================================================================
#include "Administrator.h"

namespace vms
{
   // Determines whether a user with the given role can approve requests
   bool Administrator::canApproveRequests() { return true; }

   // Determines whether a user with the given role can register/edit vehicles info
   bool Administrator::canManageVehicles() { return true; }

   // Determines whether a user with the given role can register/edit user profiles
   bool Administrator::canManageUsers() { return true; }

   // Determines whether a user with the given role can check in/check out vehicles
   bool Administrator::canCheckInVehicles() { return true; }

   // Determines whether a user with the given role can lock/unlock vehicles
   bool Administrator::canLockVehicles() { return true; }

   // Determines whether a user with the given role can delete user accounts and personal info
   bool Administrator::canDeleteUsers() { return true; }

   // Determines whether a user with the given role can delete vehicles from the system
   bool Administrator::canDeleteVehicles() { return true; }

   // Determines whether a user with the given role can delete requests from the system
   bool Administrator::canDeleteRequests() { return true; }
}
