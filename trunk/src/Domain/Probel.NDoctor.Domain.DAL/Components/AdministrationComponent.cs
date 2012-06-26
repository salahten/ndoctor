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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using NHibernate.Linq;

    using Probel.Helpers.Assertion;
    using Probel.NDoctor.Domain.DAL.Entities;
    using Probel.NDoctor.Domain.DAL.Properties;
    using Probel.NDoctor.Domain.DAL.Subcomponents;
    using Probel.NDoctor.Domain.DTO.Components;
    using Probel.NDoctor.Domain.DTO.Exceptions;
    using Probel.NDoctor.Domain.DTO.Objects;

    public class AdministrationComponent : BaseComponent, IAdministrationComponent
    {
        #region Methods

        /// <summary>
        /// Determines whether the specified item can be removed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if this instance can remove the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRemove(PathologyDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        /// <summary>
        /// Determines whether the specified item can be removed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if this instance can remove the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRemove(InsuranceDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        /// <summary>
        /// Determines whether the specified item can be removed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if this instance can remove the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRemove(PracticeDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        /// <summary>
        /// Determines whether this instance can remove the specified drug dto.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if this instance can remove the specified drug dto; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRemove(DrugDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        /// <summary>
        /// Determines whether this instance can remove the specified reputation dto.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if this instance can remove the specified reputation dto; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRemove(ReputationDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        /// <summary>
        /// Determines whether this instance can remove the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if this instance can remove the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRemove(TagDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        public bool CanRemove(ProfessionDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        /// <summary>
        /// Determines whether the specified doctor can be removed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if this instance can remove the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRemove(DoctorDto item)
        {
            return new Remover(this.Session).CanRemove(item);
        }

        /// <summary>
        /// Creates the specified profession.
        /// </summary>
        /// <param name="profession">The tag.</param>
        public long Create(ProfessionDto profession)
        {
            return new Creator(this.Session).Create(profession);
        }

        /// <summary>
        /// Creates the specified reputation.
        /// </summary>
        /// <param name="reputation">The tag.</param>
        public long Create(ReputationDto reputation)
        {
            return new Creator(this.Session).Create(reputation);
        }

        /// <summary>
        /// Creates the specified pathology.
        /// </summary>
        /// <param name="pathology">The drug.</param>
        public long Create(PathologyDto pathology)
        {
            return new Creator(this.Session).Create(pathology);
        }

        /// <summary>
        /// Creates the specified practice.
        /// </summary>
        /// <param name="practice">The drug.</param>
        public long Create(PracticeDto practice)
        {
            return new Creator(this.Session).Create(practice);
        }

        /// <summary>
        /// Creates the specified insurance.
        /// </summary>
        /// <param name="insurance">The drug.</param>
        public long Create(InsuranceDto insurance)
        {
            return new Creator(this.Session).Create(insurance);
        }

        /// <summary>
        /// Creates the specified doctor.
        /// </summary>
        /// <param name="doctor">The doctor.</param>
        /// <returns></returns>
        public long Create(DoctorDto doctor)
        {
            return new Creator(this.Session).Create(doctor);
        }

        /// <summary>
        /// Gets all doctors.
        /// </summary>
        /// <returns></returns>
        public IList<DoctorDto> GetAllDoctors()
        {
            var entities = (from d in this.Session.Query<Doctor>()
                            select d).ToList();
            return Mapper.Map<IList<Doctor>, IList<DoctorDto>>(entities);
        }

        /// <summary>
        /// Removes item with the specified id.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(TagDto item)
        {
            new Remover(this.Session).Remove(item);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void Remove(PathologyDto item)
        {
            new Remover(this.Session).Remove(item);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void Remove(DrugDto item)
        {
            new Remover(this.Session).Remove(item);
        }

        /// <summary>
        /// Removes item with the specified id.
        /// </summary>
        /// <typeparam name="T">The type of the item to remove</typeparam>
        /// <param name="id">The id of the item to remove.</param>
        public void Remove(InsuranceDto item)
        {
            new Remover(this.Session).Remove(item);
        }

        /// <summary>
        /// Removes item with the specified id.
        /// </summary>
        /// <typeparam name="T">The type of the item to remove</typeparam>
        /// <param name="id">The id of the item to remove.</param>
        public void Remove(PracticeDto item)
        {
            new Remover(this.Session).Remove(item);
        }

        /// <summary>
        /// Removes the specified doctor.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(DoctorDto item)
        {
            new Remover(this.Session).Remove(item);
        }

        /// <summary>
        /// Updates the specified tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public void Update(TagDto tag)
        {
            new Updator(this.Session).Update(tag);
        }

        /// <summary>
        /// Updates the specified profession.
        /// </summary>
        /// <param name="profession">The tag.</param>
        public void Update(ProfessionDto profession)
        {
            new Updator(this.Session).Update(profession);
        }

        /// <summary>
        /// Updates the specified reputation.
        /// </summary>
        /// <param name="reputation">The tag.</param>
        public void Update(ReputationDto reputation)
        {
            new Updator(this.Session).Update(reputation);
        }

        /// <summary>
        /// Updates the specified drug.
        /// </summary>
        /// <param name="drug">The drug.</param>
        public void Update(DrugDto drug)
        {
            new Updator(this.Session).Update(drug);
        }

        /// <summary>
        /// Updates the specified pathology.
        /// </summary>
        /// <param name="pathology">The drug.</param>
        public void Update(PathologyDto pathology)
        {
            new Updator(this.Session).Update(pathology);
        }

        /// <summary>
        /// Updates the specified practice.
        /// </summary>
        /// <param name="practice">The drug.</param>
        public void Update(PracticeDto practice)
        {
            new Updator(this.Session).Update(practice);
        }

        /// <summary>
        /// Updates the specified insurance.
        /// </summary>
        /// <param name="insurance">The drug.</param>
        public void Update(InsuranceDto insurance)
        {
            new Updator(this.Session).Update(insurance);
        }

        /// <summary>
        /// Updates the specified doctor.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Update(DoctorDto item)
        {
            new Updator(this.Session).Update(item);
        }

        #endregion Methods
    }
}