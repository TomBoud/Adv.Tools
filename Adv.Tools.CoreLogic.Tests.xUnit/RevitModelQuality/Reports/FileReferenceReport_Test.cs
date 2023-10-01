using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.CoreLogic.Tests.xUnit.CommonUtilities;

namespace Adv.Tools.CoreLogic.Tests.xUnit.RevitModelQuality.Reports
{
    
    public class FileReferenceReport_Test
    {
        [Fact]
        public async void ExecuteReportCoreLogicAsync_Successful()
        {
            //Stage
            var expectedDocuments = new List<ExpectedDocument>();
            for (int i = 0; i < 5; i++) { expectedDocuments.Add(new ExpectedDocument(i)); }
            
            var revitLinkTypes = new List<RevitLinkType>();
            for (int i = 0; i < 3; i++) { revitLinkTypes.Add(new RevitLinkType(i)); }

            var report = new FileReferenceReport()
            {
                ReportDocument = new Document(0),
                DbDataObjects = expectedDocuments,
                DocumentObjects = expectedDocuments,
                RvtDataObjects = revitLinkTypes
            };

            //Act
            var executeTask =  report.ExecuteReportCoreLogicAsync();
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }


    }

    
}
