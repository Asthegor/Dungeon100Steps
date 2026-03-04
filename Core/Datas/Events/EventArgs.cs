using Dungeon100Steps.Core;

using System;

namespace Dungeon100Steps.Core.Datas.Events
{
    public class DungeonEndedEventArgs(EventResult result) : EventArgs
    {
        public EventResult Result { get; } = result;
    }
    public class TutorialEventArgs(EventResult result) : EventArgs
    {
        public EventResult Result { get; } = result;
    }
}
