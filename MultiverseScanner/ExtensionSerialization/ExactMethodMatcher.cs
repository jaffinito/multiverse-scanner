using System.Xml.Serialization;

namespace MultiverseScanner.ExtensionSerialization
{
	[XmlRoot(ElementName = "exactMethodMatcher", Namespace = "urn:newrelic-extension")]
	internal class ExactMethodMatcher
	{
		[XmlAttribute(AttributeName = "methodName")]
		public string MethodName { get; set; }

		[XmlAttribute(AttributeName = "parameters")]
		public string Parameters { get; set; }

		[XmlIgnore]
		public string MethodSignature { get { return $"{MethodName}({Parameters ?? string.Empty})"; } }
	}
}
