namespace NUnit.Util
{
	using System;
	using Codeblast;

	public class ConsoleOptions : CommandLineOptions
	{
		[Option(Description = "Assembly to test")]
		public string assembly;

		[Option(Description = "Fixture to test")]
		public string fixture;

		[Option(Description = "Name of Xml output file")]
		public string xml;

		[Option(Description = "Name of transform file")]
		public string transform;

		[Option(Description = "Do not display the logo")]
		public bool nologo = false;

		[Option(Short="?", Description = "Display help")]
		public bool help = false;

		[Option(Description = "Require input to close console window")]
		public bool wait = false;

		private bool invalidOption = false; 

		public ConsoleOptions(String[] args) : base(args) 
		{}

		protected override void InvalidOption(string name)
		{
			invalidOption = true;
		}

		public bool Validate()
		{
			if(invalidOption || ParameterCount > 0) return false; 

			if(IsAssembly || IsFixture) return true;


			return false;
		}

		public bool IsAssembly 
		{
			get 
			{
				return (assembly != null) && (assembly.Length != 0) && 
					   (fixture == null);
			}
		}

		public bool IsFixture 
		{
			get 
			{
				return ((assembly != null) && (assembly.Length != 0)) && 
					   (fixture != null) && (fixture.Length != 0);
			}
		}

		public bool IsXml 
		{
			get 
			{
				return (xml != null) && (xml.Length != 0);
			}
		}

		public bool IsTransform 
		{
			get 
			{
				return (transform != null) && (transform.Length != 0);
			}
		}
	}
}