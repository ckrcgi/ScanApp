using System;
using ScanApp.Services;
using ScanApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanApp {
  public partial class App : Application {

    public App() {
      InitializeComponent();

      DependencyService.Register<MockDataStore>();
      DependencyService.Register<ScanService>();

      MainPage = new AppShell();
    }

    protected override void OnStart() {
    }

    protected override void OnSleep() {
    }

    protected override void OnResume() {
    }
  }
}
