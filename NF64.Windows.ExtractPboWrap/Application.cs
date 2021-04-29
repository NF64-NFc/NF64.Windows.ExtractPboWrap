using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;


namespace NF64.Windows.ExtractPboWrap
{
    internal static class Application
    {
        private static Assembly CurrentAssembly { get; } = Assembly.GetEntryAssembly();


        public static void Main(string[] args)
        {
            try
            {
                Process.Start(new ExtractPboParameter(args).GetProcessStartInfo());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, CurrentAssembly.Location, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
