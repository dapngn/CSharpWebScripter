ExpectedResult = true;
ScriptResult = (new WebInteraction(WithBrowser: WebInteraction.FireFox,
				Script: (w) =>
				{
					w.Driver.Navigate().GoToUrl("https://duckduckgo.com/");
					w.Wait.Until(p => p.FindElement(By.Name("q")));
					w.Driver.FindElement(By.Name("q")).SendKeys("I don't wanna be a playa no mo");
					w.Result = true;

				}))
			.Execute()
			.Result;