using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service
{
    /// <summary>
    /// DB클래스 입니다.
    /// </summary>
     class Project_DB
    {
        static string  conStr = @"Data Source=IS-LAB21\MGKIM1030;Initial Catalog=Smart Wireless Protect Key DB;Persist Security Info=True;User ID=sa;Password=***********";
        SqlConnection scon = new SqlConnection(conStr);

        #region DB Singleton
        static Project_DB pd;
        static Project_DB() { }
        public static Project_DB GetInstance()
        {
            if (pd == null)
            {
                pd = new Project_DB();
            }
            return pd;
        }
        #endregion

        #region 아이디 중복검사
        /// <summary>
        /// 아이디 중복검사 DB쿼리입니다.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool OverlapID(string id)
        {
            string sql = "SELECT ID FROM USER";
            using (SqlCommand scom = new SqlCommand(sql, scon))
            {
                scom.Connection.Open();
                using (SqlDataReader reader = scom.ExecuteReader())
                {
                    if (reader == null)
                    {
                        scom.Connection.Close();
                        return true;
                    }
                    while (reader.Read())
                    {
                        if (reader["ID"].Equals(id))
                        {
                            scom.Connection.Close();
                            return false;
                        }
                    }
                }
            }
                return true;
        }
        #endregion

        #region 휴대폰 번호 중복검사
        /// <summary>
        /// 휴대폰 번호 중복검사 DB쿼리입니다.
        /// </summary>
        /// <param name="pnum"></param>
        /// <returns></returns>
        public bool OverlapPhoneNum(string pnum)
        {
            string sql = "SELECT PH_NUM FROM USER";
            using (SqlCommand scom = new SqlCommand(sql, scon))
            {
                scom.Connection.Open();
                using (SqlDataReader reader = scom.ExecuteReader())
                {
                    if (reader == null)
                    {
                        scom.Connection.Close();
                        return true;
                    }
                    while (reader.Read())
                    {
                        if (reader["PH_NUM"].Equals(pnum))
                        {
                            scom.Connection.Close();
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region 회원가입
        /// <summary>
        /// 회원가입 DB쿼리입니다. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pw"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="pnum"></param>
        /// <returns></returns>
        public bool JoinMember(string id,string pw,string name, string email,string pnum)
        {
            if (OverlapID(id)==false || OverlapPhoneNum(pnum)==false)
            {
                return false;
            }
            string sql = "SELECT * FROM USER";
            SqlDataAdapter da = new SqlDataAdapter(sql,scon);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet("USER");

            DataRow myRow;
            myRow = ds.Tables["USER"].NewRow();

            myRow["ID"] = id;
            myRow["PW"] = pw;
            myRow["NAME"] = name;
            myRow["EMAIL"] = email;
            myRow["PH_NUM"] = pnum;
            ds.Tables["USER"].Rows.Add(myRow);
            da.Update(ds,"USER");
            scon.Close();
            return true;
        }
        #endregion

        #region 로그인 아이디 불일치
        /// <summary>
        /// 로그인 아이디 불일치 쿼리 입니다.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool LoginIDCheck(string id)
        {
            string sql = "SELECT ID FROM USER";
            using (SqlCommand scom = new SqlCommand(sql, scon))
            {
                scom.Connection.Open();
                using (SqlDataReader reader = scom.ExecuteReader())
                {
                    if (reader == null)
                    {
                        scom.Connection.Close();
                        return false;
                    }
                    //while (reader.Read())
                    //{
                    //    if (reader["ID"].Equals(id))
                    //    {
                    //        return true;
                    //    }
                    //}
                }
            }
                return true;
        }
        #endregion

        #region 로그인 비밀번호 불일치
        /// <summary>
        /// 로그인 비밀번호 불일치 쿼리입니다. 
        /// </summary>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool LoginPWCheck(string pw)
        {
            string sql = "SELECT PW FROM USER";
            using (SqlCommand scom = new SqlCommand(sql, scon))
            {
                scom.Connection.Open();
                using (SqlDataReader reader = scom.ExecuteReader())
                {
                    if (reader == null)
                    {
                        scom.Connection.Close();
                        return false;
                    }
                    //while (reader.Read())
                    //{
                    //    if (reader["ID"].Equals(id))
                    //    {
                    //        return true;
                    //    }
                    //}
                }
            }
            return true;
        }
        #endregion

        #region 로그인
        /// <summary>
        /// 로그인 DB쿼리 입니다.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        public bool LoginIDPWCheck(string id,string pw)
        {
            if(LoginIDCheck(id) == true||LoginPWCheck(pw)==true)
            {
                string sql = "SELECT * FROM USER";
                using (SqlCommand scom = new SqlCommand(sql, scon))
                {
                    scom.Connection.Open();
                    using (SqlDataReader reader = scom.ExecuteReader())
                    {
                        if (reader == null)
                        {
                            scom.Connection.Close();
                            return false;
                        }
                        while (reader.Read())
                        {
                            if (reader["ID"].Equals(id))
                            {
                                if (reader["PW"].Equals(pw))
                                {
                                    scom.Connection.Close();
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        #endregion
    }
}