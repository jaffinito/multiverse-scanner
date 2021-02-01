using MultiverseScanner;
using MultiverseScanner.Models;
using MultiverseScanner.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ConsoleScanner
{
	public class Program
	{
		private static List<InstrumentationReport> _instrumentationReports = new List<InstrumentationReport>();

		public static void Main(string[] args)
		{
			if (args.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
			{
				Console.WriteLine("ERROR Missing arguement: Must supply path to configuration file.");
				return;
			}

			var filePath = args[0];
			if (!File.Exists(filePath))
			{
				Console.WriteLine("ERROR File not found: Provide path was incorrect or file missing.");
				return;
			}

			var deserializer = new DeserializerBuilder()
				.WithNamingConvention(HyphenatedNamingConvention.Instance)
				.Build();

			var configuration = deserializer.Deserialize<ScannerConfiguration>(File.ReadAllText(filePath));

			// now that we have a config, we need to handle checking more than one xml or dll

			// temp to allow other testing
			var fileNames = configuration.InstrumentationSets[0].LocalAssemblies.ToArray();

			// Builds a model from the files
			Console.WriteLine($"Starting scan of '{string.Join(',', fileNames)}'");
			var assemblyAnalyzer = new AssemblyAnalyzer();
			var assemblyAnalysis = assemblyAnalyzer.RunAssemblyAnalysis(fileNames);

			// just some debugging writes
			Console.WriteLine($"Found {assemblyAnalysis.ClassesCount} classes");
			Console.WriteLine("Scan complete");


			// build a searcher to check the inst file.
			var instrumentationValidator = new InstrumentationValidator(assemblyAnalysis);
			//var report = instrumentationValidator.CheckInstrumentation()

			//PrintReport();
		}


		//private Extension ReadInstrumentionFile()
		//{
		//	using (var fileStream = File.Open("test.xml", FileMode.Open))
		//	{
		//		XmlSerializer serializer = new XmlSerializer(typeof(Extension));
		//		var definition = (Extension)serializer.Deserialize(fileStream);
		//		return definition;
		//	}
		//}
	}
}
