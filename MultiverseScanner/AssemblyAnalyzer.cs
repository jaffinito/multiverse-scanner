﻿using Mono.Cecil;
using MultiverseScanner.Models;
using System;
using System.Linq;

namespace MultiverseScanner
{
	// https://github.com/jbevain/cecil/wiki/HOWTO

	public class AssemblyAnalyzer
	{
		public AssemblyAnalysis RunAssemblyAnalysis(params string[] filePaths)
		{
			var assemblyAnalysis = new AssemblyAnalysis();

			foreach (var filePath in filePaths)
			{
				var assemblyModel = GetAssemblyModel(filePath);
				assemblyAnalysis.AssemblyModels.Add(assemblyModel.AssemblyName, assemblyModel);
			}

			return assemblyAnalysis;
		}

		private AssemblyModel GetAssemblyModel(string filePath)
		{
			var moduleDefinition = ModuleDefinition.ReadModule(filePath);
			var assemblyModel = new AssemblyModel(moduleDefinition.Assembly.Name.Name, GetAssemblyVersion(moduleDefinition));
			BuildClassModels(assemblyModel, moduleDefinition);
			return assemblyModel;
		}

		private void BuildClassModels(AssemblyModel assemblyModel, ModuleDefinition moduleDefinition)
		{
			foreach (var typeDefinition in moduleDefinition.Types)
			{
				if (!typeDefinition.IsClass || typeDefinition.FullName.StartsWith("<"))
				{
					continue;
				}

				var classModel = new ClassModel(typeDefinition.FullName, GetAccessLevel(typeDefinition));
				BuildMethodModels(classModel, typeDefinition);
				assemblyModel.AddClass(classModel);
			}
		}

		private void BuildMethodModels(ClassModel classModel, TypeDefinition typeDefinition)
		{
			foreach (var method in typeDefinition.Methods)
			{
				var methodModel = classModel.GetOrCreateMethodModel(method.Name);
				if (method.HasParameters)
				{
					var parameters = method.Parameters.Select((x) => x.ParameterType.FullName.Replace('<', '[').Replace('>', ']')).ToList();
					methodModel.ParameterSets.Add(string.Join(",", parameters));
				}
				else
				{
					// covers a method having no parameters.
					methodModel.ParameterSets.Add(string.Empty);
				}
			}
		}

		private string GetAccessLevel(TypeDefinition typeDefinition)
		{
			if (typeDefinition.IsPublic)
			{
				return "public";
			}
			else if (typeDefinition.IsNotPublic)
			{
				return "private";
			}

			
			return "";
		}

		private Version GetAssemblyVersion(ModuleDefinition moduleDefinition)
		{
			var assemblyName = new System.Reflection.AssemblyName(moduleDefinition.Assembly.FullName);
			return assemblyName.Version;
		}
	}
}
