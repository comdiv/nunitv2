using System;
using System.Collections;

namespace NUnit.Core
{
	/// <summary>
	/// TestNode represents a single test or suite in the test hierarchy.
	/// TestNode holds common info needed about a test and represents a
	/// single node - either a test or a suite - in the hierarchy of tests.
	/// 
	/// TestNode extends TestInfo, which holds all the information with
	/// the exception of the list of child classes. When constructed from
	/// a Test, TestNodes are always fully populated with child TestNodes.
	/// 
	/// Like TestInfo, TestNode is purely a data class, and is not able
	/// to execute tests.
	/// 
	/// TODO: Complete TestNode implementation
	/// 
	/// STATUS:
	///       TestNode has replaced UITestNode, previously defined 
	///       in the nunit.util assembly.
	///
	///       TestResult now uses TestNode. The TestRunner Load methods
	///       all return a TestNode. Test objects are no longer passed
	///       back to the client.
	/// 	  
	///       Currently TestNode implements ITest and so duplicates much
	///       of the functionality of Test. In the future, the ITest interface
	///       will be simplified and Test will either extend or aggregate
	///       TestNode.
	///              
	///       TestNodes should contain enough info to allow a runner
	///       to locate the actual test object and execute it.
	///       
	/// </summary>
	[Serializable]
	public class TestNode : TestInfo
	{
		#region Instance Variables
		/// <summary>
		/// For a test suite, the child tests or suites
		/// Null if this is not a test suite
		/// </summary>
		private ArrayList tests;
		#endregion

		#region Constructors
		/// <summary>
		/// Construct from a Test
		/// </summary>
		/// <param name="test">Test from which a TestNode is to be constructed</param>
		public TestNode ( Test test ) : base( test )
		{
			if ( test.IsSuite )
			{
				this.tests = new ArrayList();

				TestSuite suite = (TestSuite)test;
					
				foreach( Test child in suite.Tests )
				{
					TestNode node = new TestNode( child );
					this.Tests.Add( node );
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Array of child tests, null if this is a test case.
		/// </summary>
		public ArrayList Tests 
		{
			get { return tests; }
		}
		#endregion
	}
}