using Dungeon100Steps.Core.Datas.Items;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Timers;

namespace Dungeon100Steps.Core.Datas.Characters
{
    public abstract class Character(string name, Texture2D texture, int attack, int defense, int health, int mana, float combatdelay)
        : IDisposable
    {
        public Texture2D Texture { get; private set; } = texture;
        public string Name { get; private set; } = name;
        public float CombatDelay { get; private set; } = combatdelay;
        public bool IsReadyToAttack { get; set; }

        private Weapon? _weapon;
        private Armor? _armor;
        private int _health = health;
        private int _maxHealth = health;
        private int _mana = mana;
        private int _maxMana = mana;
        private float _timer;

        #region Vie
        public bool IsDead => Health == 0;
        public int Health
        {
            get => _health;
            set
            {
                if (_health != value)
                {
                    _health = value;
                    RaiseStatsChanged();
                }
            }
        }
        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                if (_maxHealth != value)
                {
                    _maxHealth = value;
                    RaiseStatsChanged();
                }
            }
        }
        #endregion

        #region Mana
        public int Mana
        {
            get => _mana;
            set
            {
                if (_mana != value)
                {
                    _mana = value;
                    RaiseStatsChanged();
                }
            }
        }
        public int MaxMana
        {
            get => _maxMana;
            set
            {
                if (_maxMana != value)
                {
                    _maxMana = value;
                    RaiseStatsChanged();
                }
            }
        }
        #endregion

        #region Armre et Attack
        public abstract void EquipWeapon(Weapon weapon);
        public Weapon? Weapon
        {
            get => _weapon;
            protected set
            {
                _weapon = value;
                OnWeaponChanged?.Invoke(_weapon);
                RaiseStatsChanged(); // L'attaque change quand l'arme change
            }
        }
        public int BaseAttack { get; set; } = attack;
        public int AttackAmount => BaseAttack + (Weapon?.Bonuses.FirstOrDefault(b => b.Type == BonusType.Attack)!.Amount ?? 0);

        public void Update(GameTime gametime)
        {
            if (IsReadyToAttack)
                return;

            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (_timer > CombatDelay)
            {
                IsReadyToAttack = true;
                _timer = 0;
            }
        }
        #endregion


        #region Arnure et Defense
        public abstract void EquipArmor(Armor armor);
        public Armor? Armor
        {
            get => _armor;
            protected set
            {
                _armor = value;
                OnArmorChanged?.Invoke(_armor);
                RaiseStatsChanged(); // La défense change quand l'armure change
            }
        }
        public int BaseDefense { get; set; } = defense;
        public int Defense => BaseDefense + (Armor?.Bonuses.FirstOrDefault(b => b.Type == BonusType.Defense)!.Amount ?? 0);
        #endregion


        public void TakeDamage(int damage)
        {
            Health = Math.Max(Health - damage, 0);
        }

        public void RestoreHealth(int healthToRestore)
        {
            Health = Math.Min(Health + healthToRestore, MaxHealth);
        }
        public void RestoreMana(int manaToRestore)
        {
            Mana = Math.Min(Mana + manaToRestore, MaxMana);
        }

        #region Événements pour notifier les changements
        public event Action<Weapon?>? OnWeaponChanged;
        public event Action<Armor?>? OnArmorChanged;
        public event Action? OnStatsChanged;

        private bool _disposed;

        protected void RaiseStatsChanged()
        {
            OnStatsChanged?.Invoke();
        }

        /// <summary>
        /// Libère les ressources utilisées par le Panel.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Désabonne tous les événements.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if(OnArmorChanged != null)
                {
                    foreach (Delegate d in OnArmorChanged.GetInvocationList())
                        OnArmorChanged -= (Action<Armor?>)d;
                }
                if(OnWeaponChanged != null)
                {
                    foreach (Delegate d in OnWeaponChanged.GetInvocationList())
                        OnWeaponChanged -= (Action<Weapon?>)d;
                }
                if(OnStatsChanged != null)
                {
                    foreach (Delegate d in OnStatsChanged.GetInvocationList())
                        OnStatsChanged -= (Action)d;
                }
            }
            _disposed = true;
        }
        #endregion
    }
}
