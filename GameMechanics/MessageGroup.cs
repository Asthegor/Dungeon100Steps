using DinaCSharp.Core;
using DinaCSharp.Core.Utils;
using DinaCSharp.Enums;
using DinaCSharp.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon100Steps.GameMechanics
{
    public class MessageGroup : Base
    {
        private readonly Text _message;
        private readonly Text _continue;
        private readonly Panel _panel;
        private Vector2 _screenDimensions;

        public MessageGroup(SpriteFont messageFont, string messageContent, Color messageTextColor,
                            SpriteFont continueFont, string continueContent, Color continueTextColor,
                            Vector2 screenDimensions, Color panelBackgroundColor, Color panelBorderColor, int panelBorderThickness)
        {
            _screenDimensions = screenDimensions;
            _message = new Text(messageFont, messageContent, messageTextColor,
                                horizontalalignment: HorizontalAlignment.Center, verticalalignment: VerticalAlignment.Center);
            _message.Dimensions = new Vector2(_screenDimensions.X - panelBorderThickness * 2, _message.Dimensions.Y);
            var panelPos = _message.Position - new Vector2(0, messageFont.LineSpacing);
            var panelDim = new Vector2(_screenDimensions.X - panelBorderThickness * 2, _message.TextDimensions.Y + messageFont.LineSpacing * 2);
            _panel = new Panel(panelDim, panelDim, panelBackgroundColor, panelBorderColor, panelBorderThickness);
            _continue = new Text(continueFont, continueContent, continueTextColor);
            _continue.Position = _panel.Dimensions - _continue.Dimensions;

            _message.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Text.Content))
                {
                    _panel.Dimensions = new Vector2(_panel.Dimensions.X, _message.Font.LineSpacing * 2 + _message.Dimensions.Y);
                }
            };
            _panel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Panel.Dimensions))
                {
                    _continue.Position = _panel.Position + _panel.Dimensions - _continue.Dimensions;
                    _panel.Position = new Vector2(_panel.Position.X, _screenDimensions.Y - _panel.Dimensions.Y - panelBorderThickness);
                }
                if (e.PropertyName == nameof(Panel.Position))
                {
                    _continue.Position = _panel.Position + _panel.Dimensions - _continue.Dimensions;
                    _message.Position = _panel.Position + new Vector2(0, _message.Font.LineSpacing);
                }
            };
        }
        public void Draw(SpriteBatch spritebatch)
        {
            _panel?.Draw(spritebatch);
            _message?.Draw(spritebatch);
            _continue?.Draw(spritebatch);
        }

        public void SetMessage(string newMessage)
        {
            _message.Content = newMessage;
        }

        public override Vector2 Position
        {
            get => _panel.Position;
            set => _panel?.Position = value;
        }
        public override Vector2 Dimensions
        {
            get => _panel.Dimensions;
            set => _panel?.Dimensions = value;
        }
    }
}
