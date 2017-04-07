using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LorakonSniff
{
    public static class SpectrumGeometryRules
    {
        public static List<GeometryRule> LoadGeometryRules(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("proc_spectrum_geometry_rules_select", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = null;
            Rules.Clear();

            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GeometryRule rule = new GeometryRule();
                    rule.GeometryName = Convert.ToString(reader["Geometry"]).Trim();
                    rule.Unit = Convert.ToString(reader["Unit"]).Trim();
                    rule.Minimum = Convert.ToDouble(reader["Minimum"]);
                    rule.Maximum = Convert.ToDouble(reader["Maximum"]);                    
                    Rules.Add(rule);
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }

            return Rules;
        }

        private static List<GeometryRule> Rules = new List<GeometryRule>();
    }

    public class GeometryRule
    {
        public string GeometryName { get; set; }
        public string Unit { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }        
    }
}
