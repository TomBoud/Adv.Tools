using Adv.Tools.DataAccess.MySql;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Adv.Tools.UI.Tests.xUnit.RevitModelQuality
{
    public class ConfigReportRepoTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess("Server=192.168.10.1;Port=3306;user id=Admin;password=QAZ56okm;CharSet=utf8;");
        private readonly string TestDataBaseName = "b4f912b9ef6ab43578c73e05d7e9a13d7";


        [Fact]
        public void TestDataBaseLoad_Successful()
        {
            //Stage
            var repo = new ConfigReportRepo(_access, TestDataBaseName);
            //Act
            var results = repo.GetAllReports();
            //Assert
            Assert.NotNull(results);
            Assert.True(results?.Count() > 0);
        }
    }
}
