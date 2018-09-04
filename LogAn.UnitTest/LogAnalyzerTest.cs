using System;
using NUnit.Framework;

namespace LogAn.UnitTest
{
    [TestFixture]
    public class LogAnalyzerTest
    {
        private LogAnalyzer _mLogAnalyzer;
        [SetUp]
        public void SetUp()
        {
            _mLogAnalyzer = new LogAnalyzer();
        }
        [TearDown]
        public void TearDown()
        {
            _mLogAnalyzer = null;
            // GC.Collect(GC.GetGeneration(this));
        }
        [Test]
        [TestCase("someFileName.foo", false)]
        [TestCase("someFileName.sln", false)]
        [TestCase("someFileName.SLN", false)]
        [TestCase("someFileName.SLF", true)]
        public void IsValidFileName_BadExtension_RetutnFalse(string fileName, bool expected)
        {
            bool result = _mLogAnalyzer.IsValidLogFileName(fileName);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid()
        {
            _mLogAnalyzer.IsValidLogFileName("data.json");
            Assert.False(_mLogAnalyzer.WasLastFileNameValid);
        }
        [Test]
        [TestCase("Имя файла должно быть задано", typeof(ArgumentNullException))]
        public void IsValidFileName_EmptyFileName_ThrowException(string args, Type expectedExceptionType)
        {
            Assert.That(() => _mLogAnalyzer.IsValidLogFileName(string.Empty), Throws.TypeOf<ArgumentNullException>());

            #region 2_Вариант_Обработки_Ошибок
            /*
            // 2 вариант
            Assert.Throws(expectedExceptionType, () => _mLogAnalyzer.IsValidLogFileName(string.Empty));
            */
            #endregion


            //// 3
            // var ex = Assert.Catch<Exception>(() => _mLogAnalyzer.IsValidLogFileName(""));
            // StringAssert.Contains("fileName must be norm", ex.Message);


        }
    }
}
