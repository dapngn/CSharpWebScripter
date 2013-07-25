using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScriptRunner;

namespace WebScriptRunnerTests
{
    [TestFixture]
    class WebInteractionTest
    {
	[TestFixtureSetUp]
	public void setup()
	{
	    Directory.EnumerateFiles(Environment.CurrentDirectory + "\\..\\..\\Scripts\\", "*.csx")
	    .ToList().ForEach(
		script => File.Copy(script, Environment.CurrentDirectory + "\\" + Path.GetFileName(script), true)
	    );
	}

	[TestFixtureTearDown]
	public void teardown()
	{
	    Directory.EnumerateFiles(Environment.CurrentDirectory, "*.csx")
	    .ToList().ForEach(
		script => File.Delete(script)
	    );
	}

	[Test]
	public void ScriptedWebTest()
	{
	    var result = Program.ReadAndExecute(Environment.CurrentDirectory + "\\DuckDuckGo.csx");
	    Assert.That(result.ExpectedResult, Is.EqualTo(result.ScriptResult));
	}	
    }
}
