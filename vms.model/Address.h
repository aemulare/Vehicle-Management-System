//=========================================================================================
// Address domain model.
// Contains a personal address info.
//=========================================================================================
#pragma once
#include "Stdafx.h"

namespace vms
{
   public ref class Address
   {
   public:
      Address();

      string getStreetAddress1();
      string getStreetAddress2();
      string getCity();
      string getState();
      string getZip();
      string getFullAddress();

      void setStreetAddress1(const string streetAddress1);
      void setStreetAddress2(const string streetAddress2);
      void setCity(const string city);
      void setState(const string state);
      void setZip(const string zip);

   private:
      string _streetAddress1;
      string _streetAddress2;
      string _city;
      string _state;
      string _zip;
   };
}
