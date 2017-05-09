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
using System.Data.SqlClient;

namespace LorakonSniff
{
    public static class Log
    {
        public enum Severity { Normal=0, Warning=1, Critical=2 };

        public static void Add(SqlConnection connection, Severity severity, string msg)
        {
            SqlCommand command = new SqlCommand("proc_spectrum_log_insert", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Severity", severity);
            command.Parameters.AddWithValue("@Message", msg);
            command.ExecuteNonQuery();
        }

        public static List<string> Get(SqlConnection connection, DateTime fromDate, DateTime toDate)
        {
            List<string> logMessages = new List<string>();

            SqlCommand command = new SqlCommand("proc_spectrum_log_select", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FromDate", fromDate);
            command.Parameters.AddWithValue("@ToDate", toDate);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    logMessages.Add(Convert.ToString(reader["Message"]));
            }   
                 
            return logMessages;
        }

        public static List<string> Get(SqlConnection connection, DateTime fromDate, DateTime toDate, Severity severity)
        {
            List<string> logMessages = new List<string>();

            SqlCommand command = new SqlCommand("proc_spectrum_log_select_severity", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FromDate", fromDate);
            command.Parameters.AddWithValue("@ToDate", toDate);
            command.Parameters.AddWithValue("@Severity", severity);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    logMessages.Add(Convert.ToString(reader["Message"]));
            }

            return logMessages;
        }
    }
}
