using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Honeywell.AIDC.CrossPlatform;
using ScanApp.Models;
using ScanApp.Services;
using Xamarin.Forms;

namespace ScanApp.ViewModels {
  public class ScanViewModel : BaseViewModel {
    private readonly IScanService _service;
    private string _result;

    public IScanService Service { get; }
    public string Result { get => _result; set => SetProperty(ref _result, value); }

    public ScanViewModel() {
      _service = DependencyService.Get<ScanService>();
      Result = "📦";
    }

    public void OnBarCodeDataReceived(object sender, BarcodeDataArgs e) {
      string barcode = e.Data;
      Console.WriteLine("Data received !");
      Result = barcode;
    }

    public async Task OpenScanner() => await _service.OpenAsync();
    public async Task CloseScanner() => await _service.CloseAsync();
    public void InitScanner() {
      _service.ResetSubscriptions();
      _service.BarcodeDataHasBeenReceived += OnBarCodeDataReceived;
    }
  }
}