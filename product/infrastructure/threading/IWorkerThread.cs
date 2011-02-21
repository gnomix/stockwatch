using System;
using System.ComponentModel;

namespace infrastructure.threading
{
    public interface IWorkerThread : IDisposable
    {
        event DoWorkEventHandler DoWork;
        event EventHandler Disposed;
        void begin();
    }
}