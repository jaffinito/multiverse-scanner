using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace ConsoleScanner
{
	public class InstrumentationSet
	{
		public string Name { get; set; }

		public string XmlFile { get; set; }

		public List<string> NugetAssemblies { get; set; }

		public List<string> LocalAssemblies { get; set; }
	}
}
