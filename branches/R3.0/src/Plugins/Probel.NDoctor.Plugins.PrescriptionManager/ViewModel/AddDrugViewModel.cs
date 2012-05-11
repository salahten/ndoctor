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
namespace Probel.NDoctor.Plugins.PrescriptionManager.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using Probel.Helpers.Conversions;
    using Probel.Helpers.WPF;
    using Probel.Mvvm.DataBinding;
    using Probel.NDoctor.Domain.DAL.Exceptions;
    using Probel.NDoctor.Domain.DTO.Components;
    using Probel.NDoctor.Domain.DTO.Objects;
    using Probel.NDoctor.Plugins.PrescriptionManager.Helpers;
    using Probel.NDoctor.Plugins.PrescriptionManager.Properties;
    using Probel.NDoctor.View.Core.Helpers;
    using Probel.NDoctor.View.Core.ViewModel;
    using Probel.NDoctor.View.Plugins.Helpers;

    using StructureMap;

    public class AddDrugViewModel : BaseViewModel
    {
        #region Fields

        private IPrescriptionComponent component;
        private DrugDto selectedDrug;

        #endregion Fields

        #region Constructors

        public AddDrugViewModel()
        {
            if (!Designer.IsDesignMode) this.component = ObjectFactory.GetInstance<IPrescriptionComponent>();

            this.Tags = new ObservableCollection<TagDto>();
            this.SelectedDrug = new DrugDto();

            this.AddCommand = new RelayCommand(() => this.Add(), () => this.CanAdd());
        }

        #endregion Constructors

        #region Properties

        public ICommand AddCommand
        {
            get;
            private set;
        }

        public DrugDto SelectedDrug
        {
            get { return this.selectedDrug; }
            set
            {
                this.selectedDrug = value;
                this.OnPropertyChanged(() => SelectedDrug);
            }
        }

        public ObservableCollection<TagDto> Tags
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        public void Refresh()
        {
            IList<TagDto> result;
            using (this.component.UnitOfWork)
            {
                result = this.component.FindTags(TagCategory.Drug);
            }
            this.Tags.Refill(result);
        }

        private void Add()
        {
            try
            {
                using (this.component.UnitOfWork)
                {
                    this.component.Create(this.SelectedDrug);
                }
                Notifyer.OnItemChanged(this);
                PluginContext.Host.WriteStatus(StatusType.Info, Messages.Msg_DataSaved);
                this.SelectedDrug = new DrugDto();
                InnerWindow.Close();
            }
            catch (ExistingItemException ex)
            {
                this.HandleWarning(ex, ex.Message);
            }
            catch (Exception ex)
            {
                this.HandleError(ex, Messages.Msg_ErrorOccured);
            }
        }

        private bool CanAdd()
        {
            return this.SelectedDrug != null
                && this.SelectedDrug.Tag != null
                && !string.IsNullOrWhiteSpace(this.SelectedDrug.Name);
        }

        #endregion Methods
    }
}