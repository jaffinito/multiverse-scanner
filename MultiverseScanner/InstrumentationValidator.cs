using MultiverseScanner.ExtensionSerialization;
using MultiverseScanner.Models;
using MultiverseScanner.Reporting;

namespace MultiverseScanner
{
	public class InstrumentationValidator
	{
		private AssemblyAnalysis _assemblyAnalysis;

		public InstrumentationValidator(AssemblyAnalysis assemblyAnalysis)
		{
			_assemblyAnalysis = assemblyAnalysis;
		}


		// broken since it doesn't understnad more than one assembly
		// needs to check each assembly before reporting any failures

		public InstrumentationReport CheckInstrumentation(InstrumentationModel instrumentationModel)
		{
			var instrumentationReport = new InstrumentationReport();

			// Check each AssemblyModel against all instrumentation
			// InstrumentationReport will show aggregated results from all assemblies
			foreach (var assemblyModel in _assemblyAnalysis.AssemblyModels.Values)
			{
				CheckMatch(assemblyModel, instrumentationModel, instrumentationReport);
			}

			return instrumentationReport;
		}

		private void CheckMatch(AssemblyModel assemblyModel, InstrumentationModel instrumentationModel, InstrumentationReport instrumentationReport)
		{
			foreach (var match in instrumentationModel.Matches)
			{
				if(!ValidateAssembly(assemblyModel, match, instrumentationReport))
				{
					continue;
				}

				// assembly match checking classes and methods
				ValidateClass(assemblyModel, match, instrumentationReport);
			}
		}

		private bool ValidateAssembly(AssemblyModel assemblyModel, Match match, InstrumentationReport instrumentationReport)
		{
			// okay to move on to checking classes
			if (match.AssemblyName == assemblyModel.AssemblyName)
			{
				return true;
			}

			// assembly did not match so marking all methods as false - can be changed by later validation attempts
			MarkAllMethodsAsNotValid(match, instrumentationReport);
			return false;
		}

		private void ValidateClass(AssemblyModel assemblyModel, Match match, InstrumentationReport instrumentationReport)
		{
			// check if class exists in ClassModels and get ClassModel back
			if (assemblyModel.ClassModels.TryGetValue(match.ClassName, out var classModel))
			{
				CheckExactMethodMatchers(instrumentationReport, match, classModel);
				return;
			}

			// class did not match so marking all methods as false - can be changed by later validation attempts
			MarkAllMethodsAsNotValid(match, instrumentationReport);
		}

		private void CheckExactMethodMatchers(InstrumentationReport instrumentationReport, Match match, ClassModel classModel)
		{
			foreach (var exactMethodMatcher in match.ExactMethodMatchers)
			{
				// check if method exists in MethodModels and get MethodModel back
				if (classModel.MethodModels.TryGetValue(exactMethodMatcher.MethodName, out var methodModel))
				{
					// Check if exactMethodMatcher.Parameters is empty or popluated
					if (string.IsNullOrWhiteSpace(exactMethodMatcher.Parameters))
					{
						// exactMethodMatcher has NO params, checking of MethodModel has an empty ParameterSets value
						if (methodModel.ParameterSets.Contains(string.Empty))
						{
							instrumentationReport.AddMethodValidation(match.ClassName, exactMethodMatcher, true);
							continue;
						}

						instrumentationReport.AddMethodValidation(match.ClassName, exactMethodMatcher, false);
						continue;
					}

					// exactMethodMatcher HAS params to check
					if (methodModel.ParameterSets.Contains(exactMethodMatcher.Parameters))
					{
						instrumentationReport.AddMethodValidation(match.ClassName, exactMethodMatcher, true);
						continue;
					}

					// param was not found
					instrumentationReport.AddMethodValidation(match.ClassName, exactMethodMatcher, false);
					continue;
				}

				// Did not find method in classmodel, amrking method as false
				instrumentationReport.AddMethodValidation(match.ClassName, exactMethodMatcher, false);
			}
		}

		private void MarkAllMethodsAsNotValid(Match match, InstrumentationReport instrumentationReport)
		{

			// Did not match so marking all methods as false - can be changed by later validation attempts
			foreach (var exactMethodMatcher in match.ExactMethodMatchers)
			{
				instrumentationReport.AddMethodValidation(match.ClassName, exactMethodMatcher, false);
			}
		}

	}
}
