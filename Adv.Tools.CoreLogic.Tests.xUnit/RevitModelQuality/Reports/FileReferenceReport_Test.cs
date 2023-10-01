using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;

namespace Adv.Tools.CoreLogic.Tests.xUnit.RevitModelQuality.Reports
{
    
    public class FileReferenceReport_Test
    {
        [Fact]
        public async void ExecuteReportCoreLogicAsync_Successful()
        {
            //Stage
            var report = new FileReferenceReport();
            var executeTask = report.ExecuteReportCoreLogicAsync();

            //Act
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }


    }

    
}
