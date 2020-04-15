using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Interfaces;

namespace TimeTracker.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IAutoStart
    {
        private readonly ILogger<MainWindow> _logger;

        public MainWindow(ILogger<MainWindow> logger)
        {
            _logger = logger;
            InitializeComponent();
        }

        #region Implementation of IAutoStart
        public Task Start()
        {
            this._logger.Info(nameof(Start));
            this.Show();
            return Task.CompletedTask;
        }
        #endregion Implementation of IAutoStart
    }
}
