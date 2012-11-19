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
namespace Probel.NDoctor.Domain.DAL.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using NHibernate;
    using NHibernate.Linq;

    using Probel.NDoctor.Domain.DAL.Entities;
    using Probel.NDoctor.Domain.DTO.Components;
    using Probel.NDoctor.Domain.DTO.Objects;

    /// <summary>
    /// Get the features of the patient session
    /// </summary>
    public class PatientSessionComponent : BaseComponent, IPatientSessionComponent
    {
        #region Constructors

        public PatientSessionComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientSessionComponent"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public PatientSessionComponent(ISession session)
            : base(session)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the top X patient. Where X is specified as an argument.
        /// Everytime a user is loaded in memory, a counter is incremented. This
        /// value is used to select the most 'famous' patients.
        /// </summary>
        /// <param name="x">The number of patient this method returns.</param>
        /// <returns>
        /// An array of patients
        /// </returns>
        public IList<LightPatientDto> GetTopXPatient(uint x)
        {
            var result = (from patient in this.Session.Query<Patient>()
                          orderby patient.Counter descending
                          select patient)
                            .Take((int)x)
                            .ToList();

            return Mapper.Map<IList<Patient>, IList<LightPatientDto>>(result)
                         .ToList();
        }

        /// <summary>
        /// Increments the patient counter.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public void IncrementPatientCounter(LightPatientDto patient)
        {
            var entity = this.Session.Get<Patient>(patient.Id);
            entity.Counter++;
            this.Session.Update(entity);
        }

        #endregion Methods
    }
}