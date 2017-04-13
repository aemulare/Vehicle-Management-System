//=========================================================================================
// Person domain model.
// Contains a personal data of users or passengers.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "PersistentEntity.h"
#include "Address.h"

namespace vms
{
   public ref class Person : public PersistentEntity
   {
   public:
      enum class AgeCategory { ADULT, MINOR };

      Person();
      virtual ~Person();

      string getFirstName();
      string getLastName();
      string getFullName();
      Nullable<DateTime> getDOB();
      AgeCategory getAgeCategory();

      Address^ getAddress();
      string getPhoneNumber();
      string getEmail();
      string getDriverLicense();
      string getInsurance();

      void setFirstName(const string firstName);
      void setLastName(const string lastName);
      void setDOB(const Nullable<DateTime> dob);
      void setAgeCategory(AgeCategory ageCategory);
      void setPhoneNumber(const string phoneNumber);
      void setEmail(const string email);
      void setDriverLicense(const string driverLicense);
      void setInsurance(const string insurance);

   private:
      string _firstName;
      string _lastName;
      Nullable<DateTime> _dob;
      AgeCategory _ageCategory;

      Address^ _address;
      string _phoneNumber;
      string _email;
      string _driverLicense;
      string _insurance;
   };
}
