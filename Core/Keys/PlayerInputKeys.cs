using DinaCSharp.Inputs;
using DinaCSharp.Services;

namespace Dungeon100Steps.Core.Keys
{
    public static class PlayerInputKeys
    {
        /// <summary>Action de descente.</summary>
        public static readonly Key<ActionTag> Down = Key<ActionTag>.FromString("Down");

        /// <summary>Action de montée.</summary>
        public static readonly Key<ActionTag> Up = Key<ActionTag>.FromString("Up");

        /// <summary>Action de gauche.</summary>
        public static readonly Key<ActionTag> Left = Key<ActionTag>.FromString("Left");

        /// <summary>Action de droite.</summary>
        public static readonly Key<ActionTag> Right = Key<ActionTag>.FromString("Right");

        /// <summary>Action de validation.</summary>
        public static readonly Key<ActionTag> Activate = Key<ActionTag>.FromString("Activate");

        /// <summary>Action d’annulation.</summary>
        public static readonly Key<ActionTag> Cancel = Key<ActionTag>.FromString("Cancel");
        /// <summary>Action de musique précédente.</summary>
        public static readonly Key<ActionTag> PreviousMusic = Key<ActionTag>.FromString("PreviousMusic");
        /// <summary>Action de musique suivante.</summary>
        public static readonly Key<ActionTag> NextMusic = Key<ActionTag>.FromString("NextMusic");
        /// <summary>Action de mise en pause.</summary>
        public static readonly Key<ActionTag> Start = Key<ActionTag>.FromString("Start");
        public static readonly Key<ActionTag> Select = Key<ActionTag>.FromString("Select");
    }
}
