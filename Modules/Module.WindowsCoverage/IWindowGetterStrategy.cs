using System.Windows.Automation;

namespace Module.WindowsCoverage
{
    public interface IWindowGetterStrategy
    {
        bool IsWindow(AutomationElement element, AutomationElement parentElement);
    }
}
