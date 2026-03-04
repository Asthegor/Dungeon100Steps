using DinaCSharp.Services.Localization;

namespace Dungeon100Steps.Core.Datas.Events
{
    public class TrapEvent(TrapType trapType, int difficulty, int percentage, int duration = 1, string description = "") : Event(EventType.Trap, description)
    {
        public TrapType TrapType { get; private set; } = trapType;
        public int Difficulty { get; private set; } = difficulty;
        public int Percentage { get; private set; } = percentage;
        public int Duration { get; private set; } = duration;
        public string GetDescription(int damage)
        {
            var translation = LocalizationManager.GetTranslation(Description);
            return string.Format(translation, damage);
        }
    }
}
