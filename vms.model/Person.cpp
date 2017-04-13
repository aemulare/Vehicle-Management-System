//=========================================================================================
// Person domain model.
// Contains a personal data of users or passengers.
//=========================================================================================
#include "Person.h"

namespace vms
{
   // Default constructor
   // No need to initialize all other members explicitly here because there are no primitive types
   // defined in the Person class.
   // Default constructors called for non-primitive class members automatically.
   Person::Person() : _ageCategory(AgeCategory::ADULT)
   {
      // The memory for Address type allocated in the managed heap (no need to call delete further)
      _address = gcnew Address();
   }


   // Destructor
   // No need to call delete for _address here because it is managed code with GC
   Person::~Person()
   {
      _address = nullptr;
   }


   // Getter methods:

   // Returns a first name
   string Person::getFirstName() { return _firstName; }

   // Returns a last name
   string Person::getLastName() { return _lastName; }

   // Returns a full person name as first name + last name concatenation
   string Person::getFullName() { return _lastName + ", " + _firstName; }

   // Returns a date of birth
   Nullable<DateTime> Person::getDOB() { return _dob; }

   // Returns a person age category
   Person::AgeCategory Person::getAgeCategory() { return _ageCategory; }

   // Returns a person address instance
   Address^ Person::getAddress() { return _address; }

   // Returns a phone number
   string Person::getPhoneNumber() { return _phoneNumber; }

   // Returns an email address
   string Person::getEmail() { return _email; }

   // Returns a driver license
   string Person::getDriverLicense() { return _driverLicense; }

   // Returns a request insurance
   string Person::getInsurance() { return _insurance; }


   // Setter methods:

   // Sets a first name
   void Person::setFirstName(const string firstName) { _firstName = firstName; }

   // Sets a last name
   void Person::setLastName(const string lastName) { _lastName = lastName; }

   // Sets a date of birth
   void Person::setDOB(const Nullable<DateTime> dob) { _dob = dob; }

   // Sets a person age category
   void Person::setAgeCategory(const Person::AgeCategory ageCategory)
   {
      _ageCategory = ageCategory;
   }

   // Sets a phone number
   void Person::setPhoneNumber(const string phoneNumber) { _phoneNumber = phoneNumber; }

   // Sets an email address
   void Person::setEmail(const string email) { _email = email; }

   // Sets a driver license
   void Person::setDriverLicense(const string driverLicense) { _driverLicense = driverLicense; }

   // Sets a request insurance
   void Person::setInsurance(const string insurance) { _insurance = insurance; }
}
