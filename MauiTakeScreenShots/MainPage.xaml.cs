using System.Diagnostics;
using Microsoft.Maui;

namespace MauiTakeScreenShots;

public partial class MainPage : ContentPage
{
    MemoryStream buttonmemory;
    MemoryStream pagememory;
    IScreenshotResult buttonResult;
    IScreenshotResult pageResult;
    private string buttonPath;
    private string pagePath;

    public MainPage()
	{
		InitializeComponent();
        buttonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "button.png");
        pagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "page.png");
        
        
    }
    async void getImages()
    {
        buttonResult = await myButton.CaptureAsync();
        pageResult = await myPage.CaptureAsync();
        using MemoryStream buttonmemory = new();
        await buttonResult.CopyToAsync(buttonmemory);
        using MemoryStream pagememory = new();
        await buttonResult.CopyToAsync(pagememory);
        //AndroidPicture();
        //WindowsPicture();
        ResultsLabel.Text = "Picture Captured";
        File.WriteAllBytes(buttonPath, buttonmemory.ToArray());
        File.WriteAllBytes(pagePath, pagememory.ToArray());
        Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        //File.WriteAllBytes("C:\\Users\\quick\\OneDrive\\Pictures\\Screenshots\\button.png", buttonmemory.ToArray());
        //File.WriteAllBytes("C:\\Users\\quick\\OneDrive\\Pictures\\Screenshots\\page.png", pagememory.ToArray());

    }
    //void AndroidPicture()
    //{
    //    if (buttonmemory != null)
    //    {
    //        File.WriteAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "button.png"), buttonmemory.ToArray());
    //        File.WriteAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "page.png"), pagememory.ToArray());
    //    }
    //    else
    //    {
    //        Debug.WriteLine("ScreenShot Did not run on Android");
    //    }


    //}

    //void WindowsPicture()
    //{
    //    if (buttonmemory != null)
    //    {
    //        File.WriteAllBytes("C:\\Users\\quick\\OneDrive\\Pictures\\Screenshots\\button.png", buttonmemory.ToArray());
    //        File.WriteAllBytes("C:\\Users\\quick\\OneDrive\\Pictures\\Screenshots\\page.png", pagememory.ToArray());
    //    }
    //    else
    //    {
    //        Debug.WriteLine("ScreenShot Did not run on Windows");
    //    }


    //}
    private void Button_Clicked(object sender, EventArgs e)
    {
        getImages();
        buttonImage.BackgroundColor = Colors.Green;
        pageImage.BackgroundColor = Colors.Purple;
        buttonImage.Source = buttonPath;
        pageImage.Source = pagePath;
        var toast = CommunityToolkit.Maui.Alerts.Toast.Make(buttonPath, CommunityToolkit.Maui.Core.ToastDuration.Long, 6);
        var snackbar = CommunityToolkit.Maui.Alerts.Snackbar.Make(pagePath, () => TextToSpeech.SpeakAsync("Your the nicest person I have found"), "Compliment me.");
        toast.Show();
        Task.Delay(3000);
        snackbar.Show();
    }

}

