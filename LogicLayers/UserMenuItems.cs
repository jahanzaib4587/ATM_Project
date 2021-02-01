using DataAccessLayer;
using ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LogicLayers
{
    public class UserMenuItems
    {
        readonly parentInput p;
        readonly List<UserObj> list;
        public DateTime today;
        public UserMenuItems()
        {
            p = new parentInput();
            list = p.ReadData();
            today = DateTime.Today;
        }
        public void FastCash(NameAndPswd val, int value)
        {
            int userId = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == val.SignedUserName)
                {
                    if (value <= list[i].cash)
                    {
                        userId = list[i].UserID;
                        list[i].cash = list[i].cash - value;
                        list[i].date = today;
                        CopyTransactionData(list, userId, "withdraw", value);
                        Console.WriteLine("Cash Suceessfully Withdrawn");
                        char c;
                    receiptCheck:
                        Console.Write("\nDo you wish to print a receipt(y/n)?");
                        try
                        {
                            c = System.Convert.ToChar(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter valid value (y/n)!");
                            goto receiptCheck;
                        }

                        if (c.Equals('y') || c.Equals('Y'))
                        {
                            Console.WriteLine($"\nAccount #{list[i].UserID}\nDate:  {list[i].date.ToString("dd/MM/yyyy")}\n\nWithdrawn : {value}\nBalance :{list[i].cash}\n");
                            break;
                        }
                        else if (c.Equals('n') || c.Equals('N'))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease choose right choice from (y/n)");
                            goto receiptCheck;

                        }
                    }
                    else
                    {
                        Console.WriteLine("You dont have enough balance");
                        break;
                    }


                }
            }
            p.writeData(list);
        }

        public void NormalCash(NameAndPswd val, decimal value)
        {
            int userId = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == val.SignedUserName && value <= list[i].cash)
                {
                    userId = list[i].UserID;
                    list[i].cash = list[i].cash - value;
                    //today = DateTime.Today;
                    list[i].date = today;
                    Console.WriteLine($"\nAccount #{list[i].UserID}\nDate:  {list[i].date.ToString("dd/MM/yyyy")}\n\nWithdrawn : {value}\nBalance :{list[i].cash}\n");
                    CopyTransactionData(list, userId, "withdraw", System.Convert.ToInt32(value));
                    p.writeData(list);
                }

            }
        }

        ///DeositCash
        public void DepositCash(NameAndPswd val, decimal value)
        {
            int userId = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == val.SignedUserName)
                {
                    userId = list[i].UserID;
                    list[i].cash = list[i].cash + value;
                    list[i].date = today;
                    Console.WriteLine("Cash Deposited Suceessfully.");
                    CopyTransactionData(list, userId, "deposit", System.Convert.ToInt32(value));
                    char c;
                receiptCheck:
                    Console.Write("\nDo you wish to print a receipt(y/n)?");
                    try
                    {
                        c = System.Convert.ToChar(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter valid value (y/n)!");
                        goto receiptCheck;
                    }

                    if (c.Equals('y') || c.Equals('Y'))
                    {
                        Console.WriteLine($"\nAccount #{list[i].UserID}\nDate:  {list[i].date.ToString("dd/MM/yyyy")}\n\nDeposited : {value}\nBalance :{list[i].cash}\n");
                        break;
                    }
                    else if (c.Equals('n') || c.Equals('N'))
                    {

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid value (y/n)!");
                        goto receiptCheck;
                    }
                }
            }

            p.writeData(list);
        }

        public void DisplayBalance(NameAndPswd val)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == val.SignedUserName)
                {
                    Console.WriteLine($"Account #{list[i].UserID}\nDate:  {today.ToString("dd/MM/yyyy")}\n\nBalance: {list[i].cash}\n ");
                    break;
                }


            }
        }

        public void DeleteAccount(int AccNo)
        {
            int AccNoCheck;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].UserID == AccNo && list[i].UserType != "admin")
                {
                AccountNoCheck:
                    Console.WriteLine($"You wish to delete the account held by Mr.{list[i].ID}; \nIf this information is correct please re-enter the account number: ");
                    try
                    {
                        AccNoCheck = System.Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please Enter valid account No.");
                        goto AccountNoCheck;
                    }
                    if (AccNo == AccNoCheck)
                    {


                        list.RemoveAt(i);


                        Console.WriteLine("\nAccount Deleted Successfully\n");
                        p.writeData(list);
                        return;
                    }
                    

                }
                


            }

        }



        /// <summary>
        public void searchForAccount(UserObj obj, string AccId, string name, string type, string balance, string status)
        {
            List<UserObj> lst = new List<UserObj>();
            for (int i = 0; i < list.Count; i++)
            {

                if ((AccId.Equals("") ? true : list[i].UserID == obj.UserID) && (balance.Equals("") ? true : list[i].cash == obj.cash) && (name.Equals("") ? true : list[i].ID.Equals(obj.ID)) && (type.Equals("") ? true : list[i].type.Equals(obj.type)) && (status.Equals("") ? true : list[i].status.Equals(obj.status)))
                {
                    lst.Add(list[i]);
                }
            }
            Console.WriteLine("SEARCH  MENU:\n");
            foreach (UserObj o in lst)
            {

                string s = string.Format(
                     "\nAccount ID  User ID   Holders Name         Type    Balance     Status\n {0,-10}  {1,-10}  {2,-15}  {3,-10} {4,-8} {5,-10} ",
                      o.UserID,
                      o.ID,
                      o.name,
                      o.type,
                      o.cash,
                      o.status
                     );
                Console.WriteLine(s);
            }
        }
        /// </summary>

        public void UpdateAccount(int AccNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].UserID == AccNo)
                {
                    Console.WriteLine($"Account # {list[i].UserID}\nType: {list[i].UserType}\nHolder: {list[i].name}\nBalance: {list[i].cash}\nStatus: {list[i].status}\n\n");
                    Console.WriteLine($"Please enter in the field you wish to update(leave blank otherwise)\nLogin: ");
                    string logIn = Console.ReadLine();
                    Console.Write("Holder's Name: ");
                    string name = Console.ReadLine();
                pinCheck:
                    Console.Write("Enter 5-digit Pin Code: ");
                    string pin;
                    pin = Console.ReadLine();
                    try
                    {
                        if (pin.ToString().Length > 0)
                        {
                            if (pin.ToString().Length == 5)
                            {
                                list[i].pswd = System.Convert.ToInt32(pin);
                            }
                            else
                            {
                                Console.WriteLine("Enter valid 5-digit pin");
                                goto pinCheck;
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Enter valid 5-digit pin");
                        goto pinCheck;
                    }
                    if (logIn.Length > 0)
                    {
                        list[i].ID = logIn;
                    }
                    
                    if (name.Length > 0)
                    {
                        list[i].name = name;
                    }
                    Console.WriteLine("\nAccount Information Updated Successfully\n");
                    p.writeData(list);
                }

            }
        }

        public void AccountsByAmount(int maxAmount, int minAmount)
        {
            Console.WriteLine("\n===== Search Results =====\n");
            for (int i = 0; i < list.Count; i++)
            {

                if (list[i].cash >= minAmount && list[i].cash <= maxAmount)
                {

                    string s = string.Format(
                     "User ID  Holders Name       Amount        Date\n {0,-6}  {1,-18}  {2,-10}  {3,-8} ",
                      list[i].UserID,
                      list[i].name,
                      list[i].cash,
                      list[i].date.ToString("dd/MM/yyyy")
                     );
                    Console.WriteLine(s);

                }
            }
        }


        public void ReduceAmount(decimal amount, NameAndPswd SignedInUser)
        {
            int userId = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID.Equals(SignedInUser.SignedUserName))
                {
                    userId = list[i].UserID;
                    Console.Write($"Previoius Balance :{list[i].cash}\n");
                    list[i].cash -= amount;
                    Console.WriteLine($"Remaining Balance: { list[i].cash}\nYou have successfully Transferred Rs.{amount}\n");
                    CopyTransactionData(list, userId, "Cash Transfer", System.Convert.ToInt32(amount));

                }
            }
            p.writeData(list);
        }
        public void CashTransfer(decimal amount, int AccNo, NameAndPswd SignedInUser)
        {

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].UserID == AccNo)
                {

                    list[i].cash += amount;
                    ReduceAmount(amount, SignedInUser);

                    CopyTransactionData(list, AccNo, "Cash Recieved", System.Convert.ToInt32(amount));

                }
            }

            p.writeData(list);




        }


        public int nextUserId()
        {
            int lastUserId = 0;
            for (int i = 0; i < list.Count; i++)
            {
                lastUserId = list[i].UserID;
            }
            return lastUserId + 1;
        }
        public bool checkAccountBalance(int amount, NameAndPswd SignedInUser)
        {

            bool result = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID.Equals(SignedInUser.SignedUserName))
                {
                    if (list[i].cash >= amount)
                    {
                        result = true;
                        break;
                    }

                }
            }
            return result;
        }

        public bool checkAccountNumber(int AccNo)
        {

            bool result = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].UserID == AccNo)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void createNewUser()
        {
            UserObj obj = new UserObj();
        enterAgain:
            Console.Write("Login: ");
            string logIn = Console.ReadLine();
            if (logIn.Length > 0)
            {
                obj.ID = logIn;
            }
            else
            {
                Console.WriteLine("Please enter right user name");
                goto enterAgain;
            }
        pinCode:
            Console.Write("Pin Code: ");
            try
            {
                int pswd = System.Convert.ToInt32(Console.ReadLine());
                if (pswd.ToString().Length == 5)
                {
                    obj.pswd = pswd;
                }
                else
                {
                    Console.WriteLine("Please enter 5-digit pin");
                    goto pinCode;
                }
            }
            catch (Exception)
            {
                Console.Write("You have intered invalid Pin! Please enter 5-digit pin in numbers only");
                goto pinCode;
            }
        enterAgainName:
            Console.Write("Holder's Name: ");
            string name = Console.ReadLine();
            if (name.Length > 0)
            {
                obj.name = name;
            }
            else
            {
                Console.WriteLine("Please enter valid  logIn Id");
                goto enterAgainName;
            }
        typeCheck:
            Console.Write("Type (saving,current): ");
            string type = Console.ReadLine();
            if (type.Equals("saving") || type.Equals("current"))
            {
                obj.type = type;
            }
            else
            {
                Console.WriteLine("You have entered invalid value! Please enter correct account type (saving OR current)");
                goto typeCheck;
            }
        startingBalance:
            Console.Write("Starting Balance: ");
            try
            {
                obj.cash = System.Convert.ToDecimal(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("You have entered invalid value! Please enter correct amount");
                goto startingBalance;

            }
        statusCheck:
            Console.Write("Status (active/deactive): ");
            string status = Console.ReadLine();
            if (status.Equals("active") || status.Equals("deactive"))
            {
                obj.status = status;
            }
            else
            {
                Console.WriteLine("You have entered invalid value! Please enter correct account status");
                goto statusCheck;
            }
            obj.UserID = nextUserId();
            obj.UserType = "user";
            obj.date = DateTime.Today;
            parentInput p = new parentInput();
            p.CreateNewAccount(obj);
            Console.WriteLine($"\nAccount Successfully Created - the account number assigned is: {obj.UserID}");
        }
        public void CopyTransactionData(List<UserObj> list, int UserId, string type, int cash)
        {
            TransactionsReadWrite tr = new TransactionsReadWrite();
            TransactionObj obj = new TransactionObj();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].UserID == UserId)
                {
                    obj.TransactionType = type;
                    obj.ID = list[i].ID;
                    obj.name = list[i].name;
                    obj.amount = cash;
                    obj.date = System.Convert.ToDateTime(DateTime.Today.ToString("dd/MM/yyyy"));
                }

            }
            tr.AddNewTransaction(obj);

        }

        public void AccountsByDate(string startDate, string endDate)
        {
            TransactionsReadWrite t = new TransactionsReadWrite();
            List<TransactionObj> list;
            list = t.ReadTransactionData();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].date.Date >= System.Convert.ToDateTime(startDate).Date && list[i].date.Date <= System.Convert.ToDateTime(endDate).Date)
                {
                    string s = string.Format(
                    "Transaction Type   User ID  Holders Name   Amount     Date\n {0,-16}  {1,-8}  {2,-12}  {3,-8} {4}",
                     list[i].TransactionType,
                     list[i].ID,
                     list[i].name,
                     list[i].amount,
                     list[i].date.ToString("dd/MM/yyyy")
                    );
                    Console.WriteLine(s);
                }
                else
                {
                    Console.WriteLine("No record found");
                }

            }
        }


    }
}
