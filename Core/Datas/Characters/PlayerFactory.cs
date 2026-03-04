using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Characters
{
    public static class PlayerFactory
    {
        public static Player CreatePlayer(HeroClass heroclass, Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return heroclass switch
            {
                HeroClass.Warrior => CreateWarrior(genre, texture, bagtexture),
                HeroClass.Mage => CreateMage(genre, texture, bagtexture),
                HeroClass.Thief => CreateThief(genre, texture, bagtexture),
                _ => throw new ArgumentException("Invalid class type"),
            };
        }

        // TODO: lors de l'implémentation de la sauvegarde,
        // charger les données du joueur depuis le fichier de sauvegarde
        //public static Player CreatePlayerFromSave()
        //{
        //    return new Player();
        //}

        private static Player CreateWarrior(Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return new Player(name: "WARRIOR_" + genre.ToString().ToUpperInvariant(),
                              texture: texture,
                              attack: 5, defense: 5,
                              health: 120, mana: 10,
                              bagtexture: bagtexture,
                              combatdelay: 1.5f)
            {
                Class = HeroClass.Warrior,
                Dexterity = 5,
                Constitution = 8
            };
        }
        private static Player CreateMage(Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return new Player(name: "MAGE_" + genre.ToString().ToUpperInvariant(),
                              texture: texture,
                              attack: 2, defense: 2,
                              health: 80, mana: 80,
                              bagtexture: bagtexture,
                              combatdelay: 1.2f)
            {
                Class = HeroClass.Mage,
                Dexterity = 6,
                Constitution = 5
            };
        }
        private static Player CreateThief(Genre genre, Texture2D texture, Texture2D bagtexture)
        {
            return new Player(name: "THIEF_" + genre.ToString().ToUpperInvariant(),
                              texture: texture,
                              attack: 4, defense: 3,
                              health: 100, mana: 30,
                              bagtexture: bagtexture,
                              combatdelay: 0.8f)
            {
                Class = HeroClass.Thief,
                Dexterity = 12,
                Constitution = 6
            };
        }
    }
}
