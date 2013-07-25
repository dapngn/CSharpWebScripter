using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Scripting.CSharp;

namespace WebScriptRunner.Core
{
	public class ScriptExecutor 
	{
		private readonly ScriptEngine _engine;

		public ScriptExecutor()
		{
			_engine = new ScriptEngine();

			_engine.AddReference("System");
			_engine.AddReference(this.GetType().Assembly.Location);
			_engine.AddReference(Environment.CurrentDirectory + "\\WebDriver.dll");
			_engine.AddReference(Environment.CurrentDirectory + "\\WebDriver.Support.dll");

			_engine.ImportNamespace("System");
			_engine.ImportNamespace("OpenQA.Selenium");
			_engine.ImportNamespace("WebScriptRunner");
			_engine.ImportNamespace("WebScriptRunner.Core");
		}

		public WebInteractionResult Execute(string code)
		{
			var result = new WebInteractionResult();
			var session = _engine.CreateSession(result);
			var submission = session.CompileSubmission<object>(code);
			var execution = submission.Execute();
			return result;
		}
	}
}