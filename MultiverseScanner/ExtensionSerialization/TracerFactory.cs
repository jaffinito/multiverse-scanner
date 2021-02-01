using System.Collections.Generic;
using System.Xml.Serialization;

namespace MultiverseScanner.ExtensionSerialization
{
	[XmlRoot(ElementName = "tracerFactory", Namespace = "urn:newrelic-extension")]
	internal class TracerFactory
	{
		[XmlElement(ElementName = "match", Namespace = "urn:newrelic-extension")]
		public List<Match> Matches { get; set; }
	}
}
