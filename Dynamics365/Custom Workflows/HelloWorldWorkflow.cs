using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;


namespace Custom_Workflows
{
    public class HelloWorldWorkflow : CodeActivity
    {
        [Input("Description")]
        public InArgument<string> Description { get; set; }

        [Output("Formatted Description")]
        public OutArgument<string> FormattedDescription { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            // Read input from Input Argument
            string desc = Description.Get(executionContext);

            desc = desc.ToUpper();

            FormattedDescription.Set(executionContext, desc);

        }
    }
}
