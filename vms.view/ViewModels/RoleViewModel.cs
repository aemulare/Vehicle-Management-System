using System;
using vms;

namespace Vms.ViewModels
{
   public class RoleViewModel
   {
      public RoleViewModel(Role role)
      {
         if(role == null)
            throw new ArgumentNullException(nameof(role));
         Role = role;
      }


      public Role Role { get; }


      public override string ToString() => Role.getRoleName();



      public override bool Equals(object other)
      {
         var otherRole = other as RoleViewModel;
         return Role.getId() == otherRole?.Role.getId();
      }



      public override int GetHashCode() => Role.getId().GetHashCode();
   }
}
