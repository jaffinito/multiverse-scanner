using System;
using System.Collections.Generic;
using System.Text;

namespace MultiverseScanner.Reporting
{
	public class ClassValidation
	{
		public string Name { get; }

		public List<MethodValidation> MethodValidations { get; }

		public ClassValidation(string name)
		{
			Name = name;
			MethodValidations = new List<MethodValidation>();
		}
	}
}
