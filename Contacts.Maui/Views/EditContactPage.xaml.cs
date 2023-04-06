using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
	private CoreBusiness.Contact contact;
	private readonly IViewContactUseCase _viewContactUseCase;
	private readonly IEditContactUseCase _editContactUseCase;

	public EditContactPage(IViewContactUseCase viewContactUseCase, IEditContactUseCase editContactUseCase)
	{
		InitializeComponent();
		_viewContactUseCase = viewContactUseCase;
		_editContactUseCase = editContactUseCase;
	}

	private void btnCancel_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
	}

	public string ContactId
	{
		set
		{
			contact = _viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();
			if (contact != null)
			{
				contactCtrl.Name = contact.Name;
				contactCtrl.Email = contact.Email;
				contactCtrl.Phone = contact.Phone;
				contactCtrl.Address = contact.Address;
			}
		}
	}

	private async void btnUpdate_Clicked(object sender, EventArgs e)
	{
		contact.Name = contactCtrl.Name;
		contact.Email = contactCtrl.Email;
		contact.Phone = contactCtrl.Phone;
		contact.Address = contactCtrl.Address;

		//ContactRepository.UpdateContact(contact.ContactId, contact);

		await _editContactUseCase.ExecuteAsync(contact.ContactId, contact);

		await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
	}

	private void contactCtrl_OnError(object sender, string e)
	{
		DisplayAlert("Error", e, "OK");
	}
}