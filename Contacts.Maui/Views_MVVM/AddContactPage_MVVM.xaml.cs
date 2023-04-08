using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_MVVM;

public partial class AddContactPage_MVVM : ContentPage
{
	private readonly ContactViewModel _contactViewModel;

	public AddContactPage_MVVM(ContactViewModel contactViewModel)
	{
		InitializeComponent();
		_contactViewModel = contactViewModel;
		this.BindingContext = _contactViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		_contactViewModel.Contact = new CoreBusiness.Contact(); //Limpar os campos
	}
}