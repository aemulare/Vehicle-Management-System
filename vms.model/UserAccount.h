//=========================================================================================
// User account domain model.
// Contains a user account and role related into.
//=========================================================================================
#pragma once
#include "Stdafx.h"
#include "PersistentEntity.h"
#include "Person.h"
#include "Role.h"

namespace vms
{
   public ref class UserAccount : public PersistentEntity
   {
   public:
      UserAccount(); // Need default constructor because it requested by DB layer.
      UserAccount(const string userId, const string password);
      UserAccount(const string userId, const string password, Role^ role);
      UserAccount(const string userId, const string password, Person^ person);
      UserAccount(const string userId, const string password, Role^ role, Person^ person);
      ~UserAccount();

      string getUserId();
      string getPassword();
      Role^ getRole();
      Person^ getPerson();

      void setUserId(const string userId);
      void setPassword(const string password);
      void setRole(Role^ role);

      bool authenticate(const string password);
      bool isLocked();
      void lock();
      void unlock();

   private:
      string _userId;
      string _password;
      bool _locked;

      Person^ _person;
      Role^ _role;
   };
}
