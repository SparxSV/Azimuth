using Raylib_cs;

using System.Numerics;

namespace Azimuth.UI
{
	public abstract class Widget : IComparable<Widget>
	{
		// Short hand way of writing get
		public Rectangle Bounds => new Rectangle(position.X, position.Y, size.X, size.Y);
		
		public Vector2 position;
		public Vector2 size;
		public bool focused;
		
		protected int drawLayer;
		
		// Alt + Insert shortcut to make constructor
		protected Widget(Vector2 _position, Vector2 _size)
		{
			position = _position;
			size = _size;
		}
		
		/// <summary>Higher numbers get drawn on top</summary>
		public void SetDrawLayer(int _layer)
		{
			drawLayer = _layer;
		}
		
		public virtual void Draw()
		{
			Raylib.DrawRectangleRec(Bounds, Color.WHITE);
		}

		// Virtual is a function that exists on widget, has a body, can give default functionality
		// We can add more functionality in inheriting classes
		
		public virtual void Update(Vector2 _mousepos)
		{
			focused = Raylib.CheckCollisionPointRec(_mousepos, Bounds);
		}
		
		// Overridden from System.Object - Whenever a Widget is used in string interpolation ($" ") or
		// Any sort of string operations, this will be called
		public override string ToString()
		{
			// Widget:
			//  Position: (0, 0)
			//  Size: (50, 50)
			//  Draw Layer: 5
			//  Focused: False
			return $"Widget:\n  Position: {position}\n  Size: {size}\n  DrawLayer: {drawLayer}\n  Focused: {focused}";
		}
		
		// Allows us to sort drawLayers
		public int CompareTo(Widget? _other)
		{
			if(ReferenceEquals(this, _other))
				return 0;
			
			return ReferenceEquals(null, _other) ? 1 : drawLayer.CompareTo(_other.drawLayer);
		}
	}
}