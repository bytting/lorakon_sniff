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
using System.Data;
using System.Data.SQLite;

namespace LorakonSniff
{
    public class Hashes
    {
        SQLiteConnection conn = null;

        public Hashes()
        {
            conn = new SQLiteConnection("Data Source=" + LorakonEnvironment.HashDB + ";Version=3;Compress=True;");

            if (!File.Exists(LorakonEnvironment.HashDB))
            {
                SQLiteConnection.CreateFile(LorakonEnvironment.HashDB);
                SQLiteCommand cmd = new SQLiteCommand("create table sync_objects (checksum char(64))", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }        

        private void Open()
        {
            if (conn != null && conn.State != ConnectionState.Open)
                conn.Open();
        }

        private void Close()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();            
        }

        public void InsertChecksum(string cs)
        {
            try
            {
                Open();
                SQLiteCommand cmd = new SQLiteCommand("insert into sync_objects (checksum) values('" + cs + "')", conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Close();
            }
        }

        public bool HasChecksum(string cs)
        {
            try
            {
                Open();
                SQLiteCommand cmd = new SQLiteCommand("select count(*) from sync_objects where checksum like '" + cs + "'", conn);
                object o = cmd.ExecuteScalar();
                if (o == null || o == DBNull.Value)
                    return false;
                int nRows = Convert.ToInt32(o);
                return nRows > 0;
            }
            finally
            {
                Close();
            }
        }
    }
}
