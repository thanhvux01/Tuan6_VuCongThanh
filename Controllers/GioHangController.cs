﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan4_VuCongThanh.Models;
namespace Tuan4_VuCongThanh.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        MyDataDataContext data = new MyDataDataContext();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            {
                if (lstGiohang == null)
                {
                    lstGiohang = new List<GioHang>();
                    Session["Giohang"] = lstGiohang;
                }
            }
            return lstGiohang;
        }
            public ActionResult ThemGioHang(int id,string strURL)
            {
                List<GioHang> lstGiohang = Laygiohang();
                GioHang sanpham = lstGiohang.Find(n => n.masach == id);
                if (sanpham == null)
                {
                    sanpham = new GioHang(id);
                    lstGiohang.Add(sanpham);
                    return Redirect(strURL);
                }
                else
                {
                sanpham.iSoluong++;
                return Redirect(strURL);
                }

            }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if(lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.iSoluong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if(lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if(lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.masach == id);
                if(sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.masach == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");

        }
        public ActionResult CapnhatGioHang(int id,FormCollection collection)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.masach == id);
            GioHang gioHang = lstGiohang.FirstOrDefault(m => m.masach == id);
            if(sanpham!=null)
            {   
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
                Sach sach = data.Saches.FirstOrDefault(m => m.masach == id);
                int soluong = int.Parse(collection["txtSoLg"].ToString());
                if(soluong > sach.soluongton)
                {
                    Session["Message"] = "Khong du so luong";
                    Session["AlertStatus"] = "Danger";
                    return RedirectToAction("GioHang");
                }
               gioHang.iSoluong = soluong;
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }


    }
}