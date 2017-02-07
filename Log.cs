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
using System.Data;
using System.Data.SQLite;

namespace LorakonSniff
{
    public static class Log
    {
        public static SQLiteConnection Create()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + LorakonEnvironment.LogDB + ";Version=3;Compress=True;");

            if (!File.Exists(LorakonEnvironment.LogDB))
            {
                SQLiteConnection.CreateFile(LorakonEnvironment.LogDB);
                SQLiteCommand cmd = new SQLiteCommand("create table log_entries (created datetime, message text)", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return conn;
        }

        public static void Open(SQLiteConnection conn)
        {
            if (conn != null && conn.State != ConnectionState.Open)
                conn.Open();
        }

        public static void Close(ref SQLiteConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        public static void AddMessage(SQLiteConnection conn, string message)
        {
            SQLiteCommand cmd = new SQLiteCommand("insert into log_entries (created, message) values(@created, @message)", conn);
            cmd.Parameters.AddWithValue("@created", DateTime.Now);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.ExecuteNonQuery();
        }

        public static List<string> GetEntries(SQLiteConnection conn, DateTime from, DateTime to)
        {
            SQLiteCommand cmd = new SQLiteCommand("select created, message from log_entries where created > @from and created < @to order by created desc", conn);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@to", to);
            List<string> entries = new List<string>();
            SQLiteDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                DateTime created = Convert.ToDateTime(reader["created"]);
                entries.Add(created.ToString() + " - " + reader["message"]);
            }
            reader.Close();
            return entries;
        }
    }
}
