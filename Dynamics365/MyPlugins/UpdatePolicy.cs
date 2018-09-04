using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyPlugins
{
    public class UpdatePolicy : IPlugin

    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Extract the tracing service for use in debugging sandboxed plug-ins.  
            // If you are not registering the plug-in in the sandbox, then you do  
            // not have to add any tracing service related code.  
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the organization service reference which you will need for  
            // web service calls.  
            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);


            // The InputParameters collection contains all the data passed in the message request.  
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.  
                Entity policy = (Entity)context.InputParameters["Target"];

                // If user is creating policy record without premium
                if (!policy.Attributes.Contains("rev_policypremium")) 
                {
                    return;
                }
                decimal premium = ((Money)policy.Attributes["rev_policypremium"]).Value;


                try
                {
                    // Plug-in business logic goes here.
                    // Using query attributes
                    //QueryByAttribute query = new QueryByAttribute();
                    //query.EntityName = "rev_configuration";
                    //query.ColumnSet = new ColumnSet(new string[] {"rev_value"});
                    //query.AddAttributeValue("rev_name", "Federal Tax"); // select rev_value from rev_configuration where rev_name = "Federal Tax"

                    // Method 2:  Using QueryExpression
                    QueryExpression query2 = new QueryExpression();
                    query2.EntityName = "rev_configuration";
                    query2.ColumnSet = new ColumnSet(new string[] { "rev_value"});
                    query2.Criteria.AddCondition("rev_name", ConditionOperator.Equal, "Federal Tax");


                    // call organization service
                    //EntityCollection collection = service.RetrieveMultiple(query);
                    EntityCollection collection = service.RetrieveMultiple(query2);

                   Entity config = collection.Entities.FirstOrDefault();
                   decimal federalTax =  Convert.ToDecimal(config.Attributes["rev_value"]);

                    decimal premiumAfterTax = premium + premium * federalTax / 100;
                    policy.Attributes.Add("rev_premiumaftertaxnew", new Money(premiumAfterTax));
                     
                }

                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in MyPlug-in.", ex);
                }

                catch (Exception ex)
                {
                    tracingService.Trace("MyPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
