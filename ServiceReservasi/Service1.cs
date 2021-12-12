using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=DESKTOP-ECRNKVS;Initial Catalog=WCFReservasi;Persist Security Info=True; User ID=sa;password=Bismillah12";
        SqlConnection connection;
        SqlCommand com;

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "DELETE FROM dbo.Pemesanan WHERE ID_Reservasi = '" + IDPemesanan + "' )";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return a;
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> lokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "SELECT ID_Lokasi, Nama_Lokasi, Deskripsi_Singkat, Deskripsi_Full, Kuota FROM dbo.Lokasi";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while(reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    lokasiFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return lokasiFull;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_Telpon)
        {
            string a = "gagal";
            try
            {
                string sql = "UPDATE dbo.Pemesanan SET Nama_Customer = '" + NamaCustomer + "', No_Telpon = '" + No_Telpon + "' WHERE ID_Reservasi = '" + IDPemesanan + "' )";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return a;
        }

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "INSERT INTO dbo.Pemesanan VALUES ('"+IDPemesanan+"', '"+NamaCustomer+"', '" + NoTelpon + "', '" + JumlahPemesanan + "', '" + IDLokasi+"')";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "UPDATE dbo.Lokasi SET Kuota = Kuota - " + JumlahPemesanan + " WHERE ID_Lokasi = '" + IDLokasi + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return a;
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "SELECT ID_Reservasi, Nama_Customer, No_Telpon, Jumlah_Pemesanan, Nama_Lokasi FROM dbo.Pemesanan JOIN dbo.Lokasi ON dbo.Pemesanan.ID_Lokasi = dbo.Lokasi.ID_Lokasi";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data);
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return pemesanans;
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }
    }
}
