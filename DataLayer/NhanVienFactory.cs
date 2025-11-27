using System.Data;
using System.Data.OleDb;
using CuahangNongduoc.BusinessObject;

namespace CuahangNongduoc.DataLayer
{
    public class NhanVienFactory
    {
        DataService m_Ds = new DataService();

        public NhanVien GetByCredential(string username, string password)
        {
            var ds = new DataService();
            var cmd = new OleDbCommand(
                "SELECT TOP 1 * FROM NHAN_VIEN WHERE TEN_DANG_NHAP = @user AND MAT_KHAU = @password");
            cmd.Parameters.Add("@user", OleDbType.VarChar).Value = username;
            cmd.Parameters.Add("@password", OleDbType.VarChar).Value = password;

            ds.Load(cmd);

            if (ds.Rows.Count == 0)
            {
                return null;
            }

            return Map(ds.Rows[0]);
        }

        public DataTable DanhsachNhanVien()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHAN_VIEN ORDER BY MA_NHAN_VIEN");
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable TimHoTen(string hoten)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHAN_VIEN WHERE HO_TEN LIKE '%' + @hoten + '%' ORDER BY MA_NHAN_VIEN");
            cmd.Parameters.Add("@hoten", OleDbType.VarChar).Value = hoten;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable TimTenDangNhap(string tendangnhap)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM NHAN_VIEN WHERE TEN_DANG_NHAP LIKE '%' + @tendangnhap + '%' ORDER BY MA_NHAN_VIEN");
            cmd.Parameters.Add("@tendangnhap", OleDbType.VarChar).Value = tendangnhap;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataRow NewRow()
        {
            return m_Ds.NewRow();
        }

        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }

        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }

        private NhanVien Map(DataRow row)
        {
            return new NhanVien
            {
                Id = row["ID"].ToString(),
                MaNhanVien = row["MA_NHAN_VIEN"].ToString(),
                HoTen = row["HO_TEN"].ToString(),
                TenDangNhap = row["TEN_DANG_NHAP"].ToString(),
                MatKhau = row["MAT_KHAU"].ToString(),
                VaiTro = row["VAI_TRO"].ToString()
            };
        }
    }
}

