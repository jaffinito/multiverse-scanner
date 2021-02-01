using System.Collections.Generic;
using System.Linq;

namespace MultiverseScanner.Models
{
	public class AssemblyAnalysis
	{
		internal Dictionary<string, AssemblyModel> AssemblyModels { get; }

		public AssemblyAnalysis()
		{
			AssemblyModels = new Dictionary<string, AssemblyModel>();
		}

		public int ClassesCount 
		{ 
			get { return AssemblyModels.Select((x) => x.Value.ClassModels.Count).ToList().Sum(); } 
		}
	}
}
