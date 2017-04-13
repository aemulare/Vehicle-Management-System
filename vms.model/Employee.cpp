//=========================================================================================
// Employee domain model.
// Represents a regular employee role (a base role implementation).
//=========================================================================================
#include "Employee.h"

namespace vms
{
   // Determines whether a user with the given role can approve requests
   bool Employee::canApproveRequests() { return false; }

   // Determines whether a user with the given role can register/edit vehicles info
   bool Employee::canManageVehicles() { return false; }

   // Determines whether a user with the given role can register/edit user profiles
   bool Employee::canManageUsers() { return false; }

   // Determines whether a user with the given role can check in/check out vehicles
   bool Employee::canCheckInVehicles() { return false; }

   // Determines whether a user with the given role can lock/unlock vehicles
   bool Employee::canLockVehicles() { return false; }

   // Determines whether a user with the given role can delete user accounts and personal info
   bool Employee::canDeleteUsers() { return false; }

   // Determines whether a user with the given role can delete vehicles from the system
   bool Employee::canDeleteVehicles() { return false; }

   // Determines whether a user with the given role can delete requests from the system
   bool Employee::canDeleteRequests() { return false; }

   // Returns role name
   string Employee::getRoleName() { return _roleName; }
}
