﻿#region Header

/*
    This file is part of NDoctor.

    NDoctor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    NDoctor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with NDoctor.  If not, see <http://www.gnu.org/licenses/>.
*/

#endregion Header

namespace Probel.NDoctor.View.Core.ViewModel
{
    using System;
    using System.Reflection;
    using System.Windows.Input;

    using Probel.Helpers.Strings;
    using Probel.Mvvm.DataBinding;
    using Probel.NDoctor.View.Core.Properties;
    using System.Resources;
    using System.IO;
    using System.Text;
    using System.Diagnostics;

    public class AboutBoxViewModel : BaseViewModel
    {
        #region Fields

        private string application;
        private string author;
        private string copyright;
        private string license;

        #endregion Fields


        public AboutBoxViewModel()
        {
            this.RefreshCommand = new RelayCommand(() => this.Refresh());
            this.OpenLogCommand = new RelayCommand(() => this.OpenLog());
        }


        #region Properties

        public string Application
        {
            get { return this.application; }
            set
            {
                this.application = value;
                this.OnPropertyChanged(() => Application);
            }
        }

        public string Author
        {
            get { return this.author; }
            set
            {
                this.author = value;
                this.OnPropertyChanged(() => Author);
            }
        }

        public string Copyright
        {
            get { return this.copyright; }
            set
            {
                this.copyright = value;
                this.OnPropertyChanged(() => Copyright);
            }
        }

        public string License
        {
            get { return this.license; }
            set
            {
                this.license = value;
                this.OnPropertyChanged(() => License);
            }
        }

        public ICommand RefreshCommand
        {
            get;
            private set;
        }

        public ICommand OpenLogCommand { get; private set; }
        private void OpenLog()
        {
            try
            {
                var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                Process.Start(Path.Combine(appdata, @"Probel\nDoctor"));
            }
            catch (Exception ex) { this.HandleError(ex); }
        }

        #endregion Properties

        #region Methods

        private void Refresh()
        {
            var asm = Assembly.GetAssembly(this.GetType());
            this.Application = "nDoctor {0}".FormatWith(asm.GetName().Version);
            this.Author = Messages.Title_WrittenBy.FormatWith("Jean-Baptiste Wautier");
            this.Copyright = "Copyright Probel 2006-{0}".FormatWith(DateTime.Today.Year);
            this.License = this.GetLicense().FormatWith(DateTime.Today.Year);
        }

        private string GetLicense()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Probel.NDoctor.View.Core.License.txt");
            if (stream == null) throw new NullReferenceException("The license is not foud in the resource of the executing assembly.");

            using (var reader = new StreamReader(stream, Encoding.UTF8)) { return reader.ReadToEnd(); }
        }

        #endregion Methods
    }
}