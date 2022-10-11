using System.Collections.ObjectModel;
using System.Windows.Input;

namespace services
{
    internal class ServiceViewModel : NotifyPropertyChanged
    {
        internal ServiceViewModel()
        {
            ServiceService.GetServices();
        }

        private ObservableCollection<ServiceModel> services;
        public ObservableCollection<ServiceModel> Services
        {
            get
            {
                return services;
            }
            set
            {
                services = value;
                OnPropertyChanged("Services");
            }
        }

        private ServiceModel selectedService;
        public ServiceModel SelectedService
        {
            get
            {
                return selectedService;
            }
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedService");
            }
        }

        public ICommand Start => new RelayCommand(() =>
        {
            //MainWindowModelService.Start();
        });

        public ICommand Stop => new RelayCommand(() =>
        {
            //MainWindowModelService.Stop();
        });

        public ICommand Pause => new RelayCommand(() =>
        {
            //MainWindowModelService.Pause();
        });

        public ICommand Continue => new RelayCommand(() =>
        {
            //MainWindowModelService.Continue();
        });
    }
}
