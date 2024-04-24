using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 
namespace quanlybanhang1.Class
{
    internal class Functions
    {
        public static SqlConnection cnn;  //Khai báo đối tượng kết nối 
        public static SqlDataAdapter da;
        public static DataTable dt;
        static SqlDataReader dr;
        static SqlCommand cmd;

        public static string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";
        public static void Connect(string query, DataGridView table)
        {
            try
            {
                cnn = new SqlConnection(connectionString);   //Khởi tạo đối tượng
                cnn.Open();

                da = new SqlDataAdapter(query, cnn);
                dt = new DataTable();
                da.Fill(dt);

                table.DataSource = dt;

                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }
        }

        //Hàm kiểm tra khoá trùng
        public static bool CheckKey(string sqlQuery)
        {
            bool exists = false;

            using (cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                cmd = new SqlCommand(sqlQuery, cnn);

                using (dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        exists = true;
                    }
                }
            }

            return exists;
        }

        public static bool DatabaseExists()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static string ConvertMoneyToWords(decimal inputNumber, bool suffix = true)
        {


            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return result + (suffix ? " đồng" : "");

        }


        public static string RemoveDot(string number)
        {
            return number.Replace(",", "");
        }


        public static void FormatNumberWithCommas(TextBox txb)
        {
            // Lưu vị trí con trỏ hiện tại
            int currentCursorPosition = txb.SelectionStart;

            // Xóa dấu phẩy và chuyển về chuỗi không có dấu phẩy
            string originalText = txb.Text.Replace(",", "");

            // Kiểm tra và thêm dấu phẩy sau mỗi 3 số
            int length = originalText.Length;
            if (length > 3)
            {
                int count = 0;
                StringBuilder formattedText = new StringBuilder();

                for (int i = length - 1; i >= 0; i--)
                {
                    formattedText.Insert(0, originalText[i]);
                    count++;

                    if (count == 3 && i > 0)
                    {
                        formattedText.Insert(0, ",");
                        count = 0; // Đặt lại count sau khi thêm dấu phẩy
                    }
                }

                txb.Text = formattedText.ToString();
            }

            // Đặt lại vị trí con trỏ
            txb.SelectionStart = currentCursorPosition + (txb.Text.Length - originalText.Length);
        }
    }
}

