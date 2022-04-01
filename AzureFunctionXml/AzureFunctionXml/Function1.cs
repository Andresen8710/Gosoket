using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AzureFunctionXml
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */30 * * * *")]TimerInfo myTimer, ILogger log)
        {
            string XmlPath = Environment.CurrentDirectory.Replace(@"bin\Debug\netcoreapp3.1", @"XmlResourse\Anexos.xml");

            //Load  Xml
            XDocument xml = XDocument.Load(XmlPath);

            //Get list Area's Nodes
            var LAreas = GetInfoXml.GetListArea(xml);

            int TNodeAreas = 0;
            double TSalaries = 0;

            log.LogInformation($"********************************************************************");
            //Print Area's Node Quantity 
            log.LogInformation($"Cantidad de Nodos tipo “<area>”: " + LAreas.Count);

            foreach (XElement Area in LAreas)
            {
                var LEmployees = GetInfoXml.GetListEmployee(Area);

                //get Area's Nodes with more than two employees
                if(LEmployees.Count > 2){TNodeAreas += 1;}
                log.LogInformation($"********************************************************************");
                foreach (XElement Employee in LEmployees)
                {
                    //Get the Sum of Salaries
                    TSalaries += Convert.ToDouble(Employee.Attribute("salary").Value);
                }
                log.LogInformation(String.Format("Suma Salarios Area |{0}|{1}|", Area.Element("name").Value, TSalaries));                
            }

            log.LogInformation($"********************************************************************");
            //Print Area's Nodes Quantity with more than two employees
            log.LogInformation($"Cantidad de Nodos  tipo “<area>” que contienen mas de 2 Empleados: " + TNodeAreas);

            log.LogInformation($"********************************************************************");









            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
