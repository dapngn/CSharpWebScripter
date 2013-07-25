using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebScriptRunner;
using WebScriptRunner.Core;

namespace WebScriptRunnerTests
{
	[TestFixture]
	public class FunctionalWebTests
	{	
		[TestFixtureTearDown]
		public void teardown()
		{
			Directory.EnumerateFiles(Environment.CurrentDirectory, "*.csx")
				.ToList().ForEach(
					script => File.Delete(script)
				);
		}

		[Test, TestCaseSource(typeof(ScriptedWebTestResults), "TestData")]
		public void ScriptedWebTest(bool a, bool b)
		{
			Assert.That(a == b);
		}		
	}

	public class ScriptedWebTestResults 
	{
		public ScriptedWebTestResults()
		{
			Directory.EnumerateFiles(Environment.CurrentDirectory + "\\..\\..\\Scripts\\", "*.csx")
				.ToList().ForEach(
					script => File.Copy(script, Environment.CurrentDirectory + "\\" + Path.GetFileName(script), true)
				);
		}
		public IEnumerable<TestCaseData> TestData()
		{
			var results = new List<TestCaseData>();
			foreach (var script in Directory.EnumerateFiles(Environment.CurrentDirectory, "*.csx"))
			{
				var result = Program.ReadAndExecute(script);
				results.Add((new TestCaseData( result.ScriptResult, result.ExpectedResult))
					.SetName(Path.GetFileName(script)));
			}
			return results;
		}
	}
}
