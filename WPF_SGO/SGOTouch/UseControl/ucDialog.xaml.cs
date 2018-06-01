using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;

namespace SGOTouch.UseControl
{
    /// <summary>
    /// Lógica de interacción para ucDialog.xaml
    /// </summary>
    public partial class ucDialog : UserControl
    {
        private readonly IDialogCoordinator dialogCoordinator;
        public ucDialog(IDialogCoordinator instance)
        {
            InitializeComponent();
            dialogCoordinator = instance;
        }
        private async void FooMessage()
        {
            await dialogCoordinator.ShowMessageAsync(this, "HEADER", "MESSAGE");
        }

        private async Task FooProgress()
        {
            // Show...
            ProgressDialogController controller = await dialogCoordinator.ShowProgressAsync(this, "HEADER", "MESSAGE");
            controller.SetIndeterminate();

            // Do your work... 

            // Close...
            await controller.CloseAsync();
        }
    }
}
