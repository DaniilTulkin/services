using System.Collections.ObjectModel;
using System.Windows.Input;

namespace services
{
    internal class ServiceViewModel : NotifyPropertyChanged
    {
        internal ServiceViewModel()
        {
            services = new ObservableCollection<ServiceModel>(ServiceService.GetServices());
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
            ServiceService.ControlService(selectedService, ServiceCommand.Start);
        });

        public ICommand Stop => new RelayCommand(() =>
        {
            ServiceService.ControlService(selectedService, ServiceCommand.Stop);
        });

        public ICommand Pause => new RelayCommand(() =>
        {
            ServiceService.ControlService(selectedService, ServiceCommand.Pause);
        });

        public ICommand Continue => new RelayCommand(() =>
        {
            ServiceService.ControlService(selectedService, ServiceCommand.Continue);
        });
    }
}
