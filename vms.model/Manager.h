//=========================================================================================
// Manager domain model.
// Represents a manager role in the system.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "Employee.h"

namespace vms
{
   public ref class Manager : public Employee
   {
   public:
      virtual bool canApproveRequests() override;
      virtual bool canLockVehicles() override;
   };
}
