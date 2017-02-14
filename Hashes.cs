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
using System.IO;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;

namespace LorakonSniff
{
    public class Hashes
    {
        public Hashes()
        {
        }        

        public string CalculateChecksum(byte[] buffer)
        {            
            var sha = new SHA256Managed();
            byte[] checksum = sha.ComputeHash(buffer);
            return BitConverter.ToString(checksum).Replace("-", String.Empty);
        }

        public string CalculateChecksum(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        public void StoreChecksum(SqlConnection connection, string checksum, Guid spectrumId)
        {                    
            SqlCommand command = new SqlCommand("proc_spectrum_checksum_insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Sha256Sum", checksum);
            command.Parameters.AddWithValue("@SpectrumInfoId", spectrumId);
            command.ExecuteNonQuery();
        }

        public bool LookupChecksum(SqlConnection connection, string checksum)
        {
            SqlCommand command = new SqlCommand("proc_spectrum_checksum_count", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Sha256Sum", checksum);
            object o = command.ExecuteScalar();
            if (o == null || o == DBNull.Value)
                return false;

            int nRows = Convert.ToInt32(o);
            return nRows > 0;
        }
    }
}
