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
            //EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(CollectionChanged);

            this.Results.PropertyChanged += PublishSelectedItemChanged;

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

        public RESTResult<DType> Results { get; set; } = new RESTResult<DType>();

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
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            try
            {
                // NOTE(crhodes)
                // This is easy

                var domainType = new DType();

                // Until we want to pass arguments to the DType Constructor

                //Type dType = typeof(DType);

                //var domainType = Activator.CreateInstance(dType, new object[] { EventAggregator, DialogService });


                MethodInfo getListMethod = domainType.GetType().GetMethod("GetList");

                Task<RESTResult<DType>> almostResults = (Task <RESTResult<DType>>)getListMethod.Invoke(domainType, new object[] { args });

                await almostResults;

                Results.ResultItems = almostResults.Result.ResultItems;
                Results.Count = Results.ResultItems.Count();
                //Results.RequestUri = almostResults.Result.RequestUri;
                Results.RequestResponseExchange = almostResults.Result.RequestResponseExchange;

            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_CATEGORY);
                ExceptionDialogService.DisplayExceptionDialog(DialogService, ex);
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);
            //EventAggregator.GetEvent<HttpUriEvent>().Publish(Results.RequestUri);

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void PublishSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SIEvent>().Publish(Results.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
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
