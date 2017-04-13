//=========================================================================================
// PassengerCar domain model.
// Represents a passenger car.
// Inherited from OwnedVehicle class.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "OwnedVehicle.h"

namespace vms
{
   public ref class PassengerCar : public OwnedVehicle
   {
   public:
      enum class PassengerCarType { SEDAN, COUPE, SUV, HATCHBACK, CROSSOVER, MINIVAN,
         PASSENGER_VAN, MINIBUS, COACH };

      PassengerCar();
      PassengerCar(OwnedVehicle^ vehicle);

      int getMaxPassengers();
      void setMaxPassengers(int passengersNumber);

      PassengerCarType getPassengerCarType();
      void setPassengerCarType(PassengerCarType carType);

      virtual bool allowForPersonalUse() override;

   private:
      int _maxPassengers;
      PassengerCarType _passCarType;
   };
}
