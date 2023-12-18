using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjecyTest.Entity
{
    public class XMLFileModelEntity
    {
        public XMLFileModelEntity() { }

        [XmlElement]
        public string? ElementTagName { get; set; }
        [XmlText]
        public string? ElementValue { get; set; }
        [XmlAnyAttribute]
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        public List<XMLFileModelEntity>? Elements { get; set; } = new List<XMLFileModelEntity>();


    }
}
