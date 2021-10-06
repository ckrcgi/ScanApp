using Honeywell.AIDC.CrossPlatform;
using ScanApp.Services;
using ScanApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanApp.Views {
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ScanPage : ContentPage {
    private readonly ScanViewModel _vm;

    public ScanPage() {
      InitializeComponent();
      this.BindingContext = _vm = new ScanViewModel();
    }

    protected override async void OnAppearing() {
      base.OnAppearing();
      _vm.InitScanner();
      await _vm.OpenScanner();
    }

    protected override async void OnDisappearing() {
      base.OnDisappearing();
      await _vm.CloseScanner();
    }
  }
}