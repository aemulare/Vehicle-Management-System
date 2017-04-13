//=================================================================================================
// Class UserViewModel
// User view model.
// Implements a presentation logic for user and personal info.
//=================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using vms;
using Vms.Db.Services;

namespace Vms.ViewModels
{
   /// <summary>
   /// User view model.
   /// Implements a presentation logic for user and personal info.
   /// </summary>
   public sealed class UserViewModel : ItemViewModel<UserAccount>
   {
      #region Private members

      private readonly Person person;
      private string dateOfBirth;

      #endregion

      #region Constructors

      public UserViewModel() : this(new UserAccount())
      {
         IsNew = true;
         Model.setRole(AvailableRoles.First().Role);
      }



      public UserViewModel(UserAccount user) : base(user)
      {
         this.person = user.getPerson();
         var svc = new PersistenceService();
         AvailableRoles = from role in svc.GetEntities<Employee>() select new RoleViewModel(role);
         this.dateOfBirth = this.person.getDOB()?.ToShortDateString();
      }



      public UserViewModel(Person person) : base(null)
      {
         this.person = person;
         var dob = this.person.getDOB();
         this.dateOfBirth = dob?.ToShortDateString();
      }

      #endregion

      #region Public properties

      /// <summary>
      /// User ID.
      /// </summary>
      public string UserId
      {
         get { return Model?.getUserId(); }
         set
         {
            Model.setUserId(value);
            RaisePropertyChanged(() => UserId);
         }
      }



      public string Password
      {
         get { return Model?.getPassword(); }
         set
         {
            Model.setPassword(value);
            RaisePropertyChanged(() => Password);
         }
      }



      // The list of available roles.
      public IEnumerable<RoleViewModel> AvailableRoles { get; }



      /// <summary>
      /// Role name.
      /// </summary>
      public string Role => Model?.getRole().getRoleName();



      /// <summary>
      /// Selected user role.
      /// </summary>
      public RoleViewModel SelectedRole
      {
         get
         {
            var role = Model?.getRole();
            return role != null ? new RoleViewModel(role) : null;
         }
         set
         {
            if(value != null)
            {
               Model.setRole(value.Role);
               RaisePropertyChanged(() => Role);
            }
         }
      }



      // Determines whether a user is locked.
      public bool IsLocked
      {
         get { return Model?.isLocked() ?? false; }
         set
         {
            if(Model == null)
               return;
            if(value)
               Model.@lock();
            else
               Model.@unlock();
            RaisePropertyChanged(() => IsLocked);
         }
      }



      /// <summary>
      /// Person first name.
      /// </summary>
      public string FirstName
      {
         get { return this.person.getFirstName(); }
         set
         {
            this.person.setFirstName(value);
            RaisePropertyChanged(() => FirstName);
            RaisePropertyChanged(() => FullName);
         }
      }



      /// <summary>
      /// Person last name.
      /// </summary>
      public string LastName
      {
         get { return this.person.getLastName(); }
         set
         {
            this.person.setLastName(value);
            RaisePropertyChanged(() => LastName);
            RaisePropertyChanged(() => FullName);
         }
      }



      /// <summary>
      /// Person full name.
      /// </summary>
      public string FullName => $"{LastName}, {FirstName}";



      /// <summary>
      /// Date of birth.
      /// </summary>
      public string DateOfBirth
      {
         get { return this.dateOfBirth; }
         set
         {
            this.dateOfBirth = value;
            DateTime dob;
            if(DateTime.TryParse(this.dateOfBirth, out dob))
            {
               this.person.setDOB(dob);
               RaisePropertyChanged(() => DateOfBirth);
            }
            RaisePropertyChanged(() => AgeCategory);
         }
      }



      /// <summary>
      /// Person driver license.
      /// </summary>
      public string DriverLicense
      {
         get { return this.person.getDriverLicense(); }
         set
         {
            this.person.setDriverLicense(value);
            RaisePropertyChanged(() => DriverLicense);
         }
      }



      public string Insurance
      {
         get { return this.person.getInsurance(); }
         set
         {
            this.person.setInsurance(value);
            RaisePropertyChanged(() => Insurance);
         }
      }



