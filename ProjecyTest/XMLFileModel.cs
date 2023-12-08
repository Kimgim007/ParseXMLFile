using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjecyTest
{
    public class XMLFileModel
    {
        public XMLFileModel() { }

        public XMLFileModel(string ElementName, string ElementType)
        {
            this.ElementName = ElementName;
            this.ElementType = ElementType;
         
        }
    
        public string? ElementName { get; set; }
        public string? ElementType { get; set; }
        public string? ElementText { get; set; }

        //public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        //public Dictionary<string, string> ElementsType { get; set; } = new Dictionary<string, string>();

    }
}
