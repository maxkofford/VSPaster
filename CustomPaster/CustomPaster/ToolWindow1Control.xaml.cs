//------------------------------------------------------------------------------
// <copyright file="ToolWindow1Control.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace CustomPaster
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System;
    using System.ComponentModel.Design;
    using System.Globalization;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.Editor;
    using Microsoft.VisualStudio.ComponentModelHost;
    using EnvDTE;
    using EnvDTE80;


    /// <summary>
    /// Interaction logic for ToolWindow1Control.
    /// </summary>
    public partial class ToolWindow1Control : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1Control"/> class.
        /// </summary>
        public ToolWindow1Control()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private DTE2 _dte = null;
        DTE2 GetDTE()
        {
            if (_dte == null)
            {
                _dte = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            }
            return _dte;
        }


        private void textBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox theText = ((TextBox)sender);
            theText.Focus();
          
            if (theText.Text.Length != 2)
            {

                DTE2 dte = GetDTE();
                try
                {
                    string comment = theText.Text;

                    int spot = Int32.Parse(comment.Substring(0, 1));

                    comment = PasterPackage.PasteAbles[spot];

                    dte.UndoContext.Open("Paster");
                    var selection = (TextSelection)dte.ActiveDocument.Selection;
                    if (selection != null)
                    {
                        selection.Insert(comment);
                        dte.ActiveDocument.Activate();
                        dte.ExecuteCommand("Edit.FormatDocument");
                    }

                    theText.Text = "Yo";
                    ToolWindow1Command.theFrame.CloseFrame((uint)__FRAMECLOSE.FRAMECLOSE_NoSave);
                }
                finally
                {
                    dte.UndoContext.Close();
                }
            }


            
        }

      
    }
}