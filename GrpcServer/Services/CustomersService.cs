using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomersService:Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            this.logger = logger;
        }
        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
           CustomerModel output = new CustomerModel();   
            
            if (request.UserId == 1) {

                output.FirstName = "Rohan";
                output.LastName = "Mahajan";
            }
            else if (request.UserId == 2) {

                output.FirstName = "Kusuma";
                output.LastName = "Kavya";

            }
            else
            {
                output.FirstName = "Varsha";
                output.LastName = "Pradeep";

            }

            return Task.FromResult(output);
        }
        //to add task of new rpc calls
        public override async Task GetNewCustomers(newCustomerRequest request, 
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)

        {
           
            List<CustomerModel> customers = new List<CustomerModel>()
            {
                new CustomerModel
                {
                    FirstName="Vikas",
                    LastName="Shankar",
                    EmailID="vikas@gmail.com",
                    IsALive=true,
                    Age=50
                    
                },
                new CustomerModel
                {
                    FirstName="Rohan",
                    LastName="Mahajan",
                    EmailID="rohan@gmail.com",
                    IsALive=true,
                    Age=50

                },
                new CustomerModel
                {
                    FirstName="Rahul",
                    LastName="Raj",
                    EmailID="rahulR@gmail.com",
                    IsALive=false,
                    Age=50

                }
        };

            foreach(var customer in customers)
            {
               await Task.Delay(1000);//adding delay of 1sec for ecah data 
               await responseStream.WriteAsync(customer);
            }
        }
    }
}
