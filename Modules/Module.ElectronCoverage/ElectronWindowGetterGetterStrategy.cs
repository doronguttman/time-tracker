using System.Windows.Automation;
using Module.WindowsCoverage;

namespace Module.ElectronCoverage
{
    internal class ElectronWindowGetterGetterStrategy : IWindowGetterStrategy
    {
        #region Implementation of IWindowGetterStrategy
        public bool IsWindow(AutomationElement element, AutomationElement parentElement)
        {
            var control = element.Current.ControlType;
            if (!Equals(control, ControlType.Window) && Equals(control, ControlType.Pane)) return false;

            return parentElement is null || parentElement == AutomationElement.RootElement;
        }
        #endregion Implementation of IWindowGetterStrategy
    }
}
