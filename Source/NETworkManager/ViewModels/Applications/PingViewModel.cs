﻿using MahApps.Metro.Controls.Dialogs;
using NETworkManager.Models.Network;
using NETworkManager.Models.Settings;
using NETworkManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using NETworkManager.Collections;


namespace NETworkManager.ViewModels.Applications
{
    public class PingViewModel : ViewModelBase
    {
        #region Variables
        private IDialogCoordinator dialogCoordinator;
        MetroDialogSettings dialogSettings = new MetroDialogSettings();

        CancellationTokenSource cancellationTokenSource;

        private bool _isLoading = true;

        private string _hostnameOrIPAddress;
        public string HostnameOrIPAddress
        {
            get { return _hostnameOrIPAddress; }
            set
            {
                if (value == _hostnameOrIPAddress)
                    return;

                _hostnameOrIPAddress = value;
                OnPropertyChanged();
            }
        }

        private List<string> _hostnameOrIPAddressHistory = new List<string>();
        public List<string> HostnameOrIPAddressHistory
        {
            get { return _hostnameOrIPAddressHistory; }
            set
            {
                if (value == _hostnameOrIPAddressHistory)
                    return;

                if (!_isLoading)
                    SettingsManager.Current.Ping_HostnameOrIPAddressHistory = value;

                _hostnameOrIPAddressHistory = value;
                OnPropertyChanged();
            }
        }

        private bool _isPingRunning;
        public bool IsPingRunning
        {
            get { return _isPingRunning; }
            set
            {
                if (value == _isPingRunning)
                    return;

                _isPingRunning = value;
                OnPropertyChanged();
            }
        }

        private bool _cancelPing;
        public bool CancelPing
        {
            get { return _cancelPing; }
            set
            {
                if (value == _cancelPing)
                    return;

                _cancelPing = value;
                OnPropertyChanged();
            }
        }

        private AsyncObservableCollection<PingInfo> _pingResult = new AsyncObservableCollection<PingInfo>();
        public AsyncObservableCollection<PingInfo> PingResult
        {
            get { return _pingResult; }
            set
            {
                if (value == _pingResult)
                    return;

                _pingResult = value;
            }
        }

        private int _pingsTransmitted;
        public int PingsTransmitted
        {
            get { return _pingsTransmitted; }
            set
            {
                if (value == _pingsTransmitted)
                    return;

                _pingsTransmitted = value;
                OnPropertyChanged();
            }
        }

        private int _pingsReceived;
        public int PingsReceived
        {
            get { return _pingsReceived; }
            set
            {
                if (value == _pingsReceived)
                    return;

                _pingsReceived = value;
                OnPropertyChanged();
            }
        }

        private int _pingsLost;
        public int PingsLost
        {
            get { return _pingsLost; }
            set
            {
                if (value == _pingsLost)
                    return;

                _pingsLost = value;
                OnPropertyChanged();
            }
        }

        private long _minimumTime;
        public long MinimumTime
        {
            get { return _minimumTime; }
            set
            {
                if (value == _minimumTime)
                    return;

                _minimumTime = value;
                OnPropertyChanged();
            }
        }

        private long _maximumTime;
        public long MaximumTime
        {
            get { return _maximumTime; }
            set
            {
                if (value == _maximumTime)
                    return;

                _maximumTime = value;
                OnPropertyChanged();
            }
        }

