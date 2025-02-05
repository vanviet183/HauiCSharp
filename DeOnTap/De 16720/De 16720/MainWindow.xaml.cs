﻿using System;
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

namespace De_16720
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

        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tim_Click(object sender, RoutedEventArgs e)
        {

        }

- Button elip:
	<Button.Template>
		<ControlTemplate TargetType = "Button" >
                    < Grid >
                        < Ellipse Fill="Aqua"/>
                        <ContentPresenter HorizontalAlignment = "Center" VerticalAlignment="Center"/>
                    </Grid>
                </ControlTemplate>
	</Button.Template>

     <Window.Resources>
        <Style x:Key="StyLeBorder" TargetType="{x:Type Button}">
            <Setter Property = "FontWeight" Value="Bold" />
            <Setter Property = "Template" >
                < Setter.Value >
                    < ControlTemplate TargetType="{x:Type Button}">
                        <Border CornerRadius = "100" Background="Gray" >
                            <ContentPresenter Focusable = "False" HorizontalAlignment="Center" VerticalAlignment="Center"/>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
        <Style TargetType = "{x:Type TextBox}" >
            < Setter Property="FontFamily" Value="Times New Roman" />
            <Setter Property = "Foreground" Value="Blue" />
        </Style>
    </Window.Resources>

- Show ComboBox:

    private void showComboBox()
        {
            var query = from phong in db.PhongBans
                        select phong;
            danhMuc.ItemsSource = query.ToList();
            rooms.DisplayMemberPath = "TenPhong";
            rooms.SelectedValuePath = "MaPhong";
            rooms.SelectedIndex = 0;
        }

- Check DataGrid Input:
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

        // Them
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // check manv trung
            var query = db.Nhanviens.SingleOrDefault(nv => nv.MaNv.Equals(manvI.Text));
            if (query != null)
            {
                MessageBox.Show("Manv bi trung");
                showData();
            }
            else
            {
                try
                {
                    if (CheckDL())
                    {
                        Nhanvien nv = new Nhanvien();
                        nv.MaNv = manvI.Text;
                        nv.Hoten = nameI.Text;
                        nv.Luong = int.Parse(salaryI.Text);
                        nv.Thuong = int.Parse((thuongI.Text));
                        PhongBan phongBan = (PhongBan)rooms.SelectedItem;
                        nv.MaPhong = phongBan.MaPhong;
                        db.Nhanviens.Add(nv);

                        // Save changes to database
                        db.SaveChanges();
                        MessageBox.Show("Them nhan vien thanh cong");
                        showData();

                        manvI.Clear();
                        nameI.Clear();
                        salaryI.Clear();
                        thuongI.Clear();
                        rooms.SelectedIndex = -1;
                        manvI.Focus();
                    }
                    else
                        return;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        // Sua
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var nvChange = db.Nhanviens.SingleOrDefault(nv => nv.MaNv.Equals(manvI.Text));
            if (nvChange != null)
            {
                nvChange.MaNv = manvI.Text;
                nvChange.Hoten = nameI.Text;
                nvChange.Luong = int.Parse(salaryI.Text);
                nvChange.Thuong = int.Parse(thuongI.Text);
                PhongBan phongban = (PhongBan)rooms.SelectedItem;
                nvChange.MaPhong = phongban.MaPhong;

                db.SaveChanges();
                MessageBox.Show("Sua thanh cong");
                showData();
            }
        }

        // Xoa
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var itemDel = db.Nhanviens.SingleOrDefault(nv => nv.MaNv.Equals(manvI.Text));
            if (itemDel != null)
            {
                MessageBoxResult result = MessageBox.Show("Vui long xac nhan xoa", "Thong bao", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    db.Nhanviens.Remove(itemDel);
                    db.SaveChanges();
                    MessageBox.Show("Xoa thanh cong");
                    showData();
                }
            }
            else
            {
                MessageBox.Show("Khong co item de xoa");
            }
        }

        // Choose Item in DataGrid
        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.SelectedItem != null)
            {
                try
                {
                    Type type = data.SelectedItem.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    rooms.SelectedValue = properties[0].GetValue(data.SelectedValue);
                    manvI.Text = properties[1].GetValue(data.SelectedValue).ToString();
                    nameI.Text = properties[2].GetValue(data.SelectedValue).ToString();
                    salaryI.Text = properties[3].GetValue(data.SelectedValue).ToString();
                    thuongI.Text = properties[4].GetValue(data.SelectedValue).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

- Group by:
            var phongs = from phong in db.PhongBans
                         join nv in db.Nhanviens
                         on phong.MaPhong equals nv.MaPhong
                         select new
                         {
                             phong.MaPhong,
                             phong.TenPhong,
                             nv.MaNv,
                             nv.Luong

                         };

        // hien thi du lieu  
        var results = phongs.GroupBy(n => new { n.MaPhong, n.TenPhong })
            .Select(g => new {
                g.Key.MaPhong,
                g.Key.TenPhong,
                SoLuong = g.Count(),
                TongLuong = g.Sum(g => g.Luong)
            }).ToList();




    }
}
