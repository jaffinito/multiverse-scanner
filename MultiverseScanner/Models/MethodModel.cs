using System.Collections.Generic;

namespace MultiverseScanner.Models
{
	internal class MethodModel
	{
		public string Name { get; }

		public string AccessLevel { get; }

		public List<string> ParameterSets { get; }

		public MethodModel(string name)
		{
			ParameterSets = new List<string>();
			Name = name;
		}
	}
}
