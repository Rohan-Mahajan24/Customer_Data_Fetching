using Grpc.Net.Client;
using GrpcServer;
using System;
using Grpc.Core;


namespace grpcClient
{

    class Program
    {
        static async Task Main(string[] args)
        {

        

            var channel=GrpcChannel.ForAddress("http://localhost:5242");

            var customerClient=new Customer.CustomerClient(channel);

            var clientRequest = new CustomerLookupModel { UserId = 1 };

            var customer=await customerClient.GetCustomerInfoAsync(clientRequest);

            Console.WriteLine($"{customer.FirstName},{customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("New Customer List");
            Console.WriteLine();



            using (var call = customerClient.GetNewCustomers(new newCustomerRequest())) 
            {
                while(await call.ResponseStream.MoveNext())//movenext is one inbuit used to import from grpc.core

                {
                    var currenrtCustomer=call.ResponseStream.Current;
                    Console.WriteLine($"{currenrtCustomer.FirstName},{currenrtCustomer.LastName}:{currenrtCustomer.EmailID}");

                }
            }
                Console.ReadLine();
        }
    }
}