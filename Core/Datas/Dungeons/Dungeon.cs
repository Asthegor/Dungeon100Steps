using Dungeon100Steps.Core.Datas.Events;

namespace Dungeon100Steps.Core.Datas.Dungeons
{
    public class Dungeon(Event[] events)
    {
        public int CurrentLevel { get; set; } = -1;
        public Event[] Events = events;

        public Event? NextEvent()
        {
            CurrentLevel++;
            if (CurrentLevel < Events.Length)
                return Events[CurrentLevel];
            return null;
        }
    }
}
