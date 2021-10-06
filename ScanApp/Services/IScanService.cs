using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Honeywell.AIDC.CrossPlatform;

namespace ScanApp.Services {
  public interface IScanService {
    event EventHandler<BarcodeDataArgs> BarcodeDataHasBeenReceived;
    Task OpenAsync();
    Task CloseAsync();
    void ResetSubscriptions();
  }
}
