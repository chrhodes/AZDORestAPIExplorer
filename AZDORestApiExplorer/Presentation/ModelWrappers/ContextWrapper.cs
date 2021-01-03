using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class ContextWrapper : ModelWrapper<Application.Context>
    {
        public ContextWrapper(Application.Context model) : base(model)
        {
        }

        public Domain.Core.Process SelectedProcess
        {
            get { return GetValue<Domain.Core.Process>(); }
            set { SetValue(value); }
        }

        public Project SelectedProject
        {
            get { return GetValue<Project>(); }
            set { SetValue(value); }
        }

        public Team SelectedTeam
        {
            get { return GetValue<Team>(); }
            set { SetValue(value); }
        }



        public WorkItemType SelectedWorkItemType
        {
            get { return GetValue<WorkItemType>(); }
            set { SetValue(value); }
        }

        public Domain.Dashboard.Dashboard SelectedDashboard
        {
            get { return GetValue<Domain.Dashboard.Dashboard>(); }
            set { SetValue(value); }
        }
    }
}
