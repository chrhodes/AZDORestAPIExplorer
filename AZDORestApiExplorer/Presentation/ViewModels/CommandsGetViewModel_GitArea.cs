using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain.Build;
using AZDORestApiExplorer.Domain.Build.Events;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Core.Events;
using AZDORestApiExplorer.Domain.Dashboard.Events;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Git.Events;
using AZDORestApiExplorer.Domain.Graph.Events;
using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Domain.Tokens.Events;
using AZDORestApiExplorer.Domain.WorkItemTracking;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess.Events;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    // NOTE(crhodes)
    // This file contains the Fields, Properties, and Methods for the GitArea
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {
        #region Fields and Properties

        #region Git.Items

        private string _RecursionLevel = "None";
        private string _ScopePath = "/";

        public string RecursionLevel
        {
            get => _RecursionLevel;
            set
            {
                if (_RecursionLevel == value) return;
                _RecursionLevel = value;
                OnPropertyChanged();
            }
        }

        public string RecursionLevelToolTip { get; set; }

        public string ScopePath
        {
            get => _ScopePath;
            set
            {
                if (_ScopePath == value) return;
                _ScopePath = value;
                OnPropertyChanged();
            }
        }

        public string ScopePathToolTip { get; set; }


        private bool _includePullRequestIterationCommits;

        public bool IncludePullRequestIterationCommits
        {
            get => _includePullRequestIterationCommits;
            set
            {
                if (_includePullRequestIterationCommits == value) return;
                _includePullRequestIterationCommits = value;
                OnPropertyChanged();
            }
        }

        #endregion Git.Items

        #endregion Fields and Properties

        #region Private Methods

        private void CallPullRequestDependentStuff(Domain.Git.PullRequest pullRequest)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            //return;
            if (!(pullRequest is null))
            {
                // NOTE(crhodes)
                // When Pull Request changes, clear out Context that depends on Pull Request

                _contextMainViewModel.Context.SelectedPullRequestThread = null;

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestCommitsEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestCommitsEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestIterationsEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestIterationsEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestPropertiesEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestPropertiesEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestReviewersEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestReviewersEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestThreadsEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestThreadsEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestWorkItemsEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestWorkItemsEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                return;

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestAttachmentsEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestAttachmentsEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestStatusesEvent>().Publish(
                    new Domain.Git.Events.GetPullRequestStatusesEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        PullRequest = pullRequest
                    });

                Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
            }
        }

        #region GetRepositories Command

        public DelegateCommand GetRepositoriesCommand { get; set; }
        public string GetRepositoriesContent { get; set; } = "Repositories";
        public string GetRepositoriesToolTip { get; set; } = "Get Repositories ToolTip";

        // Can get fancy and use Resources
        //public string GetAccountsContent { get; set; } = "ViewName_GetAccountsContent";
        //public string GetAccountsToolTip { get; set; } = "ViewName_GetAccountsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAccountsContent">GetAccounts</system:String>
        //    <system:String x:Key="ViewName_GetAccountsContentToolTip">GetAccounts ToolTip</system:String>

        public void GetRepositoriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetRepositoriesEvent>().Publish(
                new Domain.Git.Events.GetRepositoriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    //, Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetRepositoriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetRepositories Command

        #region GetProjectRepositories Command

        public DelegateCommand GetProjectRepositoriesCommand { get; set; }
        public string GetProjectRepositoriesContent { get; set; } = "Project Repositories";
        public string GetProjectRepositoriesToolTip { get; set; } = "Get Project Repositories ToolTip";

        // Can get fancy and use Resources
        //public string GetAccountsContent { get; set; } = "ViewName_GetAccountsContent";
        //public string GetAccountsToolTip { get; set; } = "ViewName_GetAccountsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAccountsContent">GetAccounts</system:String>
        //    <system:String x:Key="ViewName_GetAccountsContentToolTip">GetAccounts ToolTip</system:String>



        public void GetProjectRepositoriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetRepositoriesEvent>().Publish(
                new Domain.Git.Events.GetRepositoriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetProjectRepositoriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetProjectRepositories Command

        #region GetBlobs Command

        public DelegateCommand GetBlobsCommand { get; set; }
        public string GetBlobsContent { get; set; } = "Get Blobs";
        public string GetBlobsToolTip { get; set; } = "Get Blobs ToolTip";

        // Can get fancy and use Resources
        //public string GetBlobsContent { get; set; } = "ViewName_GetBlobsContent";
        //public string GetBlobsToolTip { get; set; } = "ViewName_GetBlobsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBlobsContent">GetBlobs</system:String>
        //    <system:String x:Key="ViewName_GetBlobsContentToolTip">GetBlobs ToolTip</system:String>

        public void GetBlobs()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetBlobsEvent>().Publish(
                new Domain.Git.Events.GetBlobsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBlobsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBlobs Command

        #region GetCommits Command

        public DelegateCommand GetCommitsCommand { get; set; }
        public string GetCommitsContent { get; set; } = "GetCommits";
        public string GetCommitsToolTip { get; set; } = "GetCommits ToolTip";

        // Can get fancy and use Resources
        //public string GetCommitsContent { get; set; } = "ViewName_GetCommitsContent";
        //public string GetCommitsToolTip { get; set; } = "ViewName_GetCommitsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetCommitsContent">GetCommits</system:String>
        //    <system:String x:Key="ViewName_GetCommitsContentToolTip">GetCommits ToolTip</system:String>

        public void GetCommits()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetCommitsEvent>().Publish(
                new Domain.Git.Events.GetCommitsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetCommitsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetCommits Command

        #region GetCommitChanges Command

        public DelegateCommand GetCommitChangesCommand { get; set; }

        public string GetCommitChangesContent { get; set; } = "Commit Changes";

        public string GetCommitChangesToolTip { get; set; } = "Get Commit Changes ToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetCommitChangesContent">GetCommitChanges</system:String>
        //    <system:String x:Key="ViewName_GetCommitChangesContentToolTip">GetCommitChanges ToolTip</system:String>

        // Can get fancy and use Resources
        //public string GetCommitChangesContent { get; set; } = "ViewName_GetCommitChangesContent";
        //public string GetCommitChangesToolTip { get; set; } = "ViewName_GetCommitChangesContentToolTip";

        private void CallGetCommitChange(Commit commit)
        {
            if (!(commit is null))
            {
                EventAggregator.GetEvent<Domain.Git.Events.GetCommitChangesEvent>().Publish(
                new Domain.Git.Events.GetCommitChangesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    Commit = commit
                });
            }
        }

        private void CallGetCommitChange(PullRequestCommit commit)
        {
            if (!(commit is null))
            {
                var newCommit = new Domain.Git.Commit() { commitId = commit.commitId };

                EventAggregator.GetEvent<Domain.Git.Events.GetCommitChangesEvent>().Publish(
                    new Domain.Git.Events.GetCommitChangesEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Repository = _contextMainViewModel.Context.SelectedGitRepository,
                        Commit = newCommit
                    });
            }
        }

        public void GetCommitChanges()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetCommitChangesEvent>().Publish(
                new Domain.Git.Events.GetCommitChangesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    Commit = _contextMainViewModel.Context.SelectedCommit
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetCommitChangesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null
                || _contextMainViewModel.Context.SelectedCommit is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetCommitChanges Command

        #region GetImportRequests Command

        public DelegateCommand GetImportRequestsCommand { get; set; }
        public string GetImportRequestsContent { get; set; } = "GetImportRequests";
        public string GetImportRequestsToolTip { get; set; } = "GetImportRequests ToolTip";

        // Can get fancy and use Resources
        //public string GetImportRequestsContent { get; set; } = "ViewName_GetImportRequestsContent";
        //public string GetImportRequestsToolTip { get; set; } = "ViewName_GetImportRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetImportRequestsContent">GetImportRequests</system:String>
        //    <system:String x:Key="ViewName_GetImportRequestsContentToolTip">GetImportRequests ToolTip</system:String>

        public void GetImportRequests()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetImportRequestsEvent>().Publish(
                new Domain.Git.Events.GetImportRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetImportRequestsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetImportRequests Command

        #region GetItems Command

        public DelegateCommand GetItemsCommand { get; set; }
        public string GetItemsContent { get; set; } = "GetItems";
        public string GetItemsToolTip { get; set; } = "GetItems ToolTip";

        // Can get fancy and use Resources
        //public string GetItemsContent { get; set; } = "ViewName_GetItemsContent";
        //public string GetItemsToolTip { get; set; } = "ViewName_GetItemsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetItemsContent">GetItems</system:String>
        //    <system:String x:Key="ViewName_GetItemsContentToolTip">GetItems ToolTip</system:String>

        public void GetItems()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            // TODO(crhodes)
            // Make ScopePath and RecursionLevel fancier
            // For now just pass strings
            EventAggregator.GetEvent<Domain.Git.Events.GetItemsEvent>().Publish(
                new Domain.Git.Events.GetItemsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    ,
                    ScopePath = ScopePath
                    ,
                    RecursionLevel = RecursionLevel
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetItemsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetItems Command

        #region GetMerges Command

        public DelegateCommand GetMergesCommand { get; set; }
        public string GetMergesContent { get; set; } = "GetMerges";
        public string GetMergesToolTip { get; set; } = "GetMerges ToolTip";

        // Can get fancy and use Resources
        //public string GetMergesContent { get; set; } = "ViewName_GetMergesContent";
        //public string GetMergesToolTip { get; set; } = "ViewName_GetMergesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetMergesContent">GetMerges</system:String>
        //    <system:String x:Key="ViewName_GetMergesContentToolTip">GetMerges ToolTip</system:String>

        public void GetMerges()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetMergesEvent>().Publish(
                new Domain.Git.Events.GetMergesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetMergesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetMerges Command

        #region GetPullRequests Command

        public DelegateCommand GetPullRequestsCommand { get; set; }

        public string GetPullRequestsContent { get; set; } = "GetPullRequests";

        public string GetPullRequestsToolTip { get; set; } = "GetPullRequests ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequests()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPullRequestsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        public bool GetPullRequestInfoCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null
                || _contextMainViewModel.Context.SelectedPullRequest is null)
            {
                return false;
            }

            return true;
        }

        // NOTE(crhodes)
        // All of these methods use the GetPullRequestInfoCanExecute method to enable themselves

        #region GetPullRequestAttachments Command

        public DelegateCommand GetPullRequestAttachmentsCommand { get; set; }
        public string GetPullRequestAttachmentsContent { get; set; } = "Get PR Attachments";
        public string GetPullRequestAttachmentsToolTip { get; set; } = "Get PR Attachments ToolTip";

        public void GetPullRequestAttachments()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestAttachmentsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestAttachmentsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPullRequestsCommits Command

        public DelegateCommand GetPullRequestCommitsCommand { get; set; }
        public string GetPullRequestCommitsContent { get; set; } = "Get PR Commits";
        public string GetPullRequestCommitsToolTip { get; set; } = "Get PR Commits ToolTip";

        public void GetPullRequestCommits()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestCommitsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestCommitsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);

        }

        #endregion

        #region GetPullRequestCommitChanges Command

        public DelegateCommand GetPullRequestCommitChangesCommand { get; set; }
        public string GetPullRequestCommitChangesContent { get; set; } = "PR Commit Changes";
        public string GetPullRequestCommitChangesToolTip { get; set; } = "Get PR Commit Changes ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestCommitChangesContent { get; set; } = "ViewName_GetPullRequestCommitChangesContent";
        //public string GetPullRequestCommitChangesToolTip { get; set; } = "ViewName_GetPullRequestCommitChangesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestCommitChangesContent">GetPullRequestCommitChanges</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestCommitChangesContentToolTip">GetPullRequestCommitChanges ToolTip</system:String>  

        public void GetPullRequestCommitChanges()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetPullRequestCommitChangesEvent>().Publish(
                new GetPullRequestCommitChangesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest,
                    PullRequestCommitId = int.Parse(_contextMainViewModel.Context.SelectedCommit.commitId)
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPullRequestCommitChangesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null
                || _contextMainViewModel.Context.SelectedPullRequest is null
                || _contextMainViewModel.Context.SelectedCommit is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetPullRequestIterations Command

        public DelegateCommand GetPullRequestIterationsCommand { get; set; }
        public string GetPullRequestIterationsContent { get; set; } = "Get PR Iterations";
        public string GetPullRequestIterationsToolTip { get; set; } = "Get PR Iterations ToolTip";

        public void GetPullRequestIterations()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestIterationsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestIterationsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest,
                    IncludeCommits = IncludePullRequestIterationCommits
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPullRequestIterationChanges Command

        public DelegateCommand GetPullRequestIterationChangesCommand { get; set; }
        public string GetPullRequestIterationChangesContent { get; set; } = "GetPullRequestIterationChanges";
        public string GetPullRequestIterationChangesToolTip { get; set; } = "GetPullRequestIterationChanges ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestIterationChangesContent { get; set; } = "ViewName_GetPullRequestIterationChangesContent";
        //public string GetPullRequestIterationChangesToolTip { get; set; } = "ViewName_GetPullRequestIterationChangesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestIterationChangesContent">GetPullRequestIterationChanges</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestIterationChangesContentToolTip">GetPullRequestIterationChanges ToolTip</system:String>  

        public void GetPullRequestIterationChanges()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetPullRequestIterationChangesEvent>().Publish(
                new GetPullRequestIterationChangesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest,
                    PullRequestIteration = _contextMainViewModel.Context.SelectedPullRequestIteration
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPullRequestIterationChangesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null
                || _contextMainViewModel.Context.SelectedPullRequest is null
                || _contextMainViewModel.Context.SelectedPullRequestIteration is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetPullRequestLabels Command

        public DelegateCommand GetPullRequestLabelsCommand { get; set; }
        public string GetPullRequestLabelsContent { get; set; } = "Get PR Labels";
        public string GetPullRequestLabelsToolTip { get; set; } = "Get PR Labels ToolTip";

        public void GetPullRequestLabels()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestLabelsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestLabelsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPullRequestProperties Command

        public DelegateCommand GetPullRequestPropertiesCommand { get; set; }
        public string GetPullRequestPropertiesContent { get; set; } = "Get PR Properties";
        public string GetPullRequestPropertiesToolTip { get; set; } = "Get PR Properties ToolTip";

        public void GetPullRequestProperties()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestPropertiesEvent>().Publish(
                new Domain.Git.Events.GetPullRequestPropertiesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPullRequestReviewers Command

        public DelegateCommand GetPullRequestReviewersCommand { get; set; }
        public string GetPullRequestReviewersContent { get; set; } = "Get PR Reviewers";
        public string GetPullRequestReviewersToolTip { get; set; } = "Get PR Reviewers ToolTip";

        public void GetPullRequestReviewers()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestReviewersEvent>().Publish(
                new Domain.Git.Events.GetPullRequestReviewersEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPullRequestStatuses Command

        public DelegateCommand GetPullRequestStatusesCommand { get; set; }
        public string GetPullRequestStatusesContent { get; set; } = "Get PR Statuses";
        public string GetPullRequestStatusesToolTip { get; set; } = "Get PR Statuses ToolTip";

        public void GetPullRequestStatuses()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestStatusesEvent>().Publish(
                new Domain.Git.Events.GetPullRequestStatusesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPullRequestThreads Command

        public DelegateCommand GetPullRequestThreadsCommand { get; set; }

        public string GetPullRequestThreadsContent { get; set; } = "Get PR Threads";

        public string GetPullRequestThreadsToolTip { get; set; } = "Get PR Threads ToolTip";

        public void GetPullRequestThreads()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestThreadsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestThreadsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPullRequestThreadComments Command

        // NOTE(crhodes)
        // Thread comments seem to come reliably from Thread.  May not have a use for this.
        // I wonder why there is a separate call?   Must be more to this.
        // For now just remove the automatic call.

        //private void CallGetThreadChange(PullRequestThread pullRequestThread)
        //{
        //    Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

        //    // NOTE(crhodes)
        //    // We cleared out the context but for some reason pullRequestThread is set
        //    // so check both for not null

        //    if (! (pullRequestThread is null)
        //        && !(_contextMainViewModel.Context.SelectedPullRequestThread is null))
        //    {
        //        EventAggregator.GetEvent<GetPullRequestThreadCommentsEvent>().Publish(
        //            new GetPullRequestThreadCommentsEventArgs()
        //            {
        //                Organization = _collectionMainViewModel.SelectedCollection.Organization,
        //                Project = _contextMainViewModel.Context.SelectedProject,
        //                Repository = _contextMainViewModel.Context.SelectedGitRepository,
        //                PullRequest = _contextMainViewModel.Context.SelectedPullRequest,
        //                PullRequestThread = pullRequestThread
        //            });
        //    }

        //    Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        //}

        public DelegateCommand GetPullRequestThreadCommentsCommand { get; set; }
        public string GetPullRequestThreadCommentsContent { get; set; } = "GetPullRequestThreadComments";
        public string GetPullRequestThreadCommentsToolTip { get; set; } = "GetPullRequestThreadComments ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestThreadCommentsContent { get; set; } = "ViewName_GetPullRequestThreadCommentsContent";
        //public string GetPullRequestThreadCommentsToolTip { get; set; } = "ViewName_GetPullRequestThreadCommentsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestThreadCommentsContent">GetPullRequestThreadComments</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestThreadCommentsContentToolTip">GetPullRequestThreadComments ToolTip</system:String>  

        public void GetPullRequestThreadComments()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetPullRequestThreadCommentsEvent>().Publish(
                new GetPullRequestThreadCommentsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest,
                    PullRequestThread = _contextMainViewModel.Context.SelectedPullRequestThread
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPullRequestThreadCommentsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null
                || _contextMainViewModel.Context.SelectedPullRequest is null
                || _contextMainViewModel.Context.SelectedPullRequestThread is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetPullRequestWorkItems Command

        public DelegateCommand GetPullRequestWorkItemsCommand { get; set; }

        public string GetPullRequestWorkItemsContent { get; set; } = "Get PR WorkItems";

        public string GetPullRequestWorkItemsToolTip { get; set; } = "Get PR WorkItems ToolTip";

        public void GetPullRequestWorkItems()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestWorkItemsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestWorkItemsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region GetPushes Command

        public DelegateCommand GetPushesCommand { get; set; }
        public string GetPushesContent { get; set; } = "Get Pushes";
        public string GetPushesToolTip { get; set; } = "Get Pushes ToolTip";

        // Can get fancy and use Resources
        //public string GetPushesContent { get; set; } = "ViewName_GetPushesContent";
        //public string GetPushesToolTip { get; set; } = "ViewName_GetPushesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPushesContent">GetPushes</system:String>
        //    <system:String x:Key="ViewName_GetPushesContentToolTip">GetPushes ToolTip</system:String>

        public void GetPushes()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPushesEvent>().Publish(
                new Domain.Git.Events.GetPushesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPushesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetPushes Command

        #region GetRefs Command

        public DelegateCommand GetRefsCommand { get; set; }
        public string GetRefsContent { get; set; } = "GetRefs";
        public string GetRefsToolTip { get; set; } = "GetRefs ToolTip";

        // Can get fancy and use Resources
        //public string GetRefsContent { get; set; } = "ViewName_GetRefsContent";
        //public string GetRefsToolTip { get; set; } = "ViewName_GetRefsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetRefsContent">GetRefs</system:String>
        //    <system:String x:Key="ViewName_GetRefsContentToolTip">GetRefs ToolTip</system:String>

        public void GetRefs()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetRefsEvent>().Publish(
                new Domain.Git.Events.GetRefsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetRefsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetRefs Command

        #region GetStats Command

        public DelegateCommand GetStatsCommand { get; set; }
        public string GetStatsContent { get; set; } = "Get Stats";
        public string GetStatsToolTip { get; set; } = "Get Stats ToolTip";

        // Can get fancy and use Resources
        //public string GetStatsContent { get; set; } = "ViewName_GetStatsContent";
        //public string GetStatsToolTip { get; set; } = "ViewName_GetStatsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatsContent">GetStats</system:String>
        //    <system:String x:Key="ViewName_GetStatsContentToolTip">GetStats ToolTip</system:String>

        public void GetStats()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetStatsEvent>().Publish(
                new Domain.Git.Events.GetStatsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Repository = _contextMainViewModel.Context.SelectedGitRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetStatsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedGitRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetStats Command

        #endregion GetPullRequests Commands

    }
}