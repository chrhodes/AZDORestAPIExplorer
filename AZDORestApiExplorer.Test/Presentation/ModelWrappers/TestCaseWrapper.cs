

using AZDORestApiExplorer.Domain.Test;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Test.Presentation.ModelWrappers
{
    public class TestCaseWrapper : ModelWrapper<TestCase>
    {
        public TestCaseWrapper(TestCase model) : base(model)
        {
        }
    }
}
