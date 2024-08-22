namespace FarmDiary
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            window.MaximumWidth = 1920;
            window.MaximumHeight = 1080;
            window.X = 0;
            window.Y = 0;
            window.Width = 1920;
            window.Height = 1080;

            return window;
        }

    }
}
