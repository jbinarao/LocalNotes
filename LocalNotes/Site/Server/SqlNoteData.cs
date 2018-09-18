using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LocalNotes.Site.Server
{
    public class SqlNoteData
    {
        /// <summary>
        /// Return a special DataSet of note details.
        /// NoteID: The note ID number.
        /// NoteName: The name of the note.
        /// NoteAbbr: Abbreviated part of the note text.
        /// NoteTime: The time the note was created or updated.
        /// Modified: A formulated and friendly string of when the note was created or updated from the time of the query.
        /// </summary>
        /// <param name="isRemove">Specify returning records marked as junk.</param>
        /// <param name="lenAbbrText">Specify the length of the abbreviated NoteText value to return.</param>
        /// <param name="isSortDesc">Specify the sort direction in descending order to return the the more recently modified or newer notes first.</param>
        /// <returns>DataSet of note details.</returns>
        public DataSet SelectNoteDataSet(bool isRemove, int lenAbbrText, bool isSortDesc)
        {
            string strConnString = GetAppConnString();
            string strCmdText = "spSelectNoteData";
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        List<SqlParameter> paramList = new List<SqlParameter>()
                        {
                            new SqlParameter("@isRemove", SqlDbType.Bit) { Value = isRemove },
                            new SqlParameter("@lenAbbrText", SqlDbType.Int) { Value = lenAbbrText },
                            new SqlParameter("@isSortDesc", SqlDbType.Bit) { Value = isSortDesc },
                        };
                        command.Parameters.AddRange(paramList.ToArray());

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch
            {
                throw;
            }

            return ds;
        }

        /// <summary>
        /// Update the detail for an existing note.
        /// </summary>
        /// <param name="noteID">The note ID.</param>
        /// <param name="noteName">The note name.</param>
        /// <param name="noteText">The note text.</param>
        public void UpdateNoteDetail(int noteID, string noteName, string noteText)
        {
            string strConnString = GetAppConnString();
            string strCmdText = "spUpdateNoteDetail";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        List<SqlParameter> paramList = new List<SqlParameter>()
                        {
                            new SqlParameter("@noteID", SqlDbType.Int) {Value = noteID},
                            new SqlParameter("@noteName", SqlDbType.NVarChar) { Value = noteName },
                            new SqlParameter("@noteText", SqlDbType.NVarChar) { Value = noteText },
                        };
                        command.Parameters.AddRange(paramList.ToArray());

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create a new note.
        /// </summary>
        /// <param name="noteName">The note name.</param>
        /// <param name="noteText">The note text.</param>
        /// <returns>The note ID for the new note record.</returns>
        public int CreateNote(string noteName, string noteText)
        {
            int intReturn = 0;

            string strConnString = GetAppConnString();
            string strCmdText = "spInsertNoteData";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        List<SqlParameter> paramList = new List<SqlParameter>()
                        {
                            new SqlParameter("@noteName", SqlDbType.NVarChar) { Value = noteName },
                            new SqlParameter("@noteText", SqlDbType.NVarChar) { Value = noteText },
                        };
                        command.Parameters.AddRange(paramList.ToArray());

                        connection.Open();
                        intReturn = (int)command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch
            {
                throw;
            }
            return intReturn;
        }

        /// <summary>
        /// Mark the note for removal.
        /// </summary>
        /// <param name="noteID">The note ID.</param>
        public void RemoveNote(int noteID)
        {
            string strConnString = GetAppConnString();
            string strCmdText = "spUpdateNoteRemove";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        List<SqlParameter> paramList = new List<SqlParameter>()
                        {
                            new SqlParameter("@noteID", SqlDbType.Int) { Value = noteID },
                        };
                        command.Parameters.AddRange(paramList.ToArray());

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Mark the note to be restored.
        /// </summary>
        /// <param name="noteID">The note ID.</param>
        public void RestoreNote(int noteID)
        {
            string strConnString = GetAppConnString();
            string strCmdText = "spUpdateNoteRestore";
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        List<SqlParameter> paramList = new List<SqlParameter>()
                        {
                            new SqlParameter("@noteID", SqlDbType.Int) { Value = noteID },
                        };
                        command.Parameters.AddRange(paramList.ToArray());

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Select active notes not marked for removal.
        /// </summary>
        /// <param name="noteID">The note ID.</param>
        /// <returns>DataSet of active notes.</returns>
        public DataSet SelectActiveNote(int noteID)
        {
            string strConnString = GetAppConnString();
            string strCmdText = "spSelectActiveNote";
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnString))
                {
                    using (SqlCommand command = new SqlCommand(strCmdText))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        List<SqlParameter> paramList = new List<SqlParameter>()
                        {
                            new SqlParameter("@noteID", SqlDbType.Int) { Value = noteID },
                        };
                        command.Parameters.AddRange(paramList.ToArray());

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch
            {
                throw;
            }

            return ds;
        }

        #region HELPER METHODS

        /// <summary>
        /// Obtain the connection string for the SQL data source.
        /// </summary>
        /// <returns>The connection string.</returns>
        private string GetAppConnString()
        {
            try
            {
                //return "Data Source=.;Initial Catalog=TestBadDbName;Integrated Security=True";
                return ConfigurationManager.ConnectionStrings[name: "AppConnString"].ConnectionString;
            }
            catch (ConfigurationErrorsException ceex)
            {
                throw ceex;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}