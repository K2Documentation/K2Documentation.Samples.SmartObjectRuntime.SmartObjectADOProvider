using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//import ADO.NET provider
using SourceCode.Data.SmartObjectsClient;
//import system.data namespace so we can use datatables
using System.Data;

namespace K2Learning.Samples.SmartObjectRuntime.SmartObjectADOProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteScalarReadStatement();
            ExecuteSelectStatement();
            ExecuteSelectStatementJoin();
        }

        //Sample: building a connection string to K2 smartobject server
        static SOConnection EstablishK2Connection()
        {
            SOConnectionStringBuilder hostServerConnectionString = new SOConnectionStringBuilder();
            hostServerConnectionString.Server = "localhost"; //your value may be different
            hostServerConnectionString.Port = 5555;

            SOConnection connection = new SOConnection(hostServerConnectionString.ToString());
            return connection;
        }

        //sample: executing a scalar method (Read) with the ADO.NET provider
        static void ExecuteScalarReadStatement()
        {
            Console.WriteLine("**executing a scalar READ method with the ADO.NET provider");
            Console.WriteLine("(get display name and email for current user)**");

            using (SOConnection soServerConnection = EstablishK2Connection())
            {
                //constrcut the current username (assuming the K2 label and current user id)
                string currentUserFQN = "K2:" + System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                soServerConnection.Open();
                //EXEC a scalar method with an input paramter
                string query = @"EXEC [Users_and_Groups.Get_E_mail_For_User] @User_Fully_Qualified_Name = '" + currentUserFQN + "'";
                using (SOCommand command = new SOCommand(query, soServerConnection))
                //in this sample we'll use a SQL data reader to read the records one by one
                using (SODataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Display Name: " + reader["Display_Name"].ToString()
                                             + " Email: " + reader["E_mail"].ToString());
                    }
                    Console.WriteLine("**completed executing the scalar READ method with the ADO.NET provider**");
                    Console.ReadLine();
                }
            }
        }


        //sample: executing a simple list method with the ADO.NET provider's SELECT statement
        static void ExecuteSelectStatement()
        {
            Console.WriteLine("**Executing SmartObject list method with the ADO Providers SELECT statement");
            Console.WriteLine("(return Active workflow instances only)**");

            using (SOConnection soServerConnection = EstablishK2Connection())
            {
                soServerConnection.Open();
                //build up the SQL-92 Query. Notice the user of System Names, not display names
                //you can filter with normal WHERE clauses
                string query = "SELECT * FROM WorkflowReportingService_WorkflowInstanceReport WHERE Status = 'Active'";
                using (SOCommand command = new SOCommand(query, soServerConnection))
                {
                    //in this sample we'll use a Data Adpater so we can fill a datatable
                    using (SODataAdapter adapter = new SODataAdapter(command))
                    {
                        soServerConnection.DirectExecution = true; //direct execution will show performance improvements for SQL-based smartobjects
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine("Folio: " + dr["ProcessInstanceFolio"].ToString()
                                              + " | ProcInstID: " + dr["ProcessInstanceID"].ToString());
                        }
                    }
                }
            }
            Console.WriteLine("**Completed SmartObject list method with the ADO.NET provider**");
            Console.ReadLine();
        }

        //sample: joining two List methods with a JOIN operator, filtering and returnign TOP N items
        static void ExecuteSelectStatementJoin()
        {
            Console.WriteLine("**Executing SmartObject list method with the ADO Provider SELECT statement");
            Console.WriteLine("(return top 20 active workflow instances, join to Activity instances)**");

            using (SOConnection soServerConnection = EstablishK2Connection())
            {
                soServerConnection.Open();
                //build up the SQL-92 Query. Notice the user of System Names, not display names
                //you can filter with normal WHERE clauses
                string query = "SELECT TOP 20 * FROM Process_Instance JOIN Activity_Instance ON Process_Instance.ProcessInstanceID = Activity_Instance.ProcessInstanceID WHERE Process_Instance.Status = 'Active'";
                using (SOCommand command = new SOCommand(query, soServerConnection))
                {
                    //in this sample we'll use a Data Adpater so we can fill a datatable
                    using (SODataAdapter adapter = new SODataAdapter(command))
                    {
                        soServerConnection.DirectExecution = true; //direct execution will show performance improvements for SQL-based smartobjects
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            Console.WriteLine("Folio: " + dr["Folio"].ToString()
                                              + " | ProcInstID: " + dr["ProcessInstanceID"].ToString()
                                              + " | ActivityName: " + dr["ActivityName"].ToString());
                        }
                    }
                }
            }
            Console.WriteLine("**Completed SmartObject list method JOIN with the ADO.NET provider**");
            Console.ReadLine();
        }
    }
}
