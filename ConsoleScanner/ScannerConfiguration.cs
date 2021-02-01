using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Serialization;

namespace ConsoleScanner
{
	public class ScannerConfiguration
	{
		public List<InstrumentationSet> InstrumentationSets { get; set; }
	}
}
