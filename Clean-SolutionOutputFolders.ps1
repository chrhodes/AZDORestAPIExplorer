$NugetPackages = "$HOME\.nuget\packages"

$folders = @(
    "AZDORestApiExplorer"
    , "AZDORestApiExplorer.Accounts"
    # , "AZDORestApiExplorer.Artifacts"
    # , "AZDORestApiExplorer.Audit"
    # , "AZDORestApiExplorer.Build"
    # , "AZDORestApiExplorer.Core"
    # , "AZDORestApiExplorer.Dashboard"
    # , "AZDORestApiExplorer.DistributedTask"
    # , "AZDORestApiExplorer.Domain"
    # , "AZDORestApiExplorer.ExtensionManagement"
    # , "AZDORestApiExplorer.Git"
    # , "AZDORestApiExplorer.Graph"
    # , "AZDORestApiExplorer.Identities"
    # , "AZDORestApiExplorer.MemberEntitlementManagement"
    # , "AZDORestApiExplorer.Notification"
    # , "AZDORestApiExplorer.Operations"
    # , "AZDORestApiExplorer.PermissionsReport"
    # , "AZDORestApiExplorer.Pipelines"
    # , "AZDORestApiExplorer.Policy"
    # , "AZDORestApiExplorer.Profile"
    # , "AZDORestApiExplorer.Release"
    # , "AZDORestApiExplorer.Search"
    # , "AZDORestApiExplorer.Security"
    # , "AZDORestApiExplorer.Test"
    # , "AZDORestApiExplorer.Tfvc"
    # , "AZDORestApiExplorer.TokenAdmin"
    # , "AZDORestApiExplorer.Wiki"
    # , "AZDORestApiExplorer.Work"
    # , "AZDORestApiExplorer.WorkItemTracking"
    # , "AZDORestApiExplorer.WorkItemTrackingProcess"
    # , "AZDORestApiExplorer.WorkItemTrackingProcessTemplate"
    )
    
foreach ($folder in $folders)
{   
    "Removing obj\ and bin\ folder contents in $folder
    remove-item $folder\obj -Recurse -Force
    remove-item $folder\bin -Recurse -Force    
}

Read-Host -Prompt "Press Enter to Exit"