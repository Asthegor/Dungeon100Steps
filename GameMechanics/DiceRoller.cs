using System;

namespace Dungeon100Steps.GameMechanics
{
    public class DiceRoller(Random random)
    {
        private readonly Random _random = random;

        /// <summary>
        /// Effectue un jet de compétence
        /// </summary>
        /// <param name="statValue">Valeur de la stat (DEX, CON, etc.)</param>
        /// <param name="difficulty">Difficulté du jet (10 = facile, 15 = moyen, 20 = difficile)</param>
        /// <returns>True si réussi</returns>
        public bool RollCheck(int statValue, int difficulty)
        {
            // Jet de dé 1-20 + bonus de stat
            int roll = _random.Next(1, 21); // 1 à 20
            int total = roll + statValue;

            return total >= difficulty;
        }

        /// <summary>
        /// Effectue un jet avec résultat détaillé
        /// </summary>
        public CheckResult RollCheckDetailed(int statValue, int difficulty)
        {
            int roll = _random.Next(1, 21);
            int total = roll + statValue;
            bool success = total >= difficulty;

            // Critique : 20 naturel = toujours réussi, 1 naturel = toujours raté
            if (roll == 20)
                success = true;
            if (roll == 1)
                success = false;

            return new CheckResult
            {
                DiceRoll = roll,
                StatBonus = statValue,
                Total = total,
                Difficulty = difficulty,
                Success = success,
                Critical = (roll == 20 || roll == 1)
            };
        }
    }

    public struct CheckResult
    {
        public int DiceRoll { get; set; }
        public int StatBonus { get; set; }
        public int Total { get; set; }
        public int Difficulty { get; set; }
        public bool Success { get; set; }
        public bool Critical { get; set; }
    }
}
