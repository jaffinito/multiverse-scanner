using System.Xml.Serialization;

namespace MultiverseScanner.ExtensionSerialization
{
	[XmlRoot(ElementName = "extension", Namespace = "urn:newrelic-extension")]
	internal class Extension
	{
		[XmlElement(ElementName = "instrumentation", Namespace = "urn:newrelic-extension")]
		public Instrumentation Instrumentation { get; set; }

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}
}
