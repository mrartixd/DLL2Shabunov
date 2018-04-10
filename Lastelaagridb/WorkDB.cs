using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Lastelaagridb
{
    public class WorkDB
    {

        public static List<Student> GetStudents()
        {
            List<Student> students = null;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM student";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbCommand cmdRuhm = new OleDbCommand("SELECT * FROM group", conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                students = new List<Student>();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Student student = new Student();
                        Ruhm ruhm = new Ruhm();
                        student.ID = (int)reader[0];
                        student.Nimi = reader[1].ToString();
                        student.Isikukood = (int)reader[2];
                        student.Kool = reader[3].ToString();
                        student.Klass = (int)reader[4];
                        student.Telefon = reader[5].ToString();
                        student.Aadress = reader[6].ToString();
                        student.Ruhm = ruhm.ID;
                        students.Add(student);
                    }
                }

            }
            return students;
        }

        public static List<Ruhm> GetRuhms()
        {
            List<Ruhm> ruhms = null;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM [group]";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                ruhms = new List<Ruhm>();
                using (reader)
                {
                    while(reader.Read())
                    {
                        Ruhm ruhm = new Ruhm();
                        ruhm.ID = (int)reader[0];
                        ruhm.NimiRuhm = reader[1].ToString();
                        ruhms.Add(ruhm);
                    }
                }
            }
            return ruhms;
        }

        public static List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = null;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM teacher";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbCommand cmdRuhm = new OleDbCommand("SELECT * FROM group", conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                teachers = new List<Teacher>();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Teacher teacher = new Teacher();
                        Ruhm ruhm = new Ruhm();
                        teacher.ID = (int)reader[0];
                        teacher.Nimi = reader[1].ToString();
                        teacher.Isikukood = (int)reader[2];
                        teacher.Telefon = reader[5].ToString();
                        teacher.Aadress = reader[6].ToString();
                        teacher.Ruhm = ruhm.ID;
                        teachers.Add(teacher);
                    }
                }

            }
            return teachers;
        }

        public static int InsertNewStudent(Student student)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into student(Nimi, Isikukood, Kool, Klass, Telefon, Aadress, Ruhm) values(?,?,?,?,?,?,?)";
                cmd.Parameters.AddWithValue("@nimi", student.Nimi);
                cmd.Parameters.AddWithValue("@isikukood", student.Isikukood);
                cmd.Parameters.AddWithValue("@kool", student.Kool);
                cmd.Parameters.AddWithValue("@klass", student.Klass);
                cmd.Parameters.AddWithValue("@telefon", student.Telefon);
                cmd.Parameters.AddWithValue("@aadress", student.Aadress);
                cmd.Parameters.AddWithValue("@ruhm", student.Ruhm);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }


            }
                return 1;
        }
        public static int InsertNewTeacher(Teacher teacher)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into teacher(Nimi, Isikukood, Telefon, Aadress, Ruhm) values(?,?,?,?,?)";
                cmd.Parameters.AddWithValue("@nimi", teacher.Nimi);
                cmd.Parameters.AddWithValue("@isikukood", teacher.Isikukood);
                cmd.Parameters.AddWithValue("@telefon", teacher.Telefon);
                cmd.Parameters.AddWithValue("@aadress", teacher.Aadress);
                cmd.Parameters.AddWithValue("@ruhm", teacher.Ruhm);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }


            }
            return 1;
        }

        public static int InsertNewRuhm(Ruhm ruhm)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO [group] (NimiRuhm)" + "VALUES (@nimiruhm)";
                cmd.Parameters.AddWithValue("@nimiruhm", ruhm.NimiRuhm);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }


            }
            return 1;
        }

        
        public static int DeleteRuhm(int id)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE * FROM group WHERE [ID] = @id";
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }
            }
                return 1;
        }

        public static int DeleteStudent(int id)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE * FROM student WHERE [ID] = @id";
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }
            }
            return 1;
        }

        public static int DeleteTeacher(int id)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE * FROM teacher WHERE [ID] = @id";
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = id;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }
            }
            return 1;
        }

        public static int UpdateStudent(Student student)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE student SET Nimi = @nimi, Isikukood=@isikukood, @Kool = kool, Klass = @klass, Telefon = @telefon, Aadress = @aadress, Ruhm = @ruhm";
                cmd.Parameters.AddWithValue("@nimi", student.Nimi);
                cmd.Parameters.AddWithValue("@isikukood", student.Isikukood);
                cmd.Parameters.AddWithValue("@kool", student.Kool);
                cmd.Parameters.AddWithValue("@klass", student.Klass);
                cmd.Parameters.AddWithValue("@telefon", student.Telefon);
                cmd.Parameters.AddWithValue("@aadress", student.Aadress);
                cmd.Parameters.AddWithValue("@ruhm", student.Ruhm);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }
            }
            return 1;
        }

        public static int UpdateTeacher(Teacher teacher)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE student SET Nimi = @nimi, Isikukood=@isikukood, Telefon = @telefon, Aadress = @aadress, Ruhm = @ruhm";
                cmd.Parameters.AddWithValue("@nimi", teacher.Nimi);
                cmd.Parameters.AddWithValue("@isikukood", teacher.Isikukood);
                cmd.Parameters.AddWithValue("@telefon", teacher.Telefon);
                cmd.Parameters.AddWithValue("@aadress", teacher.Aadress);
                cmd.Parameters.AddWithValue("@ruhm", teacher.Ruhm);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }


            }
            return 1;
        }

        public static int UpdateRuhm(Ruhm ruhm)
        {
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "UPDATE student SET NimiRuhm = @nimi";
                cmd.Parameters.AddWithValue("@nimi", ruhm.NimiRuhm);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    return 0;
                }


            }
            return 1;
        }

    }
}
