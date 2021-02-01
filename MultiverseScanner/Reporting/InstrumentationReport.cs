using MultiverseScanner.ExtensionSerialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiverseScanner.Reporting
{
	public class InstrumentationReport
	{
		private Dictionary<string, List<ClassValidation>> _validations;

		public string Name { get; }

		public Version Version { get; }

		public InstrumentationReport()
		{
			_validations = new Dictionary<string, List<ClassValidation>>();
		}

		// migrating from class level to assebmly level to enable report to have more than one assembly per inst xml

		internal void AddMethodValidation(Match match, ExactMethodMatcher exactMethodMatcher, bool isValid)
		{
			// check if a class item has already been added and return it
			if (_validations.TryGetValue(className, out var methodValidations))
			{
				// attempt to get an existing MethodValidation so we can update it.
				var methodValidation = methodValidations.FirstOrDefault((x) => x.MethodSignature == exactMethodMatcher.MethodSignature);
				if (methodValidation == null)
				{
					// No exsting MethodValidation
					methodValidations.Add(new MethodValidation(exactMethodMatcher, isValid));
				}

				// found an existing MethodValidation
				// Only allow changes from false to true
				if (isValid)
				{
					methodValidation.IsValid = isValid;
				}
			}

			// did not find class
			_validations.Add(className, new List<MethodValidation>());
			_validations[className].Add(new MethodValidation(exactMethodMatcher, isValid));
		}
	}
}
