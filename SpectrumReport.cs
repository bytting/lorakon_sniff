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
using System.Linq;
using System.Text;

namespace LorakonSniff
{
    class SpectrumReport
    {
        public string Laboratory { get; set; }
        public string Operator { get; set; }
        public string SampleTitle { get; set; }
        public string SampleIdentification { get; set; }
        public string SampleType { get; set; }
        public string SampleComponent { get; set; }
        public string SampleGeometry { get; set; }
        public string SampleLocationType { get; set; }
        public string SampleLocation { get; set; }
        public string SampleCommunityCounty { get; set; }
        public double SampleLatitude { get; set; }
        public double SampleLongitude { get; set; }
        public double SampleAltitude { get; set; }
        public string Comment { get; set; }
        public double SampleSize { get; set; }
        public double SampleError { get; set; }
        public string SampleUnit { get; set; }
        public DateTime SampleTime { get; set; }
        public DateTime AcquisitionTime { get; set; }
        public double Livetime { get; set; }
        public double Realtime { get; set; }
        public double Deadtime { get; set; }
        public string Filename { get; set; }
        public string BackgroundFile { get; set; }
        public string NuclideLibrary { get; set; }
        public double Sigma { get; set; }

        public List<SpectrumResult> Results = new List<SpectrumResult>();
        public List<SpectrumBackground> Backgrounds = new List<SpectrumBackground>();

        public SpectrumReport()
        {            
        }
    }

    public class SpectrumBackground
    {        
        public double Energy { get; set; }
        public double OrigArea { get; set; }
        public double OrigAreaUncertainty { get; set; }
        public double SubtractedArea { get; set; }
        public double SubtractedAreaUncertainty { get; set; }

        public SpectrumBackground()
        {
        }
    }

    public class SpectrumResult
    {
        public string NuclideName { get; set; }        
        public double Activity { get; set; }
        public double ActivityUncertainty { get; set; }
        public double MDA { get; set; }

        public SpectrumResult()
        {
        }
    }
}
