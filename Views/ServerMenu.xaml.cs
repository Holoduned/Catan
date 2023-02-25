using Catan.Models;

namespace Catan.Views;

public partial class ServerMenu : ContentPage
{
    Server server = new Server();
    private static VerticalStackLayout LogContentStatic { get; set; }
    public ServerMenu()
	{
        InitializeComponent();
        LogContentStatic = LogContent;
    }

    public static void Output(string text)
    {
        LogContentStatic.Add(new Label() { Text = $"[{DateTime.Now}] {text}" });
    }
    private void StartServer(object sender, EventArgs e)
	{
        if (server.status == ServerStatus.Start)
        {
            server.Stop();

        }
        else
        {
            server.Start();
        }
    }

    private async void StartGame(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Game());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        LogContent.Clear();
    }
}

