using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        TaskScheduler.UnobservedTaskException += (sender, args) => 
            MessageBox.Show($"Unobserved exception from {sender.GetType()}, Id={((Task)sender).Id}!");
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await FetchData();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Exception: " + ex.Message);
        }

        GC.Collect();
    }

    private static async Task FetchData()
    {
        var uri = "https://localhost:7158/test";
        await using var connection = new HubConnectionBuilder().WithUrl(uri).Build();
        await connection.StartAsync();

        await foreach (var date in connection.StreamAsync<DateTime>("Streaming"))
        {
            Console.WriteLine(date);
        }
    }
}
