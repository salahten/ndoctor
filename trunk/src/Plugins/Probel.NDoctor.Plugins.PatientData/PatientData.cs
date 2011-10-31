﻿/*
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
namespace Probel.NDoctor.Plugins.PatientData
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows.Input;
    using System.Windows.Media;

    using AutoMapper;

    using Probel.Helpers.Strings;
    using Probel.NDoctor.Domain.DTO.Components;
    using Probel.NDoctor.Domain.DTO.Objects;
    using Probel.NDoctor.Plugins.PatientData.Properties;
    using Probel.NDoctor.Plugins.PatientData.View;
    using Probel.NDoctor.Plugins.PatientData.ViewModel;
    using Probel.NDoctor.View.Plugins;
    using Probel.NDoctor.View.Plugins.Helpers;
    using Probel.NDoctor.View.Plugins.MenuData;

    [Export(typeof(IPlugin))]
    public class PatientData : Plugin
    {
        #region Fields

        private const string imgUri = @"\Probel.NDoctor.Plugins.PatientData;component/Images\{0}.png";

        private IPatientDataComponent component;
        private RibbonContextualTabGroupData contextualMenu;
        private ICommand navigateCommand;
        private ICommand saveCommand;
        private Workbench workbench;

        #endregion Fields

        #region Constructors

        [ImportingConstructor]
        public PatientData([Import("version")] Version version, [Import("host")] IPluginHost host)
            : base(version, host)
        {
            this.Validator = new PluginValidator("3.0.0.0", ValidationMode.Minimum);
            this.ConfigureAutoMapper();
        }

        #endregion Constructors

        #region Properties

        private WorkbenchViewModel ViewModel
        {
            get
            {
                if (this.workbench == null) this.workbench = new Workbench();

                return this.workbench.DataContext as WorkbenchViewModel;
            }
            set
            {
                if (this.workbench == null) this.workbench = new Workbench();
                this.workbench.DataContext = value; ;
            }
        }

        #endregion Properties

        #region Methods

        public override void Initialise()
        {
            this.component = ComponentFactory.PatientDataComponent;
            this.Host.Invoke(() =>
            {
                this.ViewModel = new WorkbenchViewModel();
                this.ViewModel.Refresh();
                this.workbench = new Workbench();
            });
            this.BuildButtons();
            this.BuildContextMenu();
        }

        private void BuildButtons()
        {
            this.navigateCommand = new RelayCommand(() => this.Navigate(), () => this.CanNavigate());
            this.saveCommand = new RelayCommand(() => this.ViewModel.Save());

            var navigateButton = new RibbonButtonData(Messages.Title_PatientDataManager
                    , imgUri.StringFormat("Properties")
                    , navigateCommand) { Order = 1 };

            this.Host.AddInHome(navigateButton, Groups.Managers);
        }

        private void BuildContextMenu()
        {
            var saveButton = new RibbonButtonData(Messages.Title_Save, imgUri.StringFormat("Save"), saveCommand);
            var cgroup = new RibbonGroupData(Messages.Menu_Actions);

            cgroup.ButtonDataCollection.Add(saveButton);

            var tab = new RibbonTabData(Messages.Menu_File, cgroup) { ContextualTabGroupHeader = Messages.Title_ContextMenu };
            this.Host.Add(tab);

            this.contextualMenu = new RibbonContextualTabGroupData(Messages.Title_ContextMenu, tab) { Background = Brushes.OrangeRed, IsVisible = false };
            this.Host.Add(this.contextualMenu);
        }

        private bool CanNavigate()
        {
            return this.Host.SelectedPatient != null;
        }

        private void ConfigureAutoMapper()
        {
            Mapper.CreateMap<LightDoctorDto, LightDoctorViewModel>();
            Mapper.CreateMap<LightDoctorViewModel, LightDoctorDto>();
        }

        private void Navigate()
        {
            this.Host.Navigate(this.workbench);
            var viewModel = new WorkbenchViewModel();
            viewModel.Refresh();
            this.workbench.DataContext = viewModel;

            this.contextualMenu.IsVisible = true;
            this.contextualMenu.TabDataCollection[0].IsSelected = true;
        }

        #endregion Methods
    }
}