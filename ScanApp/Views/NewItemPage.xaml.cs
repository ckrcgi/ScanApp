using ScanApp.Models;
using ScanApp.ViewModels;
using Xamarin.Forms;

namespace ScanApp.Views {
  public partial class NewItemPage : ContentPage {
    public Item Item { get; set; }

    public NewItemPage() {
      InitializeComponent();
      BindingContext = new NewItemViewModel();
    }
  }
}