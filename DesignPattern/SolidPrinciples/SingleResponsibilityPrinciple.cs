using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.SolidPrinciples
{
    /// <summary>
    /// A class should have only one reason to change.
    /// <br></br>
    /// class should not be loaded with multiple responsibilities and a single responsibility should not be
    /// spread across multiple classes or mixed with other responsibilities.
    /// The reason is that more changes requested in the future, the more changes the class need to apply.
    /// </summary>
    [TestClass]
    public class SingleResponsibilityPrinciple : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}