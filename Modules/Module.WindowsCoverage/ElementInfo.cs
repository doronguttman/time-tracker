using System.Windows.Automation;
using Interfaces.Model;

namespace Module.WindowsCoverage
{
    internal class ElementInfo : IElementInfo
    {
        public ElementInfo() { }

        public ElementInfo(AutomationElement element)
        {
            var current = element.Current;
            this.Control = current.ControlType.LocalizedControlType;
            this.Id = string.IsNullOrWhiteSpace(current.AutomationId) ? null : current.AutomationId;
            this.Class = string.IsNullOrWhiteSpace(current.ClassName) ? null : current.ClassName;
            this.Name = string.IsNullOrWhiteSpace(current.Name) ? null : current.Name;
            this.Selector = AutomationExtensions.FormatSelector(this.Control, this.Id, this.Class, this.Name);
        }

        #region Implementation of IElementInfo
        public string Selector { get; }
        public string Control { get; }
        public string Id { get; }
        public string Class { get; }
        public string Name { get; }
        #endregion Implementation of IElementInfo

        public static IElementInfo FromElement(AutomationElement element) => element is null ? null : new ElementInfo(element);
    }
}
