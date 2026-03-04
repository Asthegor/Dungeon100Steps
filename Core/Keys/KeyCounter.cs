using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Dungeon100Steps.Core.Keys
{
    public static class KeyCounter
    {
        /// <summary>
        /// Compte le nombre de champs statiques dans une classe dont le nom commence par un préfixe donné.
        /// </summary>
        /// <returns>Le nombre de correspondances trouvées</returns>
        public static int Count(Type classType)
        {
            return classType.GetFields(BindingFlags.Public | BindingFlags.Static).Length;
        }
    }
}
