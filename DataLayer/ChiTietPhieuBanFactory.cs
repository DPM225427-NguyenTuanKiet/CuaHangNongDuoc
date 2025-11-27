using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using CuahangNongduoc.Infrastructure;

namespace CuahangNongduoc.DataLayer
{
    public class ChiTietPhieuBanFactory
    {
        DataService m_Ds = new DataService();

        public DataTable LayChiTietPhieuBan(String idPhieuBan)
        {
            OleDbCommand cmd = new OleDbCommand(
                "SELECT CT.*, MSP.NGAY_HET_HAN, MSP.NGAY_SAN_XUAT FROM CHI_TIET_PHIEU_BAN CT " +
                "INNER JOIN MA_SAN_PHAM MSP ON CT.ID_MA_SAN_PHAM = MSP.ID WHERE CT.ID_PHIEU_BAN = @id");
            cmd.Parameters.Add("id", OleDbType.VarChar , 50).Value = idPhieuBan;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable LayChiTietPhieuBan(DateTime dtNgayBan)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT CT.*, MSP.NGAY_HET_HAN, MSP.NGAY_SAN_XUAT FROM CHI_TIET_PHIEU_BAN CT INNER JOIN PHIEU_BAN PB ON CT.ID_PHIEU_BAN = PB.ID " +
                    " INNER JOIN MA_SAN_PHAM MSP ON CT.ID_MA_SAN_PHAM = MSP.ID WHERE PB.NGAY_BAN = @ngayban");
            cmd.Parameters.Add("ngayban", OleDbType.Date).Value = dtNgayBan;
            m_Ds.Load(cmd);
            return m_Ds;
        }

        public DataTable LayChiTietPhieuBan(int thang, int nam)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT CT.*, MSP.NGAY_HET_HAN, MSP.NGAY_SAN_XUAT FROM CHI_TIET_PHIEU_BAN CT INNER JOIN PHIEU_BAN PB ON CT.ID_PHIEU_BAN = PB.ID " +
                    " INNER JOIN MA_SAN_PHAM MSP ON CT.ID_MA_SAN_PHAM = MSP.ID WHERE MONTH(PB.NGAY_BAN) = @thang AND YEAR(PB.NGAY_BAN)= @nam");
            cmd.Parameters.Add("thang", OleDbType.Integer).Value = thang;
            cmd.Parameters.Add("nam", OleDbType.Integer).Value = nam;
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
            var connection = DatabaseConnection.Instance.GetConnection();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (DataRow row in m_Ds.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            MaSanPhanFactory.CapNhatSoLuong(Convert.ToString(row["ID_MA_SAN_PHAM"]), -Convert.ToInt32(row["SO_LUONG"]), transaction);
                        }
                    }

                    bool result = m_Ds.ExecuteNoneQueryWithTransaction(transaction) > 0;
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
