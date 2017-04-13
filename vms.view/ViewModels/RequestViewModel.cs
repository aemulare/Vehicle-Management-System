using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using vms;
using Vms.Db.Services;
using Vms.Views;

namespace Vms.ViewModels
{
   /// <summary>
   /// Request view model.
   /// </summary>
   public class RequestViewModel : ItemViewModel<Request>
   {
      #region Private members

      private string vin;
      private string license;

      private IList<OwnedVehicle> vehicles;
      private IList<UserAccount> drivers;

      private string plannedStartDate;
      private string plannedEndDate;
      private int plannedStartHour;
      private int plannedEndHour;

      #endregion

      #region Constructors

      // for new request
      public RequestViewModel() : base(new Request(), isNew: true)
      {
         InitializeCommands();
         Model.setRequestId(GenerateRequestId());
         Model.setPlannedTripStart(DateTime.Today);
         Model.setPlannedTripEnd(DateTime.Today);
         Model.setRequestor(ClientSecurityContext.CurrentUser);
         Requestor = new UserViewModel(ClientSecurityContext.CurrentUser);
         DriverLicense = Requestor.DriverLicense;
         PlannedTripStart = PlannedTripEnd = DateTime.Today.ToString("MM/dd/yyyy");
         PlannedStartHour = 8;
         PlannedEndHour = 9;
         PlannedStartDateMask = new DateMask();
         PlannedEndDateMask = new DateMask();
      }


      // for existing request
      public RequestViewModel(Request request) : base(request)
      {
         InitializeCommands();
         Vehicle = new VehicleViewModel(Model.getVehicle());
         VinNumber = Vehicle.VinNumber;
         Requestor = new UserViewModel(Model.getRequestor());
         if(Model.isApproved())
            Approver = new UserViewModel(Model.getApprover());
         Driver = new UserViewModel(Model.getDriver());
         DriverLicense = Requestor.DriverLicense;
         var start = Model.getPlannedTripStart();
         var end = Model.getPlannedTripEnd();
         this.plannedStartDate = start.ToString("MM/dd/yyyy");
         this.plannedEndDate = end.ToString("MM/dd/yyyy");
         PlannedStartHour = start.Hour;
         PlannedEndHour = end.Hour;
         PlannedStartDateMask = new DateMask();
         PlannedEndDateMask = new DateMask();
         foreach(var passenger in Model.getPassengers())
            Passengers.Add(new Tuple<string, Person>(passenger.getFullName(), passenger));
      }

      #endregion

      #region WPF commands

      public ICommand AddPassengerCommand { get; private set; }

      public ICommand RemovePassengerCommand { get; private set; }

      public ICommand ClearPassengersCommand { get; private set; }

      /// <summary>
      /// Approves a request.
      /// </summary>
      public ICommand ApproveRequestCommand { get; private set; }

      /// <summary>
      /// Declines a request.
      /// </summary>
      public ICommand DeclineRequestCommand { get; private set; }

      #endregion

      #region Public properties

      /// <summary>
      /// Request ID.
      /// </summary>
      public string RequestId
      {
         get { return Model.getRequestId(); }
         set
         {
            Model.setRequestId(value);
            RaisePropertyChanged(() => RequestId);
         }
      }



      /// <summary>
      /// Request title.
      /// </summary>
      public string RequestTitle => $"Request: {RequestId}";


      /// <summary>
      /// Request status.
      /// </summary>
      public Request.RequestStatus Status => Model.getStatus();


      /// <summary>
      /// Request submission date.
      /// </summary>
      public string SubmittedAt => Model.getSubmittedAt().ToString();

      /// <summary>
      /// Requestor info.
      /// </summary>
      public string RequestorName => Model.getRequestor().getPerson().getFullName();


      /// <summary>
      /// Request approval date.
      /// </summary>
      public string ApprovedAt
      {
         get
         {
            var approvedAt = Model.getApprovedAt();
            return approvedAt > new DateTime(1870, 1, 1) ? approvedAt.ToString() : "";
         }
      }


      /// <summary>
      /// Request approver info.
      /// </summary>
      public string ApproverName => Model.getApprover()?.getPerson().getFullName();


      /// <summary>
      /// Vehicle
      /// </summary>
      public VehicleViewModel Vehicle { get; private set; }

      /// <summary>
      /// Requestor.
      /// </summary>
      public UserViewModel Requestor { get; }

      /// <summary>
      /// Request approver.
      /// </summary>
      public UserViewModel Approver { get; }

