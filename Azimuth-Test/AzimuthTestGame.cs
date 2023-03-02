using Azimuth;
using Azimuth.UI;

using Raylib_cs;

using System.Numerics;

namespace Azimuth_Test
{
	public class AzimuthTestGame : Game
	{
		private ImageWidget image;
		private Button button;
		
		public override void Load()
		{
			image = new ImageWidget(Vector2.Zero, new Vector2(800, 800), "bayomayo");
			button = new Button(new Vector2(290, 510), new Vector2(150, 75), Button.RenderSettings.normal, "hehehe butt");
			UIManager.Add(image);
			UIManager.Add(button);
		}

		public override void Unload()
		{
			
		}
	}
}