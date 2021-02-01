using ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLayer
{
    public class parentInput
    {

        /// <summary>
        /// File Writting
        /// </summary>
        public void writeData(List<UserObj> list)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("Data.csv", FileMode.Create, FileAccess.Write);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":  Error while attempting to access file - is - " + ex.ToString());

            }
            StreamWriter stwr = new StreamWriter(fs);
            try
            {
                
                foreach (UserObj s in list)
                {
                    
                    stwr.WriteLine($"{s.UserID},{s.ID},{s.pswd},{s.date},{s.cash},{s.UserType},{s.name},{s.type},{s.status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":  Error while attempting to write file - is - " + ex.ToString());

            }
            stwr.Close();
            fs.Close();
        }

        /// <summary>
        /// File Reading
        /// </summary>

        public List<UserObj> ReadData()
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream("Data.csv", FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":  Error while attempting to read file - is - " + ex.ToString());

            }
            StreamReader stwr = new StreamReader(fs);
            List<UserObj> empList = new List<UserObj>();
            string str;

            try
            {
                while ((str = stwr.ReadLine()) != null)
                {
                    var chunks = str.Split(',');
                    empList.Add(new UserObj { UserID = System.Convert.ToInt32(chunks[0]), ID = chunks[1], pswd = System.Convert.ToInt32(chunks[2]), cash = System.Convert.ToDecimal(chunks[4]), date = System.Convert.ToDateTime(chunks[3]), UserType = System.Convert.ToString(chunks[5]), name = chunks[6], type = chunks[7], status = chunks[8] });
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"There is an error in records of file {e.ToString()}");
            }
            stwr.Close();
            fs.Close();
            return empList;

        }

        public void CreateNewAccount(UserObj list)
        {
            try
            {
                StreamWriter stwr = File.AppendText("Data.csv");
                stwr.WriteLine($"{list.UserID},{list.ID},{list.pswd},{list.date},{list.cash},{list.UserType},{list.name},{list.type},{list.status}");
                stwr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine($"There is an error in records of file {e.ToString()}");

            }
           

        }
       



    }
}
