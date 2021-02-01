using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel
{
    public class NameAndPswd
    {
        public String SignedUserName = "temp";
        public int SignedUserPswd = 1;
    }
    public class SignedInUser : NameAndPswd
    {
        public NameAndPswd setValues(string name, int pin)
        {
            return new NameAndPswd { SignedUserName = name, SignedUserPswd = pin };
        }

    }
}
