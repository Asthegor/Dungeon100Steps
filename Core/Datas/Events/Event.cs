namespace Dungeon100Steps.Core.Datas.Events
{
    public abstract class Event(EventType eventtype, string description = "")
    {
        public EventType EventType { get; protected set; } = eventtype;
        public string Description { get; protected set; } = description;
        public EventResult Result { get; set; }
    }
}