      /// <summary>
      /// Expected driver.
      /// </summary>
      public UserViewModel Driver { get; set; }


      /// <summary>
      /// Request insurance.
      /// </summary>
      public string Insurance => IsForPersonalUse ? Requestor.Insurance : "Company Insurance";



      /// <summary>
      /// Planned trip start.
      /// </summary>
      public string PlannedTripStart
      {
         get { return this.plannedStartDate; }
         set
         {
            this.plannedStartDate = value;
            UpdatePlannedStartDate();
            RaisePropertyChanged(() => PlannedTripStart);
            RaisePropertyChanged(() => Duration);
         }
      }



      public int PlannedStartHour
      {
         get { return this.plannedStartHour; }
         set
         {
            this.plannedStartHour = value;
            UpdatePlannedStartDate();
            RaisePropertyChanged(() => PlannedStartHour);
            RaisePropertyChanged(() => Duration);
         }
      }



      /// <summary>
      /// Planned trip end.
      /// </summary>
      public string PlannedTripEnd
      {
         get { return this.plannedEndDate; }
         set
         {
            this.plannedEndDate = value;
            UpdatePlannedEndDate();
            RaisePropertyChanged(() => PlannedTripEnd);
            RaisePropertyChanged(() => Duration);
         }
      }



      public int PlannedEndHour
      {
         get { return this.plannedEndHour; }
         set
         {
            this.plannedEndHour = value;
            UpdatePlannedEndDate();
            RaisePropertyChanged(() => PlannedEndHour);
            RaisePropertyChanged(() => Duration);
         }
      }



      /// <summary>
      /// Planned trip duration.
      /// </summary>
      public string Duration
      {
         get
         {
            var duration = (Model.getPlannedTripEnd() - Model.getPlannedTripStart());
            return $"{duration.Days} days, {duration.Hours} hours";
         }
      } 



      /// <summary>
      /// Request purpose.
      /// </summary>
      public string Purpose
      {
         get { return Model.getPurpose(); }
         set
         {
            Model.setPurpose(value);
            RaisePropertyChanged(() => Purpose);
         }
      }



      public string Destination
      {
         get { return Model.getDestination(); }
         set
         {
            Model.setDestination(value);
            RaisePropertyChanged(() => Destination);
         }
      }



      /// <summary>
      /// Determines whether the request is intended for personal use.
      /// </summary>
      public bool IsForPersonalUse
      {
         get { return Model.isForPersonalUse(); }
         set
         {
            Model.setForPersonalUse(value);
            RaisePropertyChanged(() => IsForPersonalUse);
            RaisePropertyChanged(() => Insurance);
         }
      }



      /// <summary>
      /// Request status.
      /// </summary>
      public string RequestStatus => Model.getStatus().ToString();


      /// <summary>
      /// Request status changed date.
      /// </summary>
      public string StatusChangedAt => Model.isApproved()
         ? Model.getApprovedAt().Value.ToShortDateString()
         : Model.getSubmittedAt().ToShortDateString();


      public string VinNumber
      {
         get { return this.vin; }
         set
         {
            this.vin = value;
            var vehicle = Vehicles.FirstOrDefault(v => v.getVIN() == this.vin);
            if(vehicle != null)
            {
               Model.setVehicle(vehicle);
               Vehicle = new VehicleViewModel(vehicle);
            }
            else
               Vehicle = null;
            RaisePropertyChanged(() => VinNumber);
            RaisePropertyChanged(() => Vehicle);
            RaisePropertyChanged(() => IsPassengerMode);
            RaisePropertyChanged(() => IsUtilityMode);
            RaisePropertyChanged(() => IsCargoMode);
            RaisePropertyChanged(() => ContentTitle);
         }
      }



      public string DriverLicense
      {
         get { return this.license; }
         set
         {
            this.license = value;
            if(!IsNew)
               return;
            var driver = Drivers.FirstOrDefault(d => d.getPerson().getDriverLicense() == this.license);
            if(driver != null)
            {
               Model.setDriver(driver);
               Driver = new UserViewModel(driver);
            }
            else
               Driver = null;
            RaisePropertyChanged(() => Driver);
         }
      }



      /// <summary>
      /// Date mask.
      /// </summary>
      public DateMask PlannedStartDateMask { get; private set; }
      public DateMask PlannedEndDateMask { get; private set; }


