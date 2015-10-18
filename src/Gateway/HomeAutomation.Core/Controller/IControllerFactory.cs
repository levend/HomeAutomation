using System.Collections.Generic;

namespace HomeAutomation.Core.Controller
{
    public interface IControllerFactory
    {
        IHomeController CreateController(Dictionary<string, object> configuration);
    }
}
