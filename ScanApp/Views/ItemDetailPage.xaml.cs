using ScanApp.ViewModels;
using Xamarin.Forms;

namespace ScanApp.Views {
  public partial class ItemDetailPage : ContentPage {
    public ItemDetailPage() {
      InitializeComponent();
      BindingContext = new ItemDetailViewModel();
    }
  }
}