//=========================================================================================
// Employee domain model.
// Represents a regular employee role (a base role implementation).
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "Role.h"

namespace vms
{
   public ref class Employee : public Role
   {
   public:
      virtual bool canApproveRequests() override;
      virtual bool canManageVehicles() override;
      virtual bool canManageUsers() override;
      virtual bool canCheckInVehicles() override;
      virtual bool canLockVehicles() override;
      virtual bool canDeleteUsers() override;
      virtual bool canDeleteVehicles() override;
      virtual bool canDeleteRequests() override;

      virtual string getRoleName() override;

   private:
      string _roleName;
   };
}
