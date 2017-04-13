//=========================================================================================
// UtilityCar domain model.
// Represents an utility car.
// Inherited from OwnedVehicle class.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "OwnedVehicle.h"

namespace vms
{
   public ref class UtilityCar : public OwnedVehicle
   {
   public:
      enum class UtilityCarType { TOW_TRUCK, PLOW_TRUCK, SWEEPER_TRUCK, GARBAGE_TRUCK};

      UtilityCar();
      UtilityCar(OwnedVehicle^ vehicle);

      UtilityCarType getUtilityCarType();
      void setUtilityCarType(UtilityCarType utilityType);

      virtual bool allowForPersonalUse() override;

   private:
      UtilityCarType _utilityCarType;
   };
}
