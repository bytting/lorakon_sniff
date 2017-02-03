/*	
	Lorakon Sniff - Extract data from gamma spectrums and insert into database
    Copyright (C) 2017  Norwegian Radiation Protection Authority

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
// Authors: Dag Robole,

using System;

namespace LorakonSniff
{
    [Serializable()]
    public class Settings
    {
        public string SourceDirectory { get; set; }
        public string DestinationDirectory { get; set; }
        public string FileFilter { get; set; }
        public DateTime LastShutdownTime { get; set; }

        public Settings()
        {
            SourceDirectory = "C:\\LorakonSource";
            DestinationDirectory = "C:\\LorakonDestination";
            FileFilter = "*.cnf";
            LastShutdownTime = DateTime.MinValue;
        }        
    }
}
