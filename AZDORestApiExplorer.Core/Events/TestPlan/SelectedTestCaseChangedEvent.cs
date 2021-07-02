using AZDORestApiExplorer.Domain.Test;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Test
{
    public class SelectedTestCaseChangedEvent : PubSubEvent<TestCase> { }
}
