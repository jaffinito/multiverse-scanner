using System.Collections.Generic;

namespace MultiverseScanner.Models
{
	internal class ClassModel
	{
		public string Name { get; }

		public string AccessLevel { get; }

		public Dictionary<string, MethodModel> MethodModels { get; }

		public ClassModel(string name, string accessLevel)
		{
			MethodModels = new Dictionary<string, MethodModel>();
			Name = name;
			AccessLevel = accessLevel;
		}

		public MethodModel GetOrCreateMethodModel(string name)
		{
			if (MethodModels.TryGetValue(name, out var existingMethodModel))
			{
				return existingMethodModel;
			}

			var methodModel = new MethodModel(name);
			MethodModels.Add(name, methodModel);
			return methodModel;
		}
	}
}