      public IEnumerable<string> Hours { get; set; } = new[]
      {
         "12 AM",
         "01 AM",
         "02 AM",
         "03 AM",
         "04 AM",
         "05 AM",
         "06 AM",
         "07 AM",
         "08 AM",
         "09 AM",
         "10 AM",
         "11 AM",
         "12 AM",
         "01 PM",
         "02 PM",
         "03 PM",
         "04 PM",
         "05 PM",
         "06 PM",
         "07 PM",
         "08 PM",
         "09 PM",
         "10 PM",
         "11 PM",
      };



      public ObservableCollection<Tuple<string,Person>> Passengers { get; } = new ObservableCollection<Tuple<string,Person>>();

      public Tuple<string,Person> SelectedPassenger { get; set; }

      public bool IsPassengerMode => Vehicle != null && Vehicle.IsPassengerCar;

      public bool IsNotPassengerMode => !IsPassengerMode;


      public bool IsCargoMode => Vehicle != null && Vehicle.IsCargoCar;

      public bool IsUtilityMode => Vehicle == null || Vehicle.IsUtilityCar;

      public string ContentTitle => IsPassengerMode ? "Passengers" : "Inventory Content";


      public string ContentInventory
      {
         get { return Model.getContentInventory(); }
         set
         {
            Model.setContentInventory(value);
            RaisePropertyChanged(() => ContentInventory);
         }
      }

      #endregion

      #region Public methods

      public override void PerformValidate()
      {
         if(string.IsNullOrWhiteSpace(PlannedTripStart))
            SetValidationError(() => PlannedTripStart, "Planned trip start date cannot be blank");
         else
         {
            DateTime date;
            if(!DateTime.TryParse(PlannedTripStart, out date))
               SetValidationError(() => PlannedTripStart, "Planned trip start invalid date format");
         }
         if(string.IsNullOrWhiteSpace(PlannedTripEnd))
            SetValidationError(() => PlannedTripEnd, "Planned trip end date cannot be blank");
         {
            DateTime date;
            if(!DateTime.TryParse(PlannedTripEnd, out date))
               SetValidationError(() => PlannedTripEnd, "Planned trip end invalid date format");
         }
         if(Model.getPlannedTripEnd() <= Model.getPlannedTripStart())
            SetValidationError(() => PlannedTripEnd, "Incorrect planned request dates interval");
         if(string.IsNullOrWhiteSpace(Purpose))
            SetValidationError(() => Purpose, "Request purpose cannot be blank");
         if(string.IsNullOrWhiteSpace(Destination))
            SetValidationError(() => Destination, "Request destination cannot be blank");
         if(string.IsNullOrWhiteSpace(VinNumber))
            SetValidationError(() => VinNumber, "VIN number cannot be blank");
      }



