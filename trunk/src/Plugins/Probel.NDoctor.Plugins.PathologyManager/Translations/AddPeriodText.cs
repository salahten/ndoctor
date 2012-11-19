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

namespace Probel.NDoctor.Plugins.PathologyManager.Translations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Probel.NDoctor.Plugins.PathologyManager.Properties;

    internal class AddPeriodText
    {
        #region Properties

        public static string From
        {
            get { return Messages.Title_From; }
        }

        public static string To
        {
            get { return Messages.Title_To; }
        }

        public static string Watermark
        {
            get { return Messages.Title_SearchWatermark; }
        }

        #endregion Properties
    }
}