//=========================================================================================
// GarageAttendant domain model.
// Represents a garage attendant person.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "Employee.h"

namespace vms
{
   public ref class GarageAttendant : public Employee
   {
   public:
      virtual bool canCheckInVehicles() override;
      virtual bool canLockVehicles() override;
   };
}
