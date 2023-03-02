using Raylib_cs;

using System.Numerics;

namespace Azimuth.UI
{
	public class Button : InteractableWidget
	{
		public class RenderSettings
		{
			// this allows us to not have to worry about using new when creating new button
			public static RenderSettings normal = new RenderSettings();
			
			// setting color of the different button options
			public ColorBlock colors = new ColorBlock()
			{
				disabled = new Color(255, 255, 255, 128),
				hovered = Color.DARKGRAY,
				normal = Color.LIGHTGRAY,
				selected = Color.BLANK
			};
			public string text = "Button";
			public float roundedness = 0.1f;
			public int fontSize = 20;
			public float fontSpacing = 1f;
			public string? fontId = null;
			public Color textColor;
		}

		private readonly float roundedness;
		
		private readonly string text;
		private readonly int fontSize;
		private readonly float fontSpacing;
		
		private readonly Font font;
		private readonly Color textColor;
		private readonly Vector2 textSize;
		
		// constructor
		public Button(Vector2 _position, Vector2 _size, RenderSettings _settings, string _text) 
			: base(_position, _size, _settings.colors)
		{
			roundedness = _settings.roundedness;

			text = _settings.text;
			fontSize = _settings.fontSize;
			fontSpacing = _settings.fontSpacing;
			
			// if empty or null... get default font, else... can set it as default
			font = string.IsNullOrEmpty(_settings.fontId) ? Raylib.GetFontDefault() : Assets.Find<Font>(_settings.fontId);
			textColor = _settings.textColor;
			// takes font, text, size and spacing, and calcs size put on screen
			textSize = Raylib.MeasureTextEx(font, text, fontSize, fontSpacing) * 0.5f;

			drawLayer = 100;
		}

		public override void Draw()
		{
			Raylib.DrawRectangleRounded(Bounds, roundedness, 5, ColorFromState());
			Raylib.DrawTextPro(font, text, position + (textSize - size * 0.5f),Vector2.Zero, 0f, fontSize, fontSpacing, textColor);
		}

		protected override void OnStateChange(InteractionState _state, InteractionState _oldState)
		{
			if(_state != InteractionState.Selected && _oldState == InteractionState.Selected)
			{
				// the button is no longer being clicked, so do event.
			}
		}
	}
}