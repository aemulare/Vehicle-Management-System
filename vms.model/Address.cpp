//=========================================================================================
// Address domain model.
// Contains a personal address info.
//=========================================================================================
#include "Address.h"

namespace vms
{
   // Default constructor
   // No need to initialize all members explicitly here because there are no primitive types
   // defined in the Address class.
   // Default constructors called for non-primitive class members automatically.
   Address::Address()
   {
   }


   // Getter methods:

   // Returns an address line 1
   string Address::getStreetAddress1() { return _streetAddress1; }

   // Returns an address line 2
   string Address::getStreetAddress2() { return _streetAddress2; }

   // Returns a city
   string Address::getCity() { return _city; }

   // Returns a state code
   string Address::getState() { return _state; }

   // Returns a ZIP code value
   string Address::getZip() { return _zip; }

   // Setter methods:

   // Sets an address line 1
   void Address::setStreetAddress1(const string streetAddress1) { _streetAddress1 = streetAddress1; }

   // Sets an address line 2
   void Address::setStreetAddress2(const string streetAddress2) { _streetAddress2 = streetAddress2; }

   // Sets a city
   void Address::setCity(const string city) { _city = city; }

   // Sets a state code
   void Address::setState(const string state) { _state = state; }

   // Sets a ZIP code value
   void Address::setZip(const string zip) { _zip = zip; }



   // Returns a full address string combining all address parts
   string Address::getFullAddress()
   {
      return _streetAddress1 + " " + _streetAddress2 + " " +
         _city + " " + _state + ", " + _zip;
   }
}
