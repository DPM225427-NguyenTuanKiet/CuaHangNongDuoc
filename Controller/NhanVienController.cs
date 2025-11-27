using System.Data;
using CuahangNongduoc.BusinessObject;
using CuahangNongduoc.DataLayer;

namespace CuahangNongduoc.Controller
{
    public class NhanVienController
    {
        private readonly NhanVienFactory _factory = new NhanVienFactory();

        public NhanVien Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            return _factory.GetByCredential(username.Trim(), password.Trim());
        }

        public void HienthiNhanVienDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn)
        {
            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
            DataTable tbl = _factory.DanhsachNhanVien();
            bs.DataSource = tbl;
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public DataTable TimHoTen(string hoten)
        {
            return _factory.TimHoTen(hoten);
        }

        public DataTable TimTenDangNhap(string tendangnhap)
        {
            return _factory.TimTenDangNhap(tendangnhap);
        }

        public void HienthiTimKiemDataGridview(System.Windows.Forms.DataGridView dg, System.Windows.Forms.BindingNavigator bn, DataTable tbl)
        {
            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();
            bs.DataSource = tbl;
            bn.BindingSource = bs;
            dg.DataSource = bs;
        }

        public DataRow NewRow()
        {
            return _factory.NewRow();
        }

        public void Add(DataRow row)
        {
            _factory.Add(row);
        }

        public bool Save()
        {
            return _factory.Save();
        }
    }
}

