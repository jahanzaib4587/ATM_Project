using DataAccessLayer;
using ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayers
{

    public class LogInVerfication
    {
        public Boolean UserVerification(UserObj obj)
        {
            Boolean verified = false;


            parentInput p = new parentInput();
            List<UserObj> list = p.ReadData();
            foreach (UserObj b in list)
            {
                if (obj.ID.Equals(b.ID) && obj.pswd == b.pswd)
                {
                    if (b.UserType.Equals("user"))
                    {
                        if (b.status.Equals("active"))
                        {
                            verified = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Your account is not \"active\"");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You are not exists in records as \"User\"");
                        break;
                    }
                }
                else
                {
                    verified = false;
                }

            }
            return verified;
        }

        ///////////
        public Boolean AdminVerification(UserObj obj)
        {
            Boolean verified = false;


            parentInput p = new parentInput();
            List<UserObj> list = p.ReadData();
            foreach (UserObj b in list)
            {
                if (obj.ID.Equals(b.ID) && obj.pswd == b.pswd)
                {
                    if (b.UserType.Equals("admin"))
                    {
                        if (b.status.Equals("active"))
                        {
                            verified = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Your account is not \"active\"");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You are not exists in records as \"admin\"");
                        break;
                    }
                }
                else
                {
                    verified = false;
                }

            }
            return verified;
        }
    }
}
