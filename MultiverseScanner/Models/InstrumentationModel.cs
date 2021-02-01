using MultiverseScanner.ExtensionSerialization;
using System.Collections.Generic;
using System.Linq;

namespace MultiverseScanner.Models
{
	public class InstrumentationModel
	{
		public List<Match> Matches { get; }

		private InstrumentationModel()
		{
			Matches = new List<Match>();
		}

		public static InstrumentationModel CreateInstrumentationModel(Extension extension)
		{
			var model = new InstrumentationModel();
			model.Matches.AddRange(from tracerFactory in extension.Instrumentation.TracerFactories
								   from match in tracerFactory.Matches
								   select match);
			return model;
		}
	}
}
