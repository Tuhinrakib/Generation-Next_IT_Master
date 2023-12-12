using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Generation_Next_IT.Models.ViewModel
{
    public class MeetingMinute_ViewModel
    {

        public int MeetingMinuteMasterID { get; set; }


        [EnumDataType(typeof(CustomerType))]
        public CustomerType CustomerType { get; set; } 

        [DataType(DataType.Date)]
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


        public int MeetingMinuteDetailID { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int? Quantity { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Unit must be a non-negative value.")]
        public int Unit { get; set; }
        public string ProductServiceName { get; set; }  
        public string SelectedCustomerName { get; set; } = default!;
        public List<MeetingMinuteDetail> MeetingMinuteDetails { get; set; } = new List<MeetingMinuteDetail>();
    }
}
