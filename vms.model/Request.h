//=========================================================================================
// Request domain model.
// Represents a request for company vehicle
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "PersistentEntity.h"
#include "UserAccount.h"
#include "OwnedVehicle.h"

namespace vms
{
   public ref class Request : public PersistentEntity
   {
   public:
      // Available request statuses
      enum class RequestStatus { INITIAL, APPROVED, DECLINED, COMPLETED, CANCELED };

      Request();
      Request(const string requestId, const UserAccount^ requestor, OwnedVehicle^ vehicle);
      ~Request();

      string getRequestId();
      OwnedVehicle^ getVehicle();
      RequestStatus getStatus();

      string getPurpose();
      string getDestination();
      string getContentInventory();

      DateTime getPlannedTripStart();
      DateTime getPlannedTripEnd();
      bool isForPersonalUse();
      bool isApproved();

      const UserAccount^ getDriver();
      const IList<const Person^>^ getPassengers();

      DateTime getSubmittedAt();
      const UserAccount^ getRequestor();
      Nullable<DateTime> getApprovedAt();
      const UserAccount^ getApprover();

      void setRequestId(string requestId);
      void setVehicle(OwnedVehicle^ vehicle);
      void setRequestor(const UserAccount^ requestor);

      void setPurpose(const string purpose);
      void setDestination(const string destination);
      void setContentInventory(const string contentInventory);

      void setPlannedTripStart(DateTime plannedTripStart);
      void setPlannedTripEnd(DateTime plannedTripEnd);
      void setForPersonalUse(bool forPersonalUse);

      void setDriver(const UserAccount^ driver);
      void addPassenger(const Person^ passenger);
      void removePassenger(const Person^ passenger);
      void clearPassengers();

      void approve(const UserAccount^ approver);
      void decline();
      void complete(double actualMileage);
      void cancel();

   private:
      string _requestId;
      OwnedVehicle^ _vehicle;
      RequestStatus _status;

      string _purpose;
      string _destination;
      string _contentInventory;

      DateTime _plannedTripStart;
      DateTime _plannedTripEnd;
      bool _forPersonalUse;

      const UserAccount^ _driver;
      IList<const Person^>^ _passengers;

      DateTime _submittedAt;
      const UserAccount^ _requestor;

      Nullable<DateTime> _approvedAt;
      const UserAccount^ _approver;
   };
}
