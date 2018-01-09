using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using NUnit.Framework;
using NUnitLite;

namespace tim_tests
{
	class MainClass
	{
		public static int Main(string[] args)
		{
			Environment.ExitCode = new AutoRun().Execute(args);
			System.IO.File.WriteAllText("tests_output", Environment.ExitCode.ToString());
			Process.GetCurrentProcess().Kill();
			//Environment.Exit(Environment.ExitCode);
			return Environment.ExitCode;
		}
	}
}