        private int _averageTime;
        public int AverageTime
        {
            get { return _averageTime; }
            set
            {
                if (value == _averageTime)
                    return;

                _averageTime = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Contructor
        public PingViewModel(IDialogCoordinator instance)
        {
            dialogCoordinator = instance;

            dialogSettings.CustomResourceDictionary = new ResourceDictionary
            {
                Source = new Uri("NETworkManager;component/Resources/Styles/MetroDialogStyles.xaml", UriKind.RelativeOrAbsolute)
            };

            LoadSettings();

            _isLoading = false;
        }
        #endregion

        #region Load settings
        private void LoadSettings()
        {
            if (SettingsManager.Current.Ping_HostnameOrIPAddressHistory != null)
                HostnameOrIPAddressHistory = new List<string>(SettingsManager.Current.Ping_HostnameOrIPAddressHistory);
        }
        #endregion

        #region ICommands & Actions
        public ICommand PingCommand
        {
            get { return new RelayCommand(p => PingAction()); }
        }

        private void PingAction()
        {
            if (IsPingRunning)
                StopPing();
            else
                StartPing();
        }
        #endregion

        #region Methods      
        private async void StartPing()
        {
            IsPingRunning = true;

            PingResult.Clear();
            PingsTransmitted = 0;
            PingsReceived = 0;
            PingsLost = 0;
            AverageTime = 0;
            MinimumTime = 0;
            MaximumTime = 0;

            // Try to parse the string into an IP-Address
            IPAddress ipAddress;
            IPAddress.TryParse(HostnameOrIPAddress, out ipAddress);

            try
            {
                // Try to resolve the hostname
                if (ipAddress == null)
                {
                    IPHostEntry ipHostEntrys = await Dns.GetHostEntryAsync(HostnameOrIPAddress);

                    foreach (IPAddress ip in ipHostEntrys.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork && SettingsManager.Current.Ping_ResolveHostnamePreferIPv4)
                        {
                            ipAddress = ip;
                            continue;
                        }
                        else if (ip.AddressFamily == AddressFamily.InterNetworkV6 && !SettingsManager.Current.Ping_ResolveHostnamePreferIPv4)
                        {
                            ipAddress = ip;
                            continue;
                        }
                    }

                    // Fallback --> If we could not resolve our prefered ip protocol for the hostname
                    if (ipAddress == null)
                    {
                        foreach (IPAddress ip in ipHostEntrys.AddressList)
                        {
                            ipAddress = ip;
                            continue;
                        }
                    }
                }
            }
            catch (SocketException) // This will catch DNS resolve errors
            {               
                await dialogCoordinator.ShowMessageAsync(this, Application.Current.Resources["String_Header_DnsError"] as string, Application.Current.Resources["String_CouldNotResolveHostnameMessage"] as string, MessageDialogStyle.Affirmative, dialogSettings);

                IsPingRunning = false;

                return;
            }

            // Add the hostname or ip address to the history
            HostnameOrIPAddressHistory = new List<string>(HistoryListHelper.Modify(HostnameOrIPAddressHistory, HostnameOrIPAddress, SettingsManager.Current.Application_HistoryListEntries));

            cancellationTokenSource = new CancellationTokenSource();

            PingOptions pingOptions = new PingOptions()
            {
                Attempts = SettingsManager.Current.Ping_Attempts,
                Timeout = SettingsManager.Current.Ping_Timeout,
                Buffer = new byte[SettingsManager.Current.Ping_Buffer],
                TTL = SettingsManager.Current.Ping_TTL,
                DontFragment = SettingsManager.Current.Ping_DontFragment,
                WaitTime = SettingsManager.Current.Ping_WaitTime
            };

            Ping ping = new Ping();

            ping.PingReceived += Ping_PingReceived;
            ping.PingCompleted += Ping_PingCompleted;
            ping.UserHasCanceled += Ping_UserHasCanceled;

            ping.SendAsync(ipAddress, pingOptions, cancellationTokenSource.Token);
        }

        private void StopPing()
        {
            CancelPing = true;
            cancellationTokenSource.Cancel();
        }

        public void OnShutdown()
        {
            if (IsPingRunning)
                PingAction();
        }
        #endregion

        #region Events
        private void Ping_UserHasCanceled(object sender, System.EventArgs e)
        {
            CancelPing = false;
            IsPingRunning = false;
        }

        private void Ping_PingCompleted(object sender, System.EventArgs e)
        {
            IsPingRunning = false;
        }

        private void Ping_PingReceived(object sender, PingArgs e)
        {
            PingInfo pingInfo = PingInfo.Parse(e);

            // Add the result to the collection
            PingResult.Add(pingInfo);
            
            // Calculate statistics
            PingsTransmitted++;

            if (pingInfo.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                PingsReceived++;

                if (PingsReceived == 1)
                {
                    MinimumTime = pingInfo.Time;
                    MaximumTime = pingInfo.Time;
                }
                else
                {
                    if (MinimumTime > pingInfo.Time)
                        MinimumTime = pingInfo.Time;

                    if (MaximumTime < pingInfo.Time)
                        MaximumTime = pingInfo.Time;
                }

                // I don't know if this can slow my application if the collection is to large
                AverageTime = (int)PingResult.Average(s => s.Time);
            }
            else
            {
                PingsLost++;
            }
        }
        #endregion
    }
}