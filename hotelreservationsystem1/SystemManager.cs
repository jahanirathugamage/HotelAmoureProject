using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelreservationsystem1
{
    public class SystemManager
    {
        private List<Person> persons = new List<Person>();
        private List<Account> accounts = new List<Account>();

        public void AddPerson(Person person, Account account)
        {
            persons.Add(person);
            accounts.Add(account);
        }

        public void RemoveAccount(int guestID = -1)
        {
            try
            {
                // get all guests and admins
                Guest guestToRemove = GuestManager.GetAllGuests().FirstOrDefault(g => g.getGuestID() == guestID);

                if (guestToRemove == null)
                {
                    Console.WriteLine("No guest found with the given ID.");
                    return;
                }

                // find out whether the account ID based on guest or admin
                int accountID = guestToRemove.getAccountID();

                if (accountID != -1)
                {
                    // remove the guest/admin first to prevent foreign key issues
                    GuestManager.DeleteGuest(guestID);

                    // remove the corresponding account
                    AccountManager.DeleteAccount(accountID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
