using System;
using System.Collections.Generic;
using System.Text;
using ObjectModel;
using System.IO;

namespace DataAccessLayer
{
    public class TransactionsReadWrite
    {
        public void WriteTransactionData(List<TransactionObj> list)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream("Transaction.csv", FileMode.Open, FileAccess.Write);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":  Error while attempting to access file - is - " + ex.ToString());

            }
            StreamWriter stwr = new StreamWriter(fs);
            try
            {
                foreach (TransactionObj s in list)
                {
                    stwr.WriteLine($"{s.TransactionType},{s.ID},{s.name},{s.amount},{s.date}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":  Error while attempting to write file - is - " + ex.ToString());

            }
            stwr.Close();
            fs.Close();
        }



        public List<TransactionObj> ReadTransactionData()
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream("Transaction.csv", FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString() + ":  Error while attempting to read file - is - " + ex.ToString());

            }
            StreamReader stwr = new StreamReader(fs);
            List<TransactionObj> empList = new List<TransactionObj>();
            string str;

            try
            {
                while ((str = stwr.ReadLine()) != null)
                {
                    var chunks = str.Split(',');
                    empList.Add(new TransactionObj { TransactionType = chunks[0], ID = chunks[1], name = chunks[2], amount = System.Convert.ToDecimal(chunks[3]), date = System.Convert.ToDateTime(chunks[4]) });
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"There is an error in records of file {e}");
            }
            stwr.Close();
            fs.Close();
            return empList;

        }


        public void AddNewTransaction(TransactionObj obj)
        {
             
            try
            {
                StreamWriter stwr = File.AppendText("Transaction.csv");
                stwr.WriteLine($"{obj.TransactionType},{obj.ID},{obj.name},{obj.amount},{obj.date}");
                stwr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"There is an error in records of file {e}");

            }
            

        }
    }




}


