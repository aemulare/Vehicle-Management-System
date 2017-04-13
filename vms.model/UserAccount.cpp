//=========================================================================================
// User account domain model.
// Contains a user account and role related into.
//=========================================================================================
#include "UserAccount.h"
#include "Employee.h"

namespace vms
{
   // Default constructor
   UserAccount::UserAccount() : UserAccount("", "", gcnew Employee(), gcnew Person())
   {
   }


   // Constructor
   // The memory for Employee and Person types allocated in the managed heap
   UserAccount::UserAccount(const string userId, const string password)
      : UserAccount(userId, password, gcnew Employee(), gcnew Person())
   {
   }


   // Constructor
   // The memory for Person type allocated in the managed heap
   UserAccount::UserAccount(const string userId, const string password, Role^ role)
      : UserAccount(userId, password, role, gcnew Person())
   {
   }


   // Constructor
   // The memory for Employee type allocated in the managed heap
   UserAccount::UserAccount(const string userId, const string password, Person^ person)
      : UserAccount(userId, password, gcnew Employee(), person)
   {
   }


   // Constructor
   UserAccount::UserAccount(const string userId, const string password, Role^ role, Person^ person)
      : _userId(userId), _password(password), _role(role), _person(person), _locked(false)
   {
   }


   // Destructor
   // No need to call delete for _role and _person here because it is managed code
   UserAccount::~UserAccount()
   {
      _role = nullptr;
      _person = nullptr;
   }


   // Getter methods:

   // Returns a user ID (login)
   string UserAccount::getUserId() { return _userId; }

   // Returns a user password
   string UserAccount::getPassword() { return _password; }

   // Returns a user role
   Role^ UserAccount::getRole() { return _role; }

   // Returns a personal user info
   Person^ UserAccount::getPerson() { return _person; }

   // Setter methods:

   // Sets user ID (login)
   void UserAccount::setUserId(const string userId) { _userId = userId; }

   // Sets the password
   void UserAccount::setPassword(const string password) { _password = password; }

   // Sets a user role
   void UserAccount::setRole(Role^ role)
   {
      _role = role;
   }


   // Performs a user authentication
   bool UserAccount::authenticate(const string password)
   {
      return _password == password;
   }


   // Determines whether the user is locked
   bool UserAccount::isLocked() { return _locked; }

   // Locks a user account
   void UserAccount::lock() { _locked = true; }

   // Unlock a user account
   void UserAccount::unlock() { _locked = false; }
}
