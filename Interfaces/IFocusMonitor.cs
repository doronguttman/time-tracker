using System;
using System.Threading.Tasks;
using Interfaces.Model;

namespace Interfaces
{
    public interface IFocusMonitor
    {
        Task Start();
        Task Stop();
        IObservable<(IElementInfo window, IElementInfo element)> Focused { get; }
    }
}