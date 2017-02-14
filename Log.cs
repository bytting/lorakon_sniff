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
using System.Collections.Generic;
using System.IO;

namespace LorakonSniff
{
    public class Log
    {
        public Log()
        {
        }

        public void AddMessage(string message)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(LorakonEnvironment.LogFile))
                {
                    LogMessage(writer, message);
                }
            }
            catch
            {
            }
        }

        public void LogMessage(TextWriter writer, string message)
        {
            try
            {
                writer.WriteLine("{0} {1} === {2}", DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString(), message);                
            }
            catch
            {
            }
        }
    }
}
