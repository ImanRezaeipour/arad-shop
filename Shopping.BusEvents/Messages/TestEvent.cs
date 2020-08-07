using Shopping.BusEvents.Events;

namespace Shopping.BusEvents.Messages
{
    public class TestEvent:ITestEvent
    {
        public string Title { get; set; }
    }
}