      public void ApproveRequest()
      {
         if(MessageBox.Show($"Do you want to approve the request '{RequestId}'?",
            "Approve request", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;

         Approve();
         MessageBox.Show($"The request '{RequestId}' has been approved.",
            "Approve request", MessageBoxButton.OK, MessageBoxImage.Information);
      }



      public void DeclineRequest()
      {
         if(MessageBox.Show($"Do you want to decline the request '{RequestId}'?",
            "Decline request", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;

         Decline();
         MessageBox.Show($"The request '{RequestId}' has been declined.",
            "Decline request", MessageBoxButton.OK, MessageBoxImage.Information);
      }



      /// <summary>
      /// Determines whether the request can be approved or declined.
      /// </summary>
      public bool CanApproveDeclineRequest => !IsNew && Status == Request.RequestStatus.INITIAL;

      /// <summary>
      /// Determines whether a current user is allowed to approve requests.
      /// </summary>
      public bool AllowApproveDecline => ClientSecurityContext.CurrentUser.getRole().canApproveRequests();

      #endregion

      #region Internal properties

      /// <summary>
      /// Determines whether the request belongs to the current user.
      /// </summary>
      internal bool IsBelongToCurrentUser =>
         Model.getRequestor().getId() == ClientSecurityContext.CurrentUser.getId();

      /// <summary>
      /// Displayed name (long form).
      /// </summary>
      internal override string LongName => $"request '{RequestId}'";

      /// <summary>
      /// Displayed name (short form).
      /// </summary>
      internal override string ShortName => "request";

      /// <summary>
      /// Determines whether the given request is approved.
      /// </summary>
      internal bool IsApproved => Model.isApproved();

      /// <summary>
      /// Determines whether the request is in progress with the checked out trip.
      /// </summary>
      internal bool IsInProgress => AppServices.AnyTripFor(Model);

      #endregion

      #region Internal methods

      /// <summary>
      /// Performs start request by checkout trip operation.
      /// </summary>
      internal void Start()
      {
         Vehicle.Checkout();
      }



      /// <summary>
      /// Performs complete request by checkin trip operation.
      /// </summary>
      internal void Complete()
      {
         AppServices.Save(Model);
         Vehicle.Checkin();
      }

      #endregion

      #region Private methods

      private void InitializeCommands()
      {
         AddPassengerCommand = new DelegateCommand(args => AddPassenger(), args => CanAddPassender);
         RemovePassengerCommand = new DelegateCommand(args => RemovePassenger(), args => CanRemovePassenger);
         ClearPassengersCommand = new DelegateCommand(args => ClearPassengers(), args => CanClearPassengers);
         ApproveRequestCommand = new DelegateCommand(args => ApproveRequest(), args => CanApproveDeclineRequest);
         DeclineRequestCommand = new DelegateCommand(args => DeclineRequest(), args => CanApproveDeclineRequest);
      }



      private IList<OwnedVehicle> Vehicles
      {
         get
         {
            if(vehicles == null)
            {
               var svc = new PersistenceService();
               this.vehicles = svc.GetEntities<OwnedVehicle>();
            }
            return this.vehicles;
         }
      }



      private IList<UserAccount> Drivers
      {
         get
         {
            if(this.drivers == null)
            {
               var svc = new PersistenceService();
               this.drivers = svc.GetEntities<UserAccount>();
            }
            return this.drivers;
         }
      }


      private static string GenerateRequestId()
      {
         var now = DateTime.Now;
         return $"REQ{now.Year}.{now.Month:D2}.{now.Day:D2}.{now.Second:D4}";
      }



      private void UpdatePlannedStartDate()
      {
         DateTime date;
         if(DateTime.TryParse(this.plannedStartDate, out date))
            Model.setPlannedTripStart(date + TimeSpan.FromHours(PlannedStartHour));
      }



      private void UpdatePlannedEndDate()
      {
         DateTime date;
         if(DateTime.TryParse(this.plannedEndDate, out date))
            Model.setPlannedTripEnd(date + TimeSpan.FromHours(PlannedEndHour));
      }



      private void AddPassenger()
      {
         var view = new NewPassengerView()
         {
            Owner = Application.Current.MainWindow,
         };
         if(view.ShowDialog() == true)
         {
            var firstName = view.edFirstName.Text.Trim();
            var lastName = view.edLastName.Text.Trim();
            var passenger = new Person();
            passenger.setLastName(lastName);
            passenger.setFirstName(firstName);
            Model.addPassenger(passenger);
            var item = new Tuple<string, Person>(passenger.getFullName(), passenger);
            Passengers.Add(item);
            SelectedPassenger = item;
         }
      }



      /// <summary>
      /// Determines whether the new passenger can be added to the request.
      /// </summary>
      private bool CanAddPassender => IsEnabled && Vehicle != null && Passengers.Count < Vehicle.MaxPassengersValue;



      private void RemovePassenger()
      {
         Model.removePassenger(SelectedPassenger.Item2);
         Passengers.Remove(SelectedPassenger);
      }



      private bool CanRemovePassenger => IsEnabled && SelectedPassenger != null;



      private void ClearPassengers()
      {
         Model.clearPassengers();
         Passengers.Clear();
      }


      private bool CanClearPassengers => IsEnabled && Passengers.Count > 0;



      /// <summary>
      /// Approves a request.
      /// </summary>
      private void Approve()
      {
         Model.approve(ClientSecurityContext.CurrentUser);
         var svc = new PersistenceService();
         svc.Save(Model);

         RaisePropertyChanged(() => Status);
         RaisePropertyChanged(() => StatusChangedAt);
         RaisePropertyChanged(() => RequestStatus);
         RaisePropertyChanged(() => ApprovedAt);
         RaisePropertyChanged(() => ApproverName);
      }



      /// <summary>
      /// Declines a request.
      /// </summary>
      private void Decline()
      {
         Model.decline();
         var svc = new PersistenceService();
         svc.Save(Model);

         RaisePropertyChanged(() => Status);
         RaisePropertyChanged(() => StatusChangedAt);
         RaisePropertyChanged(() => RequestStatus);
      }

      #endregion
   }
}
