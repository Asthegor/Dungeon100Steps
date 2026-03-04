using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Enums;
using DinaCSharp.Graphics;

using Dungeon100Steps.Core.Keys;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.Core.Datas.Events
{
    public static class EventUtils
    {
        public static int GetZoneFromLevel(int level)
        {
            return level switch
            {
                < 25 => 1,
                < 50 => 2,
                < 75 => 3,
                < 100 => 4,
                _ => 5
            };
        }

        public static (TrapType, string) GetRandomTrapType(TrapType minTrapType = TrapType.None, TrapType maxTrapType = TrapType.Max)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            TrapType trapType = (TrapType)rnd.Next((int)minTrapType, (int)maxTrapType);
            string description = trapType switch
            {
                TrapType.None => "TRAP_NONE",
                TrapType.Darts => "TRAP_DARTS",
                TrapType.Pit => "TRAP_PIT",
                TrapType.PoisonGas => "TRAP_POISON",
                TrapType.PressurePlate => "TRAP_PRESSUREPLATE",
                _ => throw new InvalidDataException("Incorrect TrapType.")
            };
            return (trapType, description);
        }
        public static Group CreateMessageGroup(SpriteFont messageFont, string messageContent, SpriteFont continueFont, string continueContent, Vector2 screenDimensions, int borderThickness,
                                               out Text messageText, out Panel messagePanel, out Text continueText)
        {
            var group = new Group();

            messageText = new Text(messageFont, messageContent, PaletteColors.Message,
                                   position: new Vector2(0, messageFont.LineSpacing),
                                   horizontalalignment: HorizontalAlignment.Center, verticalalignment: VerticalAlignment.Center)
            {
                Wrap = true,
            };
            messageText.Dimensions = new Vector2(screenDimensions.X, messageText.TextDimensions.Y);

            var panelPos = new Vector2(UIScaler.Scale(borderThickness), 0);
            var panelDim = new Vector2(screenDimensions.X - UIScaler.Scale(borderThickness) * 2, messageFont.LineSpacing * 2 + messageText.Dimensions.Y);
            messagePanel = new Panel(panelPos, panelDim, PaletteColors.Message_Panel_Background)
            {
                BorderColor = PaletteColors.Message_Panel_Border,
                Thickness = UIScaler.Scale(borderThickness)
            };

            continueText = new Text(continueFont, continueContent, PaletteColors.Message);
            continueText.Position = messagePanel.Dimensions - continueText.TextDimensions - new Vector2(UIScaler.Scale(borderThickness) * 2);

            group.Add(messagePanel);
            group.Add(messageText);
            group.Add(continueText);
            return group;

        }
    }
}
