using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LoRTracker
{
    class LoRConnector
    {
        private int _port;
        private Thread _thread;
        private ClientConnectionStatus _status = ClientConnectionStatus.UNKNOWN;

        public LoRConnector(int port)
        {
            _port = port;

            _thread = new Thread(RunThread);
        }

        public void Connect()
        {
            _thread.Start();
        }

        private void RunThread()
        {
            while(true)
            {
                Tick();
                Thread.Sleep(1000);
            }
        }

        private void Tick()
        {
            if (_status == ClientConnectionStatus.UNKNOWN)
            {
                // initial connect
                OnStateChanged(new StateChangedEventArgs { NextState = ClientConnectionStatus.CONNECTING });
            }
        }

        protected virtual void OnStateChanged(StateChangedEventArgs e)
        {
            _status = e.NextState;
            StateChanged?.Invoke(this, e);
        }

        public event StateChangedEventHandler StateChanged;
        public delegate void StateChangedEventHandler(LoRConnector sender, StateChangedEventArgs e);
    }

    public class StateChangedEventArgs : EventArgs
    {
        public ClientConnectionStatus NextState { get; set; }
    }

    public enum ClientConnectionStatus
    {
        UNKNOWN,
        CONNECTING,
        CONNECTED,
        DISCONNECTED
    }
}
