//=========================================================================================
// Administrator domain model.
// Represents admin role in the system.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "Employee.h"

namespace vms
{
   public ref class Administrator : public Employee
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
   };
}
