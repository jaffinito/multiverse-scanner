using System.Collections.Generic;
using System.Xml.Serialization;

namespace MultiverseScanner.ExtensionSerialization
{
	[XmlRoot(ElementName = "match", Namespace = "urn:newrelic-extension")]
	internal class Match
	{
		[XmlElement(ElementName = "exactMethodMatcher", Namespace = "urn:newrelic-extension")]
		public List<ExactMethodMatcher> ExactMethodMatchers { get; set; }

		[XmlAttribute(AttributeName = "assemblyName")]
		public string AssemblyName { get; set; }

		[XmlAttribute(AttributeName = "className")]
		public string ClassName { get; set; }
	}
}
