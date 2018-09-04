using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Metadata;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient client = new CrmServiceClient("Url=https://revforce.crm.dynamics.com; Username=jwillia3@revforce.onmicrosoft.com; Password=Khunta96#; authtype=Office365");
            IOrganizationService service = (IOrganizationService)
            client.OrganizationWebProxyClient != null ? (IOrganizationService)client.OrganizationWebProxyClient : (IOrganizationService)client.OrganizationServiceProxy;

            //Auto Number Field Setup
            CreateAttributeRequest widgetSerialNumberAttributeRequest = new CreateAttributeRequest
            {
                EntityName = "account",
                Attribute = new StringAttributeMetadata
                {
                    //Define the format of the attribute
                    AutoNumberFormat = "WID-{SEQNUM:5}-{RANDSTRING:6}-{DATETIMEUTC:yyyyMMddhhmmss}",
                    LogicalName = "rev_autonumber",
                    SchemaName = "rev_AutoNumber",
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    MaxLength = 100, // The MaxLength defined for the string attribute must be greater than the length of the AutoNumberFormat value, that is, it should be able to fit in the generated value.
                    DisplayName = new Label("Auto Number", 1033),
                    Description = new Label("Unique Auto Incrementing field", 1033)
                }
            };
            service.Execute(widgetSerialNumberAttributeRequest);
            Console.WriteLine("Field Added.");

            //Exporting Dynamics Data - RetrieveEntityChangesRequest
            RetrieveEntityChangesRequest req = new RetrieveEntityChangesRequest();
            req.EntityName = "account";
            req.Columns = new ColumnSet("name");
            RetrieveEntityChangesResponse response = (RetrieveEntityChangesResponse)service.Execute(req);

            /*
            //Specific Entity Key
            Entity accont = new Entity("account", "accountnumber", "963963");
            accont.Attributes.Add("name", "ABCD Company");
            service.Update(accont);
            
            //Bulk Delete
            string xml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='rev_policy'>
                <attribute name='rev_policyid'/>
                </entity>
            </fetch>";

            EntityCollection delCollection = service.RetrieveMultiple(new FetchExpression(xml));
            foreach(Entity policy in delCollection.Entities)
            {
                service.Delete("rev_policy", policy.Id);
            }

            Contact c = new Contact()
            {
                FirstName = "",
                LastName = ""
            };

            //Using LINQ
            using(crmContext serviceContext = new crmContext(service))
            {
                var accountsNames = from item in serviceContext.AccountSet
                                    where item.Name == "ABYZ Company"
                                    select item.Name;

                foreach (var aName in accountsNames)
                {
                    Console.WriteLine(aName);
                }

                var accountContacts = from account in serviceContext.AccountSet
                                      join contacts in serviceContext.ContactSet
                                      on account.Id equals contacts.AccountId.Id
                                      where account.Name == "Adventure Works"
                                      select new
                                      {
                                          contacts.FullName
                                      };
            }
            Contact c = new Contact()
            {
            }

            //Call Action rev_SendEmail
            OrganizationRequest req = new OrganizationRequest("rev_sendemail");
                req.Parameters.Add("subject", "Welcome to CRM");
                req.Parameters.Add("Target", new EntityReference("contact", conGuid));
                OrganizationRequest response = service.Execute(req);

                Console.WriteLine(response.)


            */
            /*string xml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                <entity name='account'>
                <attribute name='name' />
                <attribute name='primarycontactid' />
                <attribute name='telephone1' />
                <attribute name='accountid' />
                <order attribute='name' descending='false' />
                </entity>
            </fetch>";

            EntityCollection collection = service.RetrieveMultiple(new FetchExpression(xml));

            foreach (Entity account in collection.Entities)
            {
                Console.WriteLine(account.Attributes["name"].ToString());
            }*/
            //Console.Read();
            // Create a contact record in Dynamics
            //Entity contact = new Entity("contact");
            //contact.Attributes.Add("lastname", "Bartholomew");
            //Guid contactGuid = service.Create(contact);

            //Entity incident = new Entity("incident");
            //incident.Attributes.Add("title", "New Support Case");
            //incident.Attributes.Add("customerid", new EntityReference("contact", contactGuid));

            //service.Create(incident);

            Console.ReadLine();
        }
    }
}
