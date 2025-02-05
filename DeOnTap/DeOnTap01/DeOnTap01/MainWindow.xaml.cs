﻿using DeOnTap01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeOnTap01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            showData();
            showComboBox();
        }

        QuanLyBenhNhanDBContext db = new QuanLyBenhNhanDBContext();

        private void showComboBox()
        {
            var query = from khoa in db.Khoas
                        select khoa;

            khoaCb.ItemsSource = query.ToList();
            khoaCb.DisplayMemberPath = "BenhNhan";
            khoaCb.SelectedValuePath = "MaKhoa";
            khoaCb.SelectedIndex = -1;
        }

        private void showData()
        {
            var query = from bn in db.BenhNhans
                        select bn;

            data.ItemsSource = query.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var query = db.BenhNhans.SingleOrDefault(bn => bn.MaBn.Equals(maBN.Text));
            if(query != null)
            {
                MessageBox.Show("Ma benh nhan bi trung", "Thong bao");
                showData();
            } else
            {
                try
                {
                    if(checkDataInput())
                    {
                        BenhNhan benhNhan = new BenhNhan();
                        benhNhan.MaBn = maBN.Text;
                        benhNhan.HoTen = hoTen.Text;
                        benhNhan.
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void find_Click(object sender, RoutedEventArgs e)
        {

        }


        private bool checkDataInput()
        {
            try
            {
                string tb = "";
                if (manv.Text.Trim().Equals(""))
                    tb += "Ban chua nhap ma nv";
                if (hoten.Text.Trim().Equals(""))
                    tb += "Ban chua nhap ho ten";
                if (cb.SelectedIndex < 0)
                    tb += "Ban chua chon phong ban";
                double a;
                if (luong.Text.Trim().Equals(""))
                    tb += "Ban chua nhap luong";
                else if (!double.TryParse(luong.Text, out a))
                    tb += "Luong yeu cau nhap kieu so";

                if (!tb.Equals(""))
                {
                    MessageBox.Show(tb, "Thong bao");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

        

        

        
