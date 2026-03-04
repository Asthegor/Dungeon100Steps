using DinaCSharp.Resources;
using DinaCSharp.Services;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Items
{
    public static class BagFactory
    {
        private static readonly ResourceManager? _resourceManager = ServiceLocator.Get<ResourceManager>(ProjectServiceKeys.AssetsResourceManager);

        private static readonly Lazy<Bag> _bag2Slots = new(() => CreateBag(BagKeys.Bag2Slots, 2));
        private static readonly Lazy<Bag> _bag4Slots = new(() => CreateBag(BagKeys.Bag4Slots, 4));
        private static readonly Lazy<Bag> _bag6Slots = new(() => CreateBag(BagKeys.Bag6Slots, 6));
        private static readonly Lazy<Bag> _bag8Slots = new(() => CreateBag(BagKeys.Bag8Slots, 8));
        private static readonly Lazy<Bag> _bag10Slots = new(() => CreateBag(BagKeys.Bag10Slots, 10));

        public static Bag Bag2Slots => _bag2Slots.Value;
        public static Bag Bag4Slots => _bag4Slots.Value;
        public static Bag Bag6Slots => _bag6Slots.Value;
        public static Bag Bag8Slots => _bag8Slots.Value;
        public static Bag Bag10Slots => _bag10Slots.Value;

        private static Bag CreateBag(Key<ResourceTag> textureKey, int slots)
        {
            var texture = _resourceManager!.Load<Texture2D>(textureKey)
                          ?? throw new InvalidDataException("Texture not found");
            return new Bag(texture, slots);
        }

        public static void Initialize()
        {
            _ = Bag2Slots;
            _ = Bag4Slots;
            _ = Bag6Slots;
            _ = Bag8Slots;
            _ = Bag10Slots;
        }
    }
}