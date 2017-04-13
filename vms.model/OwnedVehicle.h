//=========================================================================================
// OwnedVehicle domain model.
// An abstract class representing owned vehicle.
// Contains vehicle assigned properties belong to owner.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "Vehicle.h"
#include "UserAccount.h"

namespace vms
{
   public ref class OwnedVehicle abstract : public Vehicle
   {
   protected:
      OwnedVehicle();
      OwnedVehicle(OwnedVehicle^ vehicle);

   public:
      string getPlate();
      double getMileage();
      bool isAvailable();

      void setPlate(const string plate);
      void setMileage(double mileage);

      virtual bool allowForPersonalUse() = 0;

      void lock();
      void unlock();

   private:
      string _plate;                      // plate number
      double _mileage;
      bool _isAvailable;
   };
}
