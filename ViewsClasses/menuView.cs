using DataAccessLayer;
using LogicLayers;
using ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ViewsClasses
{
    public class MenuView
    {
        readonly UserMenuItems cash;

        public MenuView()
        {
            cash = new UserMenuItems();
        }
        public void Usermenu(NameAndPswd values)
        {
            int choice;
        ReSelectMenu:
            Console.WriteLine("\n\n********Customer Menu********");
        choiceCheckUser:
            Console.WriteLine("1----Withdraw Cash\n2----Cash Transfer\n3----Deposit Cash\n4----Display Balance\n5---Exit");
            Console.WriteLine("\nPlease choose any option from 1-5");

            try
            {
                choice = System.Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid choice.\n");
                goto choiceCheckUser;
            }


            if (choice == 1)
            {
                int amount = 0;
                char c;
            reEnter:
                Console.WriteLine("Choose (a/b)\n\na)Fast Cash\nb)Normal Cash");
                Console.Write("Please select a mode of withdrawl: ");
                try
                {
                    c = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease enter right choice (a/b) ?");
                    goto reEnter;
                }

                if (c.Equals('a'))
                {
                    int a;
                reSelect:
                    Console.Write("\n1----500\n2----1000\n3----2000\n4----5000\n5----10000\n6----15000\n7----20000\n\n Select one of the denominations of money: ");

                    try
                    {
                        a = System.Convert.ToInt32(Console.ReadLine());

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nPlease enter right choice (1-7) ?");
                        goto reSelect;
                    }

                    if (a == 1 || a == 2 || a == 3 || a == 4 || a == 5 || a == 6 || a == 7)
                    {

                        switch (a)
                        {
                            case 1:
                                amount = 500;
                                break;
                            case 2:
                                amount = 1000;
                                break;
                            case 3:
                                amount = 2000;
                                break;
                            case 4:
                                amount = 5000;
                                break;
                            case 5:
                                amount = 10000;
                                break;
                            case 6:
                                amount = 15000;
                                break;
                            case 7:
                                amount = 20000;
                                break;
                        }

                        char sure;
                    sureCheck:
                        Console.Write($"\nAre you sure you want to withdraw Rs.{amount} (y/n) ? ");
                        try
                        {
                            sure = System.Convert.ToChar(Console.ReadLine());
                            if (sure.Equals('y') || sure.Equals('Y'))
                            {
                                cash.FastCash(values, amount);

                            }
                            else if (sure.Equals('n') || sure.Equals('N'))
                            {
                                goto reSelect;
                            }
                            else
                            {
                                Console.WriteLine("Please Enter right choice (y/n) ?");
                                goto sureCheck;
                            }

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please choose right option (y/n) \n");
                            goto sureCheck;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please choose options from (1-7) !");
                        goto reSelect;
                    }
                }
                else if (c.Equals('b'))
                {
                    int x;
                AmountWithdrawCheck:
                    Console.WriteLine("Enter Amount you want to withdraw");
                    try
                    {
                        x = System.Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please Enter Valid Amount");
                        goto AmountWithdrawCheck;
                    }
                    if (cash.checkAccountBalance(x, values) == true)
                    {
                        cash.NormalCash(values, x);
                    }
                    else
                    {
                        Console.Write("You dont have enough balance!\n");
                    EnterAgain:
                        Console.WriteLine("Do you want try another amount (y/n): ");
                        char ch = System.Convert.ToChar(Console.ReadLine());
                        if (ch.Equals('y') || ch.Equals('Y'))
                        {
                            goto AmountWithdrawCheck;
                        }
                        else if (ch.Equals('n') || ch.Equals('N'))
                        {

                            goto ReSelectMenu;
                        }
                        else
                        {
                            Console.WriteLine("\nPlease choose right option (y/n) ?\n");
                            goto EnterAgain;
                        }
                    }


                }

                else
                {
                    Console.WriteLine("Please enter right choice (a/b): ");
                    goto reEnter;
                }


            }
            if (choice == 2)
            {

                decimal confirmedAmount;
                decimal depositAmount;
            ReEnterAmount:
                Console.Write("Enter amount in multiples of 500:  ");
                try
                {
                    depositAmount = System.Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Enter valid amount\n");
                    goto ReEnterAmount;
                }
                if (depositAmount % 500 == 0)
                {

                    confirmedAmount = depositAmount;

                    if (cash.checkAccountBalance(System.Convert.ToInt32(confirmedAmount), values) == true)
                    {
                        int AccNo;
                    AcountNoCheck:
                        Console.Write("\nEnter the account number to which you want to transfer:  ");
                        try
                        {
                            AccNo = System.Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please Enter Valid Value.\n");
                            goto AcountNoCheck;
                        }
                        bool verify = cash.checkAccountNumber(AccNo);
                        if (verify == true)
                        {
                            cash.CashTransfer(confirmedAmount, AccNo, values);


                        choiceCheck:
                            Console.WriteLine("\nDo you want to perform any other transaction (y/n) ? \n");
                            char c;
                            try
                            {
                                c = System.Convert.ToChar(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Please enter right choice (y/n) ?");
                                goto choiceCheck;
                            }
                            if (c.Equals('y') | c.Equals('Y'))
                            {
                                goto ReSelectMenu;
                            }
                            else if (c.Equals('n') | c.Equals('N'))
                            {
                                Console.Clear();
                                Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                                AdminUserView v = new AdminUserView();
                                v.ChooseAdminUser();
                            }
                            else
                            {
                                Console.WriteLine("Please enter right choice (y/n) ?");
                                goto choiceCheck;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Acount No.does not exists\n");
                        choiceCheck:
                            Console.WriteLine("\n Do your want to enter again (y/n) ? \n");
                            char c;
                            try
                            {
                                c = System.Convert.ToChar(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Please enter right choice (y/n) ?");
                                goto choiceCheck;
                            }
                            if (c.Equals('y') | c.Equals('Y'))
                            {
                                goto AcountNoCheck;
                            }
                            else if (c.Equals('n') | c.Equals('N'))
                            {
                                goto ReSelectMenu;
                            }
                            else
                            {
                                Console.WriteLine("Please enter right choice (y/n) ?");
                                goto choiceCheck;
                            }
                        }
                    }
                    else
                    {
                        Console.Write("You dont have enough balance!\n");
                    EnterAgain:
                        Console.WriteLine("Do you want to do any other transaction (y/n): ");
                        char c = System.Convert.ToChar(Console.ReadLine());
                        if (c.Equals('y') || c.Equals('Y'))
                        {
                            goto ReSelectMenu;
                        }
                        else if (c.Equals('n') || c.Equals('N'))
                        {

                            Console.Clear();
                            AdminUserView v = new AdminUserView();
                            v.ChooseAdminUser();
                        }
                        else
                        {
                            Console.WriteLine("\nPlease choose right option (y/n) ?\n");
                            goto EnterAgain;
                        }
                    }

                }
                else
                {
                YNCheck:
                    Console.WriteLine("You have entered wrong amount\nDo you want to enter again (y/n)\n");
                    char x = System.Convert.ToChar(Console.ReadLine());
                    if (x.Equals('y') || x.Equals('Y'))
                    {
                        goto ReEnterAmount;
                    }
                    else if (x.Equals('n') || x.Equals('N'))
                    {

                        goto ReSelectMenu;
                    }
                    else
                    {
                        Console.WriteLine("\nPlease choose right option (y/n) ?\n");
                        goto YNCheck;
                    }
                }
            }

            else if (choice == 3)
            {

                decimal x;
            cashDepositCheck:
                Console.WriteLine("Enter the cash amount to deposit: ");
                try
                {
                    x = System.Convert.ToDecimal(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Enter Valid Value.\n");
                    goto cashDepositCheck;
                }
                cash.DepositCash(values, x);
            choiceCheck:
                Console.WriteLine("\nDo you want to perform any other transaction (y/n) ? \n");
                char c;
                try
                {
                    c = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter right choice (y/n) ?");
                    goto choiceCheck;
                }
                if (c.Equals('y') | c.Equals('Y'))
                {
                    goto ReSelectMenu;
                }
                else if (c.Equals('n') | c.Equals('N'))
                {
                    Console.Clear();
                    Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                    AdminUserView v = new AdminUserView();
                    v.ChooseAdminUser();
                }
                else
                {
                    Console.WriteLine("Please enter right choice (y/n) ?");
                    goto choiceCheck;
                }
            }
            else if (choice == 4)
            {
                cash.DisplayBalance(values);
            choiceCheck:
                Console.WriteLine("\nDo you want to perform any other transaction (y/n) ? \n");
                char c;
                try
                {
                    c = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter right choice (y/n) ?");
                    goto choiceCheck;
                }
                if (c.Equals('y') | c.Equals('Y'))
                {
                    goto ReSelectMenu;
                }
                else if (c.Equals('n') | c.Equals('N'))
                {
                    Console.Clear();
                    Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                    AdminUserView v = new AdminUserView();
                    v.ChooseAdminUser();
                }
                else
                {
                    Console.WriteLine("Please enter right choice (y/n) ?");
                    goto choiceCheck;
                }
            }
            else if (choice == 5)
            {
                Console.Clear();

                Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                AdminUserView v = new AdminUserView();
                v.ChooseAdminUser();
            }

            else
            {
                Console.WriteLine("\nPlease enter right choice (1-5)? \n");
                goto ReSelectMenu;
            }


        }
        /// <summary>
        /// Admin Function
        /// </summary>
        public void AdminMenu()
        {
        choiceCheckAdmin:
            Console.WriteLine("********Admin Menu********");

            Console.WriteLine("1----Create New Account\n2----Delete Existing Account\n3----Update Account Information\n4----Search for Account\n5----View Reports\n6---Exit");
            Console.WriteLine("\nPlease choose any option from 1-6");
            int Adminchoice;
            try
            {
                Adminchoice = System.Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid choice.\n");
                goto choiceCheckAdmin;
            }
            if (Adminchoice == 1)
            {
                cash.createNewUser();
                char c;
            enterAgain:
                Console.WriteLine("Do you want to do any other operation (y/n)");
                try
                {
                    c = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease select right option (y/n)? \n");
                    goto enterAgain;
                }
                if (c.Equals('y') || c.Equals('Y'))
                {
                    goto choiceCheckAdmin;
                }
                else if (c.Equals('n') || c.Equals('N'))
                {
                    Console.Clear();
                    Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                    AdminUserView v = new AdminUserView();
                    v.ChooseAdminUser();
                }
                else
                {
                    Console.WriteLine("Please enter right choice (y/n)? ");
                    goto enterAgain;
                }

            }
            if (Adminchoice == 2)
            {
                int AccNo = 0;
            accountNoCheck:
                Console.Write("Enter the account number you wished to delete!");
                try
                {
                    AccNo = System.Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("You have entered invalid Account No. Please enter correct account number\n");
                    goto accountNoCheck;
                }
                if(cash.checkAccountNumber(AccNo)==true)
                {
                    UserMenuItems u = new UserMenuItems();
                    u.DeleteAccount(AccNo);
                    char c;
                enterAgain:
                    Console.WriteLine("Do you want to do any other operation (y/n)");
                    try
                    {
                        c = System.Convert.ToChar(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nPlease select right option (y/n)? \n");
                        goto enterAgain;
                    }
                    if (c.Equals('y') || c.Equals('Y'))
                    {
                        goto choiceCheckAdmin;
                    }
                    else if (c.Equals('n') || c.Equals('N'))
                    {
                        Console.Clear();
                        Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                        AdminUserView v = new AdminUserView();
                        v.ChooseAdminUser();
                    }
                    else
                    {
                        Console.WriteLine("Please enter right choice (y/n)? ");
                        goto enterAgain;
                    }
                }
                else
                {
                  char c;
                enterAgainChoice:
                    Console.WriteLine("Account No. does not exists\nDo you want to enter again(y/n) ?");
                    try
                    {
                        c = System.Convert.ToChar(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nPlease select right option (y/n)? \n");
                        goto enterAgainChoice;
                    }
                    if (c.Equals('y') || c.Equals('Y'))
                    {
                        goto accountNoCheck;
                    }
                    else if (c.Equals('n') || c.Equals('N'))
                    {
                        goto choiceCheckAdmin;
                    }
                    else
                    {
                        Console.WriteLine("Please enter right choice (y/n)? ");
                        goto enterAgainChoice;
                    }

                }

                


            }
            if (Adminchoice == 3)
            {
                int AccNo;
            accountNoCheck:
                Console.Write("Enter the account number you wished to Update Account Information: ");
                try
                {
                    AccNo = System.Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("You have entered invalid Account No. Please enter correct account number\n");
                    goto accountNoCheck;
                }
               if(cash.checkAccountNumber(AccNo) == true)
                {
                    UserMenuItems u = new UserMenuItems();
                    u.UpdateAccount(AccNo);

                    char c;
                enterAgain:
                    Console.WriteLine("Do you want to do any other operation (y/n)");
                    try
                    {
                        c = System.Convert.ToChar(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nPlease select right option (y/n)? \n");
                        goto enterAgain;
                    }
                    if (c.Equals('y') || c.Equals('Y'))
                    {
                        goto choiceCheckAdmin;
                    }
                    else if (c.Equals('n') || c.Equals('N'))
                    {
                        Console.Clear();
                        Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                        AdminUserView v = new AdminUserView();
                        v.ChooseAdminUser();
                    }
                    else
                    {
                        Console.WriteLine("Please enter right choice (y/n)? ");
                        goto enterAgain;
                    }
                }
                //////
                else
                {
                    Console.WriteLine("Acount No.does not exists\n");
                choiceCheck:
                    Console.WriteLine("\n Do your want to enter again (y/n) ? \n");
                    char c;
                    try
                    {
                        c = System.Convert.ToChar(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter right choice (y/n) ?");
                        goto choiceCheck;
                    }
                    if (c.Equals('y') | c.Equals('Y'))
                    {
                        goto accountNoCheck;
                    }
                    else if (c.Equals('n') | c.Equals('N'))
                    {
                        goto choiceCheckAdmin;
                    }
                    else
                    {
                        Console.WriteLine("Please enter right choice (y/n) ?");
                        goto choiceCheck;
                    }
                }
                ///

                
               

            }

            if (Adminchoice == 4)
            {
                UserObj obj = new UserObj();
                Console.WriteLine($"Please enter in the field you wish to update(leave blank otherwise)\n");
            againEnterID:
                Console.Write("Account ID: ");
                string AccId = Console.ReadLine();
                if (AccId.ToString().Length > 0)
                {
                    try
                    {
                        obj.UserID = System.Convert.ToInt32(AccId);
                    }
                    catch (Exception)
                    {
                        Console.Write("please enter integer value or keep it empty\n");
                        goto againEnterID;
                    }
                }
                Console.Write("\nUser ID: ");
                string logIn = Console.ReadLine();
                if (logIn.Length > 0)
                {
                    obj.ID = logIn;
                }
                Console.Write("\nHolder's Name: ");
                string name = Console.ReadLine();
                if (name.Length > 0)
                {
                    obj.name = name;
                }
            againEnterType:
                Console.Write("\nType (Savings Current): ");
                string type = Console.ReadLine();
                if (type.Length > 0)
                {
                    if (type.Equals("current") || type.Equals("saving"))
                    {
                        obj.type = type;
                    }
                    else
                    {
                        Console.Write("\nplease enter right choice (current/saving)\n");
                        goto againEnterType;
                    }

                }
            againEnter:
                Console.Write("\nBalance: ");
                string balance = Console.ReadLine();
                if (balance.ToString().Length > 0)
                {
                    try
                    {
                        obj.cash = System.Convert.ToInt32(balance);
                    }
                    catch (Exception)
                    {
                        Console.Write("please enter integer value or keep it empty\n");
                        goto againEnter;
                    }
                }
            againEnterStatus:
                Console.Write("\nStatus(active/deactive): ");
                string status = Console.ReadLine();
                if (status.Length > 0)
                {
                    if (status.Equals("active") || status.Equals("deactive"))
                    {
                        obj.status = status;
                    }
                    else
                    {
                        Console.Write("\nplease enter right choice (active/deactive)\n");
                        goto againEnterStatus;
                    }

                }
                cash.searchForAccount(obj, AccId, name, type, balance, status);
                char c;
            enterAgain:
                Console.WriteLine("Do you want to do any other operation (y/n)");
                try
                {
                    c = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease select right option (y/n)? \n");
                    goto enterAgain;
                }
                if (c.Equals('y') || c.Equals('Y'))
                {
                    goto choiceCheckAdmin;
                }
                else if (c.Equals('n') || c.Equals('N'))
                {
                    Console.Clear();
                    Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                    AdminUserView v = new AdminUserView();
                    v.ChooseAdminUser();
                }
                else
                {
                    Console.WriteLine("Please enter right choice (y/n)? ");
                    goto enterAgain;
                }

            }
            if (Adminchoice == 5)
            {
                int choose;
            choiceCheck:
                Console.WriteLine("1---Accounts By Amount\n2---Accounts By Date");
                try
                {
                    choose = System.Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("You have entered invalid Account No. Please enter correct account number\n");
                    goto choiceCheck;
                }


                if (choose == 1)
                {
                maxAmountCheck:
                    Console.WriteLine("Enter the maximum amount: ");
                    int maxAmount = 0;

                    try
                    {
                        maxAmount = System.Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("You have entered invalid value. Please enter correct amount\n");
                        goto maxAmountCheck;
                    }

                minAmountCheck:
                    Console.WriteLine("Enter the minimum amount: ");
                    int minAmount = 0;
                    try
                    {
                        minAmount = System.Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("You have entered invalid value. Please enter correct amount\n");
                        goto minAmountCheck;
                    }
                    UserMenuItems u = new UserMenuItems();
                    u.AccountsByAmount(maxAmount, minAmount);

                }
                if (choose == 2)
                {
                startDateCheck:
                    Console.WriteLine("Enter the starting date: ");
                    string startDate;
                    try
                    {
                        startDate = Console.ReadLine();
                    }
                    catch
                    {
                        Console.WriteLine("You have entered invalid date. Please enter correct date (dd/MM/yy)\n");
                        goto startDateCheck;
                    }

                endDateCheck:
                    Console.WriteLine("Enter the ending date: ");
                    string endDate;
                    try
                    {
                        endDate = Console.ReadLine();
                    }
                    catch
                    {
                        Console.WriteLine("You have entered invalid date. Please enter correct date (dd/MM/yy)\n");
                        goto endDateCheck;
                    }




                    cash.AccountsByDate(startDate, endDate);

                }
                char c;
            enterAgain:
                Console.WriteLine("Do you want to do any other operation (y/n)");
                try
                {
                    c = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nPlease select right option (y/n)? \n");
                    goto enterAgain;
                }
                if (c.Equals('y') || c.Equals('Y'))
                {
                    goto choiceCheckAdmin;
                }
                else if (c.Equals('n') || c.Equals('N'))
                {
                    Console.Clear();
                    Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                    AdminUserView v = new AdminUserView();
                    v.ChooseAdminUser();
                }
                else
                {
                    Console.WriteLine("Please enter right choice (y/n)? ");
                    goto enterAgain;
                }

            }
            if (Adminchoice == 6)
            {
                Console.Clear();
                Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                AdminUserView v = new AdminUserView();
                v.ChooseAdminUser();
            }
            else
            {
                Console.WriteLine("\nPlease choose right option (1-6)\n");
                goto choiceCheckAdmin;
            }

        }


    }
}





