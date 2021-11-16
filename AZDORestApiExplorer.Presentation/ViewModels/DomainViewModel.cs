using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Net;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    //public class DomainViewModel<DType, EType, EArgs, SIEvent> : GridViewModelBase, IProcessMainViewModel, IInstanceCountVM
    //    where DType : class, new()
    //    where EType : Prism.Events.PubSubEvent<EArgs>, new()
    //    where SIEvent : Prism.Events.PubSubEvent<DType>, new()
    public class DomainViewModel<DType, EType, EArgs, SIEvent> : GridViewModelBase, IInstanceCountVM
        where DType : class, new()
        where EType : PubSubEvent<EArgs>, new()
        where SIEvent : PubSubEvent<DType>, new()
    {
        #region Constructors, Initialization, and Load

        public DomainViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            InstanceCountVM++;

            EventAggregator.GetEvent<EType>().Subscribe(GetList);

            this.Results.PropertyChanged += PublishSelectedItemChangedEvent;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public override void CollectionChanged(SelectedCollectionChangedEventArgs args)
        {
            var domainType = new DType();

            OutputFileNameAndPath = $@"C:\temp\{args.Collection.Name}-{domainType.GetType().Name}";
        }

        #endregion

        #region Enums (none)


        #endregion

        #region Structures (none)


        #endregion

        #region Fields and Properties

        public RESTResult<DType> Results { 
            get; 
            set; 
        } = new RESTResult<DType>();
        //public RESTResult<DType> Results { get; set; } = new RESTResult<DType>();

        #endregion

        #region Event Handlers (none)


        #endregion

        #region Public Methods (none)

        #endregion

        #region Protected Methods (none)


        #endregion

        #region Private Methods

        private async void GetList(EArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER($"Enter: ({args.GetType()})", Common.LOG_CATEGORY);

            try
            {
                // NOTE(crhodes)
                // This is easy

                var domainType = new DType();

                // Until we want to pass arguments to the DType Constructor

                //Type dType = typeof(DType);

                //var domainType = Activator.CreateInstance(dType, new object[] { EventAggregator, DialogService });

                MethodInfo getListMethod = domainType.GetType().GetMethod("GetList");

                Task<RESTResult<DType>> almostResults = (Task<RESTResult<DType>>)getListMethod.Invoke(domainType, new object[] { args });

                await almostResults;

                Results.ResultItems = almostResults.Result.ResultItems;

                // NOTE(crhodes)
                // Magic
                Results.ResultItem = almostResults.Result.ResultItem;

                Results.Count = Results.ResultItems.Count();
                Results.RequestResponseExchange = almostResults.Result.RequestResponseExchange;

            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_CATEGORY);
                ExceptionDialogService.DisplayExceptionDialog(DialogService, ex);
            }
            finally
            {
                // NOTE(crhodes)
                // Always publish the exchange so we can see what we were trying to access
                // TODO(crhodes)
                // May need to catch exceptions in getListMethod so can always return Results.
                EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);
            }

            Log.EVENT_HANDLER($"Exit: ({args.GetType()})", Common.LOG_CATEGORY, startTicks);
        }

        private void PublishSelectedItemChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            var p = e.PropertyName;

            if (e.PropertyName.Equals("SelectedItem"))
            {
                if (Results.SelectedItem is not null)
                {
                    Int64 startTicks = Log.EVENT($"Enter:({typeof(SIEvent).Name})", Common.LOG_CATEGORY);

                    EventAggregator.GetEvent<SIEvent>().Publish(Results.SelectedItem);

                    Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
                }
                else
                {
                    Int64 startTicks = Log.EVENT($"Enter(null)", Common.LOG_CATEGORY);

                    EventAggregator.GetEvent<SIEvent>().Publish(null);

                    Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
                }
            }
        }

        #endregion

        #region IInstanceCount

        private static int _instanceCountVM;

        public int InstanceCountVM
        {
            get => _instanceCountVM;
            set => _instanceCountVM = value;
        }

        #endregion
    }
}
