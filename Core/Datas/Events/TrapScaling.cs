using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon100Steps.Core.Datas.Events
{
    public class TrapScaling
    {
        /// <summary>
        /// Retourne la difficulté de base selon la zone (1-4)
        /// </summary>
        public static int GetBaseDifficulty(int zone)
        {
            return zone switch
            {
                1 => 10,  // Zone 1 (cases 1-25)   : Facile
                2 => 13,  // Zone 2 (cases 26-50)  : Moyen
                3 => 16,  // Zone 3 (cases 51-75)  : Difficile
                4 => 19,  // Zone 4 (cases 76-100) : Très difficile
                _ => 10
            };
        }

        /// <summary>
        /// Difficulté des pièges selon la zone
        /// </summary>
        public static int GetDifficulty(int zone, TrapType trapType)
        {
            int baseDifficulty = GetBaseDifficulty(zone);

            // Certains pièges sont plus difficiles que d'autres
            int trapModifier = trapType switch
            {
                TrapType.Pit => 0,           // Difficulté standard
                TrapType.Darts => -2,        // Plus facile à esquiver
                TrapType.PoisonGas => +2,    // Plus difficile à résister
                TrapType.PressurePlate => +1, // Difficile à détecter
                _ => 0
            };

            return baseDifficulty + trapModifier;
        }

        /// <summary>
        /// Dégâts des pièges selon la zone
        /// </summary>
        public static int GetPercentage(int zone, TrapType trapType)
        {
            int damagePercent = trapType switch
            {
                TrapType.Pit => zone switch
                {
                    1 => 15,
                    2 => 20,
                    3 => 25,
                    4 => 30,
                    _ => 15
                },
                TrapType.Darts => zone switch
                {
                    1 => 10,
                    2 => 15,
                    3 => 20,
                    4 => 25,
                    _ => 10
                },
                TrapType.PoisonGas => zone switch
                {
                    1 => 05,
                    2 => 07,
                    3 => 10,
                    4 => 12,
                    _ => 05
                },
                TrapType.PressurePlate => zone switch
                {
                    1 => 20,
                    2 => 25,
                    3 => 30,
                    4 => 35,
                    _ => 20
                },
                TrapType.None => 0,
                _ => 15
            };

            return damagePercent;
        }

        public static int GetDuration(TrapType trapType) => trapType == TrapType.PoisonGas ? 3: 1;

    }
}
