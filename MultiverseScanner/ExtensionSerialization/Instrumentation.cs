using System.Collections.Generic;
using System.Xml.Serialization;

namespace MultiverseScanner.ExtensionSerialization
{
	[XmlRoot(ElementName = "instrumentation", Namespace = "urn:newrelic-extension")]
	internal class Instrumentation
	{
		[XmlElement(ElementName = "tracerFactory", Namespace = "urn:newrelic-extension")]
		public List<TracerFactory> TracerFactories { get; set; }
	}
}
