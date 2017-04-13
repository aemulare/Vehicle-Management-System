//=========================================================================================
// Request domain model.
// Represents a request for company vehicle
//=========================================================================================
#include "Request.h"

namespace vms
{
   // Default constructor (used only by NHibernate infrastructure)
   Request::Request() : Request("", nullptr, nullptr)
   {
   }


   // Constructor
   Request::Request(const string requestId, const UserAccount^ requestor, OwnedVehicle^ vehicle)
      : _requestId(requestId), _requestor(requestor), _vehicle(vehicle), _forPersonalUse(false),
      _status(RequestStatus::INITIAL)
   {
      _passengers = gcnew List<const Person^>();
      _submittedAt = DateTime::Now;
   }


   // Destructor
   Request::~Request()
   {
      _passengers->Clear();
   }


   // Getter methods:

   // Returns a request ID
   string Request::getRequestId() { return _requestId; }

   // Returns a requested vehicle
   OwnedVehicle^ Request::getVehicle() { return _vehicle; }

   // Returns a request status
   Request::RequestStatus Request::getStatus() { return _status; }

   // Returns a request purpose
   string Request::getPurpose() { return _purpose; }

   // Returns a request designation
   string Request::getDestination() { return _destination; }

   // Returns a request content inventory
   string Request::getContentInventory() { return _contentInventory; }

   // Returns a planned trip start date and time
   DateTime Request::getPlannedTripStart() { return _plannedTripStart; }

   // Returns a planned trip end date and time
   DateTime Request::getPlannedTripEnd() { return _plannedTripEnd; }

   // Determines whether a request is intended for personal use
   bool Request::isForPersonalUse() { return _forPersonalUse; }

   // Returns a requested driver
   const UserAccount^ Request::getDriver() { return _driver; }

   // Returns a list of passenders
   const IList<const Person^>^ Request::getPassengers() { return _passengers; }

   // Returns a date when a request was submitted
   DateTime Request::getSubmittedAt() { return _submittedAt; }

   // Returns a requestor who submitted the request
   const UserAccount^ Request::getRequestor() { return _requestor; }

   // Determines whether a request is approved
   bool Request::isApproved() { return _status == RequestStatus::APPROVED; }

   // Returns a date when a request was approved
   Nullable<DateTime> Request::getApprovedAt() { return _approvedAt; }

   // Returns a user who approved the request
   const UserAccount^ Request::getApprover() { return _approver; }


   // Setter methods:


   // Sets a request ID.
   void Request::setRequestId(string requestId) { _requestId = requestId; }

   // Sets a vehicle for the request
   void Request::setVehicle(OwnedVehicle^ vehicle) { _vehicle = vehicle; }

   // Sets a requestor for the request
   void Request::setRequestor(const UserAccount^ requestor) { _requestor = requestor; }

   // Sets a request purpose
   void Request::setPurpose(const string purpose) { _purpose = purpose; }

   // Sets a request destination
   void Request::setDestination(const string destination) { _destination = destination; }

   // Sets a request content inventory
   void Request::setContentInventory(const string contentInventory) { _contentInventory = contentInventory; }

   // Sets a planned start date and time
   void Request::setPlannedTripStart(DateTime plannedTripStart) { _plannedTripStart = plannedTripStart; }

   // Sets a planned end date and time
   void Request::setPlannedTripEnd(DateTime plannedTripEnd) { _plannedTripEnd = plannedTripEnd; }

   // Sets the personal use flag
   void Request::setForPersonalUse(bool forPersonalUse) { _forPersonalUse = forPersonalUse; }

   // Sets a requested driver
   void Request::setDriver(const UserAccount^ driver) { _driver = driver; }

   // Adds a passenger
   void Request::addPassenger(const Person^ passenger) { _passengers->Add(passenger); }

   // Removes a passenger
   void Request::removePassenger(const Person^ passenger) { _passengers->Remove(passenger); }

   // Cleara a list of passengers
   void Request::clearPassengers() { _passengers->Clear(); }


   // Approves a request
   void Request::approve(const UserAccount^ approver)
   {
      _approver = approver;
      _approvedAt = DateTime::Now;
      _vehicle->lock();
      _status = RequestStatus::APPROVED;
   }


   // Declines a request
   void Request::decline()
   {
      _vehicle->unlock();
      _status = RequestStatus::DECLINED;
   }


   // Completes a request
   void Request::complete(double actualMileage)
   {
      _vehicle->unlock();
      _vehicle->setMileage(_vehicle->getMileage() + actualMileage);
      _status = RequestStatus::COMPLETED;
   }


   // Cancels a request
   void Request::cancel()
   {
      _vehicle->unlock();
      _status = RequestStatus::CANCELED;
   }
}
