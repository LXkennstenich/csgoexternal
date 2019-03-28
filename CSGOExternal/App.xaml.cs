using System.Windows;
using CSGOExternal.Classes;
using ProcessMemoryWrapper;


namespace CSGOExternal
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            ProcessWrapper.CloseProcess(Memory.Handle);
        }
    }
}
