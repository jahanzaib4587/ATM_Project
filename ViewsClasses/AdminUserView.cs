using System;
using System.Collections.Generic;
using System.Text;

namespace ViewsClasses
{
    public class AdminUserView
    {
        public void ChooseAdminUser()
        {
            char choice;

        AdminUserChoiceChehck:
            Console.WriteLine("\nChoose who you are! (a/b)\na)Admin\nb)User: ");
            try
            {
                choice = System.Convert.ToChar(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("\nPlease choose right option (a/b)\n");
                goto AdminUserChoiceChehck;
            }
            LogInView view = new LogInView();
            if (choice.Equals('a'))
            {

                view.AdminLogIn();
            }
            else if (choice.Equals('b'))
            {

                view.UserLogIn();
            }
            else
            {
                Console.WriteLine("\nPlease choose right option (a/b)\n");
                goto AdminUserChoiceChehck;
            }

        }
    }
}
