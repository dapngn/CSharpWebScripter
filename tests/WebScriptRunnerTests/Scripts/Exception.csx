ExpectedResult = false;
ScriptResult = (new WebInteraction(WithBrowser: WebInteraction.FireFox,
				Script: (w) =>
				{
					throw new Exception();
				}))
			.Execute()
			.Result;