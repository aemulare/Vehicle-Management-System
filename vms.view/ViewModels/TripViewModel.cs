using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using vms;
using Vms.Db.Services;
using Vms.Views;

namespace Vms.ViewModels
{
   /// <summary>
   /// Trip view model.
   /// Implements presentation logic for the trip.
   /// </summary>
   public class TripViewModel : ItemViewModel<Trip>
   {
      #region Private members

      private string startedOn;
      private string finishedOn;
      private int startedAtHour;
      private int finishedAtHour;

      private string requestId;
      private IList<Request> requests;

      #endregion

      #region Constructors

      public TripViewModel() : base(new Trip(), isNew: true)
      {
         StartedOn = DateTime.Today.ToString("MM/dd/yyyy");
         StartedAtHour = DateTime.Now.Hour;
         GarageAttendant = new UserViewModel(ClientSecurityContext.CurrentUser);
         StartDateMask = new DateMask();
         EndDateMask = new DateMask();
      }



      public TripViewModel(Trip trip) : base(trip)
      {
         Request = new RequestViewModel(Model.getRequest());
         RequestId = Request.RequestId;
         GarageAttendant = new UserViewModel(ClientSecurityContext.CurrentUser);

         var start = Model.getStartedAt();
         StartedOn = start.ToString("MM/dd/yyyy");
         StartedAtHour = start.Hour;

         var end = Model.getFinishedAt();
//         if(end == null && IsEnabled)
//            end = DateTime.Now;

         FinishedOn = end?.ToString("MM/dd/yyyy");
         FinishedAtHour = end?.Hour ?? DateTime.Now.Hour;

         StartDateMask = new DateMask();
         EndDateMask = new DateMask();
      }

      #endregion

      #region Public properties

      public string RequestId
      {
         get { return this.requestId; }
         set
         {
            this.requestId = value;
            var request = Requests.FirstOrDefault(r => r.getRequestId() == this.requestId);
            if(request != null)
            {
               Model.setRequest(request);
               Request = new RequestViewModel(request);
            }
            else
               Request = null;
            RaisePropertyChanged(() => RequestId);
            RaisePropertyChanged(() => Request);
         }
      }


      /// <summary>
      /// Request view model.
      /// </summary>
      public RequestViewModel Request { get; private set; }

      /// <summary>
      /// Garage attendant.
      /// </summary>
      public UserViewModel GarageAttendant { get; }


      public string StartedOn
      {
         get { return this.startedOn; }
         set
         {
            this.startedOn = value;
            UpdateStartedAt();
            RaisePropertyChanged(() => StartedOn);
            RaisePropertyChanged(() => Duration);
         }
      }


      public string StartedAt => Model.getStartedAt().ToString();


      public int StartedAtHour
      {
         get { return this.startedAtHour; }
         set
         {
            this.startedAtHour = value;
            UpdateStartedAt();
            RaisePropertyChanged(() => StartedAtHour);
            RaisePropertyChanged(() => Duration);
         }
      }



      public string FinishedOn
      {
         get { return this.finishedOn; }
         set
         {
            this.finishedOn = value;
            UpdateFinishedAt();
            RaisePropertyChanged(() => FinishedOn);
            RaisePropertyChanged(() => Duration);
         }
      }



      public string FinishedAt
      {
         get
         {
            var finishedAt = Model.getFinishedAt();
            return finishedAt?.ToString() ?? "";
         }
      }



      public int FinishedAtHour
      {
         get { return this.finishedAtHour; }
         set
         {
            this.finishedAtHour = value;
            UpdateFinishedAt();
            RaisePropertyChanged(() => FinishedAtHour);
            RaisePropertyChanged(() => Duration);
         }
      }



      /// <summary>
      /// Actual trip duration.
      /// </summary>
      public string Duration
      {
         get
         {
            var duration = Model.getFinishedAt() != null
               ? Model.getFinishedAt().Value - Model.getStartedAt()
               : TimeSpan.Zero;
            return $"{duration.Days} days, {duration.Hours} hours";
         }
      }



      public double Mileage
      {
         get { return Model.getMileage(); }
         set
         {
            Model.setMileage(value);
            RaisePropertyChanged(() => Mileage);
         }
      }



      public double FuelCost
      {
         get { return Model.getFuelCost(); }
         set
         {
            Model.setFuelCost(value);
            RaisePropertyChanged(() => FuelCost);
         }
      }



      /// <summary>
      /// Date mask.
      /// </summary>
      public DateMask StartDateMask { get; private set; }

