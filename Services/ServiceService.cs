using System.ServiceProcess;

namespace services
{
    internal static class ServiceService
    {
        internal static void GetServices()
        {
            ServiceController[] scServices = ServiceController.GetServices();
        }
    }
}
