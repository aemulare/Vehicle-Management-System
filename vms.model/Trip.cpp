//=========================================================================================
// Trip domain model.
// Represents an actual trip info.
//=========================================================================================
#include "Trip.h"

namespace vms
{
   // Default constructor (required by Nhibernate library)
   Trip::Trip() : Trip(nullptr)
   {
   }


   // Constructor
   Trip::Trip(Request^ request) : _request(request), _mileage(0), _fuelCost(0)
   {
   }


   // Returns a request instance
   Request^ Trip::getRequest() { return _request; }

   // Returns a time when the request is actually started
   DateTime Trip::getStartedAt() { return _startedAt; }

   // Returns a time when the request is actually finished
   Nullable<DateTime> Trip::getFinishedAt() { return _finishedAt; }

   // Returns the actual trip mileage
   double Trip::getMileage() { return _mileage; }

   // Returns the actual tip fuel cost
   double Trip::getFuelCost() { return _fuelCost; }

   // Sets a request for the trip
   void Trip::setRequest(Request^ request) { _request = request; }

   // Sets a trip actual start date
   void Trip::setStartedAt(DateTime startedAt) { _startedAt = startedAt; }

   // Sets a trip actual finish date
   void Trip::setFinishedAt(Nullable<DateTime> finishedAt) { _finishedAt = finishedAt; }

   // Sets an actual mileage
   void Trip::setMileage(double mileage) { _mileage = mileage; }
   
   // Sets an actual fuel cost
   void Trip::setFuelCost(double fuelCost) { _fuelCost = fuelCost; }


   // Performs a trip check out (starts a trip)
   void Trip::checkOut()
   {
      _startedAt = DateTime::Now;
   }


   // Performs a trip check in (ends a trip)
   void Trip::checkIn(double actualMileage, double fuelCost)
   {
      _finishedAt = DateTime::Now;
      _mileage = actualMileage;
      _fuelCost = fuelCost;
      _request->complete(actualMileage);
   }
}
