using System.Data;

namespace WinFormsApp1
{
    public static class DataStorage
    {
        public static DataTable LopHoc = new DataTable();

        static DataStorage()
        {
            LopHoc.Columns.Add("Mã lớp");
            LopHoc.Columns.Add("Tên lớp");

            LopHoc.Rows.Add("68PM1", "Lớp 68PM1");
            LopHoc.Rows.Add("68PM2", "Lớp 68PM2");
        }
    }
}