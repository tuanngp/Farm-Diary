namespace FarmDiary
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnMenuItemTapped(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                // Lấy StackLayout từ parent của label
                var parentLayout = label.Parent as Frame;
                var stackLayout = parentLayout?.Parent as StackLayout;

                // Kiểm tra stackLayout không phải là null
                if (stackLayout != null)
                {
                    // Xóa kiểu dáng đã chọn của tất cả các mục
                    foreach (var child in stackLayout.Children)
                    {
                        if (child is Frame frame && frame.Content is Label childLabel)
                        {
                            childLabel.Style = (childLabel == label) ?
                                (Style)Resources["SelectedMenuItemStyle"] :
                                (Style)Resources["MenuItemStyle"];
                        }
                    }
                }

                if (label.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tgr)
                {
                    string menuItem = tgr.CommandParameter?.ToString();
                    NavigateToPage(menuItem);
                }
            }
        }


        private void NavigateToPage(string pageName)
        {
            View content = null;

            switch (pageName)
            {
                case "HomePage":
                    //content = new HomePage().Content;
                    break;
                case "FarmManagementPage":
                    content = new FarmManagementPage().Content;
                    break;
                case "CropManagementPage":
                    //content = new CropManagementPage().Content;
                    break;
                case "CultivationManagementPage":
                    //content = new CultivationManagementPage().Content;
                    break;
                case "StatisticsPage":
                    //content = new StatisticsPage().Content;
                    break;
                case "SourceTracingPage":
                    //content = new SourceTracingPage().Content;
                    break;
                default:
                    DisplayAlert("Navigation", "Page not implemented", "OK");
                    return;
            }

            if (content != null)
            {
                MainContentArea.Content = content;
            }
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
