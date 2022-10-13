using System.ServiceProcess;
using System.Linq;
using System;
using Microsoft.Win32;
using services.Models;
using System.Windows.Input;

namespace services
{
    internal static class ServiceService
    {
        internal static ServiceModel[] GetServices()
        {
            ServiceController[] serviceControllers = ServiceController.GetServices();
            return serviceControllers.Select(serviceController => new ServiceModel(serviceController)).ToArray();
        }

        internal static string GetAccountNameByServiceControllerName(string serviceName)
        {
            try
            {
                using RegistryKey key = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Services\\{serviceName}", false);
                object account = key?.GetValue("ObjectName");
                if (account != null) return account as string;
            }
            catch (Exception ex)
            {
                MessageService.ShowError(ex.Message);
            }
            return "";
        }

        internal static void ControlService(ref System.Collections.ObjectModel.ObservableCollection<ServiceModel> services, ServiceModel selectedService, ServiceCommand serviceCommand)
        {
            ServiceController serviceController = selectedService.Controller;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                switch (serviceCommand)
                {
                    case ServiceCommand.Start:
                        serviceController.Start();
                        serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(5));
                        break;
                    case ServiceCommand.Stop:
                        serviceController.Stop();
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(5));
                        break;
                    case ServiceCommand.Pause:
                        serviceController.Pause();
                        serviceController.WaitForStatus(ServiceControllerStatus.Paused, TimeSpan.FromSeconds(5));
                        break;
                    case ServiceCommand.Continue:
                        serviceController.Continue();
                        serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(5));
                        break;
                }
            }
            catch (System.ServiceProcess.TimeoutException ex)
            {
                MessageService.ShowError(ex.Message, "Timeout Service Controller");
            }
            catch (InvalidOperationException ex)
            {
                string message = string.Format("{0} {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                MessageService.ShowError(message, "Invalid Operation Service Controller");
                RefreshService(ref services, selectedService);
            }
            finally
            {
                SetProperties(selectedService);
                Mouse.OverrideCursor = null;
            }
        }

        private static void RefreshService(ref System.Collections.ObjectModel.ObservableCollection<ServiceModel> services, ServiceModel selectedService)
        {
            int index = services.IndexOf(selectedService);
            ServiceModel newService = GetServices().Where(service => service.Name == selectedService.Name).FirstOrDefault();
            if (newService != null) services[index] = newService;
        }

        internal static void SetProperties(ServiceModel serviceModel)
        {
            ServiceController controller = serviceModel.Controller;

            serviceModel.Status = controller.Status switch
            {
                ServiceControllerStatus.ContinuePending => "Continue Pending",
                ServiceControllerStatus.Paused => "Paused",
                ServiceControllerStatus.PausePending => "Pause Pending",
                ServiceControllerStatus.StartPending => "Start Pending",
                ServiceControllerStatus.Running => "Running",
                ServiceControllerStatus.Stopped => "Stopped",
                ServiceControllerStatus.StopPending => "Stop Pending",
                _ => "Unknown status",
            };

            serviceModel.EnableStart = controller.Status == ServiceControllerStatus.Stopped;
            serviceModel.EnableStop = controller.Status == ServiceControllerStatus.Running;
            serviceModel.EnablePause = controller.Status == ServiceControllerStatus.Running && controller.CanPauseAndContinue;
            serviceModel.EnableContinue = controller.Status == ServiceControllerStatus.Paused;
        }
    }
}