      /// <summary>
      /// Person age category.
      /// </summary>
      public string AgeCategory
      {
         get
         {
            DateTime dob;
            if(DateTime.TryParse(DateOfBirth, out dob))
            {
               return (DateTime.Today.Year - dob.Year) < 18 ? "Minor" : "Adult";
            }
            return "";
         }
      }



      /// <summary>
      /// Person phone number.
      /// </summary>
      public string PhoneNumber
      {
         get { return this.person.getPhoneNumber(); }
         set
         {
            this.person.setPhoneNumber(value);
            RaisePropertyChanged(() => PhoneNumber);
         }
      }



      /// <summary>
      /// Email address.
      /// </summary>
      public string Email
      {
         get { return this.person.getEmail(); }
         set
         {
            this.person.setEmail(value);
            RaisePropertyChanged(() => Email);
         }
      }



      /// <summary>
      /// A full person address string representation.
      /// </summary>
      public string Address => this.person.getAddress().getFullAddress();



      /// <summary>
      /// Street address (line 1).
      /// </summary>
      public string AddressLine1
      {
         get { return this.person.getAddress().getStreetAddress1(); }
         set
         {
            this.person.getAddress().setStreetAddress1(value);
            RaisePropertyChanged(() => AddressLine1);
         }
      }



      /// <summary>
      /// Street address (line 2).
      /// </summary>
      public string AddressLine2
      {
         get { return this.person.getAddress().getStreetAddress2(); }
         set
         {
            this.person.getAddress().setStreetAddress2(value);
            RaisePropertyChanged(() => AddressLine2);
         }
      }



      /// <summary>
      /// City.
      /// </summary>
      public string City
      {
         get { return this.person.getAddress().getCity(); }
         set
         {
            this.person.getAddress().setCity(value);
            RaisePropertyChanged(() => City);
         }
      }



      /// <summary>
      /// US state code.
      /// </summary>
      public string StateCode
      {
         get { return this.person.getAddress().getState(); }
         set
         {
            this.person.getAddress().setState(value);
            RaisePropertyChanged(() => StateCode);
         }
      }



      /// <summary>
      /// ZIP code.
      /// </summary>
      public string Zip
      {
         get { return this.person.getAddress().getZip(); }
         set
         {
            this.person.getAddress().setZip(value);
            RaisePropertyChanged(() => Zip);
         }
      }

      #endregion

      #region Public methods

      public override void PerformValidate()
      {
         if(string.IsNullOrWhiteSpace(FirstName))
            SetValidationError(() => FirstName, "First name cannot be blank");

         if(string.IsNullOrWhiteSpace(LastName))
            SetValidationError(() => LastName, "Last name cannot be blank");

         if(string.IsNullOrWhiteSpace(UserId))
            SetValidationError(() => UserId, "User ID cannot be blank");

         if(string.IsNullOrWhiteSpace(Password))
            SetValidationError(() => Password, "Password cannot be blank");

         if(Model != null && string.IsNullOrWhiteSpace(Insurance))
            SetValidationError(() => Insurance, "Insurance cannot be blank");

         if(!string.IsNullOrWhiteSpace(Email) && !Email.Contains("@") && !Email.Contains("."))
            SetValidationError(() => Email, "Invalid email");

         if(!string.IsNullOrWhiteSpace(Zip))
         {
            int num;
            if(!int.TryParse(Zip, out num))
            {
               SetValidationError(() => Zip, "Invalid zip format");
            }
         }

         if(!string.IsNullOrWhiteSpace(DateOfBirth))
         {
            DateTime dob;
            if(!DateTime.TryParse(DateOfBirth, out dob))
            {
               SetValidationError(() => DateOfBirth, "Invalid date format");
            }
         }
      }

      #endregion

      #region Internal properties

      /// <summary>
      /// Displayed name (long form).
      /// </summary>
      internal override string LongName => $"user '{FullName}'";

      /// <summary>
      /// Displayed name (short form).
      /// </summary>
      internal override string ShortName => "user";

      /// <summary>
      /// Determines whether the given view model represents the currently registered user.
      /// </summary>
      internal bool IsCurrentUser => ClientSecurityContext.CurrentUser.getId() == Model.getId();

      #endregion
   }
}
