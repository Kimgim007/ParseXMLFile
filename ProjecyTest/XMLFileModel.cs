using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjecyTest
{
    public class XMLFileModel
    {
        public XMLFileModel() { }
        public XMLFileModel(string ElementName, Dictionary<string, string> Attributes) 
        {
            this.ElementName = ElementName;        
            this.Attributes = Attributes;
        }

        public string ElementName { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, XMLFileModel> XMLFiles { get; set; } = new Dictionary<string, XMLFileModel>();
        public List<XMLFileModel> ChildElements { get; set; } = new List<XMLFileModel>();
    }
}
