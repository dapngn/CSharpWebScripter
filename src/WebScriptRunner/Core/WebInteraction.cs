using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebScriptRunner.Core
{
	public class WebInteraction 
	{
		public static int FireFox = 0;
		public static int Chrome = 1;
		public static int IE = 2;
		public static int PhantomJS = 3;

		private readonly TimeSpan _responseTimeout;
	
		public IWebDriver Driver;
		public WebDriverWait Wait;
		public bool Result { get; set; }

		public delegate void Interaction(WebInteraction webInteraction);
		public WebInteraction.Interaction Script;

		public WebInteraction(int WithBrowser, Interaction Script, TimeSpan? ResponseTimeout = null)
		{
			_responseTimeout = (ResponseTimeout.HasValue) ? ResponseTimeout.Value : TimeSpan.FromSeconds(15);
			this.Script = Script;

			if ((WithBrowser & FireFox) == FireFox)
			{
				Driver = new FirefoxDriver();
			}

			Wait = new WebDriverWait(Driver, _responseTimeout);
		}

		public WebInteraction Execute()
		{
			try
			{
				Script(this);
			}
			catch (Exception ex)
			{
				Result = false;
			}
			finally {
				Driver.Quit();
			}
			return this;
		}
	}
}