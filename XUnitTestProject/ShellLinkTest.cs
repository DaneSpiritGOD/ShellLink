using System;
using System.IO;
using ShellLink;
using Xunit;

namespace XUnitTestProject
{
    public class ShellLinkTest
    {
        private string GenLnkFile()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test.lnk");
            Shortcut.CreateShortcut(@"%SystemRoot%\System32\calc.exe")
                .WriteToFile(path);
            return path;
        }

        private void ClearLnkFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        [Fact]
        public void CreateLnk()
        {
            var path = GenLnkFile();

            Assert.True(File.Exists(path));

            ClearLnkFile(path);
        }

        [Fact]
        public void ReadLnk()
        {
            var path = GenLnkFile();

            var shortcut = Shortcut.ReadFromFile(path);
            var targetPath = shortcut.ExtraData.EnvironmentVariableDataBlock.TargetUnicode;
            Assert.Equal("calc.exe", Path.GetFileName(targetPath));

            ClearLnkFile(path);
        }
    }
}
