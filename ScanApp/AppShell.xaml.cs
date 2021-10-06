using System;
using System.Collections.Generic;
using ScanApp.ViewModels;
using ScanApp.Views;
using Xamarin.Forms;

namespace ScanApp {
  public partial class AppShell : Xamarin.Forms.Shell {
    public AppShell() {
      InitializeComponent();

      Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
      Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
      Routing.RegisterRoute(nameof(ScanPage), typeof(ScanPage));
    }

  }
}
