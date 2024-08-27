namespace FarmDiary
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnMenuItemTapped(object sender, TappedEventArgs e)
        {
            if (sender is Label label && label.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tgr)
            {
                string menuItem = tgr.CommandParameter?.ToString();

                switch (menuItem)
                {
                    case "Trang chủ":
                        await NavigateToPage("HomePage");
                        break;
                    case "Quản lí trang trại":
                        await NavigateToPage("FarmManagementPage");
                        break;
                    case "Danh mục cây trồng":
                        mainContentFrame.Content = new CropManagementPage().Content;
                       
                        break;
                    case "Quản lí canh tác":
                        await NavigateToPage("CultivationManagementPage");
                        break;
                    case "Thống kê":
                        await NavigateToPage("StatisticsPage");
                        break;
                    case "Truy xuất nguồn gốc":
                        await NavigateToPage("SourceTracingPage");
                        break;
                    case "Logout":
                        await PerformLogout();
                        break;
                    default:
                        await DisplayAlert("Navigation", "Page not implemented", "OK");
                        break;
                }
            }
        }

        private async Task NavigateToPage(string pageName)
        {
            await DisplayAlert("Navigation", $"Navigating to {pageName}", "OK");

            // await Navigation.PushAsync(new HomePage());
        }


        private async Task PerformLogout()
        {
            bool answer = await DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");
            if (answer)
            {
                await DisplayAlert("Logout", "You have been logged out", "OK");
            }
        }
    }

}
