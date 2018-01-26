using System.Collections.Generic;
using System.Linq;

namespace SeleniumProject
{
	public class TestDataConstructors
	{
        public IEnumerable<Dictionary<string, string>> GetuFACTS_SearchData = TestDataParameter.TestDataExcelDictionary("uFACTS_SearchData").AsEnumerable();
        public IEnumerable<Dictionary<string, string>> GetOpenGoogleEnglishData = TestDataParameter.TestDataExcelDictionary("OpenGoogleEnglishData").AsEnumerable();

    }
}