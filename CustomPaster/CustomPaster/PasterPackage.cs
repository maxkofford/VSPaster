//------------------------------------------------------------------------------
// <copyright file="PasterPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using System.IO;

namespace CustomPaster
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.CSharpProject_string)]
    [Guid(PasterPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideToolWindow(typeof(ToolWindow1))]
    public sealed class PasterPackage : Package
    {
        /// <summary>
        /// PasterPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "7f23820c-9738-4cb1-a75d-4b52007b4b09";

        public static string[] PasteAbles;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paster"/> class.
        /// </summary>
        public PasterPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.

            string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string modelPath = myDoc;
            
            string config = modelPath + @"\PasteStuff.txt";
            if (File.Exists(config))
            {
                string allStuff = File.ReadAllText(config);
                string[] allSplitStuff = allStuff.Split(new String[] { "Splitter" }, StringSplitOptions.RemoveEmptyEntries);
                PasteAbles = allSplitStuff;
            }
            else
            {
                File.WriteAllLines(config, new String[] { "" });
            }


          
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Paster.Initialize(this);
            base.Initialize();
            ToolWindow1Command.Initialize(this);
        }

        #endregion
    }
}
