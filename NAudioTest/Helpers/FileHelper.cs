using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Helpers
{
    public static class FileHelper
    {
        public static bool TryOpenFile(out string resPath)
        {
            resPath = string.Empty;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "WAV Files (*.wav, *.wave)|*.wav;*.wave";
            fileDialog.ShowDialog();
            if (fileDialog.FileName == null)
                return false;
            resPath = fileDialog.FileName;
            return true;
        }
    }
}
