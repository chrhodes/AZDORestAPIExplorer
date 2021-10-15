namespace AZDORestApiExplorer.Domain
{

    public class AvailableCollection
    {
        public string Name { get; set; }
        public Organization Organization { get; set; }
    }

    public class Organization
    {
        public Organization(string organization)
        {
            Uri = $@"https://dev.azure.com/{organization}";

            AuditServiceUri = $@"https://auditservice.dev.azure.com/{organization}"; ;
            ExtMgmtUri = $@"https://extmgmt.dev.azure.com/{organization}";
            FeedsUri = $@"https://feeds.dev.azure.com/{organization}";
            PkgsUri = $@"https://pkgs.dev.azure.com/{organization}";
            VsAexUri = $@"https://vsaex.dev.azure.com/{organization}";
            VsCltUri = $@"https://vsclt.dev.azure.com/{organization}";
            VsSpsUri = $@"https://vssps.dev.azure.com/{organization}";
            VsTmrUri = $@"https://vstmr.dev.azure.com/{organization}";

            AppVsSpsVisualStudioUri = $@"https://app.vssps.visualstudio.com/{organization}";
        }

        public string Uri { get; set; }

        public string AuditServiceUri { get; set; }
        public string ExtMgmtUri { get; set; }
        public string FeedsUri { get; set; }
        public string PkgsUri { get; set; }
        public string VsAexUri { get; set; }
        public string VsCltUri { get; set; }
        public string VsSpsUri { get; set; }
        public string VsTmrUri { get; set; }

        public string AppVsSpsVisualStudioUri { get; set; }

        public string PAT { get; set; }
    }
}
