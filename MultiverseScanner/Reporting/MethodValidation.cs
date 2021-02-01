using MultiverseScanner.ExtensionSerialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiverseScanner.Reporting
{
	public class MethodValidation
	{
		public string MethodSignature { get; }

		public bool IsValid { get; set; }

		public MethodValidation(ExactMethodMatcher exactMethodMatcher, bool isValid)
		{
			MethodSignature = exactMethodMatcher.MethodSignature;
			IsValid = isValid;
		}
	}
}
