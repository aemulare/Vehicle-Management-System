//=========================================================================================
// Trip domain model.
// Represents an actual trip info.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "Request.h"

namespace vms
{
   public ref class Trip : public PersistentEntity
   {
   public:
      Trip();
      Trip(Request^ request);

      Request^ getRequest();
      DateTime getStartedAt();
      Nullable<DateTime> getFinishedAt();

      double getMileage();
      double getFuelCost();

      void setRequest(Request^ request);
      void setStartedAt(DateTime startedAt);
      void setFinishedAt(Nullable<DateTime> finishedAt);

      void setMileage(double mileage);
      void setFuelCost(double fuelCost);

      void checkOut();
      void checkIn(double actualMileage, double fuelCost);

   private:
      Request^ _request;

      DateTime _startedAt;
      Nullable<DateTime> _finishedAt;

      double _mileage;
      double _fuelCost;
   };
}
