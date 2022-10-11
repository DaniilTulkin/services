using System;

namespace services
{
    public class Launcher
    {
        [STAThread]
        static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}