      public DateMask EndDateMask { get; private set; }



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



      /// <summary>
      /// The title of 'Save' button.
      /// </summary>
      public string OperationTitle => IsNew ? "Check Out" : "Check In";

      /// <summary>
      /// Determines whether check-in related controls in read-only mode.
      /// </summary>
      public bool IsCheckinReadOnly => IsReadOnly || IsNew;

      /// <summary>
      /// Determines whether check-in related controls enabled.
      /// </summary>
      public bool IsCheckinEnabled => !IsCheckinReadOnly;

      /// <summary>
      /// Determines whether check-out related controls in read-only mode.
      /// </summary>
      public bool IsCheckoutReadOnly => IsReadOnly || !IsNew;

      /// <summary>
      /// Determines whether check-out related controls enabled.
      /// </summary>
      public bool IsCheckoutEnabled => !IsCheckoutReadOnly;

      #endregion

      #region Public methods

      /// <summary>
      /// Performs validation.
      /// </summary>
      public override void PerformValidate()
      {
         if(string.IsNullOrWhiteSpace(RequestId) || Request == null)
            SetValidationError(() => RequestId, "Request is not specified for the trip");
         if(string.IsNullOrWhiteSpace(StartedOn))
            SetValidationError(() => StartedOn, "Trip start date can't be blank");
         if(Request != null)
         {
            if(!Request.IsApproved)
               SetValidationError(() => RequestId, "Request is NOT approved for the trip");
            if(IsNew && Request.IsInProgress)
               SetValidationError(() => RequestId, "Request is in progress belong to another trip");
         }

         if(!IsNew)
         {
            if(string.IsNullOrWhiteSpace(FinishedOn))
               SetValidationError(() => FinishedOn, "Trip end date can't be blank");
            if(Mileage <= 0)
               SetValidationError(() => Mileage, "Mileage is not specified");
            if(FuelCost <= 0)
               SetValidationError(() => FuelCost, "Fuel cost is not specified");
         }
      }

      #endregion

      #region Internal properties

      /// <summary>
      /// Determines whether the given trip is started.
      /// </summary>
      internal bool IsStarted => Model.getId() != 0;

      /// <summary>
      /// Determines whether the given trip is completed.
      /// </summary>
      internal bool IsCompleted => Model.getFinishedAt() != null;

      /// <summary>
      /// Determines whether the given trip is in progress (started but not completed).
      /// </summary>
      internal bool IsInProgress => IsStarted && !IsCompleted;

      /// <summary>
      /// Displayed name (long form).
      /// </summary>
      internal override string LongName => $"trip for request '{Request.RequestId}'";

      /// <summary>
      /// Displayed name (short form).
      /// </summary>
      internal override string ShortName => "trip";

      #endregion

      #region Protected methods

      /// <summary>
      /// Performs save model operation.
      /// </summary>
      protected override void Save()
      {
         UpdateStartedAt();
         UpdateFinishedAt();
         RaisePropertyChanged(() => StartedAt);
         RaisePropertyChanged(() => FinishedAt);
         RaisePropertyChanged(() => Duration);

         if(IsNew)
         {
            Model.checkOut();
            Request.Start();
         }
         else
         {
            Model.checkIn(Mileage, FuelCost);
            Request.Complete();
         }
         base.Save();
      }



      /// <summary>
      /// Displays a confirmation message after save operation.
      /// </summary>
      protected override void Confirmation()
      {
         var msg = IsNew ? "out" : "in";
         var caption = IsNew ? "Out" : "In";
         MessageBox.Show($"The {LongName} has been checked {msg}.", $"Check {caption} {ShortName}",
            MessageBoxButton.OK, MessageBoxImage.Information);
      }

      #endregion

      #region Private methods

      private void UpdateStartedAt()
      {
         DateTime date;
         if(DateTime.TryParse(StartedOn, out date))
            Model.setStartedAt(date + TimeSpan.FromHours(StartedAtHour));
      }



      private void UpdateFinishedAt()
      {
         DateTime date;
         if(DateTime.TryParse(FinishedOn, out date))
            Model.setFinishedAt(date + TimeSpan.FromHours(FinishedAtHour));
         else
            Model.setFinishedAt(null);
      }



      private IList<Request> Requests
      {
         get
         {
            if(requests == null)
            {
               var svc = new PersistenceService();
               this.requests = svc.GetEntities<Request>();
            }
            return this.requests;
         }
      }

      #endregion
   }
}
