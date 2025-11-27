using System;
using System.Collections.Generic;
using System.Text;
using CuahangNongduoc.DataLayer;
using CuahangNongduoc.BusinessObject;
using System.Windows.Forms;
using System.Data;

namespace CuahangNongduoc.Controller
{

    public class ChiTietPhieuBanController
    {
        ChiTietPhieuBanFactory factory = new ChiTietPhieuBanFactory();



        public void HienThiChiTiet(DataGridView dgv, String idPhieuBan)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = factory.LayChiTietPhieuBan(idPhieuBan);
            dgv.DataSource = bs;
        }
        public DataRow NewRow()
        {
            return factory.NewRow();
        }
        public void Add(DataRow row)
        {
            factory.Add(row);
        }
        public void Save()
        {
            factory.Save();
        }


        public IList<ChiTietPhieuBan> ChiTietPhieuBan(String idPhieuBan)
        {
            return MapChiTiet(factory.LayChiTietPhieuBan(idPhieuBan));
        }

        public IList<ChiTietPhieuBan> ChiTietPhieuBan(DateTime dtNgayBan)
        {
            return MapChiTiet(factory.LayChiTietPhieuBan(dtNgayBan));
        }
        public IList<ChiTietPhieuBan> ChiTietPhieuBan(int thang, int nam)
        {
            return MapChiTiet(factory.LayChiTietPhieuBan(thang, nam));
        }

        private IList<ChiTietPhieuBan> MapChiTiet(DataTable tbl)
        {
            IList<ChiTietPhieuBan> ds = new List<ChiTietPhieuBan>();
            foreach (DataRow row in tbl.Rows)
            {
                ChiTietPhieuBan ct = new ChiTietPhieuBan();
                ct.DonGia = Convert.ToInt64(row["DON_GIA"]);
                ct.SoLuong = Convert.ToInt32(row["SO_LUONG"]);
                ct.ThanhTien = Convert.ToInt64(row["THANH_TIEN"]);
                ct.MaSanPham = new BusinessObject.MaSanPham
                {
                    Id = Convert.ToString(row["ID_MA_SAN_PHAM"]),
                    NgayHetHan = row.Table.Columns.Contains("NGAY_HET_HAN")
                        ? Convert.ToDateTime(row["NGAY_HET_HAN"])
                        : DateTime.MinValue,
                    NgaySanXuat = row.Table.Columns.Contains("NGAY_SAN_XUAT")
                        ? Convert.ToDateTime(row["NGAY_SAN_XUAT"])
                        : DateTime.MinValue
                };

                ds.Add(ct);
            }
            return ds;
        }

    }
}
