using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyPlugins
{
    public class LeadCreate : IPlugin
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
                Entity lead = (Entity)context.InputParameters["Target"];
                try
                {
                    Entity taskRecord = new Entity("task");

                    //single line of text , Multiple line of text
                    taskRecord.Attributes.Add("subject", "Follow up (Plugin)");
                    taskRecord.Attributes.Add("description", "Some text");

                    //Date
                    taskRecord.Attributes.Add("scheduledend", DateTime.Now.AddDays(3));

                    //Option set
                    taskRecord.Attributes.Add("prioritycode", new OptionSetValue(2));

                    //Lookup
                    taskRecord.Attributes.Add("regardingobjectid", new EntityReference("lead", lead.Id));

                    //Currency
                    //taskRecord.Attributes.Add("someattribute", new Money(100));

                    Guid taskGuid = service.Create(taskRecord);


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
