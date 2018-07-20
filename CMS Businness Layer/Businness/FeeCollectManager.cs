using iTextSharp.text;
using iTextSharp.text.pdf;
using CMS.Models;
using SMS_Businness_Layer.Shared;
using SMS_Data_Layer.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMS_Businness_Layer.Businness
{
    public class FeeCollectManager
    {
        #region Student Fee Balances
        public static ObservableCollection<PendingMonthlyFeeModel> GetStudentFeeBalances(string studentID)
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@StudentID",  SqlDbType = SqlDbType.NVarChar, Value = studentID == "" ? null : studentID},
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentBalances, lstSqlParameters);
                return MapDatatableToFeeBalancesObject(objDatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static ObservableCollection<PendingMonthlyFeeModel> MapDatatableToFeeBalancesObject(DataTable objDatatable)
        {
            ObservableCollection<PendingMonthlyFeeModel> objPendingMonthlyFeeList = new ObservableCollection<PendingMonthlyFeeModel>();
            List<string> lstPeriods = new List<string>();
            ObservableCollection<FeeBalancesModel> objFeeBalancesList = new ObservableCollection<FeeBalancesModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    FeeBalancesModel objFeeBalance = new FeeBalancesModel();
                    objFeeBalance.id_offline = row["id_offline"] != DBNull.Value ? row["id_offline"].ToString() : string.Empty;
                    objFeeBalance.apply_from = row["apply_from"] != DBNull.Value ? Convert.ToDateTime(row["apply_from"]) : (DateTime?)null;
                    objFeeBalance.last_day = row["last_day"] != DBNull.Value ? Convert.ToInt32(row["last_day"]) : 0;
                    objFeeBalance.fine_per_day = row["fine_per_day"] != DBNull.Value ? Convert.ToDouble(row["fine_per_day"]) : 0;
                    objFeeBalance.fees_category = row["fees_category"] != DBNull.Value ? row["fees_category"].ToString() : string.Empty;
                    objFeeBalance.student_id = row["student_id"] != DBNull.Value ? row["student_id"].ToString() : string.Empty;
                    objFeeBalance.fees_id = row["fees_id"] != DBNull.Value ? row["fees_id"].ToString() : string.Empty;
                    objFeeBalance.fee_amount = row["fee_amount"] != DBNull.Value ? Convert.ToDouble(row["fee_amount"]) : 0;
                    objFeeBalance.payment_mode = row["payment_mode"] != DBNull.Value ? row["payment_mode"].ToString() : string.Empty;
                    objFeeBalance.payment_date = row["payment_date"] != DBNull.Value ? Convert.ToDateTime(row["payment_date"]) : (DateTime?)null;
                    objFeeBalance.recept_no = row["recept_no"] != DBNull.Value ? row["recept_no"].ToString() : string.Empty;
                    objFeeBalance.comment = row["comment"] != DBNull.Value ? row["comment"].ToString() : string.Empty;
                    objFeeBalance.payment_amount = row["payment_amount"] != DBNull.Value ? Convert.ToDouble(row["payment_amount"]) : 0;
                    objFeeBalance.payment_fine = row["payment_fine"] != DBNull.Value ? Convert.ToDouble(row["payment_fine"]) : 0;
                    objFeeBalance.fine = row["fine"] != DBNull.Value ? Convert.ToDouble(row["fine"]) : 0;
                    objFeeBalance.concession_amount = row["concession_amount"] != DBNull.Value ? Convert.ToDouble(row["concession_amount"]) : 0;
                    objFeeBalance.amount_to_pay = row["amount_to_pay"] != DBNull.Value ? Convert.ToDouble(row["amount_to_pay"]) : 0;
                    objFeeBalance.fine_to_pay = row["fine_to_pay"] != DBNull.Value ? Convert.ToDouble(row["fine_to_pay"]) : 0;
                    objFeeBalance.paid_amount = row["paid_amount"] != DBNull.Value ? Convert.ToDouble(row["paid_amount"]) : 0;
                    objFeeBalance.fine_paid = row["fine_paid"] != DBNull.Value ? Convert.ToDouble(row["fine_paid"]) : 0;
                    objFeeBalance.balance_amount = row["balance_amount"] != DBNull.Value ? Convert.ToDouble(row["balance_amount"]) : 0;
                    objFeeBalance.period = row["period"] != DBNull.Value ? row["period"].ToString() : string.Empty;
                    objFeeBalancesList.Add(objFeeBalance);

                    if (!lstPeriods.Contains(objFeeBalance.period))
                        lstPeriods.Add(objFeeBalance.period);

                }
                if (lstPeriods != null)
                {
                    foreach (string period in lstPeriods)
                    {
                        PendingMonthlyFeeModel objPendingMonthlyFee = new PendingMonthlyFeeModel();
                        objPendingMonthlyFee.Period = period;
                        objPendingMonthlyFee.FeeBalancesList = new ObservableCollection<FeeBalancesModel>(objFeeBalancesList.Where(feeBalance => feeBalance.period == period).OrderBy(feeBalance => feeBalance.fees_category));
                        objPendingMonthlyFee.Total = objPendingMonthlyFee.FeeBalancesList.Sum(feeBalance => feeBalance.balance_amount);
                        objPendingMonthlyFeeList.Add(objPendingMonthlyFee);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objPendingMonthlyFeeList;
        }

        public static Boolean MakePayments(MakePaymentModel objMakePayment, LoginModel CurrentLogin, SchoolModel SchoolInfo)
        {
            Boolean IsSuccess = false;
            try
            {
                foreach (FeeBalancesModel objFeeBalance in objMakePayment.SelectedFeeBalances)
                {
                    PaymentModel objPayment = new PaymentModel();
                    objPayment.id_offline = Guid.NewGuid().ToString();
                    objPayment.id_online = Guid.Empty.ToString();
                    objPayment.school_id = SchoolInfo.id_offline;
                    objPayment.student_fees_id = objFeeBalance.id_offline;
                    objPayment.payment_mode = objMakePayment.SelectedPaymentMode.name;
                    objPayment.amount = objFeeBalance.balance_amount;
                    objPayment.fine = objFeeBalance.fine;
                    objPayment.comment = objMakePayment.Payment.comment;
                    objPayment.recept_no = objMakePayment.Payment.recept_no;
                    objPayment.ip = null;
                    objPayment.created_by = CurrentLogin.User.id_offline != null ? CurrentLogin.User.id_offline : Guid.Empty.ToString();
                    objPayment.updated_by = CurrentLogin.User.id_offline != null ? CurrentLogin.User.id_offline : Guid.Empty.ToString();
                    objPayment.created_on = DateTime.Now;
                    objPayment.updated_on = DateTime.Now;
                    objPayment.payment_date = objMakePayment.Payment.payment_date;

                    DataTable objDatatable = MapPaymentToDataTable(objPayment);
                    SqlParameter objSqlParameter = new SqlParameter("@PaymentTable", SqlDbType.Structured);
                    objSqlParameter.TypeName = "dbo.PaymentModel";
                    objSqlParameter.Value = objDatatable;
                    IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.MakePayment, objSqlParameter);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return IsSuccess;
        }

        public static Boolean PrintReceipt(MakePaymentModel objMakePayment, LoginModel CurrentLogin, SchoolModel SchoolInfo, FeeCollectionStudentListModel Student)
        {
            Boolean IsSuccess = false;
            FileStream fs;
            Document doc;
            PdfWriter writer;
            try
            {
                string receipt = @"Receipts\" + objMakePayment.Payment.recept_no + ".pdf";
                if (!File.Exists(receipt))
                    Directory.CreateDirectory(Path.GetDirectoryName(receipt));
                fs = new FileStream(receipt, FileMode.Create, FileAccess.Write, FileShare.None);
                doc = new Document(PageSize.A6, 0f, 0f, 10f, 0f);
                writer = PdfWriter.GetInstance(doc, fs);

                Font fontTableHeading = new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.BOLD);
                Font fontTableCell = new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.NORMAL);
                Font fontHeading = new Font(Font.FontFamily.COURIER, 16, Font.BOLD);

                writer.PageEvent = new PDFWriterEvents("CMS", fontSize: 30f, xPosition: 150, yPosition: 250);
                doc.Open();

                //Header with logo
                {
                    PdfPTable tableHeader = new PdfPTable(4);
                    string logoPath = "./assets/images/receiptLogo.jpg";

                    Image logo = Image.GetInstance(logoPath);
                    logo.ScalePercent(30f);

                    PdfPCell cell = new PdfPCell(logo)
                    {
                        HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                        Border = Rectangle.NO_BORDER,
                    };
                    tableHeader.AddCell(cell);

                    PdfPHeaderCell headerCell = new PdfPHeaderCell()
                    {
                        Phrase = new Phrase("CMS Receipt", font: fontHeading),
                        Border = Rectangle.NO_BORDER,
                        Colspan = 3
                    };
                    tableHeader.AddCell(headerCell);

                    doc.Add(tableHeader);

                    doc.Add(new Paragraph("\n"));
                }
                //Student and Payment Details
                {
                    PdfPTable table = new PdfPTable(2);
                    PdfPCell cell = new PdfPCell(new Phrase("Student : " + Student.full_name, font: fontTableCell))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Grade : " + Student.grade_section, font: fontTableCell))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Roll Number : " + Student.roll_number, font: fontTableCell))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Payment Mode : " + objMakePayment.SelectedPaymentMode.name, font: fontTableCell))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Date : " + String.Format("{0:dd-MMM-yyyy}", objMakePayment.Payment.payment_date), font: fontTableCell))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Receipt # : " + objMakePayment.Payment.recept_no, font: fontTableCell))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = PdfPCell.ALIGN_RIGHT
                    };
                    table.AddCell(cell);

                    doc.Add(table);

                    doc.Add(new Paragraph("\n"));
                }
                //Fee Months
                {
                    PdfPTable table = new PdfPTable(5);

                    PdfPHeaderCell headerCell = new PdfPHeaderCell() { Phrase = new Phrase("Fee", font: fontTableHeading) };
                    table.AddCell(headerCell);

                    headerCell = new PdfPHeaderCell() { Phrase = new Phrase("Amount", font: fontTableHeading) };
                    table.AddCell(headerCell);

                    headerCell = new PdfPHeaderCell() { Phrase = new Phrase("Concession", font: fontTableHeading) };
                    table.AddCell(headerCell);

                    headerCell = new PdfPHeaderCell() { Phrase = new Phrase("Fine", font: fontTableHeading) };
                    table.AddCell(headerCell);

                    headerCell = new PdfPHeaderCell() { Phrase = new Phrase("Total", font: fontTableHeading) };
                    table.AddCell(headerCell);

                    foreach (FeeBalancesModel objFeeBalance in objMakePayment.SelectedFeeBalances)
                    {
                        PaymentModel objPayment = new PaymentModel();
                        objPayment.id_offline = Guid.NewGuid().ToString();
                        objPayment.id_online = Guid.Empty.ToString();
                        objPayment.school_id = SchoolInfo.id_offline;
                        objPayment.student_fees_id = objFeeBalance.id_offline;
                        objPayment.payment_mode = objMakePayment.SelectedPaymentMode.name;
                        objPayment.amount = objFeeBalance.balance_amount;
                        objPayment.fine = objFeeBalance.fine;
                        objPayment.concession_amount = objFeeBalance.concession_amount;
                        objPayment.comment = objMakePayment.Payment.comment;
                        objPayment.recept_no = objMakePayment.Payment.recept_no;
                        objPayment.ip = null;
                        objPayment.created_by = CurrentLogin.User.id_offline != null ? CurrentLogin.User.id_offline : Guid.Empty.ToString();
                        objPayment.updated_by = CurrentLogin.User.id_offline != null ? CurrentLogin.User.id_offline : Guid.Empty.ToString();
                        objPayment.created_on = DateTime.Now;
                        objPayment.updated_on = DateTime.Now;
                        objPayment.payment_date = objMakePayment.Payment.payment_date;


                        PdfPCell cell = new PdfPCell(new Phrase(objFeeBalance.fees_category + "\n" + String.Format("{0:dd-MMM-yyyy}", objFeeBalance.apply_from), font: fontTableCell));
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(objFeeBalance.amount_to_pay.ToString(), font: fontTableCell)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(objFeeBalance.concession_amount.ToString(), font: fontTableCell)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(objPayment.fine.ToString(), font: fontTableCell)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(objFeeBalance.balance_amount.ToString(), font: fontTableCell)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT };
                        table.AddCell(cell);

                    }
                    doc.Add(table);
                }
                // Payment Totals
                {
                    PdfPTable table = new PdfPTable(1);

                    PdfPHeaderCell headerCell = new PdfPHeaderCell()
                    {
                        Phrase = new Phrase("Total : Rs " + objMakePayment.TotalOfSelectedFeeBalancesGrandTotal, font: fontTableHeading),
                        HorizontalAlignment = PdfPHeaderCell.ALIGN_RIGHT
                    };
                    table.AddCell(headerCell);

                    doc.Add(table);
                }

                doc.Close();

                Process process = new Process();
                if (File.Exists(receipt))
                {
                    process.StartInfo.FileName = Path.GetFullPath(receipt);
                    process.Start();
                    //process.WaitForExit();
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs = null;
                doc = null;
                writer = null;

            }
            return IsSuccess;
        }

        private static DataTable MapPaymentToDataTable(PaymentModel objPaymentHistoryModel)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(Guid));
                table.Columns.Add("id_online", typeof(string));
                table.Columns.Add("school_id", typeof(string));
                table.Columns.Add("student_fees_id", typeof(string));
                table.Columns.Add("amount", typeof(Double));
                table.Columns.Add("fine", typeof(Double));
                table.Columns.Add("comment", typeof(string));
                table.Columns.Add("recept_no", typeof(string));
                table.Columns.Add("payment_mode", typeof(string));
                table.Columns.Add("payment_date", typeof(DateTime));
                table.Columns.Add("ip", typeof(string));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(
                                objPaymentHistoryModel.id_offline,
                                objPaymentHistoryModel.id_online,
                                objPaymentHistoryModel.school_id,
                                objPaymentHistoryModel.student_fees_id,
                                objPaymentHistoryModel.amount,
                                objPaymentHistoryModel.fine,
                                objPaymentHistoryModel.comment,
                                objPaymentHistoryModel.recept_no,
                                objPaymentHistoryModel.payment_mode,
                                objPaymentHistoryModel.payment_date,
                                objPaymentHistoryModel.ip,
                                objPaymentHistoryModel.created_by,
                                objPaymentHistoryModel.created_on,
                                objPaymentHistoryModel.updated_by,
                                objPaymentHistoryModel.updated_on
                             );
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion

        #region PaymentHistory
        public static ObservableCollection<PaymentModel> GetStudentPaymentHistory(Int64 fromRowNo, Int64 toRowNo, string studentID)
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo},
                    new SqlParameter() {ParameterName = "@StudentID",  SqlDbType = SqlDbType.NVarChar, Value = studentID == "" ? null : studentID},
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentPaymentHistory, lstSqlParameters);
                return MapDatatableToPaymentHistoryObject(objDatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static ObservableCollection<PaymentModel> MapDatatableToPaymentHistoryObject(DataTable objDatatable)
        {
            ObservableCollection<PaymentModel> objPaymentHistoryList = new ObservableCollection<PaymentModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    PaymentModel objPaymentHistory = new PaymentModel();
                    objPaymentHistory.id_offline = row["id_offline"] != DBNull.Value ? row["id_offline"].ToString() : string.Empty;
                    objPaymentHistory.id_online = row["id_online"] != DBNull.Value ? row["id_online"].ToString() : string.Empty;
                    objPaymentHistory.school_id = row["school_id"] != DBNull.Value ? row["school_id"].ToString() : string.Empty;
                    objPaymentHistory.student_fees_id = row["student_fees_id"] != DBNull.Value ? row["student_fees_id"].ToString() : string.Empty;
                    objPaymentHistory.amount = row["amount"] != DBNull.Value ? Convert.ToDouble(row["amount"]) : 0;
                    objPaymentHistory.fine = row["fine"] != DBNull.Value ? Convert.ToDouble(row["fine"]) : 0;
                    objPaymentHistory.recept_no = row["recept_no"] != DBNull.Value ? row["recept_no"].ToString() : string.Empty;
                    objPaymentHistory.payment_mode = row["payment_mode"] != DBNull.Value ? row["payment_mode"].ToString() : string.Empty;
                    objPaymentHistory.payment_date = row["payment_date"] != DBNull.Value ? Convert.ToDateTime(row["payment_date"]) : (DateTime?)null;
                    objPaymentHistory.concession_amount = row["concession_amount"] != DBNull.Value ? Convert.ToDouble(row["concession_amount"]) : 0;
                    objPaymentHistory.month = row["month"] != DBNull.Value ? row["month"].ToString() : string.Empty;
                    objPaymentHistory.apply_from = row["apply_from"] != DBNull.Value ? Convert.ToDateTime(row["apply_from"]) : (DateTime?)null;
                    objPaymentHistory.apply_to = row["apply_to"] != DBNull.Value ? Convert.ToDateTime(row["apply_to"]) : (DateTime?)null;
                    objPaymentHistory.fee_amount = row["fee_amount"] != DBNull.Value ? Convert.ToDouble(row["fee_amount"]) : 0;
                    objPaymentHistory.concession_amount = row["concession_amount"] != DBNull.Value ? Convert.ToDouble(row["concession_amount"]) : 0;
                    objPaymentHistory.category_name = row["category_name"] != DBNull.Value ? row["category_name"].ToString() : string.Empty;
                    objPaymentHistory.ip = row["ip"] != DBNull.Value ? row["ip"].ToString() : string.Empty;
                    objPaymentHistory.comment = row["comment"] != DBNull.Value ? row["comment"].ToString() : string.Empty;
                    objPaymentHistory.created_by = row["created_by"] != DBNull.Value ? row["created_by"].ToString() : string.Empty;
                    objPaymentHistory.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    objPaymentHistory.updated_by = row["updated_by"] != DBNull.Value ? row["updated_by"].ToString() : string.Empty;
                    objPaymentHistory.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    objPaymentHistoryList.Add(objPaymentHistory);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objPaymentHistoryList;
        }

        public static Boolean UpdatePaymentHistory(PaymentModel objPayment, LoginModel CurrentLogin)
        {
            try
            {
                objPayment.updated_on = DateTime.Now;
                objPayment.updated_by = CurrentLogin.User.id_offline != null ? CurrentLogin.User.id_offline : Guid.Empty.ToString();

                DataTable objDatatable = MapPaymentToDataTable(objPayment);
                SqlParameter objSqlParameter = new SqlParameter("@PaymentTable", SqlDbType.Structured);
                objSqlParameter.TypeName = "dbo.PaymentModel";
                objSqlParameter.Value = objDatatable;
                return DataAccess.ExecuteNonQuery(StoredProcedures.UpdatePayment, objSqlParameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion

        #region FeeDue
        public static ObservableCollection<FeeDueModel> GetStudentFeeDue(Int64 fromRowNo, Int64 toRowNo, string studentID, FeeDueListFiltersModel FeeDueListFilters)
        {
            try
            {
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@FromRowNo",     SqlDbType = SqlDbType.NVarChar, Value = fromRowNo},
                    new SqlParameter() {ParameterName = "@ToRowNo",  SqlDbType = SqlDbType.NVarChar, Value = toRowNo},
                    new SqlParameter() {ParameterName = "@StudentID",  SqlDbType = SqlDbType.NVarChar, Value = studentID == "" ? null : studentID},
                    new SqlParameter() {ParameterName = "@FeeCategoryID",  SqlDbType = SqlDbType.NVarChar, Value = (FeeDueListFilters.FeeCategory != null && FeeDueListFilters.FeeCategory.name ==  "All") ? null : (FeeDueListFilters.FeeCategory != null ? FeeDueListFilters.FeeCategory.id_offline : null)},
                };
                DataTable objDatable = DataAccess.GetDataTable(StoredProcedures.GetStudentFeeDue, lstSqlParameters);
                return MapDatatableToFeeDueObject(objDatable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        private static ObservableCollection<FeeDueModel> MapDatatableToFeeDueObject(DataTable objDatatable)
        {
            ObservableCollection<FeeDueModel> objFeeDueList = new ObservableCollection<FeeDueModel>();
            try
            {
                foreach (DataRow row in objDatatable.Rows)
                {
                    FeeDueModel objFeeDue = new FeeDueModel();
                    objFeeDue.id_offline = row["id_offline"] != DBNull.Value ? row["id_offline"].ToString() : string.Empty;
                    objFeeDue.student_balance = row["student_balance"] != DBNull.Value ? Convert.ToDouble(row["student_balance"]) : 0;
                    objFeeDue.student_id = row["student_id"] != DBNull.Value ? row["student_id"].ToString() : string.Empty;
                    objFeeDue.concession_amount = row["concession_amount"] != DBNull.Value ? Convert.ToDouble(row["concession_amount"]) : 0;
                    objFeeDue.apply_from = row["apply_from"] != DBNull.Value ? Convert.ToDateTime(row["apply_from"]) : (DateTime?)null;
                    objFeeDue.apply_to = row["apply_to"] != DBNull.Value ? Convert.ToDateTime(row["apply_to"]) : (DateTime?)null;
                    objFeeDue.fine = row["fine"] != DBNull.Value ? Convert.ToDouble(row["fine"]) : 0;
                    objFeeDue.category_name = row["category_name"] != DBNull.Value ? row["category_name"].ToString() : string.Empty;
                    objFeeDue.created_by = row["created_by"] != DBNull.Value ? row["created_by"].ToString() : string.Empty;
                    objFeeDue.created_on = row["created_on"] != DBNull.Value ? Convert.ToDateTime(row["created_on"]) : (DateTime?)null;
                    objFeeDue.updated_by = row["updated_by"] != DBNull.Value ? row["updated_by"].ToString() : string.Empty;
                    objFeeDue.updated_on = row["updated_on"] != DBNull.Value ? Convert.ToDateTime(row["updated_on"]) : (DateTime?)null;
                    objFeeDueList.Add(objFeeDue);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return objFeeDueList;
        }

        public static Boolean UpdateFeeDue(FeeDueModel objFeeDueModel, LoginModel CurrentLogin)
        {
            try
            {
                objFeeDueModel.updated_on = DateTime.Now;
                objFeeDueModel.updated_by = CurrentLogin.User.id_offline != null ? CurrentLogin.User.id_offline : Guid.Empty.ToString();

                DataTable objDatatable = MapFeeDueToDataTable(objFeeDueModel);
                SqlParameter objSqlParameter = new SqlParameter("@StudentFeesTable", SqlDbType.Structured);
                objSqlParameter.TypeName = "dbo.FeeDueModel";
                objSqlParameter.Value = objDatatable;
                return DataAccess.ExecuteNonQuery(StoredProcedures.UpdateFeeDue, objSqlParameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        private static DataTable MapFeeDueToDataTable(FeeDueModel objFeeDueModel)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("id_offline", typeof(string));
                table.Columns.Add("student_id", typeof(string));
                table.Columns.Add("student_balance", typeof(Double));
                table.Columns.Add("apply_from", typeof(DateTime));
                table.Columns.Add("apply_to", typeof(DateTime));
                table.Columns.Add("fine", typeof(Double));
                table.Columns.Add("concession_amount", typeof(Double));
                table.Columns.Add("created_by", typeof(string));
                table.Columns.Add("created_on", typeof(DateTime));
                table.Columns.Add("updated_by", typeof(string));
                table.Columns.Add("updated_on", typeof(DateTime));

                table.Rows.Add(objFeeDueModel.id_offline,
                                objFeeDueModel.student_id,
                                objFeeDueModel.student_balance,
                                objFeeDueModel.apply_from,
                                objFeeDueModel.apply_to,
                                objFeeDueModel.fine,
                                objFeeDueModel.concession_amount,
                                objFeeDueModel.created_by,
                                objFeeDueModel.created_on,
                                objFeeDueModel.updated_by,
                                objFeeDueModel.updated_on
                             );
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public static Boolean DeleteFeeDue(FeeDueModel objFeeDueModel)
        {
            try
            {
                Boolean CanDelete;
                Boolean IsSuccess = false;
                List<SqlParameter> lstSqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@Id",  SqlDbType = SqlDbType.NVarChar, Value = objFeeDueModel.id_offline},
                };
                CanDelete = Convert.ToBoolean(DataAccess.GetScalar(StoredProcedures.CheckIfStudentFeesCanBeDeleted, lstSqlParameters));

                if (CanDelete)
                {
                    lstSqlParameters = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@key",     SqlDbType = SqlDbType.NVarChar, Value = "id"},
                        new SqlParameter() {ParameterName = "@value",     SqlDbType = SqlDbType.NVarChar, Value = objFeeDueModel.id_offline},
                        new SqlParameter() {ParameterName = "@tableName",  SqlDbType = SqlDbType.NVarChar, Value = "student_fees"}
                    };

                    IsSuccess = DataAccess.ExecuteNonQuery(StoredProcedures.DeleteRecord, lstSqlParameters);
                }
                return IsSuccess;

            }
            catch (Exception ex)
            {
                //throw ex;
                return false;
            }
            finally
            {

            }
        }
        #endregion
    }
}
