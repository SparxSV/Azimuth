using Raylib_cs;

using System.Numerics;

namespace Azimuth.UI
{
	public class InteractableWidget : Widget
	{
		public InteractionState State { get; private set; }

		public bool Interactable { get; set; } = true;

		private ColorBlock colors;

		protected InteractableWidget(Vector2 _position, Vector2 _size, ColorBlock _colors) : base(_position, _size)
		{
			colors = _colors;
			State = InteractionState.Normal;
		}

		public override void Update(Vector2 _mousepos)
		{
			base.Update(_mousepos);
			
			// assigning bool to if mouse button clicked
			bool clicked = Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT);

			InteractionState oldState = State;
			
			// If selected and clicked
			if(State == InteractionState.Selected && !clicked)
			{
				// If focused... hovered, else... normal
				State = focused ? InteractionState.Hovered : InteractionState.Normal;
			}
			// Clicked and focused
			else if(clicked && focused)
			{
				State = InteractionState.Selected;
			}
			// mouse focused on
			else if(focused)
			{
				State = InteractionState.Hovered;
			}
			// if nothing else
			else
			{
				State = InteractionState.Normal;
			}
			
			// If not interactable
			if(!Interactable)
				State = InteractionState.Disabled;
			
			// If not the same state, make State && oldstate same
			if(State != oldState)
				OnStateChange(State, oldState);
		}

		protected virtual void OnStateChange(InteractionState _state, InteractionState _oldState)
		{
			
		}

		protected Color ColorFromState()
		{
			switch(State)
			{
				// colors == normal
				case InteractionState.Normal:
					return colors.normal;
				// colors == hovered
				case InteractionState.Hovered:
					return colors.hovered;
				// colors == selected
				case InteractionState.Selected:
					return colors.selected;
				// colors == disabled
				case InteractionState.Disabled:
					return colors.disabled;
				// otherwise == blank color
				default:
					return Color.BLANK;
			}
		}
	}
}