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

        public XMLFileModel(string ElementTagName, string ElementValue)
        {
            this.ElementTagName = ElementTagName;
            this.ElementValue = ElementValue;
        }
        public string? ElementTagName { get; set; }
        public string? ElementValue { get; set; }
        
        //public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        //public Dictionary<string, string> ElementsType { get; set; } = new Dictionary<string, string>();

    }
}
