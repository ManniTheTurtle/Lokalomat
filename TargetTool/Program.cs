﻿using LokalomatKlassen;
using System;
using System.Windows.Forms;

namespace TargetTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XtraDoc1());
        }
    }
}
