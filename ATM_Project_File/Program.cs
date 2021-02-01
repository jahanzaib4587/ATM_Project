using DataAccessLayer;
using ObjectModel;
using System;
using ViewsClasses;

namespace ATM_Project_File
{
    class Program
    {
        
        static void Main(string[] args)
        {
           
            
             Console.WriteLine("/////////////////////////////////////////\n   Welcome to ATM Managment System       \n/////////////////////////////////////////\n");
            
             AdminUserView v = new AdminUserView();
             v.ChooseAdminUser();
            
          
            
            






        }
    }
}
