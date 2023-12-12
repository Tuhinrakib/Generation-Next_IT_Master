using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Generation_Next_IT.Models
{
    public class CorporateCustomer
    {
        [Key]
        public int CorporateCustomerID { get; set; }

        [Required(ErrorMessage = "Customer Name is required."), StringLength(50), Display(Name = "Customer Name")]
        public string CorporateCustomerName { get; set; } = default!;

        // Navigation property 
        public List<MeetingMinuteMaster> MeetingMinuteMasters { get; set; } = new List<MeetingMinuteMaster>();
    }
    public class IndividualCustomer
    {
        [Key]
        public int IndividualCustomerID { get; set; }

        [Required(ErrorMessage = "Customer Name is required."), StringLength(50), Display(Name = "Customer Name")]
        public string IndividualCustomerName { get; set; } = default!;

        // Navigation property 
        public List<MeetingMinuteMaster> MeetingMinuteMasters { get; set; } = new List<MeetingMinuteMaster>();
    }
    public class MeetingMinuteMaster
    {
        [Key]
        public int MeetingMinuteMasterID { get; set; }

        [EnumDataType(typeof(CustomerType))]
        public CustomerType CustomerType { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now.Date;


        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Meeting Place is required."), StringLength(100)]
        public string MeetingPlace { get; set; } = default!;

        [Required(ErrorMessage = "Attends From Client Side is required."), StringLength(1000)]
        public string AttendsFromClient { get; set; } = default!;

        [Required(ErrorMessage = "Attends From Host Side is required."), StringLength(1000)]
        public string AttendsFromHost { get; set; } = default!;

        [Required(ErrorMessage = "Agenda is required.")]
        public string Agenda { get; set; } = default!;

        [Required(ErrorMessage = "Discussion is required.")]
        public string Discussion { get; set; } = default!;

        [Required(ErrorMessage = "Decision is required.")]
        public string Decision { get; set; } = default!;

        //FK
        [ForeignKey("CorporateCustomer")]
        public int? CorporateCustomerID { get; set; }

        [ForeignKey("IndividualCustomer")]
        public int? IndividualCustomerID { get; set; }

        // Navigation property 
        public CorporateCustomer? CorporateCustomer { get; set; }

        public IndividualCustomer? IndividualCustomer { get; set; }

        public List<MeetingMinuteDetail> MeetingMinuteDetails { get; set; } = new List<MeetingMinuteDetail>();
    }
    public class MeetingMinuteDetail
    {
        [Key]
        public int MeetingMinuteDetailID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int? Quantity { get; set; }

        // Foreign keys
        public int MeetingMinuteMasterID { get; set; }

        public int ProductServiceID { get; set; }

        // Navigation property
        public MeetingMinuteMaster? MeetingMinuteMaster { get; set; }
        public ProductService? ProductService { get; set; }
    }
    public class ProductService
    {
        [Key]
        public int ProductServiceID { get; set; }

        [Required(ErrorMessage = "Product/Service Name is required.")]
        public string ProductServiceName { get; set; } = default!;

        [Range(0, int.MaxValue, ErrorMessage = "Unit must be a non-negative value.")]
        public int Unit { get; set; }

        // Navigation property 
        public List<MeetingMinuteDetail> MeetingMinuteDetails { get; set; } = new List<MeetingMinuteDetail>();
    }
    public enum CustomerType
    {
        Corporate = 1,
        Individual
    }

}
