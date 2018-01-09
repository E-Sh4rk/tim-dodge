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
			Environment.ExitCode = new AutoRun().Execute(new string[] { "--workers=1" });
			Process.GetCurrentProcess().Kill();
			return Environment.ExitCode;
		}
	}
}
