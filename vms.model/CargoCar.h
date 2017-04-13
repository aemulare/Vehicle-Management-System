//=========================================================================================
// CargoCar domain model.
// Represents a cargo car.
// Inherited from OwnedVehicle class.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "OwnedVehicle.h"

namespace vms
{
   public ref class CargoCar : public OwnedVehicle
   {
   public:
      enum class CargoCarType { MINITRUCK, PICKUP, DELIVERY_VAN, BOX_TRUCK, DUMP_TRUCK,
         TANK_TRUCK, SEMI_TRAILER, FULL_TRAILER, REFRIDGERATOR };

      CargoCar();
      CargoCar(OwnedVehicle^ vehicle);

      int getGrossVehicleMass();
      CargoCarType getCargoType();

      void setGrossVehicleMass(int gvm);
      void setCargoCarType(CargoCarType cargoType);

      virtual bool allowForPersonalUse() override;

   private:
      int _grossVehicleMass;        // same as gross vehicle weight rating (GVWR)
      CargoCarType _cargoType;
   };
}
