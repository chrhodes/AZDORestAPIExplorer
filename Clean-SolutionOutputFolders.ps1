

$folders = @(
    "AZDORestApiExplorer"
    , "AZDORestApiExplorer.Accounts"
    , "azdorestapiexplorer.artifacts"
    , "azdorestapiexplorer.audit"
    , "azdorestapiexplorer.build"
    , "azdorestapiexplorer.core"
    , "azdorestapiexplorer.dashboard"
    , "azdorestapiexplorer.distributedtask"
    , "azdorestapiexplorer.domain"
    , "azdorestapiexplorer.extensionmanagement"
    , "azdorestapiexplorer.git"
    , "azdorestapiexplorer.graph"
    , "azdorestapiexplorer.identities"
    , "azdorestapiexplorer.memberentitlementmanagement"
    , "azdorestapiexplorer.notification"
    , "azdorestapiexplorer.operations"
    , "azdorestapiexplorer.permissionsreport"
    , "azdorestapiexplorer.pipelines"
    , "azdorestapiexplorer.policy"
    , "azdorestapiexplorer.profile"
    , "azdorestapiexplorer.release"
    , "azdorestapiexplorer.search"
    , "azdorestapiexplorer.security"
    , "azdorestapiexplorer.test"
    , "azdorestapiexplorer.tfvc"
    , "azdorestapiexplorer.tokenadmin"
    , "azdorestapiexplorer.wiki"
    , "azdorestapiexplorer.work"
    , "azdorestapiexplorer.workitemtracking"
    , "azdorestapiexplorer.workitemtrackingprocess"
    , "azdorestapiexplorer.workitemtrackingprocesstemplate"
    )
    
foreach ($folder in $folders)
{   
    "Removing obj\ and bin\ folder contents in $folder"
    
    if (Test-Path -Path $folder\obj)
    {
        remove-item $folder\obj -Recurse -Force
    }
    
    if (Test-Path -Path $folder\bin)
    {
        remove-item $folder\bin -Recurse -Force
    }
}

Read-Host -Prompt "Press Enter to Exit"