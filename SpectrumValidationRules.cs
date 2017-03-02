using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LorakonSniff
{
    public static class SpectrumValidationRules
    {
        public static List<ValidationRule> LoadValidationRules(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("proc_spectrum_validation_rules_select", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = null;
            Rules.Clear();

            try
            {
                reader = command.ExecuteReader();                
                while (reader.Read())
                {
                    ValidationRule rule = new ValidationRule();
                    rule.NuclideName = Convert.ToString(reader["NuclideName"]);
                    rule.ActivityMin = Convert.ToDouble(reader["ActivityMin"]);
                    rule.ActivityMax = Convert.ToDouble(reader["ActivityMax"]);
                    rule.ConfidenceMin = Convert.ToDouble(reader["ConfidenceMin"]);
                    rule.CanBeAutoApproved = Convert.ToBoolean(reader["CanBeAutoApproved"]);
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

        private static List<ValidationRule> Rules = new List<ValidationRule>();
    }

    public class ValidationRule
    {
        public string NuclideName { get; set; }
        public double ActivityMin { get; set; }
        public double ActivityMax { get; set; }
        public double ConfidenceMin { get; set; }
        public bool CanBeAutoApproved { get; set; }
    }
}
