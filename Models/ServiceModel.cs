using System.ServiceProcess;

namespace services
{
    internal class ServiceModel : NotifyPropertyChanged
    {
        public readonly ServiceController controller;

        public ServiceModel(ServiceController controller)
        {
            this.controller = controller;
            ServiceService.SetProperties(this);
        }

        public ServiceController Controller
        {
            get { return controller; }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public string DisplayName
        {
            get { return controller.DisplayName; }
        }

        public string Name
        {
            get { return controller.ServiceName; }
        }

        private bool enableStart;
        public bool EnableStart
        {
            get
            {
                return enableStart;
            }
            set
            {
                enableStart = value;
                OnPropertyChanged("EnableStart");
            }
        }

        private bool enableStop;
        public bool EnableStop
        {
            get
            {
                return enableStop;
            }
            set
            {
                enableStop = value;
                OnPropertyChanged("EnableStop");
            }
        }

        private bool enablePause;
        public bool EnablePause
        {
            get
            {
                return enablePause;
            }
            set
            {
                enablePause = value;
                OnPropertyChanged("EnablePause");
            }
        }

        private bool enableContinue;
        public bool EnableContinue
        {
            get
            {
                return enableContinue;
            }
            set
            {
                enableContinue = value;
                OnPropertyChanged("EnableContinue");
            }
        }

        public string Account
        {
            get
            {
                return ServiceService.GetAccountNameByServiceControllerName(controller.ServiceName);
            }
        }
    }
}
