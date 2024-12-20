﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLE_Algorithm
{
    public class ArchiveEventArgs : EventArgs
    {
        public string LogFilePath { get; set; }
        public int InputTextSize { get; set; }
        public int OutputTextSize { get; set; }
        public TextBox InputTextBox { get; set; }
        public TextBox OutputTextBox { get; set; }
        public string ErrorComment { get; set; }
        public string FileName { get; set; }
    }
}
