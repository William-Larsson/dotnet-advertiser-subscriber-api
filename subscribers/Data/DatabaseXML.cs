using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using subscribers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace subscribers.Data
{
    // A class for parsing XML to add to the database
    // as well as composing XML from the subscriber DB table. 
    public class DatabaseXML
    {
        // Parses an XML-file and inserts the content into
        // the subscriber table. 
        public static void DeserializeXML() 
        {
            return;
        }

        // Creates a file containing the data from the Subscriber table
        // as an XMl-file. 
        public static async void SerializeXML(IApplicationBuilder builder, string filename)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApiDBContext>();
                List<Subscriber> subs = await context.Subscribers.ToListAsync();

                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(subs.GetType());
                var fileStream = new FileStream(filename, FileMode.Create);
                var xmlWriter = new XmlTextWriter(fileStream, Encoding.Unicode);

                xmlSerializer.Serialize(xmlWriter, subs);
                xmlWriter.Close();
            }
        }
    }
}