using System;
using System.ServiceProcess;

namespace services
{
    internal class ServiceModel
    {
        private readonly ServiceController controller;

        public ServiceModel(ServiceController controller)
        {
            this.controller = controller;
        }

        public ServiceController Controller
        {
            get { return controller; }
        }

        public string Status
        {
            get
            {
                switch (controller.Status)
                {
                    case ServiceControllerStatus.ContinuePending:
                        return "Continue Pending";
                    case ServiceControllerStatus.Paused:
                        return "Paused";
                    case ServiceControllerStatus.PausePending:
                        return "Pause Pending";
                    case ServiceControllerStatus.StartPending:
                        return "Start Pending";
                    case ServiceControllerStatus.Running:
                        return "Running";
                    case ServiceControllerStatus.Stopped:
                        return "Stopped";
                    case ServiceControllerStatus.StopPending:
                        return "Stop Pending";
                    default:
                        return "Unknown status";
                }
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

        public bool EnableStart
        {
            get
            {
                return controller.Status == ServiceControllerStatus.Stopped;
            }
        }

        public bool EnableStop
        {
            get
            {
                return controller.Status == ServiceControllerStatus.Running;
            }
        }

        public bool EnablePause
        {
            get
            {
                return controller.Status == ServiceControllerStatus.Running && controller.CanPauseAndContinue;
            }
        }

        public bool EnableContinue
        {
            get
            {
                return controller.Status == ServiceControllerStatus.Paused;
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
