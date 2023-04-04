namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();

		List<Contact> contacts = new List<Contact>()
		{
			new Contact { Name = "John Doe", Email = "johndoe@gmail.com"},
			new Contact { Name = "Jane Doe", Email = "janedoe@gmail.com"},
			new Contact { Name = "Tom Hanks", Email = "tomhanks@gmail.com"},
			new Contact { Name = "Frank Liu", Email = "frankliu@gmail.com"}
		};

		listContacts.ItemsSource = contacts;
	}

	private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (listContacts.SelectedItem != null)
		{
			await Shell.Current.GoToAsync(nameof(EditContactPage));
		}
	}

	private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
	{
		listContacts.SelectedItem = null;
	}
}

public class Contact
{
	public string Name { get; set; }

	public string Email { get; set; }
}