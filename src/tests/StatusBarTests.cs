#region Copyright (c) 2002, James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole, Philip A. Craig
/************************************************************************************
'
' Copyright � 2002 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' Copyright � 2000-2002 Philip A. Craig
'
' This software is provided 'as-is', without any express or implied warranty. In no 
' event will the authors be held liable for any damages arising from the use of this 
' software.
' 
' Permission is granted to anyone to use this software for any purpose, including 
' commercial applications, and to alter it and redistribute it freely, subject to the 
' following restrictions:
'
' 1. The origin of this software must not be misrepresented; you must not claim that 
' you wrote the original software. If you use this software in a product, an 
' acknowledgment (see the following) in the product documentation is required.
'
' Portions Copyright � 2002 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' or Copyright � 2000-2002 Philip A. Craig
'
' 2. Altered source versions must be plainly marked as such, and must not be 
' misrepresented as being the original software.
'
' 3. This notice may not be removed or altered from any source distribution.
'
'***********************************************************************************/
#endregion

namespace NUnit.Tests.UiKit
{
	using System;
	using NUnit.Framework;
	using NUnit.Core;
	using NUnit.Util;
	using NUnit.UiKit;

	/// <summary>
	/// Summary description for StatusBarTests.
	/// </summary>
	[TestFixture]
	public class StatusBarTests
	{
		private StatusBar statusBar;
		private MockTestEventSource mockEvents;
		private string testsDll = "mock-assembly.dll";
		TestSuite suite;
		int testCount;

		[SetUp]
		public void Setup()
		{
			statusBar = new StatusBar();

			TestSuiteBuilder builder = new TestSuiteBuilder();
			suite = builder.Build( testsDll );

			mockEvents = new MockTestEventSource( testsDll, suite );
		}

		[Test]
		public void TestConstruction()
		{
			Assert.Equals( "Status", statusBar.Panels[0].Text );
			Assert.Equals( "Test Cases : 0", statusBar.Panels[1].Text );
			Assert.Equals( "Tests Run : 0", statusBar.Panels[2].Text );
			Assert.Equals( "Failures : 0", statusBar.Panels[3].Text );
			Assert.Equals( "Time : 0", statusBar.Panels[4].Text );
		}

		[Test]
		public void TestInitialization()
		{
			statusBar.Initialize( 0 );
			Assert.Equals( "", statusBar.Panels[0].Text );
			Assert.Equals( "Test Cases : 0", statusBar.Panels[1].Text );
			Assert.Equals( "Tests Run : 0", statusBar.Panels[2].Text );
			Assert.Equals( "Failures : 0", statusBar.Panels[3].Text );
			Assert.Equals( "Time : 0", statusBar.Panels[4].Text );

			statusBar.Initialize( 50 );
			Assert.Equals( "Ready", statusBar.Panels[0].Text );
			Assert.Equals( "Test Cases : 50", statusBar.Panels[1].Text );
			Assert.Equals( "Tests Run : 0", statusBar.Panels[2].Text );
			Assert.Equals( "Failures : 0", statusBar.Panels[3].Text );
			Assert.Equals( "Time : 0", statusBar.Panels[4].Text );
		}

		[Test]
		public void TestFinalDisplay()
		{
			Assert.Equals( false, statusBar.DisplayTestProgress );
			statusBar.Initialize( mockEvents );

			mockEvents.SimulateTestRun();
			Assert.Equals( "Completed", statusBar.Panels[0].Text );
			Assert.Equals( "Test Cases : 7", statusBar.Panels[1].Text );
			Assert.Equals( "Tests Run : 5", statusBar.Panels[2].Text );
			Assert.Equals( "Failures : 0", statusBar.Panels[3].Text );
			Assert.Equals( "Time : 0", statusBar.Panels[4].Text );
		}

		[Test]
		public void TestProgressDisplay()
		{
			statusBar.DisplayTestProgress = true;
			statusBar.Initialize( mockEvents );

			testCount = 0;
			mockEvents.TestFinished += new TestEventHandler( OnTestFinished );

			mockEvents.SimulateTestRun();
			Assert.Equals( "Completed", statusBar.Panels[0].Text );
			Assert.Equals( "Test Cases : 7", statusBar.Panels[1].Text );
			Assert.Equals( "Tests Run : 5", statusBar.Panels[2].Text );
			Assert.Equals( "Failures : 0", statusBar.Panels[3].Text );
			Assert.Equals( "Time : 0", statusBar.Panels[4].Text );
		}

		private void OnTestFinished( object sender, TestEventArgs e )
		{
			Assert.Equals( "Test Cases : 7", statusBar.Panels[1].Text );
			Assert.Equals( "Failures : 0", statusBar.Panels[3].Text );
			Assert.Equals( "Time : 0", statusBar.Panels[4].Text );

			// Note: Assumes delegates are called in order of adding
			switch( ++testCount )
			{
				case 1:			
					Assert.Equals( "Running : MyTest", statusBar.Panels[0].Text );
					Assert.Equals( "Tests Run : 1", statusBar.Panels[2].Text );
					break;
				case 2:
					Assert.Equals( "Running : MockTest1", statusBar.Panels[0].Text );
					Assert.Equals( "Tests Run : 2", statusBar.Panels[2].Text );
					break;
				case 3:
					Assert.Equals( "Running : MockTest2", statusBar.Panels[0].Text );
					Assert.Equals( "Tests Run : 3", statusBar.Panels[2].Text );
					break;
				case 4:
					Assert.Equals( "Running : MockTest3", statusBar.Panels[0].Text );
					Assert.Equals( "Tests Run : 4", statusBar.Panels[2].Text );
					break;
				case 5:
					Assert.Equals( "Running : MockTest5", statusBar.Panels[0].Text );
					Assert.Equals( "Tests Run : 4", statusBar.Panels[2].Text );
					break;
				case 6:
					Assert.Equals( "Running : MockTest4", statusBar.Panels[0].Text );
					Assert.Equals( "Tests Run : 4", statusBar.Panels[2].Text );
					break;
				case 7:
					Assert.Equals( "Running : TestCase", statusBar.Panels[0].Text );
					Assert.Equals( "Tests Run : 5", statusBar.Panels[2].Text );
					break;
			}
		}
	}
}
