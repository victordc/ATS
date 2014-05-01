using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    [MetadataType(typeof(CodeTableData))]
    public partial class CodeTable
    {
        #region Public Methods

        public string Save()
        {
            string result = string.Empty;
            try
            {
                using (var context = new ATSCEEntities())
                {
                    if (this.CodeTableId <= 0)
                    {
                        context.CodeTables.Add(this);
                    }
                    else
                    {
                        context.Entry(this).State = System.Data.EntityState.Modified;
                    }
                    int rowAffected = context.SaveChanges();

                    if (rowAffected <= 0)
                        result = "Record is not saved";
                }
            }
            catch (Exception ex)
            {
                // Log Exception here.
                throw new Exception("Record is not saved");
            }

            return result;
        }

        #endregion

        #region Public Static Methods

        public static CodeTable GetById(int codeTableId)
        {
            using (var context = new ATSCEEntities())
            {
                return context.CodeTables.Single(r => r.CodeTableId == codeTableId);
            }
        }

        public static IEnumerable<CodeTable> GetAll()
        {
            using (var context = new ATSCEEntities())
            {
                return context.CodeTables.ToList();
            }
        }

        #endregion


    }

    public class CodeTableData
    {
        [Required(ErrorMessage = "Group code is required")]
        [DisplayName("Group Code")]
        public string GroupCode { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [DisplayName("Code")]
        public string Code { get; set; }


        [Required(ErrorMessage = "Code description is required")]
        [DisplayName("Code Description")]
        public string CodeDesc { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DisplayName("Start Date")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DisplayName("End Date")]
        public Nullable<System.DateTime> EndDate { get; set; }

        public string Remarks { get; set; }
    }
}
