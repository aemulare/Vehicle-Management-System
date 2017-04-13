//=========================================================================================
// Role domain model.
// A base abstract class for available user roles hierarchy.
// Determines available permissions for users.
// All user role classes should be inherited from Role.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "PersistentEntity.h"

namespace vms
{
   public ref class Role abstract : public PersistentEntity
   {
   public:
      virtual bool canApproveRequests() = 0;
      virtual bool canManageVehicles() = 0;
      virtual bool canManageUsers() = 0;
      virtual bool canCheckInVehicles() = 0;
      virtual bool canLockVehicles() = 0;
      virtual bool canDeleteUsers() = 0;
      virtual bool canDeleteVehicles() = 0;
      virtual bool canDeleteRequests() = 0;

      virtual string getRoleName() = 0;
   };
}
