using LogicLayers;
using ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewsClasses
{
    public class LogInView
    {
        public void UserLogIn()
        {
            int pin;
        ReEnter:
            Console.WriteLine("\nEnter login");
            string userId = Console.ReadLine();
        pinFormatCheck:
            Console.WriteLine("Enter Pin code");
            try
            {
                pin = System.Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter 5-digit pin");
                goto pinFormatCheck;
            }
            UserObj bo = new UserObj { ID = userId, pswd = pin };
            LogInVerfication temp = new LogInVerfication();
            bool verify = temp.UserVerification(bo);
            SignedInUser s = new SignedInUser();
            MenuView m = new MenuView();
            if (verify == true)
            {
                NameAndPswd dataHolder = s.setValues(userId, pin);
                m.Usermenu(dataHolder);
            }
            else
            {
                char choose;
            ReEnter2:
                Console.WriteLine("\nYou have enetered invalid ID or PIN! Do you want to enter again (y/n)?\n");
                try
                {
                    choose = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please choose correct optioin (y/n)");
                    goto ReEnter2;
                }

                if (choose.Equals('y') || choose.Equals('Y'))
                {
                    goto ReEnter;
                }
                else if (choose.Equals('n') || choose.Equals('N'))
                {
                    Console.Clear();
                    Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                    AdminUserView v = new AdminUserView();
                    v.ChooseAdminUser();
                }
                else
                {
                    Console.WriteLine("Please choose right choice!");
                    goto ReEnter2;
                }

            }

        }

        /// <summary>
        /// Admin LogIn
        /// </summary>
        public void AdminLogIn()
        {
        ReEnterAdmin:
            Console.WriteLine("Enter login");
            string userId = Console.ReadLine();
            Console.WriteLine("Enter Pin code");

            int pin = System.Convert.ToInt32(Console.ReadLine());

            UserObj bo = new UserObj { ID = userId, pswd = pin };
            LogInVerfication temp = new LogInVerfication();
            Boolean verify = temp.AdminVerification(bo);
            SignedInUser s = new SignedInUser();
            MenuView m = new MenuView();
            if (verify == true)
            {
                NameAndPswd dataHolder = s.setValues(userId, pin);
                
                m.AdminMenu();
            }

            else
            {
                char choose;
            ReEnter2:
                Console.WriteLine("\nYou have enetered invalid ID or PIN! Do you want to enter again (y/n)?\n");
                try
                {
                    choose = System.Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please choose correct optioin (y/n)");
                    goto ReEnter2;
                }
                if (choose.Equals('y') || choose.Equals('Y'))
                {
                    goto ReEnterAdmin;
                }
                else if (choose.Equals('n') || choose.Equals('N'))
                {
                    Console.Clear();
                    Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");

                    AdminUserView v = new AdminUserView();
                    v.ChooseAdminUser();
                }
                else
                {
                    Console.WriteLine("Please choose right choice!");
                    goto ReEnter2;
                }

            }

        }
    }
}
