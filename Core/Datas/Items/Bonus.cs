using DinaCSharp.Services.Localization;

using System.Transactions;

namespace Dungeon100Steps.Core.Datas.Items
{
    public class Bonus(BonusType type, string translationkey = "", int amount = 0, int percentage = 0, int duration = 1)
    {
        public BonusType Type { get; private set; } = type;
        public string TranslationKey { get; private set; } = translationkey;
        public int GetTotalAmount(int? baseValue)
        {
            switch (Type)
            {
                case BonusType.Attack:
                case BonusType.Defense:
                    return Amount;

                case BonusType.Health:
                case BonusType.Ice:
                case BonusType.Mana:
                case BonusType.Poison:
                case BonusType.Fire:
                case BonusType.Bleed:
                    return IsPercentageBased && (baseValue ?? 0) > 0
                        ? (int)Math.Ceiling((float)(Math.Abs(Percentage) * baseValue!) / 100f)
                        : Amount;

                case BonusType.Stunt:
                    return Duration;

                case BonusType.ResistFire:
                case BonusType.ResistStunt:
                case BonusType.ResistIce:
                case BonusType.ResistBleed:
                    return Percentage;

                default:
                    throw new InvalidDataException();
            }

        }
        public string GetDescription(int? playerAttackValue)
        {
            var translation = LocalizationManager.GetTranslation(TranslationKey);
            switch (Type)
            {
                case BonusType.Attack:
                case BonusType.Defense:
                case BonusType.Health:
                case BonusType.Ice:
                case BonusType.Mana:
                    return string.Format(translation, GetTotalAmount(playerAttackValue));

                case BonusType.Poison:
                case BonusType.Fire:
                case BonusType.Bleed:
                    return string.Format(translation, GetTotalAmount(playerAttackValue), Duration);

                case BonusType.Stunt:
                    return string.Format(translation, Duration);

                case BonusType.ResistFire:
                case BonusType.ResistStunt:
                case BonusType.ResistIce:
                case BonusType.ResistBleed:
                case BonusType.ResistPoison:
                    return string.Format(translation, Percentage);

                default:
                    throw new InvalidDataException();
            }
        }
        public int Amount { get; private set; } = amount;
        public int Percentage { get; private set; } = percentage;
        public int Duration { get; private set; } = duration;
        public bool IsPercentageBased => Percentage != 0 && Amount == 0;
    }
}
