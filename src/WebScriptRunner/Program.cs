using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScriptRunner.Core;

namespace WebScriptRunner
{
	public class Program
	{
		private static readonly ScriptExecutor _scriptExecutor = new ScriptExecutor();

		/// <summary>
		/// CLI entry point
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			foreach (var script in Directory.EnumerateFiles(Environment.CurrentDirectory, args[0], SearchOption.AllDirectories))
			{
				var result = ReadAndExecute(script);
				
				// the result object can be extended and reported in any way you see fit
				// ...
			}
		}

		/// <summary>
		/// Calling this from a testing framework directly, allow you to hook into the existing reporting
		/// </summary>
		/// <param name="script"></param>
		/// <returns></returns>
		public static WebInteractionResult ReadAndExecute(string script)
		{
			return _scriptExecutor.Execute(File.ReadAllText(script));
		}
	}
}
