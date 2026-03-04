using DinaCSharp.Events;

using Dungeon100Steps.Core.Datas.Characters;
using Dungeon100Steps.Core.Datas.Items;

using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Enemies
{
    public class Enemy(string name, Texture2D texture, int attack, int defense, int health, float combatdelay, int mana = 0)
        : Character(name, texture, attack, defense, health, mana, combatdelay)
    {
        public override void EquipArmor(Armor armor)
        {
            Armor = armor;
        }

        public override void EquipWeapon(Weapon weapon)
        {
            Weapon = weapon;
        }
    }
}
