using System.Data.SqlClient;
namespace Expense_tracker_sql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string tablename = "Expensetracker";
            string a = "";
            SqlConnection con = new SqlConnection("server=IN-8JRQ8S3; database=Expense_tracker;Integrated Security=true");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Welcome TO Expense Tracker App");
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Income");
                Console.WriteLine("4. Check Available Balance");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {

                            Console.WriteLine("Enter Title: ");
                            string title = Console.ReadLine();
                            Console.WriteLine("Enter description: ");
                            string desc = Console.ReadLine();
                            Console.WriteLine("Enter Amount: ");
                            int amt = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter date: ");
                            string date = Console.ReadLine();
                            con.Open();
                            SqlCommand insertcommand = new SqlCommand($"insert into {tablename} values(@Title,@Description,@Amount,@Date)", con);
                            insertcommand.Parameters.AddWithValue("@Title", title);
                            insertcommand.Parameters.AddWithValue("@Description", desc);
                            insertcommand.Parameters.AddWithValue("@Amount", amt);
                            insertcommand.Parameters.AddWithValue("@Date", date);
                            insertcommand.ExecuteNonQuery();
                            con.Close();
                            Console.WriteLine("Record Added Successfully");

                            break;
                        }
                    case 2:
                        {
                            SqlCommand viewexpcommand = new SqlCommand($"select * from {tablename} where Amount<0", con);
                            con.Open();
                            SqlDataReader dr = viewexpcommand.ExecuteReader();
                            while (dr.Read())
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    Console.WriteLine($"{dr.GetName(i)}:{dr.GetValue(i)} \t");

                                }
                                Console.WriteLine();
                            }

                            con.Close();
                            break;
                        }
                    case 3:
                        {
                            SqlCommand viewinccommand = new SqlCommand($"select * from {tablename} where Amount>0", con);
                            con.Open();
                            SqlDataReader dr = viewinccommand.ExecuteReader();
                            while (dr.Read())
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    Console.WriteLine($"{dr.GetName(i)}:{dr.GetValue(i)} \t");

                                }
                                Console.WriteLine();
                            }
                            con.Close();
                            break;
                        }
                    case 4:
                        {
                            SqlCommand amtcommand = new SqlCommand($" select SUM(Amount) from {tablename} ", con);
                            con.Open();
                            int balance = (int)amtcommand.ExecuteScalar();
                            Console.WriteLine($"Balance: {balance}");
                            con.Close();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("you have entered wrong choice: ");
                            break;
                        }
                }
                Console.WriteLine("Do you wish to continue? [y/n] ");
                a = Console.ReadLine();
            } while (a.ToLower() == "y");            
        }
    }
}