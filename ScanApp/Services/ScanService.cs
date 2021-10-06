using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Honeywell.AIDC.CrossPlatform;
using static Honeywell.AIDC.CrossPlatform.BarcodeReaderBase;


namespace ScanApp.Services {
  public class ScanService : IScanService {
    private BarcodeReader _barCodeReader;
    private DateTime _latestScanDate = DateTime.Now;
    public event EventHandler<BarcodeDataArgs> BarcodeDataHasBeenReceived;
    private static readonly Random _random = new Random();
    private static readonly TimeSpan _timer = TimeSpan.FromMilliseconds(300);

    // Open Async
    public async Task OpenAsync() {
      if (_barCodeReader is null) {
        await InitializeAsync();
      }

      if (!_barCodeReader.IsReaderOpened) {
        Console.WriteLine("Open Reader");
        var result = await _barCodeReader.OpenAsync();
        if (result.Code == Result.Codes.SUCCESS || result.Code == Result.Codes.READER_ALREADY_OPENED) {
          await DefineBarCodeReaderSettingsAsync();
          _barCodeReader.BarcodeDataReady += OnBarCodeDataReceived;
        } else {
          Console.WriteLine($"OpenAsync failed. Code: {result.Code} | Message : {result.Message}");
        }
      }
    }

    public async Task CloseAsync() {
      if (_barCodeReader != null) {
        Console.WriteLine("Close Reader");
        // Turn off the software trigger.
        _barCodeReader.BarcodeDataReady -= OnBarCodeDataReceived;
        BarcodeDataHasBeenReceived = null;

        var result = await _barCodeReader.CloseAsync();
        if (result.Code != Result.Codes.SUCCESS) {
          Console.WriteLine($"CloseAsync failed. Code: {result.Code} | Message : {result.Message}");
        }
      }
    }

    public void ResetSubscriptions() {
      BarcodeDataHasBeenReceived = null;
    }

    private async Task InitializeAsync() {
      Console.WriteLine($"Barcode InitializeAsync");
      try {
        var readers = await BarcodeReader.GetConnectedBarcodeReaders();
        _barCodeReader = new BarcodeReader(readers.First().ScannerName);
      } catch (Exception ex) {
        Console.WriteLine($"Initialize failed. | Message : {ex.Message}");
      }
    }

    private void OnBarCodeDataReceived(object sender, BarcodeDataArgs e) {
      if (DateTime.Now - _timer >= _latestScanDate) {
        _latestScanDate = DateTime.Now;
        BarcodeDataHasBeenReceived?.Invoke(sender, e);
      }
    }

    private async Task DefineBarCodeReaderSettingsAsync() {
      try {
        var settings = new Dictionary<string, object>() {
          { _barCodeReader.SettingKeys.TriggerScanMode, _barCodeReader.SettingValues.TriggerScanMode_OneShot },
          { _barCodeReader.SettingKeys.Interleaved25Enabled, true },
          { _barCodeReader.SettingKeys.Interleaved25MaximumLength, 10 },
        };

        var result = await _barCodeReader.SetAsync(settings);
        if (result.Code != Result.Codes.SUCCESS) {
          Console.WriteLine($"Symbology settings failed. Code: {result.Code} | Message : {result.Message}");
        }
      } catch (Exception ex) {
        Console.WriteLine($"Symbology settings failed. Code: Exception: {ex.Message} ", ex);
      }
    }
  }
}