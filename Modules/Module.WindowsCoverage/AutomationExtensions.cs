using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using Interfaces.Model;

namespace Module.WindowsCoverage
{
    internal static class AutomationExtensions
    {
        private static readonly ICollection<IWindowGetterStrategy> DefaultWindowGetterGetterStrategies = new[] { new DefaultWindowGetterGetterStrategy() };

        public static string GetSelector(this AutomationElement element)
        {
            var current = element.Current;
            return FormatSelector(current.ControlType.ProgrammaticName, current.AutomationId, current.ClassName, current.Name);
        }

        public static AutomationElement GetWindow(this AutomationElement element) => GetWindow(element, DefaultWindowGetterGetterStrategies);

        public static AutomationElement GetWindow(this AutomationElement element, ICollection<IWindowGetterStrategy> windowGetterStrategies)
        {
            if (element is null) return null;
            var candidate = element;
            var parent = candidate.GetParent();
            while (!(candidate is null))
            {
                if (windowGetterStrategies.Any(s => s.IsWindow(element, parent))) return candidate;
                candidate = parent;
                parent = candidate.GetParent();
            }

            return null;
        }

        public static AutomationElement GetParent(this AutomationElement element) => element is null ? null : TreeWalker.RawViewWalker.GetParent(element);

        public static IElementInfo AsElementInfo(this AutomationElement element) => ElementInfo.FromElement(element);

        public static string FormatSelector(string controlName, string automationId, string className, string name)
        {
            var builder = new StringBuilder();
            builder.Append(controlName ?? throw new ArgumentNullException(nameof(controlName)));
            if (!string.IsNullOrWhiteSpace(automationId)) builder.Append($"[id='{automationId}']");
            if (!string.IsNullOrWhiteSpace(className)) builder.Append($".{className}");
            if (!string.IsNullOrWhiteSpace(name)) builder.Append($"[name='{name}']");
            return builder.ToString();
        }
    }
}
