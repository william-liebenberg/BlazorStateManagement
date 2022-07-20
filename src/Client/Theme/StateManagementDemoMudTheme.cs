using MudBlazor;

namespace Client.Theme
{
	public class StateManagementDemoMudTheme
	{
		public StateManagementDemoMudTheme()
		{
			Theme = new MudTheme()
			{
				LayoutProperties = new LayoutProperties()
				{
					DrawerWidthLeft = "275px"
				}
			};
		}

		public MudTheme Theme { get; set; }

	}
}
