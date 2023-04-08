using Contacts.UseCases.PluginInterfaces;
using SQLite;

namespace Contact.Plugins.DataStore.SQLLite
{
	public class ContactSQLiteRepository : IContactRepository
	{
		private SQLiteAsyncConnection _connection;

        public ContactSQLiteRepository()
        {
			_connection = new SQLiteAsyncConnection(Constants.DatabasePath);
			_connection.CreateTableAsync<Contacts.CoreBusiness.Contact>();
        }

        public async Task AddContactAsync(Contacts.CoreBusiness.Contact contact)
		{
			await _connection.InsertAsync(contact);
		}

		public async Task DeleteContactAsync(int contactId)
		{
			var contact = await GetContactByIdAsync(contactId);
			if(contact != null && contact.ContactId == contactId)
				await _connection.DeleteAsync(contact);
		}

		public async Task<Contacts.CoreBusiness.Contact> GetContactByIdAsync(int contactId)
		{
			return await _connection.Table<Contacts.CoreBusiness.Contact>().Where(x => x.ContactId == contactId).FirstOrDefaultAsync();
		}

		public async Task<List<Contacts.CoreBusiness.Contact>> GetContactsAsync(string filterText)
		{
			if(string.IsNullOrWhiteSpace(filterText))
				return await _connection.Table<Contacts.CoreBusiness.Contact>().ToListAsync();

			return await _connection.QueryAsync<Contacts.CoreBusiness.Contact>(@"
					SELECT *
					FROM Contact
					WHERE
						Name LIKE ? OR
						Email LIKE ? OR
						Phone LIKE ? OR
						Address LIKE ?",
						$"{filterText}%",
						$"{filterText}%",
						$"{filterText}%",
						$"{filterText}%");
		}

		public async Task UpdateContactAsync(int contactId, Contacts.CoreBusiness.Contact contact)
		{
			if(contactId == contact.ContactId)
				await _connection.UpdateAsync(contact);
		}
	}
}
