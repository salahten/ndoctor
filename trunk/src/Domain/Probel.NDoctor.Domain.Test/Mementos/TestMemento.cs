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

namespace Probel.NDoctor.Domain.Test.Mementos
{
    using NUnit.Framework;
    using Probel.NDoctor.Domain.DAL.Cfg;
    using Probel.NDoctor.Domain.DAL.Entities;
    using Probel.NDoctor.Domain.DAL.Mementos;
    using Probel.NDoctor.Domain.Test.Helpers;
    using System.Threading;

    [Category(Categories.Memento)]
    [TestFixture]
    public class TestMemento
    {
        [TestFixtureSetUp]
        public void Configure()
        {
            new DalConfigurator().ConfigureAutoMapper();
        }
        #region Methods

        [Test]
        public void CreateMemento_AddAState_StateAdded()
        {
            var record = new MedicalRecord() { Rtf = "1" };
            var memento = new MedicalRecordMemento();

            Assert.AreEqual(0, record.PreviousStates.Count);
            memento.SaveState(record);
            Assert.AreEqual(1, record.PreviousStates.Count);
            Assert.AreEqual("1", record.PreviousStates[0].Rtf);
        }

        [Test]
        public void CreateMemento_AddMoreThan10Items_10ItemsAreSaved()
        {
            var record = new MedicalRecord() { Rtf = "1" };
            var memento = new MedicalRecordMemento();

            for (int i = 0; i < 19; i++)
            {
                Thread.Sleep(10);
                record.Rtf = (i + 1).ToString();
                memento.SaveState(record);
            }

            Assert.AreEqual(10, record.PreviousStates.Count);
            Assert.AreEqual("19", record.PreviousStates[0].Rtf);
        }

        #endregion Methods
    }
}